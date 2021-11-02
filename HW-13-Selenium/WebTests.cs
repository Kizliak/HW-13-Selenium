using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Web.UI.WebControls;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


//Покрыть основными тестами данный сайт используя Selenium, с учетом техник тест дизайна и т.д. https://newbookmodels.com/ 
//(Регистрация, авторизация, редактирование профиля, добавление карты и всех возможных изменений аккаунта, выход из аккаунта)

namespace HW_13_Selenium
{
    public class Tests
    {
        IWebDriver driver;
        Actions action;
        static string firstname = Generators.GetRandName();
        static string secondname = Generators.GetRandName();
        static string email = Generators.GetRndEmail();
        static string password = Generators.GetRndPass();
        static string mobile = Generators.GetRndPhone();
        static string companyName = Generators.GetRandName();
        static string companyUrl = "https://" + firstname.ToLower() + ".com/";
        static int companyLocation = Generators.Randomchik.Next(100, 999);

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
            options.AddArguments("start-maximized");

            driver = new ChromeDriver(options);
            action = new Actions(driver);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test, Order(1)] // Try to register
        public void SignUp()
        {
            driver.Navigate().GoToUrl("https://newbookmodels.com/");
            IWebElement signUpButton = driver.FindElement(By.ClassName("Navbar__signUp--12ZDV"));
            signUpButton.Click();

            IWebElement firstNameField = driver.FindElement(By.CssSelector("input[name=\"first_name\"]"));
            firstNameField.SendKeys(firstname);
            
            IWebElement lastNameField = driver.FindElement(By.CssSelector("input[name=\"last_name\"]"));
            lastNameField.SendKeys(secondname);

            IWebElement emailField = driver.FindElement(By.CssSelector("input[name=\"email\"]"));
            emailField.SendKeys(email);

            IWebElement passwordField = driver.FindElement(By.CssSelector("input[name=\"password\"]"));
            passwordField.SendKeys(password);

            IWebElement passwordConfirmField = driver.FindElement(By.CssSelector("input[name=\"password_confirm\"]"));
            passwordConfirmField.SendKeys(password);

            IWebElement mobileField = driver.FindElement(By.CssSelector("input[name=\"phone_number\"]"));
            mobileField.SendKeys(mobile);

            Thread.Sleep(4000);

            IWebElement NextButton = driver.FindElement(By.CssSelector("button[class=\"SignupForm__submitButton--1m1C2 Button__button---rQSB Button__themePrimary--E5ESP Button__sizeMedium--uLCYD Button__fontSmall--1EPi5 Button__withTranslate--1qGAH\"]"));
            NextButton.Click();

            Thread.Sleep(4000);

            Assert.AreEqual("https://newbookmodels.com/join/company", driver.Url);

            IWebElement companyNameField = driver.FindElement(By.CssSelector("input[name=\"company_name\"]"));
            companyNameField.SendKeys(companyName);

            IWebElement companyUrlField = driver.FindElement(By.CssSelector("input[name=\"company_website\"]"));
            companyUrlField.SendKeys(companyUrl);

            IWebElement locationField = driver.FindElement(By.CssSelector("input[name=\"location\"]"));
            locationField.SendKeys(companyLocation.ToString());
            Thread.Sleep(1500); // заменить на отслеживание появления элемента
            locationField.SendKeys(Keys.ArrowDown);
            Thread.Sleep(500);
            locationField.SendKeys(Keys.Enter);

            //IWebElement addressDropDownItem = driver.FindElement(By.CssSelector("div[class=\"pac-item\"]"));
            //action.MoveToElement(addressDropDownItem);
            //action.Perform();

            IWebElement industry = driver.FindElement(By.CssSelector("input[name=\"industry\"]"));
            industry.Click();

            IWebElement chosenIndustry = driver.FindElements(By.ClassName("Select__option--1IbG6"))[Generators.Randomchik.Next(0, 4)];
            chosenIndustry.Click();

            Thread.Sleep(4000);

            IWebElement finishButton = driver.FindElement(By.CssSelector("button[class=\"SignupCompanyForm__submitButton--3mz3p Button__button---rQSB Button__themePrimary--E5ESP Button__sizeMedium--uLCYD Button__fontSmall--1EPi5 Button__withTranslate--1qGAH\"]"));
            finishButton.Click();

            Thread.Sleep(4000);

            Assert.AreEqual("https://newbookmodels.com/explore", driver.Url);
        }

        [TestCase("jlfunajeb@laste.ml", "A1jlfunajeb@laste.ml"), Order(2)] // Try to login
        public void Login(string loginData, string passData)
        {
            bool resultElementOnPage = true;
            driver.Navigate().GoToUrl("https://newbookmodels.com/auth/signin");
            IWebElement emailField = driver.FindElement(By.CssSelector("input[name=\"email\"]"));
            emailField.SendKeys(loginData);

            IWebElement passwordField = driver.FindElement(By.CssSelector("input[name=\"password\"]"));
            passwordField.SendKeys(passData);

            IWebElement loginButton = driver.FindElement(By.CssSelector("button[class=\"SignInForm__submitButton--cUdOV Button__button---rQSB Button__themeSealBrown--3arN6 Button__sizeMedium--uLCYD Button__fontSmall--1EPi5 Button__withTranslate--1qGAH\"]"));
            loginButton.Click();

            Thread.Sleep(4000);

            Assert.AreEqual("https://newbookmodels.com/explore", driver.Url);

            try
            {
                driver.FindElement(By.ClassName("AvatarClient__avatar--3TC7_"));
            }
            catch (NoSuchElementException)
            {
                resultElementOnPage = false;
            }
            catch (StaleElementReferenceException)
            {
                resultElementOnPage = false;
            }

            Assert.IsTrue(resultElementOnPage);
        }

        [TestCase("jlfunajeb@laste.ml", "A1jlfunajeb@laste.ml"), Order(3)] // Try to login
        public void Logout(string loginData, string passData)
        {
            Login(loginData, passData);

            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 60));
            IWebElement popupWindow = wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div[class=\"resend-email__root\"]")));
            popupWindow.Click();
            action.SendKeys(Keys.Escape).Perform();

            IWebElement avatarIcon = wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div[class=\"AvatarClient__avatar--3TC7_\"]")));
            avatarIcon.Click();

            IWebElement logoutLink = driver.FindElement(By.CssSelector("div[class=\"link link_type_logout link_active\"]"));
            action.MoveToElement(logoutLink).Perform();
            logoutLink.Click();

            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("button[class=\"SignInForm__submitButton--cUdOV Button__button---rQSB Button__themeSealBrown--3arN6 Button__sizeMedium--uLCYD Button__fontSmall--1EPi5 Button__withTranslate--1qGAH\"]")));
            Assert.AreEqual("https://newbookmodels.com/auth/signin", driver.Url);
            Thread.Sleep(1500);
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