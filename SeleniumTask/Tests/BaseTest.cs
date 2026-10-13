using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTask.Tests
{
    public abstract class BaseTest
    {
        protected IWebDriver Driver;
        protected ILogger Logger;
        private ILoggerFactory _loggerFactory;

        protected const string BaseUrl = "https://www.saucedemo.com/";

        [SetUp]
        public virtual void SetUp()
        {
            _loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddSimpleConsole(options =>
                {
                    options.IncludeScopes = true;
                    options.SingleLine = true;
                    options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
                });
            });
            Logger = _loggerFactory.CreateLogger(GetType().Name);

            Logger.LogInformation("Loading Web Driver instance...");

            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");

            // Disable password saving prompt
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);

            // Disable the "Change your password / compromised password" leak detection warning
            options.AddUserProfilePreference("profile.password_manager_leak_detection", false);

            Driver = new ChromeDriver(options);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Navigate().GoToUrl(BaseUrl);

            Logger.LogInformation("Web Driver loaded. Navigated to {BaseUrl}", BaseUrl);
        }

        [TearDown]
        public virtual void TearDown()
        {
            if (Driver != null)
            {
                Logger.LogInformation("Quitting Web Driver instance...");
                Driver.Quit();
                Driver.Dispose();
            }
            _loggerFactory?.Dispose();
        }
    }
}