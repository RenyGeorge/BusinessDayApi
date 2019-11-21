using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessDayApi;
using BusinessDayApi.Controllers;
using BusinessDayApi.Helper;

namespace BusinessDayApi.Tests.Controllers
{
  
    [TestClass]
    public class WeekdayControllerTest
    {
        static IPublicHolidayProvider publicHolidayProvider = new PublicHolidayProvider();
        static IWeekdayProvider weekDayProvider = new WeekdayProvider(publicHolidayProvider);

        public bool HasProperty(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }

        [TestMethod]
        public void Get()
        {
            // Arrange
            
            WeekDayController controller = new WeekDayController(weekDayProvider);

            // Act
            object result = controller.Get();
            Assert.IsNotNull(result);
            if (HasProperty(result, "Total"))
            {
                object total = result.GetType().GetProperty("Total").GetValue(result);
                Assert.AreEqual(254, total);                
            }
        }

        [TestMethod]
        public void GetBusinessDays()
        {
            // Arrange
            // Arrange
            WeekDayController controller = new WeekDayController(weekDayProvider);

            // Act
            string startDate = new DateTime(2019,11,20).ToString("dd-MM-yyyy"); 
            string endDate = new DateTime(2019,12,31).ToString("dd-MM-yyyy"); 
            object result = controller.Get(startDate,endDate);
            Assert.IsNotNull(result);
            if (HasProperty(result, "Total"))
            {
                object total = result.GetType().GetProperty("Total").GetValue(result);
                Assert.AreEqual(26, total);
            }
        }

    }
}
