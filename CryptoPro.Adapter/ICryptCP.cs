namespace CryptoPro.Adapter.CryptCP
{
    /// <summary>
    /// Интерфейс взаимодействия с утилитой CryptCP
    /// </summary>
    public interface ICryptCP
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task Encrypt();
        /// <summary>
        /// Подпись данных и создание сообщения (-sign)
        /// </summary>
        /// <param name="criterias"></param>
        /// <param name="directoryToSave"></param>
        /// <param name="sourceFilePath"></param>
        /// <returns>Путь к созданному подписанному сообщению</returns>
        Task<string> SignDataCreateMessage(
            CriteriasSearchCertificate criterias, 
            string directoryToSave, 
            string sourceFilePath);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task DeleteSiganture();
    }
}
