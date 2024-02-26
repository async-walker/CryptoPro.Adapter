namespace CryptoPro.Adapter.CryptCP
{
    /// <summary>
    /// Интерфейс взаимодействия с утилитой CryptCP
    /// </summary>
    public interface ICryptCP
    {
        /// <summary>
        /// Шифрование данных (-decr)
        /// </summary>
        /// <returns></returns>
        Task Encrypt();
        /// <summary>
        /// Подпись данных и создание сообщения (-sign)
        /// </summary>
        /// <param name="criterias">Критерии поиска сертификатов</param>
        /// <param name="der">Использовать формат DER вместо BASE64</param>
        /// <param name="directoryToSave">Директория, куда следует сохранить подписанное сообщение</param>
        /// <param name="sourceFilePath">Путь к исходному файлу</param>
        /// <returns>Путь к созданному подписанному сообщению</returns>
        Task<string> SignDataCreateMessage(
            CriteriasSearchCertificate criterias, 
            bool der,
            string directoryToSave, 
            string sourceFilePath);
        /// <summary>
        /// Удаление подписи с сообщения (-delsign)
        /// </summary>
        /// <returns></returns>
        Task DeleteSiganture();
    }
}
