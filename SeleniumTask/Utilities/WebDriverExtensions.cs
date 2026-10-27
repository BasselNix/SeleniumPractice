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
                    try
                    {
                        return d.FindElement(locator).Displayed;
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public static bool IsElementHidden(this IWebDriver driver, By locator, int timeoutSeconds = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
                return wait.Until(d =>
                {
                    try
                    {
                        return !d.FindElement(locator).Displayed;
                    }
                    catch (NoSuchElementException)
                    {
                        return true;
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                return true;
            }
        }

        public static void ClickWhenReady(this IWebDriver driver, By locator, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            try
            {
                wait.Until(d =>
                {
                    var element = d.FindElement(locator);
                    return element.Displayed && element.Enabled ? element : null;
                }).Click();
            }
            catch (WebDriverTimeoutException ex)
            {
                throw new WebDriverTimeoutException($"Element located by '{locator}' is not visible, enabled, or clickable.", ex);
            }
        }
    }
}