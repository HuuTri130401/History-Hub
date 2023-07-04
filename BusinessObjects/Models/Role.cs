﻿using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }
        public string Permissions { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}