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

        public static List<string> ReturnIdIssuesRandom()
        {
            string executQuerie = IssuesQueries.SearchIdIssuesRandom;
            return DataBaseHelpers.RetornaDadosQuery(executQuerie);
        }

        public static List<string> GetOneIssue()
        {
            string executQuerie = IssuesQueries.GetOneIssues;
            return DataBaseHelpers.RetornaDadosQuery(executQuerie);
        }

        public static List<string> GetNoteIssue()
        {
            string executQuerie = IssuesQueries.GetNoteIssues;
            return DataBaseHelpers.RetornaDadosQuery(executQuerie);
        }

    }
}
