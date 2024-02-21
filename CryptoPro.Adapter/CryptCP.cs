using CryptoPro.Adapter.CryptCP.Extensions;

namespace CryptoPro.Adapter.CryptCP
{
    /// <summary>
    /// 
    /// </summary>
    public class CryptCP : ICryptCP
    {
        private readonly CryptCpConfiguration _config;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public CryptCP(CryptCpConfiguration config)
        {
            _config = config;
        }

        /// <inheritdoc/>
        public Task DeleteSiganture()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task Encrypt()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<string> SignDataCreateMessage(
            CriteriasSearchCertificate csc, 
            string directoryToSave, 
            string sourceFilePath)
        {
            var criterias = csc.ExecuteParams();

            var cmd = $@"{_config.PathExe} -sign -dir {directoryToSave} {criterias} {sourceFilePath}";
            cmd.RunProcessCMD();

            var createdSigFilePath = string.Join(
                separator: ".", 
                value: [Path.Combine(directoryToSave, sourceFilePath), "sig"]);

            return Task.FromResult(createdSigFilePath);
        }
    }
}
