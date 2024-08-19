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
                await LoadUsersAsync(loggerFactory, context);
  
                await LoadSoftwaresAsync(loggerFactory, context);
                await LoadUserDownloadsAsync(loggerFactory, context);
                await LoadReviewsAsync(loggerFactory, context);
      
                await LoadSoftwareTagsAsync(loggerFactory, context);
                await LoadSoftwareListsAsync(loggerFactory, context);
            }

            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<TiendaSoftwareSeeder>();
                logger.LogError(e, "Error inicializando la data del API");
            }
        }

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
        public static async Task LoadUserDownloadsAsync(ILoggerFactory loggerFactory, TiendaSoftwareContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/UserDownloads.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var userDownloads = JsonConvert.DeserializeObject<List<UserDownloadsEntity>>(jsonContent);

                if (!await context.UserDownloads.AnyAsync())
                {
                    for (int i = 0; i < userDownloads.Count; i++)
                    {
                        userDownloads[i].CreatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        userDownloads[i].CreatedDate = DateTime.Now;
                        userDownloads[i].UpdatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        userDownloads[i].UpdatedDate = DateTime.Now;
                        
                    }

                    context.AddRange(userDownloads);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<TiendaSoftwareSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de userdownloads");
            }
        }

        public static async Task LoadSoftwareTagsAsync(ILoggerFactory loggerFactory, TiendaSoftwareContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/SoftwareTags.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var softwareTags = JsonConvert.DeserializeObject<List<SoftwareTagsEntity>>(jsonContent);

                if (!await context.SoftwareTags.AnyAsync())
                {
                    for (int i = 0; i < softwareTags.Count; i++)
                    {
                        softwareTags[i].CreatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        softwareTags[i].CreatedDate = DateTime.Now;
                        softwareTags[i].UpdatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        softwareTags[i].UpdatedDate = DateTime.Now;
                      
                    }

                    context.AddRange(softwareTags);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<TiendaSoftwareSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de softwaretagd");
            }
        }

        public static async Task LoadSoftwareListsAsync(ILoggerFactory loggerFactory, TiendaSoftwareContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/SoftwareLists.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var softwareLists = JsonConvert.DeserializeObject<List<ListSoftwareEntity>>(jsonContent);

                if (!await context.SoftwareLists.AnyAsync())
                {
                    for (int i = 0; i < softwareLists.Count; i++)
                    {
                        softwareLists[i].CreatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        softwareLists[i].CreatedDate = DateTime.Now;
                        softwareLists[i].UpdatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        softwareLists[i].UpdatedDate = DateTime.Now;
                 
                    }

                    context.AddRange(softwareLists);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<TiendaSoftwareSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de softwarelist");
            }
        }

        public static async Task LoadUsersAsync(ILoggerFactory loggerFactory, TiendaSoftwareContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/Users.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var users = JsonConvert.DeserializeObject<List<UserEntity>>(jsonContent);

                if (!await context.Users.AnyAsync())
                {
                    for (int i = 0; i < users.Count; i++)
                    {
                        users[i].CreatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        users[i].CreatedDate = DateTime.Now;
                        users[i].UpdatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        users[i].UpdatedDate = DateTime.Now;
              
                    }

                    context.AddRange(users);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<TiendaSoftwareSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de users");
            }
        }

        public static async Task LoadSoftwaresAsync(ILoggerFactory loggerFactory, TiendaSoftwareContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/Softwares.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var softwares = JsonConvert.DeserializeObject<List<SoftwareEntity>>(jsonContent);

                if (!await context.Software.AnyAsync())
                {
                    for (int i = 0; i < softwares.Count; i++)
                    {
                        softwares[i].CreatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        softwares[i].CreatedDate = DateTime.Now;
                        softwares[i].UpdatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                       
                    }

                    context.AddRange(softwares);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<TiendaSoftwareSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de software");
            }
        }

        public static async Task LoadReviewsAsync(ILoggerFactory loggerFactory, TiendaSoftwareContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/Reviews.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var reviews = JsonConvert.DeserializeObject<List<ReviewEntity>>(jsonContent);

                if (!await context.Reviews.AnyAsync())
                {
                    for (int i = 0; i < reviews.Count; i++)
                    {
                        reviews[i].CreatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        reviews[i].CreatedDate = DateTime.Now;
                        reviews[i].UpdatedBy = "7fc2cdf1-a339-4c13-88d4-82a32810d5c0";
                        reviews[i].UpdatedDate = DateTime.Now;
                        
                    }

                    context.AddRange(reviews);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<TiendaSoftwareSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de reviews");
            }
        }

    }
}