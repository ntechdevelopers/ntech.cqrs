using Microsoft.EntityFrameworkCore;
using Ntech.CQRS.Core.Entities;

namespace Ntech.CQRS.Core.Context;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
    }

    public DbSet<Student> Students => Set<Student>();
}
