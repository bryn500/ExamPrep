using System;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            var classWithEvent = new TestClass();
            var classWithEvent2 = new TestClass();
            var listener1 = new MyListener(classWithEvent, classWithEvent2);
            var listener2 = new MyListener(classWithEvent, classWithEvent2);


            classWithEvent.Test();

            // issues
            classWithEvent.Notify();
            classWithEvent.Notify = null;
        }
    }

    public class TestClass
    {
        public Action Notify;
        
        // an event is a delgate reference with two restrictions
        // 1 cannot invoke directly
        // 2 cannot assign directly

        public void Test()
        {
            Notify();
        }
    }

    public class MyListener
    {
        public MyListener(TestClass myevent, TestClass myevent2)
        {
            myevent.Notify += LogTheEvent;
            myevent2.Notify += DoSOmethingElse;
        }

        void LogTheEvent()
        {
            Console.WriteLine("test");
        }

        void DoSOmethingElse()
        {

        }
    }
}
