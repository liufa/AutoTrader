using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutoTrader
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IWebDriver driver = new ChromeDriver(@"C:\Users\LIUFA\Downloads\chromedriver_win32"))
            {
                // driver.Navigate().GoToUrl("http://www.google.com");
                driver.Navigate().GoToUrl("http://www.ebay.co.uk/sch/Computer-IT-/171265/i.html?_pgn=2&_skc=50&rt=nc");
                var elements = driver.FindElements(By.CssSelector("a.vip"));
                var pages = driver.FindElements(By.CssSelector(".gspr.next"));
                while (pages.Count != 0)
                {
                    pages[0].Click();
                    elements = driver.FindElements(By.CssSelector("a.vip"));
                    pages = driver.FindElements(By.CssSelector(".gspr.next"));
                }
            }
        }
    }
}
