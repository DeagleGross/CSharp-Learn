using System;

namespace Singleton
{
    /*
     * .NET 4+ contains `System.Lazy<T>` type to make laziness really simple.
     * http://msdn.microsoft.com/en-us/library/dd642331.aspx
     */

    public sealed class Singleton
    {
        private static readonly Lazy<Singleton> _lazyInstance = new Lazy<Singleton>(() => new Singleton());

        public static Singleton Instance => _lazyInstance.Value;

        private Singleton()
        {
        }
    }
}
