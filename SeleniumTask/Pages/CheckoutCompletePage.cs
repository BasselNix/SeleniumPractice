using OpenQA.Selenium;

namespace SeleniumTask.Pages
{
    public class CheckoutCompletePage : BasePage
    {
        public CheckoutCompletePage(IWebDriver driver) : base(driver) { }

        private By PonyExpressImage => By.CssSelector("img[alt='Pony Express']");

        public bool IsPonyExpressImageDisplayed()
        {
            return Wait.Until(d =>
            {
                var elements = d.FindElements(PonyExpressImage);
                return elements.Count > 0 && elements[0].Displayed;
            });
        }
    }
}