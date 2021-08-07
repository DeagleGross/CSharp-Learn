using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AwaitAnything.Custom
{
    public static class StringComputer
    {
        public static ControlAwaiter GetAwaiter(this Control control)
        {
            return new(control);
        }

        public readonly struct ControlAwaiter : INotifyCompletion
        {
            private readonly Control _mControl;

            public ControlAwaiter(Control control)
            {
                _mControl = control;
            }

            public bool IsCompleted => !_mControl.InvokeRequired;

            public void OnCompleted(Action continuation)
            {
                _mControl.BeginInvoke(continuation);
            }

            public void GetResult() { }
        }
    }

    public class Control
    {
        public bool InvokeRequired => DateTime.UtcNow.Second % 10 == 0;

        public void BeginInvoke(Action actionToInvoke)
        {
            Console.WriteLine("Control has been invoked");

            actionToInvoke.Invoke();
            
            Console.WriteLine("Finished after invoke of control action");
        }
    }
}
