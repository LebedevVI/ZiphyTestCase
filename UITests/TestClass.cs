using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using Microsoft.UI.Xaml.Controls;
using TestCase;
using System;
using System.Threading;

namespace UITests
{
    [TestClass]
    public class TestClass
    {
        TestCase.App testedApp = new TestCase.App();
        TestCase.MainWindow testedMainWindow = new TestCase.MainWindow();
        [UITestMethod]
        public void TestVisibility()
        {
            Assert.IsTrue(testedMainWindow.Visible);
        }
        [UITestMethod]
        public void TestMutexCheck()
        {
            Assert.IsTrue(TestCase.App.MutexCheck());
        }
        [UITestMethod]
        public void TestMakeUriCorrect()
        {
            string source = "http://yandex.ru";
            Uri param;
            var result = testedMainWindow.MakeUri(source, out param);
            Assert.IsTrue(result);
            Assert.AreEqual(param, new Uri(source));
        }
        [UITestMethod]
        public void TestMakeUriWithoutPrefix()
        {
            string source = "yandex.ru";
            Uri param;
            var result = testedMainWindow.MakeUri(source, out param);
            Assert.IsTrue(result);
            Assert.AreEqual(param, new Uri("https://" + source));
        }
        [UITestMethod]
        public void TestMakeUriinvalidated()
        {
            string source = "";
            Uri param;
            var result = testedMainWindow.MakeUri(source, out param);
            Assert.IsFalse(result);
            Assert.IsNull(param);
        }
    }
}