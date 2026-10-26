using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTask.Pages
{
    public abstract class BasePage(IWebDriver driver)
    {
        protected IWebDriver Driver = driver;
        protected WebDriverWait Wait = new(driver, TimeSpan.FromSeconds(10));
    }
}