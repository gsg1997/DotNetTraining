using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://Amazon.com/login");

            // Locate username and password fields and login button
            driver.FindElement(By.Id("username")).SendKeys("testuser");
            driver.FindElement(By.Id("password")).SendKeys("password123");
            driver.FindElement(By.XPath("//button[text()='Login']")).Click();

            // Assertion to verify login success
            string currentUrl = driver.Url;
            if (currentUrl.Contains("dashboard"))
            {
                Console.WriteLine("Login successful. Current URL: " + currentUrl);
            }
            else
            {
                Console.WriteLine("Login failed.");
            }

            driver.Quit();
        }
    }
}
