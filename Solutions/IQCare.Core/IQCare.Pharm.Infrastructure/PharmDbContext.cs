using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using IQCare.SharedKernel.Infrastructure;

namespace IQCare.Pharm.Infrastructure
{
   public class PharmDbContext :BaseContext
    {

        public PharmDbContext(DbContextOptions options):base(options)
        {

        }
    }
}
