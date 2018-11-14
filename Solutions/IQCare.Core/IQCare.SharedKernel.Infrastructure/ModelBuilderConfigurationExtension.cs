using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace IQCare.SharedKernel.Infrastructure
{
    public static class ModelBuilderConfigurationExtension
    {
        private static readonly Dictionary<Assembly, IEnumerable<Type>> typesPerAssembly = new Dictionary<Assembly, IEnumerable<Type>>();

        public static ModelBuilder ApplyEntityTypeConfigsFromAssembly(this ModelBuilder builder, Assembly assembly)
        {
            try
            {
                IEnumerable<Type> configurationTypes;
                //var assembly = Assembly.GetAssembly(typeof(PmtctDbContext));

                if (typesPerAssembly.TryGetValue(assembly, out configurationTypes) == false)
                {
                    typesPerAssembly[assembly] = configurationTypes = assembly
                        .GetExportedTypes()
                        .Where(x => (x.GetTypeInfo().IsClass == true)
                                    && (x.GetTypeInfo().IsAbstract == false)
                                    && (x.GetInterfaces().Any(y => (y.GetTypeInfo().IsGenericType == true)
                                                                   && (y.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))));
                }

                var configurations = configurationTypes.Select(x => Activator.CreateInstance(x));

                foreach (dynamic configuration in configurations)
                {
                    builder.ApplyConfiguration(configuration);
                }
                return builder;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
