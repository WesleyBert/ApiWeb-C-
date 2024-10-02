using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AzureFunctionsAppExemplo
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "pvc")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-type", "text/plain; charset=utf-8");

            response.WriteString("Bem vindo programa vc ");

            return response;
        }
    }
}
