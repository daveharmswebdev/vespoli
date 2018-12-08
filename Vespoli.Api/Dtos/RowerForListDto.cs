using System;
using System.Collections.Generic;
using Vespoli.Domain;

namespace Vespoli.Api.Dtos
{
    public class RowerForListDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public DateTime Created { get; set; }
        public string School { get; set; }
        public int NumberWorkouts { get; set; }
    }
}