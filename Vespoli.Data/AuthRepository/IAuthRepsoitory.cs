using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vespoli.Domain;

namespace Vespoli.Data
{
    public interface IAuthRepository
    {
        Task<Rower> Register(Rower rower, string password);
        Task<Rower> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
