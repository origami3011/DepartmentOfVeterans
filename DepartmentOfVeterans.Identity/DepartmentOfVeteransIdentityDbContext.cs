using DepartmentOfVeterans.Identity.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentOfVeterans.Identity
{
    public class DepartmentOfVeteransIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public DepartmentOfVeteransIdentityDbContext()
        {
        }

        public DepartmentOfVeteransIdentityDbContext(DbContextOptions<DepartmentOfVeteransIdentityDbContext> options) : base(options)
        {
        }
    }
}
