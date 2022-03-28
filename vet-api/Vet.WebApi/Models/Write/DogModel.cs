using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vet.WebApi.Models.Write
{
    public class DogModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Race { get; set; }
        public int OwnerId { get; set; }
    }
}