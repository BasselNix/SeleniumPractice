using OpenQA.Selenium;

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
            Driver.FindElement(FirstNameInput).SendKeys(firstName);
            Driver.FindElement(LastNameInput).SendKeys(lastName);
            Driver.FindElement(PostalCodeInput).SendKeys(postalCode);
            Driver.FindElement(ContinueButton).Click();
        }
    }
}