using OpenQA.Selenium;
using SeleniumTask.Utilities;

namespace SeleniumTask.Pages
{
    public class CheckoutCompletePage(IWebDriver driver) : BasePage(driver)
    {
        private static By PonyExpressImage => By.CssSelector("img[alt='Pony Express']");

        public bool IsPonyExpressImageDisplayed() => Driver.IsElementDisplayed(PonyExpressImage);
    }
}