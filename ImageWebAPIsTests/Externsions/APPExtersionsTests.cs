using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageWebAPIs.Externsions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageWebAPIs.Externsions.Tests
{
    [TestClass()]
    public class APPExtersionsTests
    {
        [TestMethod()]
        public void ToIntTest()
        {
            string text = "123";
           var result= text.ToInt(10);
            Assert.AreEqual(result, 123);

            text = "1w21";
            result = text.ToInt(10);
            Assert.AreEqual(result, 10);

        }
    }
}