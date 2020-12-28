using NUnit.Framework;
using RestSharp;
using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.DBSteps.Filters;
using RestSharpNetCoreDesafioB2.Requests.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpNetCoreDesafioB2.Tests.Filters
{
    [TestFixture]
    public class FiltersTests : TestBase
    {
        private DEL_DeleteOneFilterRequest deleteFilter;
        private GET_OneFilterRequest oneFilter;

        IRestResponse<dynamic> response;

        #region GET 
        [Test]
        [Parallelizable]
        public void GetOneFilterSucess()
        {
            #region Parameters
            FiltersBDSteps.ClearAllFilters();
            FiltersBDSteps.InsertFilter();

            List<string> dataFilters = FiltersBDSteps.SearchOneFilter();
            string filter_id = dataFilters[0];
            string filter_name = dataFilters[1];

            //Expected Result
            string statusCodeExpected = "OK";
            string filterName = "REQ";
            #endregion

            oneFilter = new GET_OneFilterRequest(filter_id);
            
            response = oneFilter.ExecuteRequest();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
                Assert.AreEqual(filter_id, response.Data["filters"][0]["id"].ToString());
                Assert.AreEqual(filterName, response.Data["filters"][0]["name"].ToString());
            });
        }

        [Test]
        [Parallelizable]
        public void GetOneFilterNonExistent()
        {
            #region Parameters
            string filter_id = "9999";

            //Expected Result
            string statusCodeExpected = "OK";
            string content = "{\"filters\":[]}";
            #endregion

            #region Request
            oneFilter = new GET_OneFilterRequest(filter_id);

            response = oneFilter.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
                Assert.AreEqual(content, response.Content.ToString());
            });
            #endregion
        }
        #endregion

        #region DEL
        [Test]
        [Parallelizable]
        public void DeleteOneFilterSucess()
        {
            #region Parameters

            FiltersBDSteps.ClearAllFilters();
            FiltersBDSteps.InsertFilter();

            List<string> dataFilters = FiltersBDSteps.SearchOneFilter();
            string filter_id = dataFilters[0];

            //Expected Result
            int statusCode = 204;
            string statusResponse = "NoContent";
            #endregion

            #region Request
            deleteFilter = new DEL_DeleteOneFilterRequest(filter_id);

            response = deleteFilter.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCode, (int)response.StatusCode);
                Assert.AreEqual(statusResponse, response.StatusCode.ToString());
            });
            #endregion
        }
        #endregion
    }
}
