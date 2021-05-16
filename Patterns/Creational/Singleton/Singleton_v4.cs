namespace Singleton.Models
{
    /*
     * Static constructors are executed only when an instance of the class is created
     * or a static member is referenced (our case). And they are executed only once per AppDomain!
     * 
     * - It could not be that lazy - if other static members exist, singleton instance is created when other properties are accessed
     * - If type isn't marked with special flag `beforefieldinit`, then it is lazy.
     *   C# compiler marks all types which don't have a static constructor as `beforefieldinit`.
     */

    public sealed class Singleton_v4
    {
        private static readonly Singleton_v4 _instance = new Singleton_v4();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Singleton_v4() { }

        private Singleton_v4() { }

        public static Singleton_v4 Instance => _instance;
    }
}
