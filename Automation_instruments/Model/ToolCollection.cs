using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Automation_instruments.Model
{
    class Collection : DbContext
    {
        internal Collection() : base("DB_Connection") { }

        public DbSet<Caliber> Calibers { get; set; }
        public DbSet<Clamp> Clamps { get; set; }
        public DbSet<Plug> Plugs { get; set; }
        public DbSet<Ring> Rings { get; set; }
        public DbSet<Template> Templates { get; set; }
    }
}
