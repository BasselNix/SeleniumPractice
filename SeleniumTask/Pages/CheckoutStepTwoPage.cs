using OpenQA.Selenium;

namespace SeleniumTask.Pages
{
    public class CheckoutStepTwoPage(IWebDriver driver) : BasePage(driver)
    {
        private static By ItemTotalLabel => By.ClassName("summary_subtotal_label");
        private static By FinishButton => By.Id("finish");

        public string GetItemTotalText() => Wait.Until(d => { return d.FindElement(ItemTotalLabel).Text; });

        public void ClickFinish() => Driver.FindElement(FinishButton).Click();
    }
}