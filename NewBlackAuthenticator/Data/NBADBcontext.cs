using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewBlackAuthenticator.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace NewBlackAuthenticator.Data
{
    public class NBADBcontext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TwoFARegistration> TwoFARegistrations { get; set; }


        public NBADBcontext(DbContextOptions<NBADBcontext> options)
    : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlite(ConfigurationManager.ConnectionStrings["NBADB"].ConnectionString);
            //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["NBADB"].ConnectionString);

           optionsBuilder.UseSqlite("Data Source=nbadb.db");

        }
    }
}
