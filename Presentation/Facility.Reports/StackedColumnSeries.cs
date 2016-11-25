using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Controls.DataVisualization.Charting;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Windows.Controls.DataVisualization;
using System.Collections;
using System.Linq;
using System.Globalization;
using System.Diagnostics;

namespace Facility.Reports
{
    [SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance", Justification = "Depth of hierarchy is necessary to avoid code duplication.")]
    [StyleTypedProperty(Property = "DataPointStyle", StyleTargetType = typeof(ColumnDataPoint))]
    [StyleTypedProperty(Property = "LegendItemStyle", StyleTargetType = typeof(LegendItem))]
    [TemplatePart(Name = DataPointSeries.PlotAreaName, Type = typeof(Canvas))]
    public sealed partial class StackedColumnSeries : ColumnBarBaseSeries<ColumnDataPoint>
    {
        protected override void UpdateDataPoint(DataPoint dataPoint)
        {
            if (SeriesHost == null || PlotArea == null)
            {
                return;
            }

            object category = dataPoint.IndependentValue ?? (this.ActiveDataPoints.IndexOf(dataPoint) + 1);
            Range<UnitValue> coordinateRange = GetCategoryRange(category);
            if (!coordinateRange.HasData)
            {
                return;
            }
            else if (coordinateRange.Maximum.Unit != Unit.Pixels || coordinateRange.Minimum.Unit != Unit.Pixels)
            {
                throw new InvalidOperationException("This Series Does Not Support Radial Axes");
            }

            double minimum = (double)coordinateRange.Minimum.Value;
            double maximum = (double)coordinateRange.Maximum.Value;

            double plotAreaHeight = ActualDependentRangeAxis.GetPlotAreaCoordinate(ActualDependentRangeAxis.Range.Maximum).Value.Value;
            IEnumerable<StackedColumnSeries> columnSeries = SeriesHost.Series.OfType<StackedColumnSeries>().Where(series => series.ActualIndependentAxis == ActualIndependentAxis);
            int numberOfSeries = columnSeries.Count();
            double coordinateRangeWidth = (maximum - minimum);
            double segmentWidth = coordinateRangeWidth * 0.8;

            double columnWidth = segmentWidth; // / numberOfSeries;
            int seriesIndex = columnSeries.IndexOf(this);

            double dataPointY = ActualDependentRangeAxis.GetPlotAreaCoordinate(ValueHelper.ToDouble(dataPoint.ActualDependentValue)).Value.Value;
            double zeroPointY = ActualDependentRangeAxis.GetPlotAreaCoordinate(ActualDependentRangeAxis.Origin).Value.Value;

            // Need to shift the columns up to take account of the other columns that are already rendered, to make them
            // appear stacked
            int dataPointIndex = ActiveDataPoints.IndexOf(dataPoint);

            for (int i = numberOfSeries - 1; i > seriesIndex; i--)
            {
                StackedColumnSeries prevSeries = columnSeries.ElementAt<StackedColumnSeries>(i);

                if (prevSeries.ActiveDataPointCount >= dataPointIndex + 1)
                {
                    DataPoint currDataPoint = prevSeries.ActiveDataPoints.ElementAt<DataPoint>(dataPointIndex);

                    // Need to move positive columns up and negative ones down
                    if ((double)dataPoint.ActualDependentValue > 0 && (double)currDataPoint.ActualDependentValue > 0)
                    {
                        dataPointY += currDataPoint.Height;
                        zeroPointY += currDataPoint.Height;
                    }
                    else if ((double)dataPoint.ActualDependentValue < 0 && (double)currDataPoint.ActualDependentValue < 0)
                    {
                        dataPointY -= currDataPoint.Height;
                        zeroPointY -= currDataPoint.Height;
                    }
                }
            }

            double offset = 0; 
            double dataPointX = minimum + offset;

            if (GetIsDataPointGrouped(category))
            {
                IGrouping<object, DataPoint> categoryGrouping = GetDataPointGroup(category);
                int index = categoryGrouping.IndexOf(dataPoint);
                dataPointX += (index * (columnWidth * 0.2)) / (categoryGrouping.Count() - 1);
                columnWidth *= 0.8;
                Canvas.SetZIndex(dataPoint, -index);
            }

            if (ValueHelper.CanGraph(dataPointY) && ValueHelper.CanGraph(dataPointX) && ValueHelper.CanGraph(zeroPointY))
            {
                double left = Math.Round(dataPointX);
                double width = Math.Round(columnWidth);

                double top = Math.Round(plotAreaHeight - Math.Max(dataPointY, zeroPointY) + 0.5);
                double bottom = Math.Round(plotAreaHeight - Math.Min(dataPointY, zeroPointY) + 0.5);
                double height = bottom - top + 1;

                Canvas.SetLeft(dataPoint, left);
                Canvas.SetTop(dataPoint, top);
                dataPoint.Width = width;
                dataPoint.Height = height;
            }
        }

        protected override IEnumerable<ValueMargin> GetValueMargins(IValueMarginConsumer consumer)
        {
            double dependentValueMargin = this.ActualHeight / 10;
            IAxis axis = consumer as IAxis;
            if (axis != null && ActiveDataPoints.Any())
            {
                Func<DataPoint, IComparable> selector = null;
                if (axis == InternalActualIndependentAxis)
                {
                    selector = (dataPoint) => (IComparable)dataPoint.ActualIndependentValue;

                    DataPoint minimumPoint = ActiveDataPoints.MinOrNull(selector);
                    DataPoint maximumPoint = ActiveDataPoints.MaxOrNull(selector);

                    double minimumMargin = minimumPoint.GetMargin(axis);
                    yield return new ValueMargin(selector(minimumPoint), minimumMargin, minimumMargin);

                    double maximumMargin = maximumPoint.GetMargin(axis);
                    yield return new ValueMargin(selector(maximumPoint), maximumMargin, maximumMargin);
                }
                else if (axis == InternalActualDependentAxis)
                {
                    IEnumerable<StackedColumnSeries> columnSeries = SeriesHost.Series.OfType<StackedColumnSeries>().Where(series => series.ActualIndependentAxis == ActualIndependentAxis);

                    IList<double> maximums = new List<double>();
                    IList<double> minimums = new List<double>();

                    foreach (StackedColumnSeries currSeries in columnSeries)
                    {
                        for (int i = 0; i < currSeries.ActiveDataPointCount; i++)
                        {
                            if (maximums.Count <= i)
                            {
                                maximums.Add(0.0);
                            }

                            if (minimums.Count <= i)
                            {
                                minimums.Add(0.0);
                            }

                            DataPoint currDataPoint = currSeries.ActiveDataPoints.ElementAt<DataPoint>(i);

                            if ((double)currDataPoint.ActualDependentValue > 0)
                            {
                                maximums[i] += (double)currDataPoint.ActualDependentValue;
                            }
                            else
                            {
                                minimums[i] += (double)currDataPoint.ActualDependentValue;
                            }
                        }
                    }
                    double maximum = 0.0;
                    double minimum = 0.0;

                    for (int i = 0; i < minimums.Count; i++)
                    {
                        if (maximums[i] > maximum)
                        {
                            maximum = maximums[i];
                        }

                        if (minimums[i] < minimum)
                        {
                            minimum = minimums[i];
                        }
                    }
                    yield return new ValueMargin(minimum, dependentValueMargin, dependentValueMargin);
                    yield return new ValueMargin(maximum, dependentValueMargin, dependentValueMargin);
                }
            }
            else
            {
                yield break;
            }
        }

        #region Properties and methods used by ColumnSeries being in-accessible

        private Panel _plotArea;

        internal Panel PlotArea
        {
            get
            {
                return _plotArea;
            }
            private set
            {
                Panel oldValue = _plotArea;
                _plotArea = value;
                if (_plotArea != oldValue)
                {
                    OnPlotAreaChanged(oldValue, value);
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PlotArea = GetTemplateChild(PlotAreaName) as Panel;
        }

        #endregion

        #region Copied from System.Windows.Controls.DataVisualization.Charting.ColumnSeries

        #region public IRangeAxis DependentRangeAxis

        public IRangeAxis DependentRangeAxis
        {
            get { return GetValue(DependentRangeAxisProperty) as IRangeAxis; }
            set { SetValue(DependentRangeAxisProperty, value); }
        }

        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "This member is necessary because the base classes need to share this dependency property.")]
        public static readonly DependencyProperty DependentRangeAxisProperty =
            DependencyProperty.Register(
                "DependentRangeAxis",
                typeof(IRangeAxis),
                typeof(StackedColumnSeries),
                new PropertyMetadata(null, OnDependentRangeAxisPropertyChanged));

        private static void OnDependentRangeAxisPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StackedColumnSeries source = (StackedColumnSeries)d;
            IRangeAxis newValue = (IRangeAxis)e.NewValue;
            source.OnDependentRangeAxisPropertyChanged(newValue);
        }

        private void OnDependentRangeAxisPropertyChanged(IRangeAxis newValue)
        {
            this.InternalDependentAxis = (IAxis)newValue;
        }
        #endregion public IRangeAxis DependentRangeAxis

        #region public IAxis IndependentAxis

        public IAxis IndependentAxis
        {
            get { return GetValue(IndependentAxisProperty) as IAxis; }
            set { SetValue(IndependentAxisProperty, value); }
        }

        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "This member is necessary because the base classes need to share this dependency property.")]
        public static readonly DependencyProperty IndependentAxisProperty =
            DependencyProperty.Register(
                "IndependentAxis",
                typeof(IAxis),
                typeof(StackedColumnSeries),
                new PropertyMetadata(null, OnIndependentAxisPropertyChanged));

        private static void OnIndependentAxisPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StackedColumnSeries source = (StackedColumnSeries)d;
            IAxis newValue = (IAxis)e.NewValue;
            source.OnIndependentAxisPropertyChanged(newValue);
        }

        private void OnIndependentAxisPropertyChanged(IAxis newValue)
        {
            this.InternalIndependentAxis = (IAxis)newValue;
        }
        #endregion public IAxis IndependentAxis

        public StackedColumnSeries()
        {
        }

        protected override void GetAxes(DataPoint firstDataPoint)
        {
            GetAxes(
                firstDataPoint,
                (axis) => axis.Orientation == AxisOrientation.X,
                () => new CategoryAxis { Orientation = AxisOrientation.X },
                (axis) =>
                {
                    IRangeAxis rangeAxis = axis as IRangeAxis;
                    return rangeAxis != null && rangeAxis.Origin != null && axis.Orientation == AxisOrientation.Y;
                },
                () =>
                {
                    IRangeAxis rangeAxis = CreateRangeAxisFromData(firstDataPoint.DependentValue);
                    rangeAxis.Orientation = AxisOrientation.Y;
                    if (rangeAxis == null || rangeAxis.Origin == null)
                    {
                        throw new InvalidOperationException("No Suitable Axis Available For Plotting Dependent Value");
                    }
                    DisplayAxis axis = rangeAxis as DisplayAxis;
                    if (axis != null)
                    {
                        axis.ShowGridLines = true;
                    }
                    return rangeAxis;
                });
        }

        #endregion
    }

    #region Silverilght Toolkit

    internal static class ValueHelper
    {
        public const double Radian = Math.PI / 180.0;

        public static bool CanGraph(double value)
        {
            return !double.IsNaN(value) && !double.IsNegativeInfinity(value) && !double.IsPositiveInfinity(value) && !double.IsInfinity(value);
        }

        public static bool TryConvert(object value, out double doubleValue)
        {
            doubleValue = default(double);
            try
            {
                if (value != null &&
                    (value is double
                    || value is int
                    || value is byte
                    || value is short
                    || value is decimal
                    || value is float
                    || value is long
                    || value is uint
                    || value is sbyte
                    || value is ushort
                    || value is ulong))
                {
                    doubleValue = ValueHelper.ToDouble(value);
                    return true;
                }
            }
            catch (FormatException)
            {
            }
            catch (InvalidCastException)
            {
            }
            return false;
        }

        public static bool TryConvert(object value, out DateTime dateTimeValue)
        {
            dateTimeValue = default(DateTime);
            if (value != null && value is DateTime)
            {
                dateTimeValue = (DateTime)value;
                return true;
            }

            return false;
        }

        public static double ToDouble(object value)
        {
            return Convert.ToDouble(value, CultureInfo.InvariantCulture);
        }

        public static DateTime ToDateTime(object value)
        {
            return Convert.ToDateTime(value, CultureInfo.InvariantCulture);
        }

        public static IEnumerable<DateTime> GetDateTimesBetweenInclusive(DateTime start, DateTime end, long count)
        {
            Debug.Assert(count >= 2L, "Count must be at least 2.");

            return GetIntervalsInclusive(start.Ticks, end.Ticks, count).Select(value => new DateTime(value));
        }

        public static IEnumerable<TimeSpan> GetTimeSpanIntervalsInclusive(TimeSpan timeSpan, long count)
        {
            Debug.Assert(count >= 2L, "Count must be at least 2.");

            long distance = timeSpan.Ticks;

            return GetIntervalsInclusive(0, distance, count).Select(value => new TimeSpan(value));
        }

        public static IEnumerable<long> GetIntervalsInclusive(long start, long end, long count)
        {
            Debug.Assert(count >= 2L, "Count must be at least 2.");

            long interval = end - start;
            for (long index = 0; index < count; index++)
            {
                double ratio = (double)index / (double)(count - 1);
                long value = (long)((ratio * interval) + start);
                yield return value;
            }
        }

        internal static double RemoveNoiseFromDoubleMath(double value)
        {
            if (value == 0.0 || Math.Abs((Math.Log10(Math.Abs(value)))) < 27)
            {
                return (double)((decimal)value);
            }
            return Double.Parse(value.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
        }

        public static Range<double> ToDoubleRange(this Range<IComparable> range)
        {
            if (!range.HasData)
            {
                return new Range<double>();
            }
            else
            {
                return new Range<double>((double)range.Minimum, (double)range.Maximum);
            }
        }

        public static Range<DateTime> ToDateTimeRange(this Range<IComparable> range)
        {
            if (!range.HasData)
            {
                return new Range<DateTime>();
            }
            else
            {
                return new Range<DateTime>((DateTime)range.Minimum, (DateTime)range.Maximum);
            }
        }

        public static int Compare(IComparable left, IComparable right)
        {
            if (left == null && right == null)
            {
                return 0;
            }
            else if (left == null && right != null)
            {
                return -1;
            }
            else if (left != null && right == null)
            {
                return 1;
            }
            else
            {
                return left.CompareTo(right);
            }
        }

        public static Point Translate(this Point origin, Point offset)
        {
            return new Point(origin.X + offset.X, origin.Y + offset.Y);
        }

        public static Range<IComparable> ToComparableRange(this Range<double> range)
        {
            if (range.HasData)
            {
                return new Range<IComparable>(range.Minimum, range.Maximum);
            }
            else
            {
                return new Range<IComparable>();
            }
        }

        public static double LeftOrDefault(this Rect rectangle, double value)
        {
            return rectangle.IsEmpty ? value : rectangle.Left;
        }

        public static double RightOrDefault(this Rect rectangle, double value)
        {
            return rectangle.IsEmpty ? value : rectangle.Right;
        }

        public static double WidthOrDefault(this Rect rectangle, double value)
        {
            return rectangle.IsEmpty ? value : rectangle.Width;
        }

        public static double HeightOrDefault(this Rect rectangle, double value)
        {
            return rectangle.IsEmpty ? value : rectangle.Height;
        }

        public static double BottomOrDefault(this Rect rectangle, double value)
        {
            return rectangle.IsEmpty ? value : rectangle.Bottom;
        }

        public static double TopOrDefault(this Rect rectangle, double value)
        {
            return rectangle.IsEmpty ? value : rectangle.Top;
        }

        public static Range<IComparable> ToComparableRange(this Range<DateTime> range)
        {
            if (range.HasData)
            {
                return new Range<IComparable>(range.Minimum, range.Maximum);
            }
            else
            {
                return new Range<IComparable>();
            }
        }

        public static TimeSpan? GetLength(this Range<DateTime> range)
        {
            return range.HasData ? range.Maximum - range.Minimum : new TimeSpan?();
        }

        public static double? GetLength(this Range<double> range)
        {
            return range.HasData ? range.Maximum - range.Minimum : new double?();
        }

        public static bool IsEmptyOrHasNoSize(this Rect rect)
        {
            return rect.IsEmpty || (rect.Width == 0 && rect.Height == 0);
        }

        public static void SetStyle(this FrameworkElement element, Style style)
        {
#if SILVERLIGHT
            if (element.Style == null)
            {
#endif
                element.Style = style;
#if SILVERLIGHT
            }
#endif
        }
    }

    internal static class EnumerableFunctions
    {
        public static int FastCount(this IEnumerable that)
        {
            IList list = that as IList;
            if (list != null)
            {
                return list.Count;
            }
            return that.Cast<object>().Count();
        }

        public static bool IsEmpty<T>(this IEnumerable<T> that)
        {
            IEnumerator<T> enumerator = that.GetEnumerator();
            return !enumerator.MoveNext();
        }

        public static T MinOrNull<T>(this IEnumerable<T> that, Func<T, IComparable> projectionFunction)
            where T : class
        {
            IComparable result = null;
            T minimum = default(T);
            if (!that.Any())
            {
                return minimum;
            }

            minimum = that.First();
            result = projectionFunction(minimum);
            foreach (T item in that.Skip(1))
            {
                IComparable currentResult = projectionFunction(item);
                if (result.CompareTo(currentResult) > 0)
                {
                    result = currentResult;
                    minimum = item;
                }
            }

            return minimum;
        }

        public static double SumOrDefault(this IEnumerable<double> that)
        {
            if (!that.Any())
            {
                return 0.0;
            }
            else
            {
                return that.Sum();
            }
        }

        public static T MaxOrNull<T>(this IEnumerable<T> that, Func<T, IComparable> projectionFunction)
            where T : class
        {
            IComparable result = null;
            T maximum = default(T);
            if (!that.Any())
            {
                return maximum;
            }

            maximum = that.First();
            result = projectionFunction(maximum);
            foreach (T item in that.Skip(1))
            {
                IComparable currentResult = projectionFunction(item);
                if (result.CompareTo(currentResult) < 0)
                {
                    result = currentResult;
                    maximum = item;
                }
            }

            return maximum;
        }

        public static IEnumerable<R> Zip<T0, T1, R>(IEnumerable<T0> enumerable0, IEnumerable<T1> enumerable1, Func<T0, T1, R> func)
        {
            IEnumerator<T0> enumerator0 = enumerable0.GetEnumerator();
            IEnumerator<T1> enumerator1 = enumerable1.GetEnumerator();
            while (enumerator0.MoveNext() && enumerator1.MoveNext())
            {
                yield return func(enumerator0.Current, enumerator1.Current);
            }
        }

        public static IEnumerable<T> Iterate<T>(T value, Func<T, T> nextFunction)
        {
            yield return value;
            while (true)
            {
                value = nextFunction(value);
                yield return value;
            }
        }

        public static int IndexOf(this IEnumerable that, object value)
        {
            int index = 0;
            foreach (object item in that)
            {
                if (object.ReferenceEquals(value, item) || value.Equals(item))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public static void ForEachWithIndex<T>(this IEnumerable<T> that, Action<T, int> action)
        {
            int index = 0;
            foreach (T item in that)
            {
                action(item, index);
                index++;
            }
        }

        public static T? MaxOrNullable<T>(this IEnumerable<T> that)
            where T : struct, IComparable
        {
            if (!that.Any())
            {
                return null;
            }
            return that.Max();
        }

        public static T? MinOrNullable<T>(this IEnumerable<T> that)
            where T : struct, IComparable
        {
            if (!that.Any())
            {
                return null;
            }
            return that.Min();
        }

        public static IEnumerable<TSource> DistinctOfSorted<TSource>(this IEnumerable<TSource> source)
        {
            IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (enumerator.MoveNext())
            {
                TSource last = enumerator.Current;
                yield return last;
                while (enumerator.MoveNext())
                {
                    if (!enumerator.Current.Equals(last))
                    {
                        last = enumerator.Current;
                        yield return last;
                    }
                }
            }
        }

        public static T FastElementAt<T>(this IEnumerable that, int index)
        {
            {
                IList<T> list = that as IList<T>;
                if (list != null)
                {
                    return list[index];
                }
            }
            {
                IList list = that as IList;
                if (list != null)
                {
                    return (T)list[index];
                }
            }
            return that.Cast<T>().ElementAt(index);
        }
    }

    internal static class FrameworkElementExtensions
    {
        public static double GetActualMargin(this FrameworkElement element, IAxis axis)
        {
            double length = 0.0;
            if (axis.Orientation == AxisOrientation.X)
            {
                length = element.ActualWidth;
            }
            else if (axis.Orientation == AxisOrientation.Y)
            {
                length = element.ActualHeight;
            }
            return length / 2.0;
        }

        public static double GetMargin(this FrameworkElement element, IAxis axis)
        {
            double length = 0.0;
            if (axis.Orientation == AxisOrientation.X)
            {
                length = !double.IsNaN(element.Width) ? element.Width : element.ActualWidth;
            }
            else if (axis.Orientation == AxisOrientation.Y)
            {
                length = !double.IsNaN(element.Height) ? element.Height : element.ActualHeight;
            }
            return length / 2.0;
        }
    }

    #endregion
}
