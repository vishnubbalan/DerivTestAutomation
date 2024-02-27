using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DerivWebAutomationTest
{
    public class MyInfoPage : Driver
    {

        public MyInfoPage()
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = ".//label[contains(text(),'Date of Birth')]/parent::div/following-sibling::div//input")]
        public IWebElement DateOfBirthDatePicker { get; set; }

       
        [FindsBy(How = How.XPath, Using = ".//label[contains(text(),'Date of Birth')]//ancestor::form//button")]
        public IWebElement SavePersonalDetailsButton { get; set; }

        public MyInfoPage verifyDateOfBirthValue()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            wait.Until(T => DateOfBirthDatePicker.Enabled);
            string dob = DateOfBirthDatePicker.GetAttribute("value");
            DateTime parsedDate;
            string[] formats = { "yyyy-MM-dd" };
            var isValidFormat = DateTime.TryParseExact(dob, formats, new CultureInfo("en-US"), DateTimeStyles.None, out parsedDate);
            Assert.IsTrue(isValidFormat, "No valid date time Date of Birth");
            return this;
        }

        public MyInfoPage verifySavedDateofBirth(string dateOfBirth)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            wait.Until(T => DateOfBirthDatePicker.Enabled);
            string dob = DateOfBirthDatePicker.GetAttribute("value");
            Assert.IsTrue(dob.Contains(dateOfBirth), "Mismatch in saved date of birth");
            return this;
        }


        public MyInfoPage ChangeDateOfBirth(string newDate)
        {
            //DateOfBirthDatePicker.Clear();
            char[] datearray = newDate.ToCharArray();
            int l = datearray.Length;
            foreach(char c in datearray)
            {
                DateOfBirthDatePicker.SendKeys(Keys.Backspace);
            }
            DateOfBirthDatePicker.SendKeys(newDate);
            SavePersonalDetailsButton.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            wait.Until(T => DateOfBirthDatePicker.Enabled);
            return this;
        }
    }
}
