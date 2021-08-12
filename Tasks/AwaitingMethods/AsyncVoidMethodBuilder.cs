using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// https://referencesource.microsoft.com/#mscorlib/system/runtime/compilerservices/AsyncMethodBuilder.cs,c983aa3f7c40052f,references
    /// </summary>
    public class AsyncVoidMethodBuilder
    {
        public AsyncVoidMethodBuilder()
        {
        }

        public static AsyncVoidMethodBuilder Create() => new();

        public void SetResult() => Console.WriteLine("SetResult");

        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            Console.WriteLine("Start");
            stateMachine.MoveNext();
        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
        }

        [SecuritySafeCritical]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
        }

        public void SetException(Exception exception)
        {
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }
    }
}
