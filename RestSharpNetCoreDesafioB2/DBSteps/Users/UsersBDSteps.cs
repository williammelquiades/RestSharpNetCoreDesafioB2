using RestSharpNetCoreDesafioB2.Helpers;
using RestSharpNetCoreDesafioB2.Queries.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpNetCoreDesafioB2.DBSteps.Users
{
    public class UsersBDSteps
    {
        public static List<string> ReturnMyUser()
        {
            string executQuerie = UsersQueries.SearchMyUser;
            return DataBaseHelpers.RetornaDadosQuery(executQuerie);
        }

        public static List<string> ReturnUsersRandom()
        {
            string executQuerie = UsersQueries.SearchUserRandom;
            return DataBaseHelpers.RetornaDadosQuery(executQuerie);
        }
    }
}
