using System;
using System.Collections.Generic;
using System.Text;
using RestSharpNetCoreDesafioB2.Queries.Issues;
using RestSharpNetCoreDesafioB2.Helpers;

namespace RestSharpNetCoreDesafioB2.DBSteps.Issues
{
    public class IssuesBDSteps
    {

        public static List<string> ReturnIssuesRandom()
        {
            string executQuerie = IssuesQueries.SearchIssuesRandom;
            return DataBaseHelpers.RetornaDadosQuery(executQuerie);
        }

    }
}
