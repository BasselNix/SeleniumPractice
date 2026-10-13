using SeleniumTask.Pages;

namespace SeleniumTask.Tests
{
    [TestFixture]
    public class Task3Tests : BaseTest
    {
        private LoginPage _loginPage;
        private ProductsPage _productsPage;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            // Task 3 Step 1: Log into store in Setup
            _loginPage = new LoginPage(Driver);
            _loginPage.Login("standard_user", "secret_sauce");
            _productsPage = new ProductsPage(Driver);
        }

        [TestCase("Test.allTheThings() T-Shirt (Red)")]
        [TestCase("Sauce Labs Fleece Jacket")]
        [TestCase("Sauce Labs Bike Light")]
        public void VerifyProductPriceInCart(string productName)
        {
            // Task 3 Step 2: Remember price and add item to cart
            var expectedData = _productsPage.GetItemData(productName);
            _productsPage.AddItemToCart(productName);

            // Task 3 Step 3: Go to cart and check price
            _productsPage.GoToCart();
            var cartPage = new CartPage(Driver);
            var cartItemData = cartPage.GetCartItemData(productName);

            Assert.That(cartItemData.Price, Is.EqualTo(expectedData.Price), $"Price mismatch for '{productName}' in cart.");
        }

        [TearDown]
        public override void TearDown()
        {
            // Task 3 Step 4: Reset App State in TearDown
            if (Driver != null)
            {
                try
                {
                    _productsPage.ResetAppState();
                }
                catch
                {
                    // Fallback log if reset fails prior to quit
                }
            }

            base.TearDown();
        }
    }
}