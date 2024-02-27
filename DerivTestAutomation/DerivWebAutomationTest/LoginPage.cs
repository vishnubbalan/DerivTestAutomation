using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace DerivWebAutomationTest
{
    public  class LoginPage : Driver
    {
        public LoginPage() 
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Name, Using = "username")]
        public IWebElement UserNameTxtBox { get; set; }

        [FindsBy(How = How.Name, Using = "password")]
        public IWebElement PasswordTxtBox { get; set; }

        [FindsBy(How = How.XPath, Using = ".//button[@type='submit']")]
        public IWebElement LoginButton { get; set; }

        public DashBoardPage DoUserLogin(string username, string password)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            wait.Until(T => UserNameTxtBox.Enabled);

            UserNameTxtBox.SendKeys(username);
            PasswordTxtBox.SendKeys(password);
            LoginButton.Click();

            return new DashBoardPage();
        }

        public void VeryfyLoginPage()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            wait.Until(T => UserNameTxtBox.Enabled);
            Assert.IsTrue(driver.Url.Contains("login"), "Login page not found");
            Assert.IsTrue(PasswordTxtBox.Displayed, "Password text box not displayed");
        }
    }
}
