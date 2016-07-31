using System;

namespace Fulbert.Tests.Common.Helpers
{
    public class EventCapture<T> where T : EventArgs
    {
        public int CallCount { get; private set; } = 0;
        public bool IsCalled { get; private set; } = false;

        public T LastCallArguments { get; private set; }

        public void Handler(object sender, T args)
        {
            IsCalled = true;
            CallCount++;
            LastCallArguments = args;
        }
    }
}
