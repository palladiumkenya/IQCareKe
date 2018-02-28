using Microsoft.EntityFrameworkCore;

namespace IQCare.SharedKernel.Infrastructure
{
    public abstract class BaseContext : DbContext
    {
        protected BaseContext(DbContextOptions options) : base(options)
        {
        }
    }
}