using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTask.Models;
using SeleniumTask.Utilities;
using System.Globalization;

namespace SeleniumTask.Pages
{
    public class ProductsPage(IWebDriver driver) : BasePage(driver)
    {
        private static By MenuButton => By.Id("react-burger-menu-btn");
        private static By ResetSidebarLink => By.Id("reset_sidebar_link");
        private static By InventoryContainer => By.Id("inventory_container");
        private static By SortDropdown => By.ClassName("product_sort_container");
        private static By ItemPrices => By.ClassName("inventory_item_price");
        private static By ShoppingCartBadge => By.ClassName("shopping_cart_badge");
        private static By ShoppingCartLink => By.ClassName("shopping_cart_link");
        private static By GetItemNameLocator(string productName) =>
            By.XPath($"//div[@class='inventory_item']//div[starts-with(@class, 'inventory_item_name') and text()='{productName}']");
        private static By GetItemDescLocator(string productName) =>
            By.XPath($"//div[@class='inventory_item' and .//div[starts-with(@class, 'inventory_item_name') and text()='{productName}']]//div[@class='inventory_item_desc']");
        private static By GetItemPriceLocator(string productName) =>
           By.XPath($"//div[@class='inventory_item' and .//div[starts-with(@class, 'inventory_item_name') and text()='{productName}']]//div[@class='inventory_item_price']");
        private static By GetAddToCartButtonLocator(string productName) =>
            By.XPath($"//div[@class='inventory_item' and .//div[starts-with(@class, 'inventory_item_name') and text()='{productName}']]//button");

        public void ResetAppState()
        {
            Driver.ClickWhenReady(MenuButton);
            Driver.ClickWhenReady(ResetSidebarLink);
        }

        public enum ProductSortOption
        {
            ByNameAscending,
            ByNameDescending,
            ByPriceLowToHigh,
            ByPriceHighToLow
        }

        public bool IsInventoryContainerDisplayed() => Driver.IsElementDisplayed(InventoryContainer);

        private static string GetSortOptionValue(ProductSortOption option) => option switch
        {
            ProductSortOption.ByNameAscending => "az",
            ProductSortOption.ByNameDescending => "za",
            ProductSortOption.ByPriceLowToHigh => "lohi",
            ProductSortOption.ByPriceHighToLow => "hilo",
            _ => throw new ArgumentOutOfRangeException(nameof(option), option, $"Failed to find sort option: {option}")
        };

        public void SelectSortOption(ProductSortOption sortOption)
        {
            var selectElement = new SelectElement(Driver.FindElement(SortDropdown));
            selectElement.SelectByValue(GetSortOptionValue(sortOption));
        }

        public List<decimal> GetProductPrices()
        {
            // Wait until at least one price element is visible on the page
            Wait.Until(d => d.FindElements(ItemPrices).Any(e => e.Displayed));

            return Driver.FindElements(ItemPrices)
                .Select(e => decimal.Parse(e.Text.Replace("$", "").Trim(), CultureInfo.InvariantCulture))
                .ToList();
        }

        public void AddItemToCart(string productName) => Driver.ClickWhenReady(GetAddToCartButtonLocator(productName));

        public string GetItemButtonText(string productName) => Wait.Until(d => d.FindElement(GetAddToCartButtonLocator(productName))).Text;

        public ItemData GetItemData(string productName) =>
            new ItemData
            {
                Name = Wait.Until(d => d.FindElement(GetItemNameLocator(productName))).Text,
                Description = Wait.Until(d => d.FindElement(GetItemDescLocator(productName))).Text,
                Price = Wait.Until(d => d.FindElement(GetItemPriceLocator(productName))).Text,
            };

        public string GetCartBadgeText() => Wait.Until(d => d.FindElement(ShoppingCartBadge)).Text;

        public bool IsCartBadgeDisplayed() => Driver.IsElementDisplayed(ShoppingCartBadge);

        public bool IsCartBadgeHidden() => Driver.IsElementHidden(ShoppingCartBadge);

        public void GoToCart() => Driver.ClickWhenReady(ShoppingCartLink);
    }
}