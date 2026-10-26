using OpenQA.Selenium;

namespace SeleniumTask.Pages
{
    public class CheckoutStepOnePage(IWebDriver driver) : BasePage(driver)
    {
        private static By FirstNameInput => By.XPath("//input[@placeholder='First Name']");
        private static By LastNameInput => By.XPath("//input[@placeholder='Last Name']");
        private static By PostalCodeInput => By.XPath("//input[@placeholder='Zip/Postal Code']");
        private static By ContinueButton => By.XPath("//input[@value='Continue']");

        public void FillFormAndContinue(string firstName, string lastName, string postalCode)
        {
            Wait.Until(d => d.FindElement(FirstNameInput)).SendKeys(firstName);
            Wait.Until(d => d.FindElement(LastNameInput)).SendKeys(lastName);
            Wait.Until(d => d.FindElement(PostalCodeInput)).SendKeys(postalCode);
            Wait.Until(d => d.FindElement(ContinueButton)).Click();
        }
    }
}