using RestSharpNetCoreDesafioB2.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using RestSharpNetCoreDesafioB2.Helpers;

namespace RestSharpNetCoreDesafioB2.Requests.Issues
{
    public class POST_CreateAnIssueRequest : RequestBase
    {

        public POST_CreateAnIssueRequest()
        {
            requestService = "/api/rest/issues";

            method = Method.POST;

        }

        public void SetJsonBody(string summary,
                                string description,
                                string categoryName,
                                string projectName
            )
       
        {
            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Issues/CreateIssueMinimal.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$summary", summary)
                               .Replace("$description", description)
                               .Replace("$nomePriority", categoryName)
                               .Replace("$projectName", projectName);


        }
    }
}
