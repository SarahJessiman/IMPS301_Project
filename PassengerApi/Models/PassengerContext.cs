using Microsoft.EntityFrameworkCore;
using PassengerAPI.Models;

namespace PassengerAPI.Models;

public class PassengerContext : DbContext
{
    public PassengerContext(DbContextOptions<PassengerContext> options)
        : base(options)
    {
    }

    public DbSet<PassengerItem> PassengerItems { get; set; } = null!;
}
