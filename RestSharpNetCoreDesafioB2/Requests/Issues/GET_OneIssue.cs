using RestSharpNetCoreDesafioB2.Bases;
using System;
using RestSharp;
using System.Collections.Generic;
using System.Text;

namespace RestSharpNetCoreDesafioB2.Requests.Issues
{
    public class GET_OneIssue : RequestBase
    {
        public GET_OneIssue(string issue_id)
        {
            requestService = "/api/rest/issues/{issue_id}";

            method = Method.GET;

            parameters.Add("issue_id", issue_id);
        }
    }
}
