using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace SimpleTests
{
    [TestFixture]
    public class SimpleTests
    {
       
        private IWebDriver driver;
        private WebDriverWait wait;
        IWebElement element;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test_1()
        {
            
            driver.Navigate().GoToUrl("https://www.google.com.ua/");
            driver.FindElement(By.XPath("//input[@id='lst-ib']")).SendKeys("Вікіпедія" + Keys.Enter);
            driver.FindElement(By.PartialLinkText("Вікіпедія")).Click();
            Assert.AreEqual("Вікіпедія", driver.Title);

        }
        [Test]
        public void TEST_2()
        {
            driver.Url = "https://uk.wikipedia.org/wiki";
            Assert.AreEqual("Вікіпедія", driver.Title);
            DateTime currentDate = DateTime.Now;
            string textToCheck = driver.FindElement(By.LinkText(currentDate.ToString("d MMMM"))).Text;
            Assert.AreEqual(currentDate.ToString("d MMMM"), textToCheck); //должен быть установлен календарь на ПК на украинском языке так как сравниваем украинскую дату на украинской Википедии
        }
        [Test]
        public void TEST_3()
        {
            driver.Url = "https://uk.wikipedia.org/wiki";
            Assert.AreEqual("Вікіпедія", driver.Title);
            driver.FindElement(By.LinkText("Поточні події")).Click();
            Assert.AreEqual("Вікіпедія:Поточні події — Вікіпедія", driver.Title);

        }
        [Test]
        public void TEST_4()
        {
            driver.Url = "https://uk.wikipedia.org/wiki";
            Assert.AreEqual("Вікіпедія", driver.Title);
            driver.FindElement(By.LinkText("Поточні події")).Click();
            Assert.AreEqual("Вікіпедія:Поточні події — Вікіпедія", driver.Title);
            IWebElement posttitle = wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='mw-content-text']/div/table/tbody/tr[1]/td[1]/div[4]/ul[2]/li[5]/ul/li[1]")));
            string expectedText = posttitle.Text;
            StringAssert.AreEqualIgnoringCase("Рада Безпеки ООН скликає екстренне засідання через хіматаку в Сирії[20]", expectedText, "Ошибка текста"); //поменяла проверку события так как за 3ее октября то событие не нашла

        }

        [Test]
        public void TEST_5()
        {
            driver.Url = "https://uk.wikipedia.org/wiki";
            Assert.AreEqual("Вікіпедія", driver.Title);
            IWebElement element1 = driver.FindElement(By.XPath("//*[@id='searchInput']"));
            element1.SendKeys("Smoke testing");
            element1.SendKeys(Keys.Enter);
            Thread.Sleep(4000);
            Assert.AreEqual("Smoke testing — Вікіпедія", driver.Title);

        }


        [TearDown]
        public void TearDown() => driver.Quit();
    }
}


