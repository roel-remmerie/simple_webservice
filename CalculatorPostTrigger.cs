using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MCT.Functions
{
    public class CalculatorPostTrigger
    {
        private readonly ILogger<CalculatorPostTrigger> _logger;

        public CalculatorPostTrigger(ILogger<CalculatorPostTrigger> logger)
        {
            _logger = logger;
        }

        [Function("CalculatorPostTrigger")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "calculator")] HttpRequest req)
        {
            CalculationRequest request = await req.ReadFromJsonAsync<CalculationRequest>();

            int result;

            switch (request.Operation)
            {
                case "add": result = request.A + request.B; break;
                case "subtract": result = request.A - request.B; break;
                case "multiply": result = request.A * request.B; break;
                case "divide": result = request.A / request.B; break;
                default: return new BadRequestObjectResult($"Invalid operation: {request.Operation}");
            }
            CalculatorResult r = new CalculatorResult();
            r.A = request.A;
            r.B = request.B;
            r.Result = result; 

            return new OkObjectResult(r);
        }
    }
}
