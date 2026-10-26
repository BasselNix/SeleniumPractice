using OpenQA.Selenium;
using SeleniumTask.Models;

namespace SeleniumTask.Pages
{
    public class CartPage(IWebDriver driver) : BasePage(driver)
    {
        private static By ContinueShoppingButton => By.Id("continue-shopping");
        private static By CheckoutButton => By.Id("checkout");
        private static By GetItemNameLocator(string productName) =>
            By.XPath($"//div[@class='cart_item']//div[starts-with(@class, 'inventory_item_name') and text()='{productName}']");
        private static By GetItemDescLocator(string productName) =>
            By.XPath($"//div[@class='cart_item' and .//div[starts-with(@class, 'inventory_item_name') and text()='{productName}']]//div[@class='inventory_item_desc']");
        private static By GetItemPriceLocator(string productName) =>
           By.XPath($"//div[@class='cart_item' and .//div[starts-with(@class, 'inventory_item_name') and text()='{productName}']]//div[@class='inventory_item_price']");
        private static By GetRemoveFromCartButtonLocator(string productName) =>
            By.XPath($"//div[@class='cart_item' and .//div[starts-with(@class, 'inventory_item_name') and text()='{productName}']]//button");

        public ItemData GetCartItemData(string productName) => 
            new ItemData
            {
                Name = Wait.Until(d => d.FindElement(GetItemNameLocator(productName))).Text,
                Description = Wait.Until(d => d.FindElement(GetItemDescLocator(productName))).Text,
                Price = Wait.Until(d => d.FindElement(GetItemPriceLocator(productName))).Text,
            };

        public void RemoveItem(string productName) => Wait.Until(d => d.FindElement(GetRemoveFromCartButtonLocator(productName))).Click();

        public void ClickContinueShopping() => Wait.Until(d => { return d.FindElement(ContinueShoppingButton); }).Click();

        public void ClickCheckout() => Wait.Until(d => { return d.FindElement(CheckoutButton); }).Click();
    }
}