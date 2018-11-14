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
        private static readonly ConcurrentDictionary<Assembly, IEnumerable<Type>> TypesPerAssembly =
                        new ConcurrentDictionary<Assembly, IEnumerable<Type>>();

        public static ModelBuilder ApplyEntityTypeConfigsFromAssembly(this ModelBuilder builder, Assembly assembly)
        {
            try
            {
                var configurationTypes = TypesPerAssembly.GetOrAdd(assembly, GetConfigurationTypes(assembly));

                var configurations = GetConfigurationTypes(assembly).Select(Activator.CreateInstance);

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
            return assembly.GetExportedTypes().Where(x => x.GetTypeInfo().IsClass && !x.GetTypeInfo().IsAbstract && x.GetInterfaces()
            .Any(y => y.GetTypeInfo().IsGenericType && y.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
        }
    }
}
