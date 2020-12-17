using RestSharp;
using System.Text;
using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.Helpers;


namespace RestSharpNetCoreDesafioB2.Requests.Users
{
    public class PUT_ResetUserRequest : RequestBase
    {
        public PUT_ResetUserRequest(string user_id)
        {
            requestService = "/api/rest/users/{user_id}/reset";

            method = Method.PUT;

            parameters.Add("user_id", user_id);
        }
    }
}
