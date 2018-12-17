using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DERS232C;
using System.Globalization;

namespace DERS232C.Tests
{
    [TestClass()]
    public class DEClassTests
    {
        [TestMethod()]
        public void DisplayDateAndTimeTest()
        {
            //Assert.Fail();
        }

        [TestMethod]
        public void GetDateAndTimeInformation_Default_ReturnsDatetimeString()
        {
            //Arrange
            DEClass xPrinter = new DEClass("COM4");

            //Act
            var xdate = xPrinter.GetDateAndTimeInformation();

            //Assert
            DateTime dDate;
            Assert.IsTrue(DateTime.TryParseExact(xdate, "dd-MM-yy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dDate));

        }
    }
}


