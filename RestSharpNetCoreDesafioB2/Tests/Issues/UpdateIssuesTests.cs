using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RestSharp;
using RestSharpNetCoreDesafioB2.Bases;
using RestSharpNetCoreDesafioB2.DBSteps.Issues;
using RestSharpNetCoreDesafioB2.Helpers;
using RestSharpNetCoreDesafioB2.Requests.Issues;

namespace RestSharpNetCoreDesafioB2.Tests.Issues
{


    
    [TestFixture]
    public class UpdateIssuesTests : TestBase
    {
        private PUT_UpdateOneIssueRequest updateIsse;

        IRestResponse<dynamic> response;

        [Test]
        [Parallelizable]
        public void UpdateAnIssue() {


            #region Parameters
            List<string> issue_id = IssuesBDSteps.ReturnIdIssuesRandom();

            string idProblema = issue_id[0];
            
            string summary = "Test API Rest Update " + GeneralHelpers.ReturnStringWithRandomNumbers(2);
            string categoryName = "Rest Automation API Updaten" + GeneralHelpers.ReturnStringWithRandomNumbers(2);

            //Expected Result
            string statusCodeExpected = "OK";

            #endregion

            #region Request
            updateIsse = new PUT_UpdateOneIssueRequest(idProblema);

            updateIsse.SetJsonBoby(summary, categoryName);

            response = updateIsse.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
            });
            #endregion
        }

        [Test]
        [Parallelizable]
        public void UpdateAnIssueInvalid()
        {
            #region Parameters
            string idProblema = "01988a";
            string summary = "This is a test rest issue" + GeneralHelpers.ReturnStringWithRandomNumbers(2);
            string categoryName = "General 12" + GeneralHelpers.ReturnStringWithRandomNumbers(2);

            //Expected Result
            string statusCodeExpected = "NotFound";
            string message = "Issue #1988 not found";
            string code = "1100";
            string localized = "Issue 1988 not found.";

            #endregion

            #region Request
            updateIsse = new PUT_UpdateOneIssueRequest(idProblema);

            updateIsse.SetJsonBoby(summary, categoryName);

            response = updateIsse.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
                Assert.AreEqual(message, response.Data["message"].ToString());
                Assert.AreEqual(code, response.Data["code"].ToString());
                Assert.AreEqual(localized, response.Data["localized"].ToString());
            });
            #endregion
        }

        [Test]
        [Parallelizable]
        public void UpdateAnIssueNoRightParameter()
        {

            #region Parameters
            List<string> issue_id = IssuesBDSteps.ReturnIdIssuesRandom();

            string idProblema = issue_id[0];

            string summary = "";
            string categoryName = "Gene" ;

            //Expected Result
            string statusCodeExpected = "OK";
            string statusResponse = "Error";

            #endregion

            #region Request
            updateIsse = new PUT_UpdateOneIssueRequest(idProblema);

            updateIsse.SetJsonBoby(summary, categoryName);

            response = updateIsse.ExecuteRequest();
            #endregion

            #region Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusCodeExpected, response.StatusCode.ToString());
                Assert.AreEqual(statusResponse, response.ResponseStatus.ToString());

            });
            #endregion
        }

    }
}
