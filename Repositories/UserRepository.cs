﻿using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public User getUserByEmailAndPassword(string email, string password)
        {
            return _historyHubContext.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
