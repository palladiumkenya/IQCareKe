using System;
using System.Collections.Generic;
using System.Text;
using IQCare.SharedKernel.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IQCare.AIR.Infrastructure
{
    public class AirDbContext : BaseContext
    {
        public AirDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
