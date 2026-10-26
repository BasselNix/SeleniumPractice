using SeleniumTask.Pages;

namespace SeleniumTask.Tests
{
    [TestFixture]
    public class Task1Tests : BaseTest
    {
        private LoginPage _loginPage;

        [SetUp]
        public void Task1SetUp()
        {
            _loginPage = new LoginPage(Driver);
        }

        [Test]
        public void Task1Scenario()
        {
            _loginPage.Login("locked_out_user", "secret_sauce");

            // Check user is on login page (login_logo present)
            Assert.That(_loginPage.IsLoginLogoDisplayed(), Is.True, "User is not on the login page.");

            // Check warning message
            string expectedMessage = "Epic sadface: Sorry, this user has been locked out.";
            Assert.That(_loginPage.GetErrorMessageText(), Is.EqualTo(expectedMessage), "Warning message does not match.");

            // Close warning message
            _loginPage.CloseErrorMessage();
            Assert.That(_loginPage.IsErrorMessageHidden(), Is.True, "Warning message is still displayed after closing.");
        }
    }
}