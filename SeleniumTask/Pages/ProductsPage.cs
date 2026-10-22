using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTask.Models;

namespace SeleniumTask.Pages
{
    public class ProductsPage : BasePage
    {
        public ProductsPage(IWebDriver driver) : base(driver) { }

        private By InventoryContainer => By.Id("inventory_container");
        private By SortDropdown => By.ClassName("product_sort_container");
        private By ItemPrices => By.ClassName("inventory_item_price");
        private By ShoppingCartBadge => By.ClassName("shopping_cart_badge");
        private By ShoppingCartLink => By.ClassName("shopping_cart_link");

        public bool IsInventoryContainerDisplayed()
        {
            return Wait.Until(d =>
            {
                var elements = d.FindElements(InventoryContainer);
                return elements.Count > 0 && elements[0].Displayed;
            });
        }

        public void SelectSortOption(string value)
        {
            var selectElement = new SelectElement(Driver.FindElement(SortDropdown));
            selectElement.SelectByValue(value);
        }

        public List<decimal> GetProductPrices()
        {
            var priceElements = Driver.FindElements(ItemPrices);
            return priceElements
                .Select(e => decimal.Parse(e.Text.Replace("$", "")))
                .ToList();
        }

        private IWebElement GetItemCard(string productName)
        {
            try
            {
                return Wait.Until(d =>
                {
                    var cards = d.FindElements(By.ClassName("inventory_item"));

                    foreach (var card in cards)
                    {
                        try
                        {
                            var titleElement = card.FindElement(By.ClassName("inventory_item_name"));
                            if (titleElement.Text.Equals(productName, StringComparison.OrdinalIgnoreCase))
                            {
                                return card;
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

        public void AddItemToCart(string productName)
        {
            var card = GetItemCard(productName);
            card.FindElement(By.TagName("button")).Click();
        }

        public string GetItemButtonText(string productName)
        {
            var card = GetItemCard(productName);
            return card.FindElement(By.TagName("button")).Text;
        }

        public ItemData GetItemData(string productName)
        {
            var card = GetItemCard(productName);
            return new ItemData
            {
                Name = card.FindElement(By.ClassName("inventory_item_name")).Text,
                Description = card.FindElement(By.ClassName("inventory_item_desc")).Text,
                Price = card.FindElement(By.ClassName("inventory_item_price")).Text
            };
        }

        public string GetCartBadgeText()
        {
            return Wait.Until(d => { return d.FindElement(ShoppingCartBadge); }).Text;
        }

        public bool IsCartBadgeDisplayed()
        {
            return Wait.Until(d => { return d.FindElements(ShoppingCartBadge); }).Count > 0;
        }

        public void GoToCart()
        {
            Wait.Until(d => { return d.FindElement(ShoppingCartLink); }).Click();
        }
    }
}