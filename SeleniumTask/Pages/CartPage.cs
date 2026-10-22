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
            try
            {
                return Wait.Until(d =>
                {
                    var items = Driver.FindElements(By.ClassName("cart_item"));

                    foreach (var item in items)
                    {
                        try
                        {
                            var title = item.FindElement(By.ClassName("inventory_item_name")).Text;
                            if (title.Equals(productName, StringComparison.OrdinalIgnoreCase))
                            {
                                return item;
                            }
                        }
                        catch (NoSuchElementException)
                        {
                            // Do nothing and let the loop continue checking other cards.
                        }
                    }

                    // If no matching product name is found yet, return null to force Wait.Until to retry
                    return null;
                });
            }
            catch (WebDriverTimeoutException)
            {
                throw new NoSuchElementException($"Product '{productName}' not found on the page within the timeout period.");
            }
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
            Wait.Until(d => { return d.FindElement(ContinueShoppingButton); }).Click();
        }

        public void ClickCheckout()
        {
            Wait.Until(d => { return d.FindElement(CheckoutButton); }).Click();
        }
    }
}