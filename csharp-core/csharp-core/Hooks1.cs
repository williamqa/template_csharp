using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace fapc_core
{
    [Binding]
    [DeploymentItem(@"Data")]
    [DeploymentItem(@"Deploy")]
    public sealed class Hooks1
    {
        private TestBase testBase = new TestBase();

        [BeforeScenario]
        [System.Obsolete]
        public void BeforeScenario()
        {
            testBase.TestContext = TestContextHook;
            testBase.MyTestInitialize();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            testBase.MyTestCleanup();
        }

        #region Properties

        private TestContext mTestContext;

        [System.Obsolete]
        public TestContext TestContextHook
        {
            get
            {
                if (mTestContext == null)
                {
                    mTestContext = ScenarioContext.Current.ScenarioContainer
                        .Resolve<TestContext>();
                }

                return mTestContext;
            }
            set { mTestContext = value; }
        }

        #endregion
    }
}