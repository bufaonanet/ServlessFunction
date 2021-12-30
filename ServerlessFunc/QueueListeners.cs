using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace ServerlessFunc
{
    public static class QueueListeners
    {
        [FunctionName("QueueListeners")]
        public static async Task Run(
            [QueueTrigger("todos", Connection = "AzureWebJobsStorage")] Todo todo,
            [Blob("todos", Connection = "AzureWebJobsStorage")] CloudBlobContainer container,
            ILogger log)
        {
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference($"{todo.Id}.txt");
            await blob.UploadTextAsync($"Created a new task: {todo.TaskDescrition}");
            log.LogInformation($"C# Queue trigger function processed: {todo.TaskDescrition}");
        }
    }
}