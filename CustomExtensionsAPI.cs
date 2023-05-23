using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Customers.Publix
{
    public static class CustomExtensionsAPI
    {
        [FunctionName("CustomExtensionsAPI")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            // Read the correlation ID from the Azure AD  request    
            string correlationId = data?.data.authenticationContext.correlationId;

            // Claims to return to Azure AD
            ResponseContent r = new ResponseContent();
            r.data.actions[0].claims.CorrelationId = correlationId;
            r.data.actions[0].claims.ApiVersion = "1.0.0";
            r.data.actions[0].claims.DateOfBirth = "01/01/2000";
            r.data.actions[0].claims.CustomRoles.Add("Writer");
            r.data.actions[0].claims.CustomRoles.Add("Editor");
            return new OkObjectResult(r);
        }
    }
}
