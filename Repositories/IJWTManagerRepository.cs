using ChequesApi.Models;
using ChequesApi.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChequesApi.Repositories
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(UserViewModel user);
    }
}
