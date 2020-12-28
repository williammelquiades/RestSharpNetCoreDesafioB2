using RestSharpNetCoreDesafioB2.Helpers;
using RestSharpNetCoreDesafioB2.Queries.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpNetCoreDesafioB2.DBSteps.Filters
{
    public class FiltersBDSteps
    {
        public static List<string> InsertFilter()
        {
            string executQuerie = FiltersQueries.InsertOneFilter;
            return DataBaseHelpers.RetornaDadosQuery(executQuerie);
        }

        public static List<string> SearchOneFilter()
        {
            string executQuerie = FiltersQueries.SearchFilterRandom;
            return DataBaseHelpers.RetornaDadosQuery(executQuerie);
        }

        public static List<string> ClearAllFilters()
        {
            string executQuerie = FiltersQueries.ClearFilters;
            return DataBaseHelpers.RetornaDadosQuery(executQuerie);
        }
    }
}
