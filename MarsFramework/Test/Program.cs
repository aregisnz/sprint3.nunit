using NUnit.Framework;
using MarsFramework.Global;
using MarsFramework.Pages;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using System;
using static MarsFramework.Global.GlobalDefinitions;
using System.IO;

namespace MarsFramework
{
    [TestFixture]
    class Program : Base
    {        
        [OneTimeSetUp]
        public void StartExtentReports()
        {
            // Initialize ExtentReports
            var htmlReporter = new ExtentHtmlReporter(ReportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }


        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void T01_EnterShareSkill(int testCase)
        {
            // Create Extentreport test, name extracted from current method name
            test = extent.CreateTest(System.Reflection.MethodBase.GetCurrentMethod().Name + "_" + testCase.ToString());            

            try
            {
                // Action
                ShareSkill ShareSkillObj = new ShareSkill();
                ManageListings ManageListingsObj = new ManageListings();
                ShareSkillObj.EnterShareSkill(testCase);

                // Assertion
                string resultStatusNotification = ManageListingsObj.GetNotification();
                string expectedStatusNotification = "Service Listing Added successfully";
                Assert.That(resultStatusNotification, Is.EqualTo(expectedStatusNotification));                
                // Log status in Extentreports
                test.Log(Status.Pass, "Passed, action successfull.");
                
            }
            catch (Exception ex)
            {
                // Log status in Extentreports
                test.Log(Status.Fail, "Failed, action unsuccessfull.");
                test.Log(Status.Info, ex.Message);
            }
        }


        [Test]
        public void T02_EditShareSkill()
        {
            // Create Extentreport test, name extracted from current method name
            test = extent.CreateTest(System.Reflection.MethodBase.GetCurrentMethod().Name);

            try
            {
                // Action
                ShareSkill ShareSkillObj = new ShareSkill();
                ManageListings ManageListingsObj = new ManageListings();
                ShareSkillObj.EditShareSkill();

                // Assertion
                string enteredCategory = ManageListingsObj.GetCategory();
                string enteredTitle = ManageListingsObj.GetTitle();
                string enteredDescription = ManageListingsObj.GetDescription();
                string expectedCategory = ExcelLib.ReadData(2, "Category");
                string expectedTitle = ExcelLib.ReadData(2, "Title");
                string expectedDescription = ExcelLib.ReadData(2, "Description").Substring(0, 30);
                Assert.That(enteredCategory, Is.EqualTo(expectedCategory));
                Assert.That(enteredTitle, Is.EqualTo(expectedTitle));
                Assert.That(enteredDescription.Contains(expectedDescription));

                // Log status in Extentreports
                test.Log(Status.Pass, "Passed, action successfull");
            }
            catch (Exception ex)
            {
                // Log status in Extentreports
                test.Log(Status.Fail, "Failed, action unsuccessfull");
                test.Log(Status.Info, ex.Message);
            }
        }


        [Test]
        public void T03_DeleteShareSkill()
        {
            // Create Extentreport test, name extracted from current method name
            test = extent.CreateTest(System.Reflection.MethodBase.GetCurrentMethod().Name);

            try
            {
                // Action                
                ManageListings ManageListingsObj = new ManageListings();
                ManageListingsObj.DeleteShareSkill();

                // Assertion              
                string resultStatusNotification = ManageListingsObj.GetNotification();
                string expectedStatusNotification = "has been deleted";
                Assert.That(resultStatusNotification.Contains(expectedStatusNotification));                

                // Log status in Extentreports
                test.Log(Status.Pass, "Passed, action successfull");
            }
            catch (Exception ex)
            {
                // Log status in Extentreports
                test.Log(Status.Fail, "Failed, action unsuccessfull");
                test.Log(Status.Info, ex.Message);
            }
        }


        [Test]
        public void T04_NegativeTest_EnterShareSkill_TitleIsRequired()
        {
            // Create Extentreport test, name extracted from current method name
            test = extent.CreateTest(System.Reflection.MethodBase.GetCurrentMethod().Name);
            int excelTestData = 4; 

            try
            {
                // Action
                ShareSkill ShareSkillObj = new ShareSkill();
                ShareSkillObj.EnterShareSkill_TitleIsRequired(excelTestData);

                // Assertion
                string resultStatusNotification = ShareSkillObj.GetTitleIsRequiredNotification();
                string expectedStatusNotification = "Title is required";
                Assert.That(resultStatusNotification, Is.EqualTo(expectedStatusNotification));

                // Log status in Extentreports
                test.Log(Status.Pass, "Passed, action successfull.");
            }
            catch (Exception ex)
            {
                // Log status in Extentreports
                test.Log(Status.Fail, "Failed, action unsuccessfull.");
                test.Log(Status.Info, ex.Message);
            }
        }


        [Test]
        public void T05_NegativeTest_EnterShareSkill_SubcategoryIsRequired()
        {
            // Create Extentreport test, name extracted from current method name
            test = extent.CreateTest(System.Reflection.MethodBase.GetCurrentMethod().Name);
            int excelTestData = 5;

            try
            {
                // Action
                ShareSkill ShareSkillObj = new ShareSkill();                
                ShareSkillObj.EnterShareSkill_SubcategoryIsRequired(excelTestData);

                // Assertion
                string resultStatusNotification = ShareSkillObj.GetSubcategoryIsRequiredNotification();
                string expectedStatusNotification = "Subcategory is required";
                Assert.That(resultStatusNotification, Is.EqualTo(expectedStatusNotification));

                // Log status in Extentreports
                test.Log(Status.Pass, "Passed, action successfull.");
            }
            catch (Exception ex)
            {
                // Log status in Extentreports
                test.Log(Status.Fail, "Failed, action unsuccessfull.");
                test.Log(Status.Info, ex.Message);
            }
        }


        [Test]
        public void T06_NegativeTest_EnterShareSkill_FileuploadInvalidFileType()
        {
            // Create Extentreport test, name extracted from current method name
            test = extent.CreateTest(System.Reflection.MethodBase.GetCurrentMethod().Name);
            int excelTestData = 6;

            try
            {
                // Action
                ShareSkill ShareSkillObj = new ShareSkill();                
                ShareSkillObj.EnterShareSkill_FileuploadInvalidFileType(excelTestData);

                // Assertion
                string resultStatusNotification = ShareSkillObj.GetInvalidFileTypeNotification();                
                string expectedStatusNotification = "Max file size is 2 MB and supported file types are gif / jpeg / png / jpg / doc(x) / pdf / txt / xls(x)";
                Assert.That(resultStatusNotification, Is.EqualTo(expectedStatusNotification));

                // Log status in Extentreports
                test.Log(Status.Pass, "Passed, action successfull.");
            }
            catch (Exception ex)
            {
                // Log status in Extentreports
                test.Log(Status.Fail, "Failed, action unsuccessfull.");
                test.Log(Status.Info, ex.Message);
            }
        }



        [OneTimeTearDown]
        public void SaveExtentReports()
        {
            // Save Extentereport html file
            extent.Flush();
        }
    }
}