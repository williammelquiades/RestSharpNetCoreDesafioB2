using RestSharp;
using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.Helpers;
using System.IO;
using System.Text;

namespace RestSharpNetCoreDesafioB2.Requests.Users
{
    public class POST_CreateOneUserRequest : RequestBase
    {
        public POST_CreateOneUserRequest()
        {
            requestService = "/api/rest/users/";
            
            method = Method.POST;
        }

        public void SetJsonBody(string username,
                                string password,
                                string real_name,
                                string email,
                                string nameLevel,
                                string enabled,
                                string protectedLevel)
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Users/CreateUser.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$username", username);
            jsonBody = jsonBody.Replace("$password", password);
            jsonBody = jsonBody.Replace("$real_name", real_name);
            jsonBody = jsonBody.Replace("$email", email);
            jsonBody = jsonBody.Replace("$nameLevel", nameLevel);
            jsonBody = jsonBody.Replace("$enabled", enabled);
            jsonBody = jsonBody.Replace("$projectName", protectedLevel);

        }
    }
}
