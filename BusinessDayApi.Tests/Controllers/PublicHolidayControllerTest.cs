using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessDayApi;
using BusinessDayApi.Controllers;
using BusinessDayApi.Helper;
using BusinessDayApi.Models;
using System.Collections.Generic;

namespace BusinessDayApi.Tests.Controllers
{
    [TestClass]
    public class PublicHolidayControllerTest
    {
        static IPublicHolidayProvider publicHolidayProvider = new PublicHolidayProvider();       

        
        [TestMethod]
        public void Get()
        {
            // Arrange
            PublicHolidayController controller = new PublicHolidayController(publicHolidayProvider);

            // Act
            object result = controller.Get();
            Assert.IsNotNull(result);
            if (result.GetType().GetProperty("PublicHolidays")!=null)
            {
                List<PublicHoliday> publicHolidays = (List<PublicHoliday>)result.GetType().GetProperty("PublicHolidays").GetValue(result);
                Assert.AreEqual(8, publicHolidays.Count);
            }
        }

        [TestMethod]
        public void GetPublicHolidays()
        {
            // Arrange
            // Arrange
            PublicHolidayController controller = new PublicHolidayController(publicHolidayProvider);

            // Act
            string startDate = DateTime.Now.ToString("dd-MM-yyyy");
            string endDate = new DateTime(2019, 12, 31).ToString("dd-MM-yyyy"); 
            object result = controller.GetByDate(startDate, endDate);
            Assert.IsNotNull(result);
            if (result.GetType().GetProperty("PublicHolidays") != null)
            {
                List<PublicHoliday> publicHolidays = (List<PublicHoliday>)result.GetType().GetProperty("PublicHolidays").GetValue(result);
                Assert.AreEqual(2, publicHolidays.Count);
            }
        }

        [TestMethod]
        public void GetPublicHolidaysByYear()
        {
            
            // Arrange
            PublicHolidayController controller = new PublicHolidayController(publicHolidayProvider);

            // Act            
            object result = controller.GetByYear(2019);
            Assert.IsNotNull(result);
            if (result.GetType().GetProperty("PublicHolidays") != null)
            {
                List<PublicHoliday> publicHolidays = (List<PublicHoliday>)result.GetType().GetProperty("PublicHolidays").GetValue(result);
                Assert.AreEqual(8, publicHolidays.Count);
            }
        }
    }
}
