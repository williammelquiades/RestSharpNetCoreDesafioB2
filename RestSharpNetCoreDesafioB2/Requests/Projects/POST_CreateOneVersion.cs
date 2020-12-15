using RestSharpNetCoreDesafioB2.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharpNetCoreDesafioB2.Helpers;
using System.IO;
using System.Data;

namespace RestSharpNetCoreDesafioB2.Requests.Projects
{
    public class POST_CreateOneVersion : RequestBase
    {
        DateTime dataNow = DateTime.Now;

        public POST_CreateOneVersion(string project_id)
        {
            requestService = "/api/rest/projects/{project_id}/versions/";

            method = Method.POST;

            parameters.Add("project_id", project_id);
        }

        public void SetJsonBody(
            string version,
            string description) 
        {
            string nowDate = dataNow.ToString("yyyy-MM-dd");

            jsonBody = File.ReadAllText(GeneralHelpers.ReturnProjectPath() + "Jsons/Projects/CreateVersion.json", Encoding.UTF8);
            jsonBody = jsonBody.Replace("$version", version) 
            .Replace("$description", description)
            .Replace("$dateNow", nowDate);
        }
    }
}
