using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DerivWebAutomationTest
{
    public class Driver
    {
        static string url = "https://opensource-demo.orangehrmlive.com/";
        public static IWebDriver driver = null;

        public static void initialise()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
        }
    }
}
