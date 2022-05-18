using MarsFramework.Global;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using static MarsFramework.Global.GlobalDefinitions;
using NUnit.Framework;
using AutoItX3Lib;


namespace MarsFramework.Pages
{
    public class ShareSkill 
    {
        public ShareSkill()
        {
            PageFactory.InitElements(driver, this);
        }

        #region Initialize Web Elements (Page Factory style)

        //Click on ShareSkill Button
        [FindsBy(How = How.LinkText, Using = "Share Skill")]
        private IWebElement ShareSkillButton { get; set; }

        //Enter the Title in textbox
        [FindsBy(How = How.Name, Using = "title")]
        private IWebElement Title { get; set; }

        //Enter the Description in textbox
        [FindsBy(How = How.Name, Using = "description")]
        private IWebElement Description { get; set; }

        //Click on Category Dropdown
        [FindsBy(How = How.Name, Using = "categoryId")]
        private IWebElement CategoryDropDown { get; set; }

        //Click on SubCategory Dropdown
        [FindsBy(How = How.Name, Using = "subcategoryId")]
        private IWebElement SubCategoryDropDown { get; set; }

        //Enter Tag names in textbox
        [FindsBy(How = How.XPath, Using = "//body/div/div/div[@id='service-listing-section']/div[contains(@class,'ui container')]/div[contains(@class,'listing')]/form[contains(@class,'ui form')]/div[contains(@class,'tooltip-target ui grid')]/div[contains(@class,'twelve wide column')]/div[contains(@class,'')]/div[contains(@class,'ReactTags__tags')]/div[contains(@class,'ReactTags__selected')]/div[contains(@class,'ReactTags__tagInput')]/input[1]")]
        private IWebElement Tags { get; set; }

        //Select the Service type: Hourly basis
        [FindsBy(How = How.XPath, Using = "//*[@id='service-listing-section']/div[2]/div/form/div[5]/div[2]/div[1]/div[1]/div")]
        private IWebElement ServiceTypeHourlyBasis { get; set; }

        //Select the Service type: One-off
        [FindsBy(How = How.XPath, Using = "//*[@id='service-listing-section']/div[2]/div/form/div[5]/div[2]/div[1]/div[2]/div/input")]
        private IWebElement ServiceTypeOneOff { get; set; }

        //Select the Location Type
        [FindsBy(How = How.XPath, Using = "//form/div[6]/div[@class='twelve wide column']/div/div[@class = 'field']")]
        private IWebElement LocationTypeOption { get; set; }

        //Click on Start Date dropdown
        [FindsBy(How = How.Name, Using = "startDate")]
        private IWebElement StartDateDropDown { get; set; }

        //Click on End Date dropdown
        [FindsBy(How = How.Name, Using = "endDate")]
        private IWebElement EndDateDropDown { get; set; }

        //Storing the table of available days
        [FindsBy(How = How.XPath, Using = "//*[@id='service-listing-section']/div[2]/div/form/div[7]/div[2]/div/div[3]/div[1]/div/input")]
        private IWebElement Days { get; set; }

        //Click on StartTime dropdown
        [FindsBy(How = How.XPath, Using = "//div[3]/div[2]/input[1]")]
        private IWebElement StartTimeDropDown { get; set; }

        //Click on EndTime dropdown
        [FindsBy(How = How.XPath, Using = "//div[3]/div[3]/input[1]")]
        private IWebElement EndTimeDropDown { get; set; }

        //Click on Skill Trade option
        [FindsBy(How = How.XPath, Using = "//form/div[8]/div[@class='twelve wide column']/div/div[@class = 'field']")]
        private IWebElement SkillTradeOption { get; set; }

        //Enter Skill Exchange
        [FindsBy(How = How.XPath, Using = "//div[@class='form-wrapper']//input[@placeholder='Add new tag']")]
        private IWebElement SkillExchange { get; set; }

        //Click on Work Sample
        [FindsBy(How = How.XPath, Using = "//*[@id='service-listing-section']/div[2]/div/form/div[9]/div/div[2]/section/div/label/div/span/i")]
        private IWebElement WorkSample { get; set; }

