namespace Singleton.Models
{
    /*
     * - Hard to implement (unreadable)
     * - Is not super performant.
     * + Better than v2, because there is no need to lock every time `Instance` is accessed
     */

    public sealed class Singleton_v3
    {
        private static Singleton_v3 _instance = null;
        private static readonly object Padlock = new object();

        private Singleton_v3() { }

        public static Singleton_v3 Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Padlock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Singleton_v3();
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
