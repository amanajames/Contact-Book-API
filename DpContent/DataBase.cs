using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseContext
{
    public class DataBase : IdentityDbContext<User>
    {
        public DataBase(DbContextOptions<DataBase> options) : base(options)    
        {

        }
          
    }
}
