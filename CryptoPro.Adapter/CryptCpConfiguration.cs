namespace CryptoPro.Adapter.CryptCP
{
    /// <summary>
    /// Конфигурация для работы с утилитой CryptCP
    /// </summary>
    public class CryptCpConfiguration
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="CryptCpConfiguration"/>
        /// </summary>
        /// <param name="pathExe">Путь к исполняемому exe-файлу утилиты</param>
        public CryptCpConfiguration(string pathExe)
        {
            PathExe = pathExe;
        }

        /// <summary>
        /// Расположение исполняемого exe-файла
        /// </summary>
        public string PathExe { get; set; }
    }
}
