# graph-tutorial-aad

This repo is heavily based on this MS example: https://docs.microsoft.com/en-us/graph/tutorials/dotnet-core but tweaked slightly to get all AAD groups for a user.

# Steps

1. Read through the tutorial above.
2. Follow the tutorial steps above to setup a new **App registration** in Azure AD for your new console app.
3. Within the portal settings for the app, under **Authentication** setup a new 'Mobile and desktop applications' platform configuration. Use the settings in the tutorial.
4. Under **API premissions**, make sure to add MS Graph - Delegated permissions for User.Read and Directory.ReadAll
5. Matching those to your user-secrets within VS locally:
```
{
  "scopes": "User.Read;Directory.Read.All;",
  "appId": "YOUR-APP-ID-GUID"
}
```
6. Run the app, it will prompt you to authenticate via https://microsoft.com/devicelogin and provide a code to use.
7. Once authenticated, choose 1 or 2 from the menu options of the console app to view the Group IDs or Group Names for the user within AAD e.g.
![image](https://user-images.githubusercontent.com/94632631/149662468-67a01c77-fe14-4055-a131-5222493e60fc.png)
