using RestSharpNetCoreDesafioB2.Helpers;
using RestSharpNetCoreDesafioB2.Queries.Projects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpNetCoreDesafioB2.DBSteps.Projects
{
    class ProjectsBDSteps
    {
        public static int ReturnMaxProjectsBD()
        {
            string executQuerie = ProjectsQueries.CountProjectBD;
            ExtentReportHelpers.AddTestInfo(2, "Total search of project in table");
            return Convert.ToInt32(DataBaseHelpers.RetornaDadosQuery(executQuerie)[0]);
        }

        public static List<string> ReturnProjectAndID()
        {
            string executQuerie = ProjectsQueries.SearchIdAndProject;
            return DataBaseHelpers.RetornaDadosQuery(executQuerie);
        }

        public static List<string> ReturnProjectByName()
        {
            string executQuerie = ProjectsQueries.SearchProjectByName;
            return DataBaseHelpers.RetornaDadosQuery(executQuerie);
        }

        public static List<String> ReturnProjectByNameRandom()
        {
            string executQuerie = ProjectsQueries.SearchNameProjectsRandom;
            return DataBaseHelpers.RetornaDadosQuery(executQuerie);
        }


        public static List<string> ReturnAllProject()
        {
            string executQuerie = ProjectsQueries.SearchIdProjects;
            return DataBaseHelpers.RetornaDadosQuery(executQuerie);
        }

        public static List<string> ReturnProjectIDRandom()
        {
            string executQuerie = ProjectsQueries.SearchIdProjectsRandom;
            //return Convert.ToInt32(DataBaseHelpers.RetornaDadosQuery(executQuerie));
            return DataBaseHelpers.RetornaDadosQuery(executQuerie);
        }


        public static void DeleteHierarchyProject()
        {
            string executQuerie = ProjectsQueries.ClearHierarchyProject;
            DataBaseHelpers.ExecuteQuery(executQuerie);
        }
    }
}
