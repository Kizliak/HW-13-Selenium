using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;


//Покрыть основными тестами данный сайт используя Selenium, с учетом техник тест дизайна и т.д. https://newbookmodels.com/ 
//(Регистрация, авторизация, редактирование профиля, добавление карты и всех возможных изменений аккаунта, выход из аккаунта)

namespace HW_13_Selenium
{
    public class Tests
    {
        IWebDriver driver;
        Actions action;

        [OneTimeSetUp]
        public void ClearProccesses()
        {
            foreach (var proc in Process.GetProcessesByName("IEDriverServer")) //https://question-it.com/questions/1816962/kak-ubit-okno-konsoli-iedriverserverexe-posle-zapuska-testa-internetexplorerdriver-selenium
            {
                proc.Kill();
            }
        }

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            Proxy proxy = new Proxy();
            //proxy.setHttpProxy("myhttpproxy:3337");
            //options.setCapability("proxy", proxy);
            options.AddArguments("start-maximized");

            driver = new ChromeDriver(options);
            action = new Actions(driver);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://rozetka.com.ua/lego_10875/p40502640/");
            //driver.Manage().Window.Maximize();
        }

        [Test]
        public void SignUp()
        {
            Random rnd = new Random();
            string name = Generators.RndFirstName();

            driver.Navigate().GoToUrl("https://newbookmodels.com/");
            IWebElement signUpButton = driver.FindElement(By.ClassName("Navbar__signUp--12ZDV"));
            signUpButton.Click();

            IWebElement nameField = driver.FindElement(By.TagName("input")).FindElement(By.CssSelector("name=\"first_name\""));
            nameField.SendKeys(name);



        }

        [TearDown]
        public void After()
        {
            driver.Close();
            driver.Quit();
            foreach (var proc in Process.GetProcessesByName("IEDriverServer")) //https://question-it.com/questions/1816962/kak-ubit-okno-konsoli-iedriverserverexe-posle-zapuska-testa-internetexplorerdriver-selenium
            {
                proc.Kill();
            }
        }
    }
}