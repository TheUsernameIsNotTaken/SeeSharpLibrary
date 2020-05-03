using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library_Models;
using WebAPI_Server.Repositories;

namespace WebAPI_Server.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            var people = PersonRepository.GetPeople();
            return Ok(people);
        }

        [HttpGet("{id}")]
        public ActionResult<Person> Get(long id)
        {
            /* //JSON:
            //Get all
            var people = PersonRepository.GetPeople();
            //Filter
            var person = people.FirstOrDefault(x => x.Id == id);
            */

            //DataBase:
            var person = PersonRepository.GetPerson(id);
            //Check successs
            if (person != null)
            {
                return Ok(person);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult Post(Person person)
        {
            /* //JSON:
            var people = PersonRepository.GetPeople();

            var newId = GetNewId(people);
            person.Id = newId;

            people.Add(person);
            PersonRepository.StorePeople(people);
            */

            //DataBase:
            PersonRepository.AddPerson(person);

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(Person person, long id)
        {
            /* //JSON:
            var people = PersonRepository.GetPeople();

            var oldPerson = people.FirstOrDefault(x => x.Id == person.Id);

            if (oldPerson != null)
            {
                oldPerson.FirstName = person.FirstName;
                oldPerson.LastName = person.LastName;
                oldPerson.DateOfBirth = person.DateOfBirth;
            }
            else
            {
                var newId = GetNewId(people);
                person.Id = newId;
                people.Add(person);
            }

            PersonRepository.AddPeople(people);
            return Ok();
            */

            //DataBase:
            var dbPerson = PersonRepository.GetPerson(id);
            //Update if it exist
            if (dbPerson != null) {
                PersonRepository.UpdatePerson(person);
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            /* //JSON:
            var people = PersonRepository.GetPeople();

            var person = people.FirstOrDefault(x => x.Id == id);

            if (person != null)
            {
                people.Remove(person);
                PersonRepository.AddPeople(people);
                return Ok();
            }

            return NotFound();
            */

            //DataBase:
            var person = PersonRepository.GetPerson(id);
            if (person != null)
            {
                PersonRepository.DeletePerson(person);
                return Ok();
            }
            return NotFound();
        }
    }
}