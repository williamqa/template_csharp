using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Oracle.ManagedDataAccess.Client;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace fapc_core
{
    public class TestBase
    {
        #region Globals
        public static Jira Jira = new Jira();
        public enum DataBaseType { ORACLE, SQL }
        public enum DriverType { DESKTOP, MOBILE, SERVICE, WEB }
        public enum FrontEnd { ANDROID, IOS, CHROME, FIREFOX, IE, OPERA, SAFARI, LINUX, MAC, WINDOWS, REST, SOAP }
        public const int DefaultTimeout = 30;
        public static RemoteWebDriver Driver;
        public static string Logger = "";
        public static DriverOptions Options;
        public static AndroidDriver<AndroidElement> AndroidDriver;
        public static IOSDriver<IOSElement> IosDriver;
        private static bool IsDesktopApplication = false;
        private static Screenshot Screenshot;
        private static string EvidenceFileName;
        public static string TestInfo;
        private static string TestResultsDirectory;
        private static string Report;
        public static string Description = "";
        public static string Title = "";
        public static Ambiente Ambiente;
        public static FrontEnd _FrontEnd;
        public static DriverType _DriverType;
        public static string ErrorMessage = "";
        public static object GlobalParam1;
        #endregion

        #region Attr
        [TestInitialize]
        public void MyTestInitialize()
        {
            TestResultsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestResults", DateTime.Now.ToString("dd-MM-yyyy hh_mm_ss"));

            Report = Path.Combine(TestResultsDirectory, TestContext.TestName + '_' + DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".html");

            if (!Directory.Exists(TestResultsDirectory))
            {
                Directory.CreateDirectory(TestResultsDirectory);
            }

            using (StreamWriter sw = new StreamWriter(Report, true, Encoding.UTF8))
            {
                sw.WriteLine($"<h2 style='color:@Outcome;'>{TestContext.TestName}</h2>");
                sw.WriteLine($"<h2 style='color:@Outcome;'>@ErrorMessage</h2><hr/>");
            }

            Process.Start("cmd", "/C taskkill /im notepad.exe /f /t");
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            try
            {
                Driver.Quit();
            }
            catch { }

            using (StreamWriter sw = new StreamWriter(Report, true, Encoding.UTF8))
            {
                sw.WriteLine("<style>img {border: 1px solid #ddd;border-radius: 4px;padding: 5px;width: 150px;} img:hover {box-shadow: 0 0 2px 1px rgba(0, 140, 186, 0.5);}</style><script>function OpenImage(src){ var newTab = window.open(); newTab.document.body.innerHTML = " + '"' + "<img src=" + '"' + " + src + " + '"' + ">" + '"' + ";}</script>");
            }

            if (TestContext.CurrentTestOutcome.Equals(UnitTestOutcome.Passed))
            {
                File.WriteAllText(Report, File.ReadAllText(Report).Replace("@Outcome", "green"));
                File.WriteAllText(Report, File.ReadAllText(Report).Replace("@ErrorMessage", TestContext.CurrentTestOutcome.ToString()));
            }
            else
            {
                File.WriteAllText(Report, File.ReadAllText(Report).Replace("@Outcome", "red"));
                File.WriteAllText(Report, File.ReadAllText(Report).Replace("@ErrorMessage", ErrorMessage));
            }

            Console.WriteLine(Report);

            if (!string.IsNullOrEmpty(Jira.Api))
            {
                if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
                {
                    JiraUpload(Jira, Report);
                    JiraComment(Jira, TestContext.TestName + " falhou");
                }
                else
                {
                    JiraUpload(Jira, Report);
                }
            }
        }

        #endregion

        #region Properties
        private TestContext testContext;
        public TestContext TestContext
        {
            get
            {
                return testContext;
            }
            set
            {
                testContext = value;
            }
        }
        #endregion

        #region Methods

        public void JiraComment(string issue, string comment, string almConfigFile)
        {
            var keys = JObject.Parse(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Deploy", almConfigFile))).Values();
            var comments = JObject.Parse(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Deploy", "Comment.json"))).Values();
            var baseUrl = keys.First(k => k.Path.Equals("baseUrl")).ToString();
            var email = keys.First(k => k.Path.Equals("email")).ToString();
            var token = keys.First(k => k.Path.Equals("token")).ToString();
            IRestClient client = new RestClient(baseUrl);
            client.Authenticator = new HttpBasicAuthenticator(email, token);

            #region Comment         

            var request = new RestRequest(string.Format("issue/{0}/comment", issue), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody("{" + '"' + "body" + '"' + ":" + '"' + comment + '"' + "}");

            var response = client.Execute(request);

            Console.WriteLine(response.StatusCode == HttpStatusCode.OK ? $"Comentário: {Path.GetFileName(comment)} enviado com sucesso" : response.Content);

            #endregion
        }

        public void JiraComment(Jira jira, string comment)
        {
            var baseUrl = jira.Api;
            var email = jira.Email;
            var token = jira.Token;
            var issue = jira.Issue;

            IRestClient client = new RestClient(baseUrl);
            client.Authenticator = new HttpBasicAuthenticator(email, token);

            #region Comment         

            var request = new RestRequest(string.Format("issue/{0}/comment", issue), Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody("{" + '"' + "body" + '"' + ":" + '"' + comment + '"' + "}");

            var response = client.Execute(request);

            Console.WriteLine(response.StatusCode == HttpStatusCode.OK ? $"Comentário: {Path.GetFileName(comment)} enviado com sucesso" : response.Content);

            #endregion
        }

        public void JiraUpload(string issue, string evidence, string almConfigFile)
        {
            var keys = JObject.Parse(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Deploy", almConfigFile))).Values();
            var baseUrl = keys.First(k => k.Path.Equals("baseUrl")).ToString();
            var email = keys.First(k => k.Path.Equals("email")).ToString();
            var token = keys.First(k => k.Path.Equals("token")).ToString();
            IRestClient client = new RestClient(baseUrl);
            client.Authenticator = new HttpBasicAuthenticator(email, token);

            #region Attachment         

            var file = File.ReadAllBytes(evidence);
            var request = new RestRequest(string.Format("issue/{0}/attachments", issue), Method.POST);

            request.AddHeader("X-Atlassian-Token", "nocheck");
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddFileBytes("file", file, Path.GetFileName(evidence), "application/octet-stream");

            var response = client.Execute(request);

            Console.WriteLine(response.StatusCode == HttpStatusCode.OK ? $"{Path.GetFileName(evidence)} anexado com sucesso" : response.Content);

            #endregion
        }

        public void JiraUpload(Jira jira, string evidence)
        {
            IRestClient client = new RestClient(jira.Api);
            client.Authenticator = new HttpBasicAuthenticator(jira.Email, jira.Token);

            #region Attachment         

            var file = File.ReadAllBytes(evidence);
            var request = new RestRequest(string.Format("issue/{0}/attachments", jira.Issue), Method.POST);

            request.AddHeader("X-Atlassian-Token", "nocheck");
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddFileBytes("file", file, Path.GetFileName(evidence), "application/octet-stream");

            var response = client.Execute(request);

            Console.WriteLine(response.StatusCode == HttpStatusCode.OK ? $"{Path.GetFileName(evidence)} anexado com sucesso" : response.Content);

            #endregion
        }

        public static List<List<KeyValuePair<string, string>>> ExecuteQuery(string dataBaseConfig, string script, Dictionary<string, string> param = null, DataBaseType dataBaseType = DataBaseType.SQL)
        {
            List<KeyValuePair<string, string>> lstResultado;
            List<List<KeyValuePair<string, string>>> lstResultados = new List<List<KeyValuePair<string, string>>>();
            DataBaseConfig config;
            string query;
            string connectionString = "";

            if (!dataBaseConfig.Contains(".json"))
            {
                dataBaseConfig = dataBaseConfig + ".json";
            }

            if (!File.Exists(dataBaseConfig))
            {
                dataBaseConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Deploy", dataBaseConfig);

                if (!File.Exists(dataBaseConfig))
                {
                    Fail($"Não foi possível encontrar o arquivo: {dataBaseConfig}");
                }
            }

            config = JsonConvert.DeserializeObject<DataBaseConfig>(File.ReadAllText(dataBaseConfig));

            if (!script.Contains(".sql"))
            {
                script = script + ".sql";
            }

            if (!File.Exists(script))
            {
                script = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", script);

                if (!File.Exists(script))
                {
                    Fail($"Não possível encontrar o script: {script}");
                }
            }

            query = File.ReadAllText(script);

            if (param != null)
            {
                foreach (string k in param.Keys)
                {
                    string keyName = "@" + k;
                    query = query.Replace(keyName, param[k]);
                }
            }

            switch (dataBaseType)
            {
                case DataBaseType.ORACLE:
                    if (string.IsNullOrEmpty(config.ConnectionString))
                    {
                        if (string.IsNullOrEmpty(config.DataSource) || string.IsNullOrEmpty(config.UserId) || string.IsNullOrEmpty(config.Password))
                        {
                            Fail($"Os campos DataSource, UserId e Password não podem ser nulos. DataSource: {config.DataSource} | UserId: {config.UserId} | Password: {config.Password}");
                        }

                        connectionString = $"Data Source={config.DataSource}; User Id={config.UserId}; Password={config.Password};";
                    }
                    else
                    {
                        connectionString = config.ConnectionString;
                    }

                    using (OracleConnection oracleConnection = new OracleConnection(connectionString))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            try
                            {
                                OracleCommand oracleCommand = new OracleCommand(query);
                                oracleCommand.Connection = oracleConnection;
                                oracleConnection.Open();
                                OracleDataReader oracleDataReader = oracleCommand.ExecuteReader();

                                while (oracleDataReader.Read())
                                {
                                    lstResultado = new List<KeyValuePair<string, string>>();

                                    for (int j = 0; j < oracleDataReader.FieldCount; j++)
                                    {
                                        lstResultado.Add(new KeyValuePair<string, string>(oracleDataReader.GetName(j), oracleDataReader[j].ToString()));
                                    }

                                    lstResultados.Add(lstResultado);
                                }

                                oracleDataReader.Close();
                                oracleCommand.Dispose();
                                oracleConnection.Close();
                            }
                            catch
                            {
                                Thread.Sleep(3000);
                            }

                            if (lstResultados.Count() > 0)
                            {
                                break;
                            }
                        }
                    }

                    break;
                case DataBaseType.SQL:
                    if (string.IsNullOrEmpty(config.ConnectionString))
                    {
                        if (string.IsNullOrEmpty(config.DataSource) || string.IsNullOrEmpty(config.UserId) || string.IsNullOrEmpty(config.Password) || (string.IsNullOrEmpty(config.DataBase) && string.IsNullOrEmpty(config.InitialCatalog)))
                        {
                            Fail($"Os campos DataSource, UserId, Password, Initial Catalog e DataBase não podem ser nulos. DataSource: {config.DataSource} | UserId: {config.UserId} | Password: {config.Password} | DataBase: {config.DataBase} | Initial Catalog: {config.InitialCatalog}");
                        }

                        if (!string.IsNullOrEmpty(config.DataBase))
                        {
                            connectionString = $"Data Source={config.DataSource}; Database={config.DataBase}; User Id={config.UserId}; Password={config.Password};";
                        }
                        else
                        {
                            connectionString = $"Data Source={config.DataSource}; Inital Catalog=PrimeManage; User Id={config.UserId}; Password={config.Password};";
                        }
                    }
                    else
                    {
                        connectionString = config.ConnectionString;
                    }

                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            try
                            {
                                lstResultados = new List<List<KeyValuePair<string, string>>>();
                                sqlConnection.Open();
                                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                                while (sqlDataReader.Read())
                                {
                                    lstResultado = new List<KeyValuePair<string, string>>();

                                    for (int j = 0; j < sqlDataReader.FieldCount; j++)
                                    {
                                        lstResultado.Add(new KeyValuePair<string, string>(sqlDataReader.GetName(j), sqlDataReader[j].ToString()));
                                    }

                                    lstResultados.Add(lstResultado);
                                }

                                sqlDataReader.Close();
                                sqlCommand.Dispose();
                                sqlConnection.Close();
                            }
                            catch
                            {
                                Thread.Sleep(3000);
                            }

                            if (lstResultados.Count() > 0)
                            {
                                break;
                            }
                        }
                    }

                    break;
                default:
                    Fail($"Não foi possível definir o DataBaseType: {dataBaseType}");
                    break;
            }

            return lstResultados;
        }

        public string GetErrorMessage()
        {
            const BindingFlags privateGetterFlags = BindingFlags.GetField |
                                                    BindingFlags.GetProperty |
                                                    BindingFlags.NonPublic |
                                                    BindingFlags.Instance |
                                                    BindingFlags.FlattenHierarchy;

            var mMessage = string.Empty; // Returns empty if TestOutcome is not failed
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                // Get hold of TestContext.m_currentResult.m_errorInfo.m_stackTrace (contains the stack trace details from log)
                var field = TestContext.GetType().GetField("m_currentResult", privateGetterFlags);
                if (field != null)
                {
                    var mCurrentResult = field.GetValue(TestContext);
                    field = mCurrentResult.GetType().GetField("m_errorInfo", privateGetterFlags);
                    if (field != null)
                    {
                        var mErrorInfo = field.GetValue(mCurrentResult);
                        field = mErrorInfo.GetType().GetField("m_stackTrace", privateGetterFlags);
                        if (field != null) mMessage = field.GetValue(mErrorInfo) as string;
                    }
                }
            }

            return mMessage;
        }

        public static void TaskKill()
        {
            switch (_FrontEnd)
            {
                case FrontEnd.ANDROID:
                    break;
                case FrontEnd.CHROME:
                    Process.Start("cmd", "/C taskkill /im chrome.exe /f /t");
                    Process.Start("cmd", "/C taskkill /im chromedriver.exe /f /t");
                    Thread.Sleep(3000);
                    break;
                case FrontEnd.FIREFOX:
                    break;
                case FrontEnd.IE:
                    break;
                case FrontEnd.IOS:
                    break;
                case FrontEnd.LINUX:
                    break;
                case FrontEnd.MAC:
                    break;
                case FrontEnd.OPERA:
                    break;
                case FrontEnd.REST:
                    break;
                case FrontEnd.SAFARI:
                    break;
                case FrontEnd.SOAP:
                    break;
                case FrontEnd.WINDOWS:
                    break;
            }
        }

        public static void SetAmbiente(string configFile)
        {
            if (!File.Exists(configFile))
            {
                configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Deploy", configFile);

                if (!File.Exists(configFile))
                {
                    Assert.Inconclusive($"Não foi possível encontrar o arquivo: {configFile}");
                }
            }

            string json = File.ReadAllText(configFile);
            Ambiente = new Ambiente();
            Ambiente = JsonConvert.DeserializeObject<Ambiente>(json);
        }

        public static void ReadJson(string configFile)
        {
            if (!File.Exists(configFile))
            {
                configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Deploy", configFile);

                if (!File.Exists(configFile))
                {
                    Assert.Inconclusive($"Não foi possível encontrar o arquivo: {configFile}");
                }
            }

            var jObj = JObject.Parse(File.ReadAllText(configFile));

            foreach (var item in jObj.Properties())
            {
                if (!Options.GetType().GetProperties().Any(p => p.Name.ToUpperInvariant().Equals(item.Name.ToUpperInvariant())))
                {
                    Options.AddAdditionalCapability(item.Name, ((JValue)item.Value).Value);
                }
                else
                {
                    Options.GetType().GetProperties().First(p => p.Name.ToUpperInvariant().Equals(item.Name.ToUpperInvariant())).SetMethod.Invoke(Options, new object[] { ((JValue)item.Value).Value });
                }
            }
        }

        public static void ConfigFile(string configFile)
        {
            if (!File.Exists(configFile))
            {
                configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Deploy", configFile);

                if (!File.Exists(configFile))
                {
                    Assert.Inconclusive($"Não foi possível encontrar o arquivo: {configFile}");
                }
            }

            if (configFile.Contains(".json"))
            {
                ReadJson(configFile);

                return;
            }
        }

        public static void SetDriverType(DriverType driverType, FrontEnd frontEnd, string configFile = "")
        {
            _DriverType = driverType;
            _FrontEnd = frontEnd;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Deploy");

            switch (driverType)
            {
                case DriverType.DESKTOP:
                    Options = new AppiumOptions();
                    switch (frontEnd)
                    {
                        case FrontEnd.LINUX:
                            break;
                        case FrontEnd.MAC:
                            break;
                        case FrontEnd.WINDOWS:
                            if (string.IsNullOrEmpty(configFile))
                            {
                                ConfigFile(Path.Combine(path, "DesktopConfig.json"));
                            }
                            else
                            {
                                ConfigFile(configFile);
                            }

                            Driver = new WindowsDriver<WindowsElement>((AppiumOptions)Options);
                            break;
                        default:
                            Assert.Inconclusive($"Não foi possível definir o FrontEnd: {frontEnd} para o DriverType. {driverType}");
                            break;
                    }
                    break;
                case DriverType.MOBILE:
                    switch (frontEnd)
                    {
                        case FrontEnd.ANDROID:
                            Options = new AppiumOptions();
                            if (string.IsNullOrEmpty(configFile))
                            {
                                ConfigFile("AndroidConfig.json");
                            }
                            else
                            {
                                ConfigFile(configFile);
                            }

                            Driver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), Options);

                            AndroidDriver = (AndroidDriver<AndroidElement>)Driver;
                            break;
                        case FrontEnd.IOS:
                            Options = new AppiumOptions();
                            if (string.IsNullOrEmpty(configFile))
                            {
                                ConfigFile("IosConfig.json");
                            }
                            else
                            {
                                ConfigFile(configFile);
                            }

                            Driver = new IOSDriver<IOSElement>(new Uri("http://127.0.0.1:4723/wd/hub"), Options);

                            IosDriver = (IOSDriver<IOSElement>)Driver;
                            break;
                        default:
                            Assert.Inconclusive($"Não foi possível configurar o FrontEnd: {frontEnd} para o DriverType: {driverType}");
                            break;
                    }
                    break;
                case DriverType.SERVICE:
                    switch (frontEnd)
                    {
                        case FrontEnd.REST:
                            break;
                        case FrontEnd.SOAP:
                            break;
                        default:
                            Assert.Inconclusive($"Não foi possível configurar o FrontEnd: {frontEnd}");
                            break;
                    }
                    break;
                case DriverType.WEB:
                    switch (frontEnd)
                    {
                        case FrontEnd.CHROME:
                            string driverPath = "";
                            Options = new ChromeOptions();
                            Options.AcceptInsecureCertificates = true;
                            if (string.IsNullOrEmpty(configFile))
                            {
                                SetAmbiente("WebConfig.json");
                            }
                            else
                            {
                                SetAmbiente(configFile);
                            }

                            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                            {
                                driverPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Deploy", "Linux");
                                Environment.SetEnvironmentVariable("webdriver.chrome.driver", driverPath + "/chromedriver");
                            }
                            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                            {
                                driverPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Deploy", "Windows");
                                Environment.SetEnvironmentVariable("webdriver.chrome.driver", driverPath + "\\chromedriver.exe");
                            }
                            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                            {
                                driverPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Deploy", "Mac");
                                Environment.SetEnvironmentVariable("webdriver.chrome.driver", driverPath + "/chromedriver");
                            }

                            Driver = new ChromeDriver(driverPath, (ChromeOptions)Options);
                            Driver.Manage().Window.Maximize();

                            if (!string.IsNullOrEmpty(Ambiente.Url))
                            {
                                Driver.Url = Ambiente.Url;
                            }

                            break;
                        case FrontEnd.FIREFOX:
                            break;
                        case FrontEnd.IE:
                            break;
                        case FrontEnd.OPERA:
                            break;
                        case FrontEnd.SAFARI:
                            break;
                        default:
                            Assert.Inconclusive($"Não foi possível configurar o FrontEnd: {frontEnd}");
                            break;
                    }
                    break;
                default:
                    Assert.Inconclusive($"Não foi possível configurar o DriverType: {driverType}");
                    break;
            }
        }

        public static void Checkpoint(bool condition, string message, bool takeScreenShot = true, bool stopOnFail = true)
        {
            if (takeScreenShot)
            {
                if (IsDesktopApplication)
                {
                    //var size = AutoItX.WinGetPos(WinHandle);

                    //Bitmap screenshot = new Bitmap(size.Width, size.Height);

                    //Graphics graphics = Graphics.FromImage(screenshot);

                    //graphics.CopyFromScreen(size.X, size.Y, 0, 0, screenshot.Size);

                    //EvidenceFileName = Path.Combine(TestResultsDirectory, "evidence" + DateTime.Now.ToString("ddMMyyyyThhmmss") + ".png");

                    //screenshot.Save(EvidenceFileName, ImageFormat.Png);
                }
                else
                {
                    Screenshot = ((ITakesScreenshot)Driver).GetScreenshot();

                    EvidenceFileName = Path.Combine(TestResultsDirectory,
                        "evidence" + DateTime.Now.ToString("ddMMyyyyThhmmss") + ".png");

                    Screenshot.SaveAsFile(EvidenceFileName, ScreenshotImageFormat.Png);
                }

                if (condition)
                {
                    TestInfo = "<h4>" + Logger + "</h4><h10 style='color:green;'>" + message + "</h10><br/><a target='_blank' onclick=OpenImage('data:image/png;base64," + ConvertImageToBase64(EvidenceFileName) + "')><img src='data:image/png;base64," + ConvertImageToBase64(EvidenceFileName) + "' alt='Forest'></a>";
                }
                else
                {
                    TestInfo = "<h4>" + Logger + "</h4><h10 style='color:red;'>" + message + "</h10><br/><a target='_blank' onclick=OpenImage('data:image/png;base64," + ConvertImageToBase64(EvidenceFileName) + "')><img src='data:image/png;base64," + ConvertImageToBase64(EvidenceFileName) + "' alt='Forest'></a>";
                }

                File.Delete(EvidenceFileName);
            }
            else
            {
                TestInfo = "<h4>" + Logger + "</h4><h10>" + message + "</h10>";
            }

            using (StreamWriter sw = new StreamWriter(Report, true, Encoding.UTF8))
            {
                sw.WriteLine(TestInfo);
            }

            Logger = "";

            if (!condition)
            {
                Fail(message);
            }
        }

        public static void Fail(string message)
        {
            ErrorMessage = message;
            Console.WriteLine(ErrorMessage);
            Assert.Fail(message);
        }

        public static string ConvertImageToBase64(string fileName)
        {
            byte[] imageArray = File.ReadAllBytes(fileName);
            return Convert.ToBase64String(imageArray);
        }

        #endregion
    }
}

//gabriel