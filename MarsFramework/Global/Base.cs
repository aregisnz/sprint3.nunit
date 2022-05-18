using AventStack.ExtentReports;
using MarsFramework.Config;
using MarsFramework.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;
using static MarsFramework.Global.GlobalDefinitions;


namespace MarsFramework.Global
{
    class Base
    {
        #region To access Path from resource file

        public static int Browser = Int32.Parse(MarsResource.Browser);
        public static string ExcelPath = Path.GetFullPath(Directory.GetCurrentDirectory() + MarsResource.ExcelPath);
        public static string ScreenshotPath = Path.GetFullPath(Directory.GetCurrentDirectory() + MarsResource.ScreenshotPath);
        public static string ReportPath = Path.GetFullPath(Directory.GetCurrentDirectory() + MarsResource.ReportPath);
        public static string FileUploadPath = Path.GetFullPath(Directory.GetCurrentDirectory() + MarsResource.FileUploadPath);
               
        #endregion

        #region Report and Tests for ExtentReports  

        public static ExtentReports extent;
        public static ExtentTest test;
        
        #endregion



        #region setup and tear down

        [SetUp]
        public void Inititalize()
        {
            switch (Browser)
            {
                case 1:
                    driver = new FirefoxDriver();
                    break;
                case 2:
                    driver = new ChromeDriver();
                    break;
            }

            // Maximize browser window
            driver.Manage().Window.Maximize();

            
            if (MarsResource.IsLogin == "true")
            {
                SignIn loginobj = new SignIn();
                loginobj.LoginSteps();
            }
            else
            {
                SignUp obj = new SignUp();
                obj.register();
            }
        }


        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.ToString() != "Passed")
            {
                // Take a screenshot          
                string img = SaveScreenShotClass.SaveScreenshot(driver, "Screenshot");
                test.AddScreenCaptureFromPath(img);
            }

            // Quit browser
            driver.Quit();
        }

        #endregion
    }
}