using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vespoli.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Vespoli.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly VespoliContext _context;
        private readonly ILogger<AuthRepository> _logger;

        public AuthRepository(VespoliContext context, ILogger<AuthRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Rower> Login(string username, string password)
        {
            var rower = await _context.Rowers.FirstOrDefaultAsync(x => x.UserName == username);

            if (rower == null)
                return null;

            if (!VerifyPasswordHash(password, rower.PasswordHash, rower.PasswordSalt))
                return null;

            return rower;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }

        public async Task<Rower> Register(Rower rower, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            rower.PasswordHash = passwordHash;
            rower.PasswordSalt = passwordSalt;

            await _context.Rowers.AddAsync(rower);
            await _context.SaveChangesAsync();

            return rower;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
                
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Rowers.AnyAsync(r => r.UserName == username))
                return true;

            return false;
        }
    }
}
