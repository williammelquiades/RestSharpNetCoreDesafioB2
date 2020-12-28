using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.Requests.Lang;
using System.Collections.Generic;
using NUnit.Framework;
using RestSharp;

namespace RestSharpNetCoreDesafioB2.Tests.Lang
{
    [TestFixture]
    public class LangTests : TestBase
    {
        private GET_OneLocalizedStringRequest localizedString;

        IRestResponse<dynamic> response;

        [Test]
        [Parallelizable]
        public void GetOneLocalizedSucess()
        {
            #region Parameters
            string allProjects = "all_projects";

            //Expected Result
            string statusCodeExpected = "OK";
            string language = "english";
            #endregion

            #region Request
            localizedString = new GET_OneLocalizedStringRequest(allProjects);

            response = localizedString.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
                Assert.AreEqual(language, response.Data["language"].ToString());
            });
            #endregion
        }
    }
}
