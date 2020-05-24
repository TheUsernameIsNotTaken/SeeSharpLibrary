using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Library_Models;
using WebAPI_Server.Repositories;

namespace WebAPI_Server.Controllers
{
    [Route("api/archive")]
    [ApiController]
    public class ArchiveDataController : ControllerBase
    {

        /* -------------------- */
        /*      SIMPLE GET      */
        /* -------------------- */

        //Send all archived data.
        [HttpGet]
        public ActionResult<IEnumerable<ArchiveData>> Get()
        {
            var allData = ArchiveDataRepository.GetAll();
            return Ok(allData);
        }

        //Send a specific archived data.
        [HttpGet("{id}")]
        public ActionResult<ArchiveData> Get(long id)
        {
            var data = ArchiveDataRepository.GetSingle(id);
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

        //Send a specific archived data by the two ids.
        [HttpGet("specific/{bookId}+{borrowerId}")]
        public ActionResult<ArchiveData> GetSpecific(long bookId, long borrowerId)
        {
            var data = ArchiveDataRepository.GetLastByIds(bookId, borrowerId);
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

        /* ------------------------- */
        /*      SEARCH WITH GET      */
        /* ------------------------- */

        //Send a specific archived data by the book id.
        [HttpGet("book/{bookId}")]
        public ActionResult<ArchiveData> GetBook(long bookId)
        {
            var data = ArchiveDataRepository.GetManyByBook(bookId);
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

        //Send a specific archived data by the person id.
        [HttpGet("person/{borrowerId}")]
        public ActionResult<ArchiveData> GetPerson(long borrowerId)
        {
            var data = ArchiveDataRepository.GetManyByPerson(borrowerId);
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

        /* ------------- */
        /*      POST     */
        /* ------------- */

        //Receive and store a single archived data.
        [HttpPost]
        public ActionResult Post(ArchiveData data)
        {
            ArchiveDataRepository.AddData(data);

            return Ok();
        }

        /* ------------ */
        /*      PUT     */
        /* ------------ */

        //Receive and update a single archived data.
        [HttpPut("{id}")]
        public ActionResult Put(ArchiveData person, long id)
        {
            var dbPerson = ArchiveDataRepository.GetSingle(id);
            //Update if it exist
            if (dbPerson != null) {
                ArchiveDataRepository.UpdateData(person);
                return Ok();
            }
            return NotFound();
        }

        /* ---------------- */
        /*      DELETE      */
        /* ---------------- */

        //Delete a single archived data from the server
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            var person = ArchiveDataRepository.GetSingle(id);
            if (person != null)
            {
                ArchiveDataRepository.DeleteData(person);
                return Ok();
            }
            return NotFound();
        }
    }
}