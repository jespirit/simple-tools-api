using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using simple_tools_api.Calculator;
using simple_tools_api.Models;
using System.Text.RegularExpressions;
using System.Web;

namespace simple_tools_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HtmlEncodeController : ControllerBase
    {
        public HtmlEncodeController()
        {
        }

        [HttpPost]
        public IActionResult PostInvalid([FromForm] string htmlString)
        {
            try
            {

                var encodedHtml = HttpUtility.HtmlEncode(htmlString);

                return Ok(new
                {
                    result = encodedHtml
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("encode")]
        public IActionResult Post([FromBody] HtmlEncodeRequestModel model)
        {
            try
            {

                var rand = new Random();

                if (rand.Next() % 2 == 0)
                {
                    throw new Exception("bad luck: you got even number");
                }

                var encodedHtml = HttpUtility.HtmlEncode(model.HtmlString);

                return Ok(new
                {
                    result = encodedHtml
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
