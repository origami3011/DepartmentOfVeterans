using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentOfVeterans.Application.Features.Users.Queries.GetUserDetail
{
    public class GetUserDetailQuery : IRequest<UserDetailVM>
    {
        public Guid Id { get; set; }
    }
}
