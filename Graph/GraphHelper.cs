using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Microsoft.Graph;

namespace GraphTutorial
{
    public class GraphHelper
    {
        private static DeviceCodeCredential tokenCredential;
        private static GraphServiceClient graphClient;

        public static void Initialize(string clientId,
                                      string[] scopes,
                                      Func<DeviceCodeInfo, CancellationToken, Task> callBack)
        {
            tokenCredential = new DeviceCodeCredential(callBack, "common", clientId);
            graphClient = new GraphServiceClient(tokenCredential, scopes);
        }

        public static async Task<string> GetAccessTokenAsync(string[] scopes)
        {
            var context = new TokenRequestContext(scopes);
            var response = await tokenCredential.GetTokenAsync(context);
            return response.Token;
        }

        // <GetMeSnippet>
        public static async Task<User> GetMeAsync()
        {
            try
            {
                // GET /me
                return await graphClient.Me
                    .Request()
                    .Select(u => new {
                        u.DisplayName,
                        u.Id
                    })
                    .GetAsync();
            }
            catch (ServiceException ex)
            {
                Console.WriteLine($"Error getting signed-in user: {ex.Message}");
                return null;
            }
        }

        public static async Task<IDirectoryObjectGetMemberGroupsCollectionPage> GetMyGroupIDs()
        {
            try
            {
                // GET /me
                var securityEnabledOnly = true;

                return await graphClient.Me
                    .GetMemberGroups(securityEnabledOnly)
                    .Request()
                    .PostAsync();
            }
            catch (ServiceException ex)
            {
                Console.WriteLine($"Error getting signed-in user groups: {ex.Message}");
                return null;
            }
        }

        public static async Task<IEnumerable<string>> GetMyGroupsNames(Microsoft.Graph.User user)
        {
            try
            {
                var page = await graphClient.Users["{"+user.Id+"}"].MemberOf
                    .Request()
                    .GetAsync();

                var groupNames = new List<string>();

                foreach (var item in page)
                {
                    if (item is Group)
                    {
                        groupNames.Add(((Group)item).DisplayName);
                    }
                }

                return groupNames;
            }
            catch (ServiceException ex)
            {
                Console.WriteLine($"Error getting signed-in user groups: {ex.Message}");
                return null;
            }
        }
 
    }
}