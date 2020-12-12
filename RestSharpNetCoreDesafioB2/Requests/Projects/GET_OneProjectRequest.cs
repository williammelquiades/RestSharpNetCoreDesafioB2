using RestSharpNetCoreDesafioB2.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpNetCoreDesafioB2.Requests.Projetos
{
    public class GET_OneProjectRequest : RequestBase
    {
        public GET_OneProjectRequest(string project_id)
        {
            requestService = "/api/rest/projects/{project_id}";

            method = Method.GET;

            parameters.Add("project_id", project_id);

        }
    }
}
