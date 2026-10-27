using OpenQA.Selenium;
using SeleniumTask.Utilities;

namespace SeleniumTask.Pages
{
    public class LoginPage(IWebDriver driver) : BasePage(driver)
    {
        private static By UsernameInput => By.XPath("//input[@placeholder='Username']");
        private static By PasswordInput => By.XPath("//input[@placeholder='Password']");
        private static By LoginButton => By.XPath("//input[@value='Login']");
        private static By LoginLogo => By.ClassName("login_logo");
        private static By ErrorMessage => By.CssSelector("h3[data-test='error']");
        private static By CloseErrorButton => By.ClassName("error-button");

        public void EnterUsername(string username) => Driver.FindElement(UsernameInput).SendKeys(username);

        public void EnterPassword(string password) => Driver.FindElement(PasswordInput).SendKeys(password);

        public void ClickLogin() => Driver.ClickWhenReady(LoginButton);

        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }

        public bool IsLoginLogoDisplayed() => Driver.IsElementDisplayed(LoginLogo);

        public string GetErrorMessageText() => Driver.FindElement(ErrorMessage).Text;

        public void CloseErrorMessage() => Driver.FindElement(CloseErrorButton).Click();

        public bool IsErrorMessageDisplayed() => Driver.IsElementDisplayed(ErrorMessage);

        public bool IsErrorMessageHidden() => Driver.IsElementHidden(ErrorMessage);
    }
}