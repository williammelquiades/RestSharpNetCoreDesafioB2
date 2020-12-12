using RestSharpNetCoreDesafioB2.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpNetCoreDesafioB2.Requests.Projetos
{
    public class POST_CreateSubProjectResquest : RequestBase
    {
        public POST_CreateSubProjectResquest(string project_id)
        {
            requestService = "/api/rest/projects/{project_id}/subprojects";

            method = Method.POST;

        }
    }
}
