using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTask.Pages
{
    public class CheckoutStepOnePage : BasePage
    {
        public CheckoutStepOnePage(IWebDriver driver) : base(driver) { }

        private By FirstNameInput => By.Id("first-name");
        private By LastNameInput => By.Id("last-name");
        private By PostalCodeInput => By.Id("postal-code");
        private By ContinueButton => By.Id("continue");

        public void FillFormAndContinue(string firstName, string lastName, string postalCode)
        {
            Wait.Until(d => d.FindElement(FirstNameInput)).SendKeys(firstName);
            Wait.Until(d => d.FindElement(LastNameInput)).SendKeys(lastName);
            Wait.Until(d => d.FindElement(PostalCodeInput)).SendKeys(postalCode);
            Wait.Until(d => d.FindElement(ContinueButton)).Click();
        }
    }
}