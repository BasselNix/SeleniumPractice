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
            productsPage.SelectSortOption(ProductSortOption.ByPriceLowToHigh);

            // Verify prices are sorted low to high
            List<decimal> prices = productsPage.GetProductPrices();
            Assert.That(prices, Is.Ordered.Ascending, "Products are not properly sorted by price (Low to High).");

            // Add 'Sauce Labs Bolt T-Shirt' to cart and verify that it has been added
            string targetProduct1 = "Sauce Labs Bolt T-Shirt";
            productsPage.AddItemToCart(targetProduct1);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(productsPage.GetItemButtonText(targetProduct1), Is.EqualTo("Remove"), "Button text did not change to 'Remove'.");
                Assert.That(productsPage.GetCartBadgeText(), Is.EqualTo("1"), "Cart item count is not 1.");
            }

            // Save item data
            var savedItem1 = productsPage.GetItemData(targetProduct1);

            // Go to cart & verify data match
            productsPage.GoToCart();
            var cartPage = new CartPage(Driver);
            var cartItem1 = cartPage.GetCartItemData(targetProduct1);

            Assert.That(cartItem1, Is.EqualTo(savedItem1).UsingPropertiesComparer(), "Cart item does not match product.");

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
            checkoutStepOne.FillCheckoutForm("Bassel", "Yasser", "123abc");
            checkoutStepOne.ClickContinue();

            // On Overview page verify Item Total matches saved price
            var checkoutStepTwo = new CheckoutStepTwoPage(Driver);
            string itemTotalText = checkoutStepTwo.GetItemTotalText();
            string extractedPrice = itemTotalText.Split(':').Last().Trim();
            Assert.That(extractedPrice, Is.EqualTo(savedItem2.Price), $"Item total price '{extractedPrice}' does not match saved item price '{savedItem2.Price}'.");

            checkoutStepTwo.ClickFinish();

            // Verify Pony Express image on Thank You screen
            var checkoutComplete = new CheckoutCompletePage(Driver);
            Assert.That(checkoutComplete.IsPonyExpressImageDisplayed(), Is.True, "Pony Express image with alt='Pony Express' is missing.");
        }
    }
}