using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System.Net;

namespace DerivWebAutomationTest
{
    [TestClass]
    public class WebUITest
    {
        string url = "https://opensource-demo.orangehrmlive.com/";
        IWebDriver driver = null;
        
        [TestInitialize]
        public void init()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
        }
        
        [TestMethod]
        public void UserLogin()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            WebDriverWait wait = new   WebDriverWait(driver, TimeSpan.FromSeconds(45));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            wait.Until(T => driver.FindElement(By.Name("username")).Enabled);
            IWebElement usernameTxtBox = driver.FindElement(By.Name("username"));
            IWebElement passwordTxtBox = driver.FindElement(By.Name("password"));
            IWebElement LoginButton = driver.FindElement(By.XPath(".//button[@type='submit']"));
            
            usernameTxtBox.SendKeys("admin");
            passwordTxtBox.SendKeys("admin123");
            LoginButton.Click();

            string newUrl = driver.Url;
            Assert.IsTrue(newUrl.Contains("dashboard"), "User not navigated to dashboard url");

            IWebElement headerBoard = driver.FindElement(By.XPath(".//header//span/h6"));
            string headerText = headerBoard.Text;

            Assert.IsTrue(headerText.Contains("Dashboard"), "Dashboard header not available");
        }
    }
}