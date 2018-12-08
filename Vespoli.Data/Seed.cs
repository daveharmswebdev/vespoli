using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Vespoli.Domain;

namespace Vespoli.Data
{
    public class Seed
    {
        private readonly VespoliContext _context;
        public Seed(VespoliContext context)
        {
            _context = context;
        }

        public void SeedUsers()
        {
            var rowerData = System.IO.File.ReadAllText("..\\Vespoli.Data\\SeedData.json");
            var rowers = JsonConvert.DeserializeObject<List<Rower>>(rowerData);

            foreach (var rower in rowers)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);

                rower.PasswordHash = passwordHash;
                rower.PasswordSalt = passwordSalt;
                rower.UserName = rower.UserName.ToLower();

                _context.Rowers.Add(rower);
            }

            _context.SaveChanges();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

        }
    }
}
