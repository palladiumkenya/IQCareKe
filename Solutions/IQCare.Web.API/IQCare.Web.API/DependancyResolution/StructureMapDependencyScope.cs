using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace IQCare.Web.Api.DependancyResolution {
    /// <summary>
    /// The structure map dependency scope.
    /// </summary>
    public class StructureMapDependencyScope : ServiceLocatorImplBase {
        #region Constants and Fields

        private const string NestedContainerKey = "Nested.Container.Key";

        #endregion

        #region Constructors and Destructors

        public StructureMapDependencyScope(IContainer container) {
            if (container == null) {
                throw new ArgumentNullException("container");
            }
            Container = container;
        }

        #endregion

        #region Public Properties

        public IContainer Container { get; set; }

        public IContainer CurrentNestedContainer {
            get {
                return (IContainer)HttpContext.Items[NestedContainerKey];
            }
            set {
                HttpContext.Items[NestedContainerKey] = value;
            }
        }

        #endregion

        #region Properties

        private HttpContextBase HttpContext {
            get {
                var ctx = Container.TryGetInstance<HttpContextBase>();
                return ctx ?? new HttpContextWrapper(System.Web.HttpContext.Current);
            }
        }

        #endregion

        #region Public Methods and Operators

        public void CreateNestedContainer() {
            if (CurrentNestedContainer != null) {
                return;
            }
            CurrentNestedContainer = Container.GetNestedContainer();
        }

        public void Dispose() {
            DisposeNestedContainer();
            Container.Dispose();
        }

        public void DisposeNestedContainer() {
            if (CurrentNestedContainer != null) {
                CurrentNestedContainer.Dispose();
				CurrentNestedContainer = null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return DoGetAllInstances(serviceType);
        }

        #endregion

        #region Methods

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType) {
            return (CurrentNestedContainer ?? Container).GetAllInstances(serviceType).Cast<object>();
        }

        protected override object DoGetInstance(Type serviceType, string key) {
            IContainer container = (CurrentNestedContainer ?? Container);

            if (string.IsNullOrEmpty(key)) {
                return serviceType.IsAbstract || serviceType.IsInterface
                    ? container.TryGetInstance(serviceType)
                    : container.GetInstance(serviceType);
            }

            return container.GetInstance(serviceType, key);
        }

        #endregion
    }

    public class StructureMapDependencyScopes : IDependencyScope
    {
        protected readonly IContainer Container;

        public StructureMapDependencyScopes(IContainer container)
        {
            Container = container;
        }

        public object GetService(Type serviceType)
        {
            return serviceType.IsAbstract || serviceType.IsInterface
                ? Container.TryGetInstance(serviceType)
                : Container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.GetAllInstances(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }

    public class StructureMapDependencyResolver : StructureMapDependencyScope, IDependencyResolver
    {
        public StructureMapDependencyResolver(IContainer container) : base(container) { }

        public IDependencyScope BeginScope()
        {
            var childContainer = Container.GetNestedContainer();
            return new StructureMapDependencyScopes(childContainer);
        }
    }
}

