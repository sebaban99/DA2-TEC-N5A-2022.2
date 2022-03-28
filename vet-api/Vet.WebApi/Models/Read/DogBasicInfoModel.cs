using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vet.WebApi.Models.Read
{
    public class DogBasicInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
    }
}