using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebServer2.Models;

namespace WebServer2.Data
{
    public class WebServer2Context : DbContext
    {
        public WebServer2Context (DbContextOptions<WebServer2Context> options)
            : base(options)
        {
        }

        public DbSet<WebServer2.Models.Contact>? Contact { get; set; }

        public DbSet<WebServer2.Models.Message> Message { get; set; }

        public DbSet<WebServer2.Models.User> User { get; set; }
    }
}
