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
        //Send all person's data.
        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            var people = PersonRepository.GetPeople();
            return Ok(people);
        }

        //Send a specific person's data.
        [HttpGet("{id}")]
        public ActionResult<Person> Get(long id)
        {
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

        //Search person by their name.
        [HttpGet("search/{name}")]
        public ActionResult<IEnumerable<Book>> SearchByCode(string name)
        {
            var books = PersonRepository.SearchPersonByName(name);
            //Check successs
            if (books != null)
            {
                return Ok(books);
            }
            else
            {
                return NotFound();
            }
        }

        //Receive and store a single person's data.
        [HttpPost]
        public ActionResult Post(Person person)
        {
            PersonRepository.AddPerson(person);

            return Ok();
        }

        //Receive and update a single person's data.
        [HttpPut("{id}")]
        public ActionResult Put(Person person, long id)
        {
            var dbPerson = PersonRepository.GetPerson(id);
            //Update if it exist
            if (dbPerson != null) {
                PersonRepository.UpdatePerson(person);
                return Ok();
            }
            return NotFound();
        }

        //Delete a single person's data from the server.
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
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