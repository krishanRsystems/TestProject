using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestPrimeReact
{
    public class Tests
    {
        private IWebDriver WebDriver { get; set; } = null;
        private string DriverPath { get; set; } = Environment.CurrentDirectory;
        private string BaseUrl { get; set; } = "https://www.primefaces.org/primereact-v5/#/datatable/selection";

        [SetUp]
        public void Setup()
        {
            WebDriver = GetChromeDriver();
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
            WebDriver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            WebDriver.Quit();
        }

        [Test]
        public void TestCheckListItem()
        {
            WebDriver.Navigate().GoToUrl(BaseUrl);
            Assert.AreEqual("PrimeReact",WebDriver.Title);
            Console.WriteLine("Title verification successful..!");
            LandingPage landing = new LandingPage(WebDriver);
            landing.CheckItem("Name", "Blue Band");
            //landing.CheckItem("Name", "Blue T-Shirt");
            //landing.CheckItem("Code", "244wgerg2");
        }

        private WebDriver GetChromeDriver()
        {
            var options = new ChromeOptions();
            return new ChromeDriver(DriverPath, options, TimeSpan.FromSeconds(300));
        }
    }
}