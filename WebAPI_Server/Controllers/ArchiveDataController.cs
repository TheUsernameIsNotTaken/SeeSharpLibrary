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
    [Route("api/archive")]
    [ApiController]
    public class ArchiveDataController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ArchiveData>> Get()
        {
            var allData = ArchiveDataRepository.GetPeople();
            return Ok(allData);
        }

        [HttpGet("{id}")]
        public ActionResult<ArchiveData> Get(long id)
        {
            var data = ArchiveDataRepository.GetPerson(id);
            //Check successs
            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult Post(ArchiveData data)
        {
            ArchiveDataRepository.AddPerson(data);

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(ArchiveData person, long id)
        {
            var dbPerson = ArchiveDataRepository.GetPerson(id);
            //Update if it exist
            if (dbPerson != null) {
                ArchiveDataRepository.UpdatePerson(person);
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            var person = PersonRepository.GetPerson(id);
            if (person != null)
            {
                ArchiveDataRepository.DeletePerson(person);
                return Ok();
            }
            return NotFound();
        }
    }
}