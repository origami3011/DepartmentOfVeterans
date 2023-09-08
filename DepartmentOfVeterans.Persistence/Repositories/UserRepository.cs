using DepartmentOfVeterans.Application.Contracts.Persistence;
using DepartmentOfVeterans.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentOfVeterans.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DepartmentOfVeteransDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> IsUserNameUnigue(string username)
        {
            var matches = _dbContext.Users.Any(e => e.UserName.Equals(username));
            return Task.FromResult(matches);
        }
    }
}
