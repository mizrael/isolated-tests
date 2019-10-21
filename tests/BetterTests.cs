using System;
using library;
using Xunit;
using NSubstitute;

namespace tests
{
    public class BetterTests
    {
        private Class1 CreateSut(Action<IDependencyA> configureDepA = null, Action<IDependencyB> configureDepB = null){
            var depAMock = Substitute.For<IDependencyA>();
            if(null != configureDepA)
                configureDepA(depAMock);

            var depBMock = Substitute.For<IDependencyB>();
            if(null != configureDepB)
                configureDepB(depBMock);

            var sut = new Class1(depAMock, depBMock);
            return sut;
        }

        [Fact]
        public void BetterTest1()
        {
            var expectedEx = new Exception("exception from dependency A");
            IDependencyA depAMock = null;
            IDependencyB depBMock = null;

            var sut = CreateSut(a => {
                a.WhenForAnyArgs(a => a.Foo()).Throw(expectedEx);
                depAMock = a;
            }, b => depBMock = b);

            var ex = Assert.Throws<Exception>(() => sut.DoSomething());
            Assert.Equal(expectedEx, ex);

            depAMock.Received(1).Foo();
            depBMock.Received(0).Bar();
        }
        
        [Fact]
        public void BetterTest2()
        {
            IDependencyA depAMock = null;
            IDependencyB depBMock = null;
            
            var sut = CreateSut(a => depAMock = a, b => depBMock = b);

            sut.DoSomething();

            depAMock.Received(1).Foo();
            depBMock.Received(1).Bar();
        }
        
    }
}
