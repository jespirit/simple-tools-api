using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using simple_tools_api.Calculator;
using simple_tools_api.Models;
using System.Text.RegularExpressions;

namespace simple_tools_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        public LoanController() { }

        [HttpPost]
        public IActionResult Post([FromForm]LoanModel model)
        {
            try
            {
                var numberRegex = new Regex(@"^[0-9]+(\.[0-9]+)?$");

                // Validate input
                if (!numberRegex.IsMatch(model.Principal))
                {
                    throw new Exception("Invalid principal amount format");
                }

                if (!numberRegex.IsMatch(model.Rate))
                {
                    throw new Exception("Invalid rate amount format");
                }

                if (!numberRegex.IsMatch(model.Years))
                {
                    throw new Exception("Invalid months format");
                }

                var A = decimal.Parse(model.Principal);
                var r = double.Parse(model.Rate);
                var n = int.Parse(model.Years);

                var P = MortgageCalculator.CalculateMonthlyMortgagePayment(A, r, n);

                return Ok(new
                {
                    payment = P
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/api/[controller]/house/form")]
        public IActionResult Post([FromForm] LoanRequestModel model)
        {
            try
            {
                var P = MortgageCalculator.CalculateMonthlyMortgagePayment(model.Principal, model.InterestRate, model.Years);

                return Ok(new
                {
                    payment = P
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("house")]
        //[Route("/api/[controller]/house")]
        public IActionResult PostWithJson([FromBody] LoanRequestModel model)
        {
            try
            {
                var P = MortgageCalculator.CalculateMonthlyMortgagePayment(model.Principal, model.InterestRate, model.Years);

                return Ok(new
                {
                    payment = P
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
