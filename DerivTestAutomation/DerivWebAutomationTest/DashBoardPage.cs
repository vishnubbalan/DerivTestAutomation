using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DerivWebAutomationTest
{
    public class DashBoardPage : Driver
    {
        public DashBoardPage()
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = ".//header//span/h6")]
        public IWebElement headerBoard { get; set; }

        [FindsBy(How = How.Id, Using = "password")]
        public IWebElement PasswordTxtBox { get; set; }

        [FindsBy(How = How.Id, Using = ".//button[@type='submit']")]
        public IWebElement LoginButton { get; set; }

        [FindsBy(How = How.XPath, Using = ".//a[contains(@href,'dashboard')]")]
        public IWebElement DashBoardPane { get; set; }

        [FindsBy(How = How.XPath, Using = ".//a[contains(@href,'MyDetails')]")]
        public IWebElement MyInfoSelector { get; set; }

        [FindsBy(How = How.XPath, Using = ".//span[@class='oxd-userdropdown-tab']")]
        public IWebElement MyProfileSelector { get; set; }

        [FindsBy(How = How.PartialLinkText, Using = "Logout")]
        public IWebElement logOutLink { get; set; }

        public DashBoardPage VerifyDashBoardPageLoaded()
        {
            string newUrl = driver.Url;
            Assert.IsTrue(newUrl.Contains("dashboard"), "User not navigated to dashboard url");
            string headerText = headerBoard.Text;
            Assert.IsTrue(headerText.Contains("Dashboard"), "Dashboard header not available");
            string backgroundColor = DashBoardPane.GetCssValue("background-color");
            Assert.IsTrue(backgroundColor.Equals("rgba(255, 123, 29, 1)"), "Dashboardpane is not selected in default");
            return new DashBoardPage();
        }

        public MyInfoPage GotoMyInfoPage()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            MyInfoSelector.Click();
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            Thread.Sleep(10000);
            return new MyInfoPage();
        }

        public LoginPage UserLogOut()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            MyProfileSelector.Click();
            logOutLink.Click();
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            return new LoginPage();
        }
    }
}
