using NUnit.Framework;
using RestSharp;
using RestSharpNetCoreDesafioB2.DBSteps.Issues;
using RestSharpNetCoreDesafioB2.DBSteps.Projects;
using RestSharpNetCoreDesafioB2.Requests.Issues;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpNetCoreDesafioB2.Tests.Issues
{
    [TestFixture]
    public class MonitorIssueTests
    {

        private POST_CreateAnIssueRequest sendOneIssue;
        private GET_OneIssueRequest oneIssue;
        private POST_CreateIssueWithAtttachmentsRequest issueAttachments;
        private GET_IssueForPageRequest issuePage;
        private GET_IssueReportMeRequest reportMe;
        private POST_MonitorsIssueRequest monitorIssue;

        IRestResponse<dynamic> response;

        [Test]
        [Parallelizable]
        public void MonitoredIssueSucess()
        {
            #region Parameters
            List<string> dataIssue = IssuesBDSteps.GetOneIssue();
            string issue_id = dataIssue[0];

            //List<string> dataProject = ProjectsBDSteps.ReturnProjectAndID();
            //string projectId = dataProject[0];

            //Expected Result
            string statusCodeExpected = "Created";
            #endregion

            #region Request
            monitorIssue = new POST_MonitorsIssueRequest(issue_id);

            response = monitorIssue.ExecuteRequest();
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
        public void MonitoredInvalidProblema()
        {
            #region Parameters
            string issue_id = "0";

            //Expected Result
            string statusCodeExpected = "BadRequest";
            string message = "'issue_id' must be >= 1";
            string code = "29";
            string localized = "Invalid value for 'issue_id'";
            #endregion

            #region Request
            monitorIssue = new POST_MonitorsIssueRequest(issue_id);

            response = monitorIssue.ExecuteRequest();
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
        public void MonitoredInvalidProblemaString()
        {
            #region Parameters
            string issue_id = "william";

            //Expected Result
            string statusCodeExpected = "BadRequest";
            string message = "'issue_id' must be numeric";
            string code = "29";
            string localized = "Invalid value for 'issue_id'";
            #endregion

            #region Request
            monitorIssue = new POST_MonitorsIssueRequest(issue_id);

            response = monitorIssue.ExecuteRequest();
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
        public void MonitoredInvalidProblemaID()
        {
            #region Parameters
            string issue_id = "19880515";

            //Expected Result
            string statusCodeExpected = "NotFound";
            string message = "Issue #19880515 not found";
            string code = "1100";
            string localized = "Issue 19880515 not found.";
            #endregion

            #region Request
            monitorIssue = new POST_MonitorsIssueRequest(issue_id);

            response = monitorIssue.ExecuteRequest();
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

    }
}
