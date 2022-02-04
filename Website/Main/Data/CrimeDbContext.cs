using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Main.Data;

public class CrimeDbContext : IdentityDbContext
{
    public CrimeDbContext(DbContextOptions<CrimeDbContext> options)
        : base(options)
    {
    }
}
