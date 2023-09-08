﻿using DepartmentOfVeterans.Domain.Common;

namespace DepartmentOfVeterans.Domain.Entities
{
    public class User : AuditableEntity
    {
        public Guid UserId { get; set; } 
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Picture { get; set; }
        public string? Gender { get; set; }
    }
}