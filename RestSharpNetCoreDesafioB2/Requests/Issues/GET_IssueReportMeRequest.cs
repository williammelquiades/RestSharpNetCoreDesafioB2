using RestSharpNetCoreDesafioB2.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpNetCoreDesafioB2.Requests.Issues
{
    public class GET_IssueReportMeRequest : RequestBase
    {
        public GET_IssueReportMeRequest(string filter_id)
        {
            requestService = "/api/rest/issues";

            method = Method.GET;

            parameters.Add("filter_id", filter_id);
        }
    }
}
