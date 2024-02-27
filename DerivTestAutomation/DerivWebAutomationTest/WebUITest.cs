using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System.Net;

namespace DerivWebAutomationTest
{
    [TestClass]
    public class WebUITest : Driver
    {
        
        [TestInitialize]
        public void init()
        {
            initialise();
        }
        
        [TestMethod]
        public void UserLogin()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            new LoginPage()
                .DoUserLogin("admin", "admin123")
                .VerifyDashBoardPageLoaded();
        }

        [TestMethod]
        public void VerifyAndChangeDateOfBirth()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            Random random = new Random();
            string year = random.Next(2000, 2020).ToString();
            new LoginPage()
                .DoUserLogin("admin", "admin123")
                .GotoMyInfoPage()
                .verifyDateOfBirthValue()
                .ChangeDateOfBirth($"{year}-10-10")
                .verifySavedDateofBirth($"{year}-10-10");
        }

        [TestMethod]
        public void UserLogOut()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            new LoginPage()
                .DoUserLogin("admin", "admin123")
                .VerifyDashBoardPageLoaded()
                .UserLogOut()
                .VeryfyLoginPage();
                
        }

        [TestCleanup]
        public void cleanup()
        {
            driver.Quit();
        }
    }
}