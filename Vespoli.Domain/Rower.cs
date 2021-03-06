﻿using System;
using System.Collections.Generic;

namespace Vespoli.Domain
{
    public class Rower
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ICollection<Workout> Workouts { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        public string Introduction { get; set; }
        public string School { get; set; }
    }
}
