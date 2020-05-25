using Microsoft.AspNetCore.Mvc;

namespace WebAPI_Server.Controllers
{
    [Route("api/hello")]
    [ApiController]
    public class HelloController : ControllerBase
    {

        /* -------------------- */
        /*      SIMPLE GET      */
        /* -------------------- */

        //A single get to test the server's status
        [HttpGet]
        public string Get()
        {
            return "Hello world!";
        }
    }
}