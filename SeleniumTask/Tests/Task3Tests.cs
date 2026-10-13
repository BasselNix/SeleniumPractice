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

            _loginPage = new LoginPage(Driver);
            _loginPage.Login("standard_user", "secret_sauce");
            _productsPage = new ProductsPage(Driver);
        }

        [TestCase("Test.allTheThings() T-Shirt (Red)")]
        [TestCase("Sauce Labs Fleece Jacket")]
        [TestCase("Sauce Labs Bike Light")]
        public void VerifyProductPriceInCart(string productName)
        {
            var expectedData = _productsPage.GetItemData(productName);
            _productsPage.AddItemToCart(productName);

            _productsPage.GoToCart();
            var cartPage = new CartPage(Driver);
            var cartItemData = cartPage.GetCartItemData(productName);

            Assert.That(cartItemData.Price, Is.EqualTo(expectedData.Price), $"Price mismatch for '{productName}' in cart.");
        }

        [TearDown]
        public override void TearDown()
        {
            _productsPage.ResetAppState();
            base.TearDown();
        }
    }
}