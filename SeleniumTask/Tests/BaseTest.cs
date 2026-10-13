using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTask.Tests
{
    public abstract class BaseTest
    {
        protected IWebDriver Driver;
        protected const string BaseUrl = "https://www.saucedemo.com/";

        [SetUp]
        public virtual void SetUp()
        {
            Console.WriteLine("[LOG] Loading Web Driver instance...");
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
            Console.WriteLine($"[LOG] Web Driver loaded. Navigated to {BaseUrl}");
        }

        [TearDown]
        public virtual void TearDown()
        {
            if (Driver != null)
            {
                Console.WriteLine("[LOG] Quitting Web Driver instance...");
                Driver.Quit();
                Driver.Dispose();
            }
        }
    }
}