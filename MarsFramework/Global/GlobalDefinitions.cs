using ExcelDataReader;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;


namespace MarsFramework.Global
{
    class GlobalDefinitions
    {
        //Initialise the browser
        public static IWebDriver driver { get; set; }


        #region WaitforElement 

        public static void wait(int time)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(time);
        }


        public static IWebElement WaitForElement(IWebDriver driver, By by, int timeOutinSeconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutinSeconds));
            return (wait.Until(ExpectedConditions.ElementIsVisible(by)));
        }


        public static void WaitFor(string xpath, string description = null) 
        {
            // description paramter is optional,
            // it is just for descirbing the element.

            WaitForElement(driver, By.XPath(xpath));
        }


        public static void WaitForPageToLoad() 
        {
            String title = driver.Title;

            if (title == "Profile")
            {

            }            
            else if (title == "ServiceListing") // ShareSkill
            {
                WaitFor("//*[@name='title']", "Title field");
                // Adding time for unexpected time delay
                Thread.Sleep(1000);
            }
            else if (title == "ListingManagement") // Manage Listings
            {
                WaitFor("//*[@id='listing-management-section']/div[2]/div[1]/div[2]", "Pagination at the bottom page");
            }
            else
            { 
                // do nothing
            }
        }

        #endregion


        #region Excel 
        public class ExcelLib
        {
            static List<Datacollection> dataCol = new List<Datacollection>();

            public class Datacollection
            {
                public int rowNumber { get; set; }
                public string colName { get; set; }
                public string colValue { get; set; }
            }

            public static void ClearData()
            {
                dataCol.Clear();
            }

            private static DataTable ExcelToDataTable(string fileName, string sheetName)
            {
                //Register encoding provider to avoid "No data is available for encoding 1252" error
                //"System.Text.Encoding.CodePages" nuget package needs to be installed for this to work
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                // Open file and return as Stream
                using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });
                        //Get all the tables
                        var table = result.Tables;
                        // store it in data table
                        var resultTable = table[sheetName];
                        return resultTable;
                    }
                }
            }

            public static string ReadData(int rowNumber, string columnName)
            {
                try
                {
                    //Retriving Data using LINQ to reduce much of iterations

                    rowNumber = rowNumber - 1;
                    string data = (from colData in dataCol
                                   where colData.colName == columnName && colData.rowNumber == rowNumber
                                   select colData.colValue).SingleOrDefault();

                    //var datas = dataCol.Where(x => x.colName == columnName && x.rowNumber == rowNumber).SingleOrDefault().colValue;
                    return data.ToString();
                }

                catch (Exception e)
                {
                    //Added by Kumar
                    Console.WriteLine("Exception occurred in ExcelLib Class ReadData Method!" + Environment.NewLine + e.Message.ToString());
                    return null;
                }
            }

            public static void PopulateInCollection(string fileName, string SheetName)
            {
                ExcelLib.ClearData();
                DataTable table = ExcelToDataTable(fileName, SheetName);

                //Iterate through the rows and columns of the Table
                for (int row = 1; row <= table.Rows.Count; row++)
                {
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        Datacollection dtTable = new Datacollection()
                        {
                            rowNumber = row,
                            colName = table.Columns[col].ColumnName,
                            colValue = table.Rows[row - 1][col].ToString()
                        };

                        //Add all the details for each row
                        dataCol.Add(dtTable);
                    }
                }
            }
        }

        #endregion


        #region screenshots
        public class SaveScreenShotClass
        {
            public static string SaveScreenshot(IWebDriver driver, string ScreenShotFileName) // Definition
            {
                var folderLocation = (Base.ScreenshotPath);

                if (!System.IO.Directory.Exists(folderLocation))
                {
                    System.IO.Directory.CreateDirectory(folderLocation);
                }

                var screenShot = ((ITakesScreenshot)driver).GetScreenshot();
                var fileName = new StringBuilder(folderLocation);

                fileName.Append(ScreenShotFileName);
                fileName.Append(DateTime.Now.ToString("_yyyy-MM-dd_HHmmss"));
                fileName.Append(".jpeg");
                screenShot.SaveAsFile(fileName.ToString(), ScreenshotImageFormat.Jpeg);
                return fileName.ToString();
            }
        }
		
        #endregion
    }
}