using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungmenSoftware.Models.NUMACFirmware
{
    public class NumacFirewareDbContext:DbContext
    {
        public NumacFirewareDbContext()
            :base("name=NUMACFirmware")
        {
            
        }

        public DbSet<Chassis> Chassis { get; set; }
        public DbSet<ChassisBoard> ChassisBoards { get; set; }
        public DbSet<EPROM> EPROMs { get; set; }


    }
    
}
