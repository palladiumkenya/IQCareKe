using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace IQCare.SharedKernel.Infrastructure
{
    public static class ModelBuilderConfigurationExtension
    {
        private static readonly ConcurrentDictionary<Assembly, IEnumerable<Type>> typesPerAssembly =
            new ConcurrentDictionary<Assembly, IEnumerable<Type>>();

        public static ModelBuilder ApplyEntityTypeConfigsFromAssembly(this ModelBuilder builder, Assembly assembly)
        {
            try
            {
               var assembyAdded = typesPerAssembly.TryAdd(assembly, GetConfigurationTypes(assembly));

                if (!assembyAdded)
                    return builder;

                var configurations = GetConfigurationTypes(assembly).Select(x => Activator.CreateInstance(x));

                foreach (dynamic configuration in configurations)
                    builder.ApplyConfiguration(configuration);

                return builder;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private static IEnumerable<Type> GetConfigurationTypes(Assembly assembly)
        {
            return assembly.GetExportedTypes().Where(x => (x.GetTypeInfo().IsClass == true) && (x.GetTypeInfo().IsAbstract == false)
                                      && (x.GetInterfaces().Any(y => (y.GetTypeInfo().IsGenericType == true) 
                                      && (y.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))));
        }
    }
}
