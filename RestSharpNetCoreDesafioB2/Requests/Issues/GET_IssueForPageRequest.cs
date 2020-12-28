using RestSharpNetCoreDesafioB2.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpNetCoreDesafioB2.Requests.Issues
{
    public class GET_IssueForPageRequest : RequestBase
    {
        public GET_IssueForPageRequest(string page_size, string page)
        {
            requestService = "api/rest/issues";

            method = Method.GET;

            parameters.Add("page_size", page_size);
            parameters.Add("page", page);
        }
    }
}
