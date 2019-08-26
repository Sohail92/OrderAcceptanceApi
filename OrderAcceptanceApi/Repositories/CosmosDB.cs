using Microsoft.Azure.Cosmos;
using OrderAcceptanceApi.Interfaces;
using OrderAcceptanceApi.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace OrderAcceptanceApi.Repositories
{
    public class CosmosDB : ILogToDB
    {
        // The Azure Cosmos DB endpoint 
        private string EndpointUrl = "https://orderacceptance.documents.azure.com:443/";
        // The primary key for the Azure DocumentDB account.
        private string PrimaryKey = "dVPoqYtmEvbAjLLwBu9uU5nAdk9ZjbwL0ZUfzNHf2mUdzE5QNYuJIdULRplts2YyEpyDG3LSUMNJ2zafeY8HMA==";
        // The Cosmos client instance
        private CosmosClient cosmosClient;
        // The database we will create or use
        private Database database;
        // The container we will create or use
        private Container container;
        // The name of the database and container we will create
        private string databaseId = "orderacceptance";
        private string containerId = "orders";

        public CosmosDB()
        {
            cosmosClient = new CosmosClient(EndpointUrl, PrimaryKey);
        }

        public async Task LogBuyOrderToDB(BuyOrder order)
        {
            try
            {
                await CreateDatabaseAsync();
                await CreateContainerAsync();
                await AddBuyOrderToDbAsync(order);
            }
            catch (CosmosException ce)
            {
                // Log Error
            }
            catch (Exception e)
            {
                // Log Error
            }
        }

        public async Task LogSellOrderToDB(SellOrder order)
        {
            try
            {
                await CreateDatabaseAsync();
                await CreateContainerAsync();
                await AddSellOrderToDbAsync(order);
            }
            catch (CosmosException ce)
            {
                // Log Error
            }
            catch (Exception e)
            {
                // Log Error
            }
        }

        /// <summary>
        /// Creates a database if it doesnt already exist based on the databaseId
        /// </summary>
        private async Task CreateDatabaseAsync()
        {
            database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
        }

        /// <summary>
        /// Creates a container if it doesnt already exist. 
        /// Specifiy "/SearchValue" as the partition key since we're storing user search values, to ensure good distribution of requests and storage.
        /// </summary>
        private async Task CreateContainerAsync()
        {
            container = await database.CreateContainerIfNotExistsAsync(new ContainerProperties() { Id = containerId, PartitionKeyPath = "/PartnerId" });
        }

        private async Task AddBuyOrderToDbAsync(BuyOrder order)
        {
            try
            {
                // Read the item to see if it exists. ReadItemAsync will throw an exception if the item does not exist and return status code 404 (Not found).
                ItemResponse<BuyOrder> searchResponse = await container.ReadItemAsync<BuyOrder>(order.id.ToString(), new PartitionKey(order.PartnerId));
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Create an item in the container representing the users search. Note we provide the value of the partition key for the item
                ItemResponse<BuyOrder> searchResponse = await container.CreateItemAsync(order, new PartitionKey(order.PartnerId));
            }
        }

        private async Task AddSellOrderToDbAsync(SellOrder order)
        {
            try
            {
                // Read the item to see if it exists. ReadItemAsync will throw an exception if the item does not exist and return status code 404 (Not found).
                ItemResponse<BuyOrder> searchResponse = await container.ReadItemAsync<BuyOrder>(order.id.ToString(), new PartitionKey(order.PartnerId));
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Create an item in the container representing the users search. Note we provide the value of the partition key for the item
                ItemResponse<SellOrder> searchResponse = await container.CreateItemAsync(order, new PartitionKey(order.PartnerId));
            }
        }

        public Task LogError(Exception ex)
        {
            // Here we could write error logging functionality. A decision would have to be made in regards to whether we log to Cosmos or to a file/service such as Elmah
            return null;
        }
    }
}
