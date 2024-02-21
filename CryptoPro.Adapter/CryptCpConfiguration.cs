namespace CryptoPro.Adapter.CryptCP
{
    /// <summary>
    /// 
    /// </summary>
    public class CryptCpConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathExe"></param>
        public CryptCpConfiguration(string pathExe)
        {
            PathExe = pathExe;
        }

        /// <summary>
        /// 
        /// </summary>
        public string PathExe { get; set; }
    }
}
