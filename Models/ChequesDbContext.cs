using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChequesApi.Models
{
    public class ChequesDbContext: DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=ChequesDB;Integrated Security=True;Pooling=False");
            //optionsBuilder.UseSqlServer(@"Data Source=LAP0301TEC357\SQLEXPRESS;Initial Catalog=FacturacionDB;Trusted_Connection=True;");

        }
    }
}
