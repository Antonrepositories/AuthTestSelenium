using NUnit.Framework;
using OpenQA.Selenium;

namespace AuthTestSelenium
{
	public class Tests
	{
		private IWebDriver _driver;
		private readonly By emailform = By.XPath("/html/body/div/main/div/form[1]/div[1]/input");
		private readonly By passform = By.XPath("/html/body/div/main/div/form[1]/div[2]/input");
		private readonly By loginbutton = By.XPath("/html/body/div/main/div/form[1]/div[4]/input");
		private readonly By errorpage = By.XPath("/html/body/div/main/div/h2");
		private readonly By logoutbutton = By.XPath("/html/body/header/nav/div/div/ul/li[4]/form/button");
		private readonly By statbutton = By.XPath("/html/body/header/nav/div/a");
		private readonly By statbar = By.XPath("/html/body/div[1]/main/h3");
		private readonly By clubsbutton = By.XPath("/html/body/header/nav/div/div/ul/li[3]/a");
		private readonly By createnewbutton = By.XPath("/html/body/div/main/p/a");
		private readonly By editbutton = By.XPath("/html/body/div/main/table/tbody/tr[1]/td[5]/a[1]");
		private readonly By deletebutton = By.XPath("/html/body/div/main/table/tbody/tr[1]/td[5]/a[3]");
		private readonly By detailsbutton = By.XPath("/html/body/div/main/table/tbody/tr[1]/td[5]/a[2]");
		private readonly By rolesbutton = By.XPath("/html/body/header/nav/div/div/ul/li[5]/a");
		private readonly By userlistbutton = By.XPath("/html/body/div/main/a");
		private readonly By googlebutton = By.XPath("/html/body/div/main/div/form[2]/div/button");
		private readonly By mvcdetails = By.XPath("/html/body/div[1]/div[1]/div[2]/div/c-wiz/div/div[1]/div/div/span/button");
		[SetUp]
		public void Setup()
		{
			_driver = new OpenQA.Selenium.Edge.EdgeDriver();
			_driver.Navigate().GoToUrl("https://localhost:7054/Account/Login");
		}

		[Test]
		public void TestOnIncorrectData()
		{
			var inputemail = _driver.FindElement(emailform);
			inputemail.Click();
			inputemail.SendKeys("admin@gmail.com");
			var inputpassword = _driver.FindElement(passform);
			inputpassword.Click();
			inputpassword.SendKeys("incorrectpassword");
			var login = _driver.FindElement(loginbutton);
			//_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
			login.Click();
			Thread.Sleep(2000);
			var correctHandling = _driver.FindElement(errorpage).Text;
			Assert.AreEqual(correctHandling, "General errors", "Not redirrected on error page!");
			_driver.Quit();

		}
		[Test]
		public void TestOnCorrectData()
		{
			//_driver.Navigate().GoToUrl("https://localhost:7054/Account/Login");
			var inputemail = _driver.FindElement(emailform);
			inputemail.Click();
			inputemail.SendKeys("admin@gmail.com");
			var inputpassword = _driver.FindElement(passform);
			inputpassword.Click();
			inputpassword.SendKeys("P@ss1234");
			var login = _driver.FindElement(loginbutton);
			login.Click();
			Thread.Sleep(2000);
			var logout = _driver.FindElement(logoutbutton);
			Assert.AreEqual(logout.Text, "Logout", "User is not signed in properly!");
			logout.Click();
			_driver.Quit();

		}
		[Test]
		public void TestAuthorizationAdmin()
		{
			var inputemail = _driver.FindElement(emailform);
			inputemail.Click();
			inputemail.SendKeys("admin@gmail.com");
			var inputpassword = _driver.FindElement(passform);
			inputpassword.Click();
			inputpassword.SendKeys("P@ss1234");
			var login = _driver.FindElement(loginbutton);
			login.Click();
			Thread.Sleep(2000);
			var stats = _driver.FindElement(statbutton);
			stats.Click();
			Thread.Sleep(1000);
			var statsb = _driver.FindElement(statbar);
			Assert.AreEqual(statsb.Text, "Clubs statistics", "Error on stats page");
			//Roles
			var roles = _driver.FindElement(rolesbutton);
			roles.Click();
			Thread.Sleep(3000);
			var userl = _driver.FindElement(userlistbutton);
			Assert.AreEqual(userl.Text, "User List", "No user list!");
			//goto clubs
			var clubs = _driver.FindElement(clubsbutton);
			clubs.Click();
			Thread.Sleep(2000);
			//crud buttons
			var create = _driver.FindElement(createnewbutton);
			Assert.AreEqual(create.Text, "Create New", "No create button!");
			var delete = _driver.FindElement(deletebutton);
			Assert.AreEqual(delete.Text, "Delete", "No delete button!");
			var details = _driver.FindElement(detailsbutton);
			Assert.AreEqual(details.Text, "Details", "No details button!");
			var edit = _driver.FindElement(editbutton);
			Assert.AreEqual(edit.Text, "Edit", "No edit button!");
			var logout = _driver.FindElement(logoutbutton);
			logout.Click();
			_driver.Quit();
		}
		[Test]
		public void TestOauth()
		{
			var oauth = _driver.FindElement(googlebutton);
			oauth.Click();
			Thread.Sleep(3000);
			var mvc = _driver.FindElement(mvcdetails).Text;
			Assert.AreEqual(mvc, "LabMVC", "No edit button!");
			//logout.Click();
			_driver.Quit();
		}
		[TearDown]
		public void TearDown()
		{
			//_driver.Quit();
		}
	}
}