using Microsoft.EntityFrameworkCore;
using SSRUnited.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSRUnited.Shared.Data
{
    public class ApplicatonDbContext : DbContext
    {
        public ApplicatonDbContext(DbContextOptions<ApplicatonDbContext> options) : base (options)
        {

        }
        public  DbSet<Human> humans { get; set; }
    }
}
