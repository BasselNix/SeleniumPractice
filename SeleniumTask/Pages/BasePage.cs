using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTask.Pages
{
    public abstract class BasePage
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void OpenSidebarMenu()
        {
            var menuBtn = Wait.Until(d =>
            {
                var element = d.FindElement(By.Id("react-burger-menu-btn"));
                return (element.Displayed && element.Enabled) ? element : null;
            });
            menuBtn.Click();
        }

        public void ResetAppState()
        {
            OpenSidebarMenu();
            var resetLink = Wait.Until(d =>
            {
                var element = d.FindElement(By.Id("reset_sidebar_link"));
                return (element.Displayed && element.Enabled) ? element : null;
            });

            resetLink.Click();
        }
    }
}