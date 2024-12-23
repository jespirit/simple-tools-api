using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Web;

namespace simple_tools_api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SumController : ControllerBase
    {
        public class PriceRequestModel
        {
            public PriceRequestModel() { }

            [JsonPropertyName("prices")]
            public List<double> Values { get; set; }
        }

        public SumController()
        {
        }

        [HttpPost]
        public IActionResult Post([FromBody] PriceRequestModel model)
        {
            try
            {

                var sum = 0.0d;

                // values is a multi-line string where prices are delimited by newlines

                var numberRegex = new Regex(@"^[0-9]+(\.[0-9]+)?$");
                //var prices = Regex.Split(values, @"\r?\n");

                foreach (var price in model.Values)
                {
                    sum += price;
                }

                return Ok(new
                {
                    total = sum
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
