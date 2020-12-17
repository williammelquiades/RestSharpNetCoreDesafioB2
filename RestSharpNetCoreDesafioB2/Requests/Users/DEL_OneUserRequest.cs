using System.IO;
using System.Text;
using RestSharp;
using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.Helpers;

namespace RestSharpNetCoreDesafioB2.Requests.Users
{
    public class DEL_OneUserRequest : RequestBase
    {
        public DEL_OneUserRequest(string user_id)
        {
            requestService = "/api/rest/users/{user_id}";

            method = Method.DELETE;

            parameters.Add("user_id", user_id);
        }
    }
}
