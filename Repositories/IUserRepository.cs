﻿using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User getUserByEmailAndPassword(string email, string password);

        Task<bool> UpdateStatusUser(int? userId, bool status);
    }
}
