using System;
using System.Collections.Generic;
using System.Text;
using IQCare.SharedKernel.Infrastructure;
using JetBrains.Annotations;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IQCare.Queue.Infrastructure
{
   public class QueueDbContext:BaseContext
    {
         public QueueDbContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyEntityTypeConfigsFromAssembly();

        }
    }

    public static class ModelBuilderConfigurationExtension
    {
        private static readonly Dictionary<Assembly, IEnumerable<Type>> typesPerAssembly = new Dictionary<Assembly, IEnumerable<Type>>();

        public static ModelBuilder ApplyEntityTypeConfigsFromAssembly(this ModelBuilder builder)
        {
            IEnumerable<Type> configurationTypes;
            var assembly = Assembly.GetAssembly(typeof(QueueDbContext));

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
