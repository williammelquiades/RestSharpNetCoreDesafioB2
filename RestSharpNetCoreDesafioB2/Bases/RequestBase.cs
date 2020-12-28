using RestSharp;
using RestSharpNetCoreDesafioB2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpNetCoreDesafioB2.Bases
{
    public class RequestBase
    {
        #region Parameters
        protected string jsonBody = null;

        protected string url = JsonBuilder.ReturnParameterAppSettings("URL");

        protected string requestService = null;

        protected Method method;

        protected bool httpBasicAuthenticator = false;

        protected bool ntlmAuthenticator = false;

        protected IDictionary<string, string> headers = new Dictionary<string, string>()
        {
            //Dicionário de headeres deve ser iniciado com os headers comuns a todos os métodos da API
            {"Content-Type", "application/json"},
            //{"Content-Type", "multipart/form-data"},
            {"Authorization", JsonBuilder.ReturnParameterAppSettings("TOKEN")}
        };

        protected IDictionary<string, string> cookies = new Dictionary<string, string>()
        {
            //Dicionário de cookies deve ser iniciado com os headers comuns à todas os métodos da API
        };

        
        protected IDictionary<string, string> parameters = new Dictionary<string, string>();
        
        protected IDictionary<string, string> submitFiles = new Dictionary<string, string>();

        protected bool parameterTypeIsUrlSegment = true;
        #endregion

        #region Actions
        public IRestResponse<dynamic> ExecuteRequest()
        {
            IRestResponse<dynamic> response = RestSharpHelpers.ExecuteRequest(url, requestService, method, headers, cookies, parameters, parameterTypeIsUrlSegment, jsonBody, httpBasicAuthenticator, ntlmAuthenticator);

            ExtentReportHelpers.AddTestInfo(url, requestService, method.ToString(), headers, cookies, parameters, jsonBody, httpBasicAuthenticator, ntlmAuthenticator, response);

            return response;
        }

        public void RemoveHeader(string header)
        {
            headers.Remove(header);
        }

        public void RemoveCookie(string cookie)
        {
            cookies.Remove(cookie);
        }

        public void RemoveParameter(string parameter)
        {
            parameters.Remove(parameter);
        }

        public void SetMethod(Method method)
        {
            this.method = method;
        }
        #endregion
    }
}
