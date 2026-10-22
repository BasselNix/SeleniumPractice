using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTask.Pages
{
    public class CheckoutStepOnePage : BasePage
    {
        public CheckoutStepOnePage(IWebDriver driver) : base(driver) { }

        private By FirstNameInput => By.XPath("//input[@placeholder='First Name']");
        private By LastNameInput => By.XPath("//input[@placeholder='Last Name']");
        private By PostalCodeInput => By.XPath("//input[@placeholder='Zip/Postal Code']");
        private By ContinueButton => By.XPath("//input[@value='Continue']");

        public void FillFormAndContinue(string firstName, string lastName, string postalCode)
        {
            Wait.Until(d => d.FindElement(FirstNameInput)).SendKeys(firstName);
            Wait.Until(d => d.FindElement(LastNameInput)).SendKeys(lastName);
            Wait.Until(d => d.FindElement(PostalCodeInput)).SendKeys(postalCode);
            Wait.Until(d => d.FindElement(ContinueButton)).Click();
        }
    }
}