using RestSharpNetCoreDesafioB2.Helpers;
using RestSharpNetCoreDesafioB2.Queries.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpNetCoreDesafioB2.DBSteps.General
{
    public class GeneralBDSteps
    {

        public static void CreateMassTests()
        {
            string executQueries = GeneralQueries.GlobalMass;
            DataBaseHelpers.ExecuteQuery(executQueries);
        }

        public static void DeleteMassDB()
        {
            string executQueries = GeneralQueries.ClearBD;
            DataBaseHelpers.ExecuteQuery(executQueries);
        }
    }
}
