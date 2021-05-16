namespace Singleton.Models
{
    /*
     * This is thread-safe code, but not performant enough
     * (the lock is acquired every time there is a call to an instance)
     */

    public sealed class Singleton_v2
    {
        private static Singleton_v2 _instance = null;
        
        private static readonly object Padlock = new object();

        private Singleton_v2() { }

        public static Singleton_v2 Instance
        {
            get
            {
                // better to lock on private objects, so that other code couldn't access that specific lock
                lock (Padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new Singleton_v2();
                    }

                    return _instance;
                }
            }
        }
    }
}
