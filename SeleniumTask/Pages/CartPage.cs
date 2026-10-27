using OpenQA.Selenium;
using SeleniumTask.Models;
using SeleniumTask.Utilities;

namespace SeleniumTask.Pages
{
    public class CartPage(IWebDriver driver) : BasePage(driver)
    {
        // can be fortified with trimming whitespace and applying case-neutrality using translate() trick
        private static By ContinueShoppingButton => By.XPath("//button[text()='Continue Shopping']");
        private static By CheckoutButton => By.XPath("//button[text()='Checkout']");
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

        public void RemoveItem(string productName) => Driver.ClickWhenReady(GetRemoveFromCartButtonLocator(productName));

        public void ClickContinueShopping() => Driver.ClickWhenReady(ContinueShoppingButton);

        public void ClickCheckout() => Driver.ClickWhenReady(CheckoutButton);
    }
}