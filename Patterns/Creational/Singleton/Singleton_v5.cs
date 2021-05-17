namespace Singleton.Models
{
    /*
     * This type of instantiation is better than v4 due to
     * instance is really created only on that specific instance access call.
     */

    public sealed class Singleton_v5
    {
        private Singleton_v5() { }

        public static Singleton_v5 Instance => Nested.instance;

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly Singleton_v5 instance = new Singleton_v5();
        }
    }
}
