using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace IQCare.PMTCT.Services.Interface.Triage
{
    public static class ZscoreDependenciesInstaller
    {
        public static void AddGetZscoreParametersService(this IServiceCollection services)
        {
            services.AddSingleton<IGetZscoreLmsParameters, GetZscoreLmsParameters>();
        }

        public static void AddZscoreCalculatorServices(this IServiceCollection services)
        {
            var zscoreCalculatorInterface = typeof(IZscoreCalculator);

            var zscoreCalculatorImplementations = Assembly.GetAssembly(zscoreCalculatorInterface).GetTypes()
                .Where(x => x.IsClass)
                .Where(x => zscoreCalculatorInterface.IsAssignableFrom(x)).ToList();

            foreach (var implementation in zscoreCalculatorImplementations)
                services.AddSingleton(zscoreCalculatorInterface, implementation);
        }


        public static void AddZscoreTypeAndSprocDictionaryMapping(this IServiceCollection services)
        {
            var zscoreSprocMapping = new ConcurrentDictionary<ZscoreType, string>();
            zscoreSprocMapping.TryAdd(ZscoreType.HeightForAge, "[dbo].[Get_HeightForAge_Zscore_Constants]");
            zscoreSprocMapping.TryAdd(ZscoreType.Bmiz, "[dbo].[Get_Bmi_Zscore_Constants]");
            zscoreSprocMapping.TryAdd(ZscoreType.WeightForAge, "[dbo].[Get_Weight_ForAge_Zscore_Constants]");
            zscoreSprocMapping.TryAdd(ZscoreType.WeightForHeight, "[dbo].[Get_Weight_ForHeight_Zscore_Constants]");

            services.AddSingleton(zscoreSprocMapping);
        }
    }
}
