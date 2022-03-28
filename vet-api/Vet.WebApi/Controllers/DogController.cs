using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Vet.WebApi.Models.Read;
using Vet.WebApi.Models.Write;

namespace Vet.Api.BusinessLogic
{
    [ApiController]
    [Route("dogs")]
    public class DogController : ControllerBase
    {
        private static IEnumerable<Dog> _dogs;

        public DogController()
        {
            _dogs = new List<Dog>();
        }

        [HttpGet]
        public IActionResult GetAllDogs()
        {
            return Ok(_dogs.Select(dog => new DogBasicInfoModel
            {
                Id = dog.Id,
                Name = dog.Name
            }));
        }

        [HttpGet("{dogId}", Name = "GetDogById")]
        public IActionResult GetDog(int dogId)
        {
            var dogSaved = _dogs.FirstOrDefault(dog => dog.Id == dogId);

            if (dogSaved is null)
            {
                return NotFound();
            }

            return Ok(new DogDetailInfoModel
            {
                Id = dogSaved.Id,
                Name = dogSaved.Name,
                Age = dogSaved.Age,
                Race = dogSaved.Race,
                Owner = new OwnerBasicInfoModel
                {
                    Id = dogSaved.OwnerId,
                    Name = dogSaved.Owner.Name,
                    Address = dogSaved.Owner.Address,
                    PhoneNumber = dogSaved.Owner.PhoneNumber
                }
            });
        }

        [HttpPost]
        public IActionResult CreateAdog(DogModel dog)
        {
            var newDog = new Dog
            {
                Id = _dogs.Count() + 1,
                Name = dog.Name,
                Age = dog.Age,
                Race = dog.Race,
                OwnerId = dog.OwnerId
            };

            _dogs.ToList().Add(newDog);

            return CreatedAtRoute("GetDogById", new { dogId = newDog.Id }, newDog);
        }

        [HttpPut("{dogId}")]
        public IActionResult UpdateAdog(int dogId, DogModel dog)
        {
            var dogSaved = _dogs.FirstOrDefault(dog => dog.Id == dogId);

            if (dogSaved is null)
            {
                return NotFound();
            }

            dogSaved.Age = dog.Age;

            return NoContent();
        }

        [HttpDelete("{dogId}")]
        public IActionResult DeleteAdog(int dogId)
        {
            var dogSaved = _dogs.FirstOrDefault(dog => dog.Id == dogId);

            if (dogSaved is null)
            {
                return NotFound();
            }
            
            _dogs = _dogs.Where(dog => dog.Id != dogId);

            return NoContent();
        }
    }

    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public string Race { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
