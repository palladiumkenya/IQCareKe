using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using IQCare.Common.Core.Models;
using IQCare.SharedKernel.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IQCare.Common.Infrastructure
{
    public class CommonDbContext : BaseContext
    {
        public CommonDbContext(DbContextOptions<CommonDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<LookupItemView>().ToTable("LookupItemView").HasKey(x => x.RowID);
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
            var assembly = Assembly.GetAssembly(typeof(CommonDbContext));

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