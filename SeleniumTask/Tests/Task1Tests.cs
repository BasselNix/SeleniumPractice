using SeleniumTask.Pages;

namespace SeleniumTask.Tests
{
    [TestFixture]
    public class Task1Tests : BaseTest
    {
        [Test]
        public void Task1Scenario()
        {
            var loginPage = new LoginPage(Driver);

            loginPage.Login("locked_out_user", "secret_sauce");

            // Check user is on login page (login_logo present)
            Assert.That(loginPage.IsLoginLogoDisplayed(), Is.True, "User is not on the login page.");

            // Check warning message
            string expectedMessage = "Epic sadface: Sorry, this user has been locked out.";
            Assert.That(loginPage.GetErrorMessageText(), Is.EqualTo(expectedMessage), "Warning message does not match.");

            // Close warning message
            loginPage.CloseErrorMessage();
            Assert.That(loginPage.IsErrorMessageDisplayed(), Is.False, "Warning message is still displayed after closing.");
        }
    }
}