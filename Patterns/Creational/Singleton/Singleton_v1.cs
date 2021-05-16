namespace Singleton.Models
{
    /*
     * Code is not thread-safe.
     * Two different threads could reach `if (_instance == null)` statement
     * and create 2 instances of singleton.
     */

    // ReSharper disable once InconsistentNaming
    public sealed class Singleton_v1
    {
        private static Singleton_v1 _instance = null;

        private Singleton_v1() { }

        public static Singleton_v1 Instance
        {
            get
            {
                // no thread-safety here
                if (_instance == null)
                {
                    _instance = new Singleton_v1();
                }

                return _instance;
            }
        }
    }
}
