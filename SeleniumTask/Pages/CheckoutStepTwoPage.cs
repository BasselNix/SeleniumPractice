using OpenQA.Selenium;
using SeleniumTask.Utilities;

namespace SeleniumTask.Pages
{
    public class CheckoutStepTwoPage(IWebDriver driver) : BasePage(driver)
    {
        private static By ItemTotalLabel => By.XPath("//div[starts-with(@class, 'summary_subtotal_label')]");
        private static By FinishButton => By.XPath("//button[text()='Finish']");

        public string GetItemTotalText() => Wait.Until(d => d.FindElement(ItemTotalLabel)).Text;

        public void ClickFinish() => Driver.ClickWhenReady(FinishButton);
    }
}