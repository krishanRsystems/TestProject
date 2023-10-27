using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestPrimeReact
{
    public class Tests
    {
        private IWebDriver webDriver { get; set; } = null;
        private string driverPath { get; set; } = Environment.CurrentDirectory;
        private string baseUrl { get; set; } = "https://www.primefaces.org/primereact-v5/#/datatable/selection";

        [SetUp]
        public void Setup()
        {
            webDriver = GetChromeDriver();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
            webDriver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
        }

        [Test]
        public void TestCheckListItem()
        {
            webDriver.Navigate().GoToUrl(baseUrl);
            Assert.AreEqual("PrimeReact", webDriver.Title);
            Console.WriteLine("Title verification successful..!");
            LandingPage landing = new LandingPage(webDriver);
            landing.CheckItem("Name", "Blue Band");
            //landing.CheckItem("Name", "Bracelet");
            //landing.CheckItem("Code", "244wgerg2");
        }

        private WebDriver GetChromeDriver()
        {
            var options = new ChromeOptions();
            return new ChromeDriver(driverPath, options, TimeSpan.FromSeconds(300));
        }
    }
}
