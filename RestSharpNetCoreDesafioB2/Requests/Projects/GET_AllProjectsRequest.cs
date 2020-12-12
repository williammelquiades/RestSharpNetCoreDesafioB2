using RestSharpNetCoreDesafioB2.Bases;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpNetCoreDesafioB2.Requests.Projetos
{
    public class GET_AllProjectsRequest : RequestBase
    {

        public GET_AllProjectsRequest()
        {
            requestService = "/api/rest/projects/";

            method = Method.GET;
        }
    }
}
