using OpenQA.Selenium;

namespace SeleniumTask.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver) { }

        private By UsernameInput => By.Id("user-name");
        private By PasswordInput => By.Id("password");
        private By LoginButton => By.Id("login-button");
        private By LoginLogo => By.ClassName("login_logo");
        private By ErrorMessage => By.CssSelector("h3[data-test='error']");
        private By CloseErrorButton => By.ClassName("error-button");

        public void EnterUsername(string username)
        {
            Driver.FindElement(UsernameInput).SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            Driver.FindElement(PasswordInput).SendKeys(password);
        }

        public void ClickLogin()
        {
            Driver.FindElement(LoginButton).Click();
        }

        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }

        public bool IsLoginLogoDisplayed()
        {
            return Driver.FindElements(LoginLogo).Count > 0 && Driver.FindElement(LoginLogo).Displayed;
        }

        public string GetErrorMessageText()
        {
            return Driver.FindElement(ErrorMessage).Text;
        }

        public void CloseErrorMessage()
        {
            Driver.FindElement(CloseErrorButton).Click();
        }

        public bool IsErrorMessageDisplayed()
        {
            return Driver.FindElements(ErrorMessage).Count > 0;
        }
    }
}