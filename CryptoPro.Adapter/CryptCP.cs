using CryptoPro.Adapter.CryptCP.Extensions;

namespace CryptoPro.Adapter.CryptCP
{
    /// <summary>
    /// Класс взаимодействия с утилитой командной строки CryptCP
    /// </summary>
    public class CryptCP : ICryptCP
    {
        private readonly CryptCpConfiguration _config;

        /// <summary>
        /// Инициализация экземпляра <see cref="CryptCP"/>
        /// </summary>
        /// <param name="config">Конфигурация для настройки работы с утилитой</param>
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
            bool der,
            string directoryToSave, 
            string sourceFilePath)
        {
            var criterias = csc.ExecuteParams();

            var cmd = $@"{_config.PathExe} -sign -dir {directoryToSave} {criterias} {UseDer(der)} {sourceFilePath}";
            cmd.RunProcessCMD();
            
            var createdSigFilePath = string.Join(
                separator: ".", 
                value: [Path.Combine(directoryToSave, sourceFilePath), "sig"]);

            return Task.FromResult(createdSigFilePath);
        }

        static string UseDer(bool der)
        {
            if (der)
                return "-der";
            else return string.Empty;
        }
    }
}
