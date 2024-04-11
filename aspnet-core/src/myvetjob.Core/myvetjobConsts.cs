using myvetjob.Debugging;

namespace myvetjob
{
    public class myvetjobConsts
    {
        public const string LocalizationSourceName = "myvetjob";

        public const string ConnectionStringName = "myvetjobDb";

        public const bool MultiTenancyEnabled = false;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "5d299a437c3d46f5b8a2852aab0b523e";
    }
}
