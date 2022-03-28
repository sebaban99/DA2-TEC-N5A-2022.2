using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vet.WebApi.Models.Read
{
    public class DogDetailInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Race { get; set; }
        public OwnerBasicInfoModel Owner { get; set; }
    }
}