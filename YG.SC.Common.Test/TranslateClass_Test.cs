
using System;

namespace YG.SC.Common.NUnitTest
{
    using NUnit.Framework;

    /// <summary>
    /// 类名称：TranslateClass_Test
    /// 命名空间：YG.SC.Common.NUnitTest
    /// 类功能：测试实体类直接的转换
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/11 14:22
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    [TestFixture]
    public class TranslateClass_Test
    {
        [Test]
        public void Test_Normal()
        {
            //var helloWorldEntity = new HelloWorldEntity { Id = 100, UserName = "孟祺宙", Age = 26 };
            //var target = TranslateUtility<HelloWorldEntity, HelloWorldParameter>.Translate(helloWorldEntity);
            //Console.WriteLine(target.ToString());
        }

        [Test]
        public void Test_Phone()
        {
            var phone = "13264232563";
            Console.WriteLine(phone.Substring(5));
        }
    }
}
