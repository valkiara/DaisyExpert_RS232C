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
            DaisyExpert xPrinter = new DaisyExpert("COM3");

            //Act
            var xdate = xPrinter.Information.GetDateAndTimeInformation();

            //Assert
            DateTime dDate;
            Assert.IsTrue(DateTime.TryParseExact(xdate.Result, "dd-MM-yy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dDate));

        }

        [TestMethod]
        public void DiagnosticInformation_ReturnedStringStartsWithExpert()
        {
            //Arrange
            DaisyExpert xPrinter = new DaisyExpert("COM3");

            //Act
            var info = xPrinter.Information.DiagnosticInformation();

            //Assert
            Assert.IsTrue(info.Result.StartsWith("eXpert"));
        }

    }
}


