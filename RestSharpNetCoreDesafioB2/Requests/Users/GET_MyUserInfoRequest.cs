using System.IO;
using System.Text;
using RestSharp;
using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.Helpers;


namespace RestSharpNetCoreDesafioB2.Requests.Users
{
    public class GET_MyUserInfoRequest : RequestBase
    {
        public GET_MyUserInfoRequest()
        {
            requestService = "/api/rest/users/me";

            method = Method.GET;
        }
    }
}
