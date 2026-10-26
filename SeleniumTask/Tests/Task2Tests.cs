using SeleniumTask.Pages;
using static SeleniumTask.Pages.ProductsPage;

namespace SeleniumTask.Tests
{
    [TestFixture]
    public class Task2Tests : BaseTest
    {
        private LoginPage _loginPage;

        [SetUp]
        public void Task2SetUp()
        {
            _loginPage = new LoginPage(Driver);
        }

        [Test]
        public void Task2Scenario()
        {
            _loginPage.Login("standard_user", "secret_sauce");

            // Check inventory container present
            var productsPage = new ProductsPage(Driver);
            Assert.That(productsPage.IsInventoryContainerDisplayed(), Is.True, "Product list page container is not displayed.");

            // Change sorting to Price (Low to High)
            productsPage.SelectSortOption(ProductSortOption.lohi);

            // Verify prices are sorted low to high
            List<decimal> prices = productsPage.GetProductPrices();
            Assert.That(prices, Is.Ordered.Ascending, "Products are not properly sorted by price (Low to High).");

            // Add 'Sauce Labs Bolt T-Shirt' to cart and verify button text changes to 'Remove'
            string targetProduct1 = "Sauce Labs Bolt T-Shirt";
            productsPage.AddItemToCart(targetProduct1);
            Assert.That(productsPage.GetItemButtonText(targetProduct1), Is.EqualTo("Remove"), "Button text did not change to 'Remove'.");

            // Check cart count is 1
            Assert.That(productsPage.GetCartBadgeText(), Is.EqualTo("1"), "Cart item count is not 1.");

            // Save item data
            var savedItem1 = productsPage.GetItemData(targetProduct1);

            // Go to cart & verify data match
            productsPage.GoToCart();
            var cartPage = new CartPage(Driver);
            var cartItem1 = cartPage.GetCartItemData(targetProduct1);

            Assert.That(cartItem1.Name, Is.EqualTo(savedItem1.Name), "Product names do not match.");
            Assert.That(cartItem1.Description, Is.EqualTo(savedItem1.Description), "Product descriptions do not match.");
            Assert.That(cartItem1.Price, Is.EqualTo(savedItem1.Price), "Product prices do not match.");

            // Click Remove & verify cart badge counter is not displayed
            cartPage.RemoveItem(targetProduct1);
            Assert.That(productsPage.IsCartBadgeHidden(), Is.True, "Cart count badge should not be displayed after removing all items.");

            // Return to products page via 'Continue Shopping'
            cartPage.ClickContinueShopping();

            // Add 'Sauce Labs Backpack' to cart & save item data
            string targetProduct2 = "Sauce Labs Backpack";
            productsPage.AddItemToCart(targetProduct2);
            var savedItem2 = productsPage.GetItemData(targetProduct2);

            // Go to cart and click 'Checkout'
            productsPage.GoToCart();
            cartPage.ClickCheckout();

            // Fill form and continue
            var checkoutStepOne = new CheckoutStepOnePage(Driver);
            checkoutStepOne.FillFormAndContinue("Bassel", "Yasser", "123abc");

            // On Overview page verify Item Total matches saved price
            var checkoutStepTwo = new CheckoutStepTwoPage(Driver);
            string itemTotalText = checkoutStepTwo.GetItemTotalText();
            Assert.That(itemTotalText, Does.Contain(savedItem2.Price), $"Item total '{itemTotalText}' does not match item price '{savedItem2.Price}'.");

            // Click Finish
            checkoutStepTwo.ClickFinish();

            // Verify Pony Express image on Thank You screen
            var checkoutComplete = new CheckoutCompletePage(Driver);
            Assert.That(checkoutComplete.IsPonyExpressImageDisplayed(), Is.True, "Pony Express image with alt='Pony Express' is missing.");
        }
    }
}