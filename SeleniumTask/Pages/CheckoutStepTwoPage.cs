using OpenQA.Selenium;

namespace SeleniumTask.Pages
{
    public class CheckoutStepTwoPage : BasePage
    {
        public CheckoutStepTwoPage(IWebDriver driver) : base(driver) { }

        private By ItemTotalLabel => By.ClassName("summary_subtotal_label");
        private By FinishButton => By.Id("finish");

        public string GetItemTotalText()
        {
            return Driver.FindElement(ItemTotalLabel).Text; // Format: "Item total: $29.99"
        }

        public void ClickFinish()
        {
            Driver.FindElement(FinishButton).Click();
        }
    }
}