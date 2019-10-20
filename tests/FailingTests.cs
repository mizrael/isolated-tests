using System;
using library;
using Xunit;
using NSubstitute;

namespace tests
{
    public class FailingTests
    {
        private static readonly Class1 _sut;
        private static readonly IDependencyA _depAMock;
        private static readonly IDependencyB _depBMock;

        static FailingTests(){
            Console.WriteLine("in the cTor...");

            _depAMock = Substitute.For<IDependencyA>();
            _depBMock = Substitute.For<IDependencyB>();
            _sut = new Class1(_depAMock, _depBMock);
        }

        [Fact]
        public void Test1()
        {
            var expectedEx = new Exception("exception from dependency A");
            _depAMock.WhenForAnyArgs(a => a.Foo()).Throw(expectedEx);

            var ex = Assert.Throws<Exception>(() => _sut.DoSomething());
            Assert.Equal(expectedEx, ex);

            _depAMock.Received(1).Foo();
            _depBMock.Received(0).Bar();
        }
        
        [Fact]
        public void Test2()
        {
            _sut.DoSomething();

            _depAMock.Received(1).Foo();
            _depBMock.Received(1).Bar();
        }
        
    }
}
