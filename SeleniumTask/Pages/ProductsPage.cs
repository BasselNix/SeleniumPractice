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
            return Driver.FindElements(InventoryContainer).Count > 0;
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
            var items = Driver.FindElements(By.ClassName("inventory_item"));
            foreach (var item in items)
            {
                var title = item.FindElement(By.ClassName("inventory_item_name")).Text;
                if (title.Equals(productName, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }
            throw new NoSuchElementException($"Product '{productName}' not found on page.");
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
            return Driver.FindElement(ShoppingCartBadge).Text;
        }

        public bool IsCartBadgeDisplayed()
        {
            return Driver.FindElements(ShoppingCartBadge).Count > 0;
        }

        public void GoToCart()
        {
            Driver.FindElement(ShoppingCartLink).Click();
        }
    }
}