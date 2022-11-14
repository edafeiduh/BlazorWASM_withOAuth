using FXProject.Shared.DbTables;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FXProject.Server.ForexDbContext
{

    public class FXDbContext : DbContext
    {
        public FXDbContext(DbContextOptions<FXDbContext> options)
            : base(options)
        {

        }
        
        public DbSet<FX_Location> FX_Location { get; set; } = null!;

        public DbSet<FX_UserList> FX_UserList { get; set; } = null!;

       

    }
}
