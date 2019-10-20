using System;

namespace library
{
    
    public interface IDependencyA{
        void Foo();
    }

    public interface IDependencyB{
        void Bar();
    }

    public class Class1
    {
        private readonly IDependencyA _depA;
        private readonly IDependencyB _depB;

        public Class1(IDependencyA depA, IDependencyB depB){
            _depA = depA;
            _depB = depB;
        }

        public void DoSomething(){
            //System.Threading.Thread.Sleep(500);
            _depA.Foo();
            //System.Threading.Thread.Sleep(500);
            _depB.Bar();
        }
    }

}
