using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace FunctionApp1
{
    public static class Function01
    {
        [Function("Function01")]
        public static async Task<HttpResponseData> Run([
            HttpTrigger(AuthorizationLevel.Anonymous, "get","post", Route = null)]
        HttpRequestData res, FunctionContext context)
        {
            var logger = context.GetLogger("Function01");
            logger.LogInformation("C# HTTP trigger function processed a resquest");

            string name = res.Query["name"];
            string requestBody = await new StreamReader(res.Body).ReadToEndAsync();

            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var response = res.CreateResponse(HttpStatusCode.OK);
            if(name != null)
            {
                await response.WriteStringAsync($"Hello, {name}");
            }
            else
            {
                response = res.CreateResponse(HttpStatusCode.BadRequest);
                await response.WriteStringAsync("Por favor adicione um nome ao corpo da requisição!");
            }
            return response;
        }
    }
}