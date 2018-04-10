using IQCare.HTS.Core.Model;
using IQCare.SharedKernel.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IQCare.HTS.Infrastructure
{
    public class HtsDbContext : BaseContext
    {

        public HtsDbContext(DbContextOptions<HtsDbContext> options) 
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyEntityTypeConfigsFromAssembly();
        }
    }

    public static class ModelBuilderConfigurationExtension
    {
        private static readonly Dictionary<Assembly, IEnumerable<Type>> typesPerAssembly = new Dictionary<Assembly, IEnumerable<Type>>();

        public static ModelBuilder ApplyEntityTypeConfigsFromAssembly(this ModelBuilder builder)
        {
            IEnumerable<Type> configurationTypes;
            var assembly = Assembly.GetAssembly(typeof(HtsDbContext));

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
    }
}