        //Enter the amount for Credit
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Amount']")]
        private IWebElement CreditAmount { get; set; }

        //Click on Active/Hidden option
        [FindsBy(How = How.XPath, Using = "//form/div[10]/div[@class='twelve wide column']/div/div[@class = 'field']")]
        private IWebElement ActiveOption { get; set; }

        //Click on Save button
        [FindsBy(How = How.XPath, Using = "//input[@value='Save']")]
        private IWebElement Save { get; set; }

        //Click on Manage Listings Link
        [FindsBy(How = How.LinkText, Using = "Manage Listings")]
        private IWebElement manageListingsLink { get; set; }

        //Storing the 'Title is required' Notification
        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Title is required')]")]
        private IWebElement titleIsRequiredNotification { get; set; }

        //Storing the 'Subcategory is required' Notification
        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Subcategory is required')]")]
        private IWebElement subcategoryIsRequiredNotification { get; set; }

        //Storing the 'Invalid file type' Notification
        [FindsBy(How = How.XPath, Using = "//div[contains(text(),'Max file size is 2 MB and supported file types are')]")]
        private IWebElement invalidFileTypeNotification { get; set; }

        #endregion


        public void EnterShareSkill(int testCase)
        {
            // Referencing to an excel file and sheet name
            ExcelLib.PopulateInCollection(Base.ExcelPath, "ShareSkill");
            testCase = testCase + 1;

            try
            {
                ShareSkillButton.Click();
                WaitForPageToLoad();
                Title.SendKeys(ExcelLib.ReadData(testCase, "Title"));
                Description.SendKeys(ExcelLib.ReadData(testCase, "Description"));
                CategoryDropDown.SendKeys(ExcelLib.ReadData(testCase, "Category"));
                SubCategoryDropDown.SendKeys(ExcelLib.ReadData(testCase, "Subcategory"));
                Tags.SendKeys(ExcelLib.ReadData(testCase, "Tags"));
                Tags.SendKeys(Keys.Enter);
                ServiceTypeOption(ExcelLib.ReadData(testCase, "Service Type"));                
                LocationTypeOption.Click();
                StartDateDropDown.SendKeys(ExcelLib.ReadData(testCase, "Start date"));
                EndDateDropDown.SendKeys(ExcelLib.ReadData(testCase, "End date"));
                Days.Click();
                PopulateTimeInfo("start", ExcelLib.ReadData(testCase, "Start time"));
                PopulateTimeInfo("end", ExcelLib.ReadData(testCase, "End time"));
                SkillTradeOption.Click();
                SkillExchange.SendKeys(ExcelLib.ReadData(testCase, "Skill-Exchange"));
                SkillExchange.SendKeys(Keys.Enter);
                WorkSample.Click();
                Thread.Sleep(1000);
                
                AutoItX3 autoIt = new AutoItX3();
                string workSampleFile = (ExcelLib.ReadData(testCase, "Work Samples"));    
                autoIt.WinActivate("Open");
                Thread.Sleep(1000);
                autoIt.Send(Base.FileUploadPath + workSampleFile);
                Thread.Sleep(2000);
                autoIt.Send("{ENTER}");
                Thread.Sleep(1000);

                ActiveOption.Click();
                Thread.Sleep(5000);
                Save.Click();
                Thread.Sleep(500);
            }
            catch (Exception e) 
            {
                Assert.Fail(e.Message);
            }
        }


        public void EditShareSkill()
        {
            // Referencing to an excel file and sheet name
            ExcelLib.PopulateInCollection(Base.ExcelPath, "SSkillEdit");

            try
            {             
                manageListingsLink.Click();
                WaitForPageToLoad();
                ManageListings ManageListngObj = new ManageListings();
                ManageListngObj.edit.Click();
                WaitForPageToLoad();
                Title.Clear();
                Title.SendKeys(ExcelLib.ReadData(2, "Title"));
                wait(1);
                Description.Clear();
                Description.SendKeys(ExcelLib.ReadData(2, "Description"));
                wait(1);
                CategoryDropDown.SendKeys(ExcelLib.ReadData(2, "Category"));
                wait(1);
                Save.Click();
                WaitForPageToLoad();
            }
            catch (Exception e) 
            {
                Assert.Fail(e.Message);                
            }
        }

