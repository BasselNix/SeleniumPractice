using SeleniumTask.Pages;

namespace SeleniumTask.Tests
{
    [TestFixture]
    public class Task1Tests : BaseTest
    {
        [Test]
        public void LockedOutUser_ShowsWarningMessage()
        {
            var loginPage = new LoginPage(Driver);

            // Step 2: Login with locked_out_user
            loginPage.Login("locked_out_user", "secret_sauce");

            // Step 3: Check user is on login page (login_logo present)
            Assert.That(loginPage.IsLoginLogoDisplayed(), Is.True, "User is not on the login page.");

            // Step 4: Check warning message
            string expectedMessage = "Epic sadface: Sorry, this user has been locked out.";
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(Driver, System.TimeSpan.FromSeconds(10));
            bool isPonyLoaded = wait.Until(d => loginPage.IsErrorMessageDisplayed());
            Assert.That(loginPage.GetErrorMessageText(), Is.EqualTo(expectedMessage), "Warning message does not match.");

            // Step 5: Close warning message
            loginPage.CloseErrorMessage();
            Assert.That(loginPage.IsErrorMessageDisplayed(), Is.False, "Warning message is still displayed after closing.");
        }
    }
}