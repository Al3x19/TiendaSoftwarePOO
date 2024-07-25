using TiendaSoftware.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TiendaSoftware.DataBase
{
    public class TiendaSoftwareSeeder
    {
        public static async Task LoadDataAsync(
            TiendaSoftwareContext context,
            ILoggerFactory loggerFactory
            ) 
        {
            try
            {
                await LoadPublishersAsync(loggerFactory, context);
            }
			
            catch (Exception e) 
            {
                var logger = loggerFactory.CreateLogger<TiendaSoftwareSeeder>();
                logger.LogError(e, "Error inicializando la data del API");
            }
        }

        // TODO: Seed de usuarios

        public static async Task LoadPublishersAsync(ILoggerFactory loggerFactory, TiendaSoftwareContext context) 
        {
            try
            {
                var jsonFilePath = "SeedData/Publishers.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var publishers = JsonConvert.DeserializeObject<List<PublisherEntity>>(jsonContent);

                if (!await context.Publishers.AnyAsync()) 
                {
                    for (int i = 0; i < publishers.Count; i++) 
                    {
                        publishers[i].CreatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        publishers[i].CreatedDate = DateTime.Now;
                        publishers[i].UpdatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        publishers[i].UpdatedDate = DateTime.Now;
                    }
                    
                    context.AddRange(publishers);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e) 
            {
                var logger = loggerFactory.CreateLogger<TiendaSoftwareSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de desarrolladores");
            }
        }

    }
}