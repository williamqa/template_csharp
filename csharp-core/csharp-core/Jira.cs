using System;

namespace csharp_core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Jira : Attribute
    {
        public string Api;
        public string Email;
        public string Token;
        public string Issue;

        public Jira()
        {

        }

        public Jira(string issue, string api, string email, string token)
        {
            Api = api;
            Email = email;
            Token = token;
            Issue = issue;

            TestBase.Jira.Api = api;
            TestBase.Jira.Email = email;
            TestBase.Jira.Token = token;
            TestBase.Jira.Issue = issue;
        }
    }
}