        public void EnterShareSkill_TitleIsRequired(int testCase)
        {
            // Referencing to an excel file and sheet name
            ExcelLib.PopulateInCollection(Base.ExcelPath, "ShareSkill");
            testCase = testCase + 1;

            try
            {
                ShareSkillButton.Click();
                WaitForPageToLoad();
                // Title.SendKeys(ExcelLib.ReadData(testCase, "Title"));
                Description.SendKeys(ExcelLib.ReadData(testCase, "Description"));
                CategoryDropDown.SendKeys(ExcelLib.ReadData(testCase, "Category"));
                SubCategoryDropDown.SendKeys(ExcelLib.ReadData(testCase, "Subcategory"));
                Tags.SendKeys(ExcelLib.ReadData(testCase, "Tags"));
                Tags.SendKeys(Keys.Enter);
                ServiceTypeOption(ExcelLib.ReadData(testCase, "Service Type"));
                LocationTypeOption.Click();
                StartDateDropDown.SendKeys(ExcelLib.ReadData(testCase, "Start date"));
                EndDateDropDown.SendKeys(ExcelLib.ReadData(testCase, "End date"));
                Days.Click();
                PopulateTimeInfo("start", ExcelLib.ReadData(testCase, "Start time"));
                PopulateTimeInfo("end", ExcelLib.ReadData(testCase, "End time"));
                SkillTradeOption.Click();
                SkillExchange.SendKeys(ExcelLib.ReadData(testCase, "Skill-Exchange"));
                SkillExchange.SendKeys(Keys.Enter);
                WorkSample.Click();
                Thread.Sleep(1000);

                AutoItX3 autoIt = new AutoItX3();
                string workSampleFile = (ExcelLib.ReadData(testCase, "Work Samples"));
                autoIt.WinActivate("Open");
                Thread.Sleep(1000);
                autoIt.Send(Base.FileUploadPath + workSampleFile);
                Thread.Sleep(2000);
                autoIt.Send("{ENTER}");
                Thread.Sleep(1000);

                ActiveOption.Click();
                Thread.Sleep(5000);
                Save.Click();
                Thread.Sleep(500);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        public void EnterShareSkill_SubcategoryIsRequired(int testCase)
        {
            // Referencing to an excel file and sheet name
            ExcelLib.PopulateInCollection(Base.ExcelPath, "ShareSkill");
            testCase = testCase + 1;

            try
            {
                ShareSkillButton.Click();
                WaitForPageToLoad();
                Title.SendKeys(ExcelLib.ReadData(testCase, "Title"));
                Description.SendKeys(ExcelLib.ReadData(testCase, "Description"));
                CategoryDropDown.SendKeys(ExcelLib.ReadData(testCase, "Category"));
                // SubCategoryDropDown.SendKeys(ExcelLib.ReadData(testCase, "Subcategory"));
                Tags.SendKeys(ExcelLib.ReadData(testCase, "Tags"));
                Tags.SendKeys(Keys.Enter);
                ServiceTypeOption(ExcelLib.ReadData(testCase, "Service Type"));
                LocationTypeOption.Click();
                StartDateDropDown.SendKeys(ExcelLib.ReadData(testCase, "Start date"));
                EndDateDropDown.SendKeys(ExcelLib.ReadData(testCase, "End date"));
                Days.Click();
                PopulateTimeInfo("start", ExcelLib.ReadData(testCase, "Start time"));
                PopulateTimeInfo("end", ExcelLib.ReadData(testCase, "End time"));
                SkillTradeOption.Click();
                SkillExchange.SendKeys(ExcelLib.ReadData(testCase, "Skill-Exchange"));
                SkillExchange.SendKeys(Keys.Enter);
                WorkSample.Click();
                Thread.Sleep(1000);

                AutoItX3 autoIt = new AutoItX3();
                string workSampleFile = (ExcelLib.ReadData(testCase, "Work Samples"));
                autoIt.WinActivate("Open");
                Thread.Sleep(1000);
                autoIt.Send(Base.FileUploadPath + workSampleFile);
                Thread.Sleep(2000);
                autoIt.Send("{ENTER}");
                Thread.Sleep(1000);

                ActiveOption.Click();
                Thread.Sleep(5000);
                Save.Click();
                Thread.Sleep(500);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        public void EnterShareSkill_FileuploadInvalidFileType(int testCase)
        {
            // Referencing to an excel file and sheet name
            ExcelLib.PopulateInCollection(Base.ExcelPath, "ShareSkill");
            testCase = testCase + 1;

            try
            {
                ShareSkillButton.Click();
                WaitForPageToLoad();
                Title.SendKeys(ExcelLib.ReadData(testCase, "Title"));
                Description.SendKeys(ExcelLib.ReadData(testCase, "Description"));
                CategoryDropDown.SendKeys(ExcelLib.ReadData(testCase, "Category"));
                SubCategoryDropDown.SendKeys(ExcelLib.ReadData(testCase, "Subcategory"));
                Tags.SendKeys(ExcelLib.ReadData(testCase, "Tags"));
                Tags.SendKeys(Keys.Enter);
                ServiceTypeOption(ExcelLib.ReadData(testCase, "Service Type"));
                LocationTypeOption.Click();
                StartDateDropDown.SendKeys(ExcelLib.ReadData(testCase, "Start date"));
                EndDateDropDown.SendKeys(ExcelLib.ReadData(testCase, "End date"));
                Days.Click();
                PopulateTimeInfo("start", ExcelLib.ReadData(testCase, "Start time"));
                PopulateTimeInfo("end", ExcelLib.ReadData(testCase, "End time"));
                SkillTradeOption.Click();
                SkillExchange.SendKeys(ExcelLib.ReadData(testCase, "Skill-Exchange"));
                SkillExchange.SendKeys(Keys.Enter);
                WorkSample.Click();
                Thread.Sleep(1000);

                AutoItX3 autoIt = new AutoItX3();
                string workSampleFile = (ExcelLib.ReadData(testCase, "Work Samples"));
                autoIt.WinActivate("Open");
                Thread.Sleep(1000);
                autoIt.Send(Base.FileUploadPath + workSampleFile);
                Thread.Sleep(2000);
                autoIt.Send("{ENTER}");
                Thread.Sleep(1000);

                ActiveOption.Click();
                Thread.Sleep(3000);
                Save.Click();
                // Thread.Sleep(500);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }



        public void ServiceTypeOption(string type)
        {
            if (type == "Hourly basis service")
            {
                ServiceTypeHourlyBasis.Click();
            }
            else if (type == "One-off service") 
            {
                ServiceTypeOneOff.Click();
            }
        }

        public void PopulateTimeInfo(string whichTime, string timeToExtract) 
        {
            // breaking string apart to extract date
            string[] _eTime = timeToExtract.Split(' ');

            // breaking date to hour and minute chunks
            string[] eTime = _eTime[1].Split(':');

            if (whichTime == "start")
            {
                // hours
                StartTimeDropDown.SendKeys(eTime[0]);

                // minutes
                StartTimeDropDown.SendKeys(eTime[1]);

                // am/pm
                StartTimeDropDown.SendKeys(timeToExtract.Substring(eTime.Length - 2));
            }
            else if (whichTime == "end") 
            {
                // hours
                EndTimeDropDown.SendKeys(eTime[0]);

                // minutes
                EndTimeDropDown.SendKeys(eTime[1]);

                // am/pm
                EndTimeDropDown.SendKeys(timeToExtract.Substring(eTime.Length - 2));
            }
        }


        public string GetTitleIsRequiredNotification()
        {
            return titleIsRequiredNotification.Text;
        }

        public string GetSubcategoryIsRequiredNotification()
        {
            return subcategoryIsRequiredNotification.Text;
        }

        public string GetInvalidFileTypeNotification()
        {
            return invalidFileTypeNotification.Text;
        }
    }
}