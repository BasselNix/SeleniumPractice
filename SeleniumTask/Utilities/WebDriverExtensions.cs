using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTask.Utilities
{
    public static class WebDriverExtensions
    {
        public static bool IsElementDisplayed(this IWebDriver driver, By locator, int timeoutSeconds = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
                return wait.Until(d =>
                {
                    var elements = d.FindElements(locator);
                    return elements.Count > 0 && elements[0].Displayed;
                });
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}