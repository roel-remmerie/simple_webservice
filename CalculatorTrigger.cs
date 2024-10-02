using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MCT.Functions
{
    public class CalculatorTrigger
    {
        private readonly ILogger<CalculatorTrigger> _logger;

        public CalculatorTrigger(ILogger<CalculatorTrigger> logger)
        {
            _logger = logger;
        }

        [Function("CalculatorTrigger")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "calculator/{a:int}/{b:int}")] HttpRequest req,
            string operation,
            int a,
            int b)
        {
            int result = a + b;
            CalculatorResult r = new CalculatorResult();
            r.Result = result;
            r.A = a;
            r.B = b;
            return new OkObjectResult(r);
        }
    }
}
