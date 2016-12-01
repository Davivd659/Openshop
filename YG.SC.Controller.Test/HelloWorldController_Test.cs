using System;
using System.Linq;
using Moq;
using YG.SC.Service;


namespace YG.SC.Controller.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class HelloWorldController_Test
    {
        private Mock<IHelloWorldService> _helloWorldServiceMock;
        //private HelloWorldController _helloWorldController;

        [SetUp]
        public void SetUp()
        {
            _helloWorldServiceMock = new Mock<IHelloWorldService>();
            _helloWorldServiceMock.Setup(p => p.Hello(1)).Returns("测试控制器");
            //_helloWorldController = new HelloWorldController(_helloWorldServiceMock.Object);
        }

        [Test]
        public void Index_Test_01()
        {
            //var result = _helloWorldController.Index(1);

            //Assert.IsNotNull(_helloWorldController.ViewBag.HelloWorld);
            //Console.WriteLine(_helloWorldController.ViewBag.HelloWorld);
        }

        [Test]
        public void Test_1()
        {
            var source = new int[] { 1, 2, 3 };
            var data = new int[] { 1, 8 };

            var arr = data.Except(source);
            foreach (var i in arr)
            {
                Console.WriteLine(i);
            }
        }
    }
}
