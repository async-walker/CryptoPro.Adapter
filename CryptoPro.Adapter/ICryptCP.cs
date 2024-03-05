using CryptoPro.Adapter.CryptCP.Types;

namespace CryptoPro.Adapter.CryptCP
{
    /// <summary>
    /// Интерфейс взаимодействия с утилитой CryptCP
    /// </summary>
    public interface ICryptCP
    {
        /// <summary>
        /// Шифрование данных и создание сообщения (-encr)
        /// </summary>
        /// <returns></returns>
        Task EncryptDataCreateMessage();
        /// <summary>
        /// Расшифровка данных из сообщения (-decr)
        /// </summary>
        /// <returns></returns>
        Task DecryptMessageData();
        /// <summary>
        /// Подпись данных и создание сообщения (-sign)
        /// </summary>
        /// <param name="criterias">КПС автора подписи</param>
        /// <param name="der">Использовать формат DER вместо BASE64</param>
        /// <param name="directoryToSave">Директория, куда следует сохранить подписанное сообщение</param>
        /// <param name="sourceFilePath">Путь к исходному файлу</param>
        /// <returns>Путь к созданному подписанному сообщению</returns>
        Task<string> SignDataCreateMessage(
            CriteriasSearchCertificates criterias, 
            bool der,
            string directoryToSave, 
            string sourceFilePath);
        /// <summary>
        /// Удаление подписи из сообщения (-delsign)
        /// </summary>
        /// <returns></returns>
        Task DeleteMessageSignature();
        /// <summary>
        /// Проверка подписи сообщения (-verify)
        /// </summary>
        /// <param name="criterias">КПС авторов подписей</param>
        /// <param name="sourceFilePath">Исходный файл, содержащий сообщение</param>
        /// <param name="destinationFilePath">Файл, в который будут записаны данные из сообщения</param>
        /// <returns></returns>
        Task VerifyMessageSignature(
            CriteriasSearchCertificates criterias,
            string sourceFilePath,
            string destinationFilePath);
    }
}
