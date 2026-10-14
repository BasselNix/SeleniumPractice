using OpenQA.Selenium;
using SeleniumTask.Models;

namespace SeleniumTask.Pages
{
    public class CartPage : BasePage
    {
        public CartPage(IWebDriver driver) : base(driver) { }

        private By ContinueShoppingButton => By.Id("continue-shopping");
        private By CheckoutButton => By.Id("checkout");

        private IWebElement GetCartItem(string productName)
        {
            var items = Driver.FindElements(By.ClassName("cart_item"));
            foreach (var item in items)
            {
                var title = item.FindElement(By.ClassName("inventory_item_name")).Text;
                if (title.Equals(productName, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }
            throw new NoSuchElementException($"Cart item '{productName}' not found.");
        }

        public ItemData GetCartItemData(string productName)
        {
            var item = GetCartItem(productName);
            return new ItemData
            {
                Name = item.FindElement(By.ClassName("inventory_item_name")).Text,
                Description = item.FindElement(By.ClassName("inventory_item_desc")).Text,
                Price = item.FindElement(By.ClassName("inventory_item_price")).Text
            };
        }

        public void RemoveItem(string productName)
        {
            var item = GetCartItem(productName);
            item.FindElement(By.TagName("button")).Click();
        }

        public void ClickContinueShopping()
        {
            Driver.FindElement(ContinueShoppingButton).Click();
        }

        public void ClickCheckout()
        {
            Driver.FindElement(CheckoutButton).Click();
        }
    }
}