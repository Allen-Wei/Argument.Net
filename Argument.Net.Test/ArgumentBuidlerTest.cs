using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Argument.Net.Test
{
    [TestClass]
    public class ArgumentBuidlerTest
    {
        public class OptionTest
        {
            [Argument(Alias = "app-name")]
            public String Name { get; set; }
            [Argument]
            public bool Recursive { get; set; }
            [Argument]
            public bool ShowError { get; set; }
        }
        [TestMethod]
        public void BoolArgOnTailTest()
        {
            OptionTest instance = ArgumentBuilder.Build<OptionTest>(new[] { "--app-name", "helloworld", "--recursive" }).GetArgs();

            Assert.AreEqual(instance.Name, "helloworld");
            Assert.AreEqual(instance.Recursive, true);
            Assert.AreEqual(instance.ShowError, false);
        }
        [TestMethod]
        public void BoolArgOnHeadTest()
        {
            OptionTest instance = ArgumentBuilder.Build<OptionTest>(new[] { "--recursive", "--showerror", "--app-name", "helloworld" }).GetArgs();

            Assert.AreEqual(instance.Name, "helloworld");
            Assert.AreEqual(instance.Recursive, true);
            Assert.AreEqual(instance.ShowError, true);

        }
    }
}
