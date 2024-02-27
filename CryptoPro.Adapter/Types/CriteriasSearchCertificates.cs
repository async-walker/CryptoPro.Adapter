using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace CryptoPro.Adapter.CryptCP.Types
{
    /// <summary>
    /// Критерий поиска сертификатов (КПС) используется для задания сведений о
    /// субъектах, чьи сертификаты будут использоваться при выполнении команды(например,
    /// шифрование или подпись данных). 
    /// <para>Если команда такова, что КПС должен удовлетворять только
    /// один сертификат, то такой КПС будет обозначаться КПС1.</para>
    /// </summary>
    public class CriteriasSearchCertificates
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="CriteriasSearchCertificates"/>
        /// </summary>
        /// <param name="stringsSearchRDN">
        /// <para>Список строк, используемых для поиска сертификатов</para>
        /// <para>Будут найдены сертификаты, в RDN субъекта/издателя которых присутствуют все эти строки</para>
        /// </param>
        /// <param name="issuerRDN">Используется RDN издателя для поиска</param>
        /// <param name="storeLocation">Расположение сертификата</param>
        /// <param name="storeName">Название хранилища</param>
        /// <param name="certFileName">В качестве хранилища используется сообщение или файл сертификата</param>
        /// <param name="thumbprint">Отпечаток сертификата</param>
        /// <param name="countSearch">Количество сертификатов для поиска</param>
        /// <param name="nochain">Не проверять цепочки найденных сертификатов</param>
        /// <param name="norev">Не проверять сертификаты в цепочке на предмет отозванности</param>
        /// <param name="errchain">Завершать выполнение с ошибкой, если хотя бы один сертификат не прошел проверку</param>
        public CriteriasSearchCertificates(
            List<string>? stringsSearchRDN = default,
            string? issuerRDN = default,
            StoreLocation storeLocation = StoreLocation.CurrentUser,
            StoreName storeName = StoreName.My,
            string? certFileName = default,
            string? thumbprint = default,
            CountSearchCertificatesCriteria? countSearch = default,
            bool nochain = false,
            bool norev = false,
            bool errchain = false)
        {
            StringsSearchRDN = stringsSearchRDN;
            IssuerRDN = issuerRDN;
            StoreLocation = storeLocation;
            StoreName = storeName;
            CertFileName = certFileName;
            Thumbprint = thumbprint;
            CountSearch = countSearch;
            NoChain = nochain;
            NoRev = norev;
            ErrChain = errchain;
        }

        /// <summary>
        /// Список строк, используемых для поиска сертификатов
        /// </summary>
        public List<string>? StringsSearchRDN { get; set; } = default;
        /// <summary>
        /// RDN издателя для поиска
        /// </summary>
        public string? IssuerRDN { get; set; } = default;
        /// <summary>
        /// Расположение сертификата
        /// </summary>
        public StoreLocation StoreLocation { get; set; }
        /// <summary>
        /// Название хранилища
        /// </summary>
        public StoreName StoreName { get; set; }
        /// <summary>
        /// Имя файла, который используется в качестве хранилища сообщения или файла сертификата
        /// </summary>
        public string? CertFileName { get; set; }
        /// <summary>
        /// Отпечаток сертификата
        /// </summary>
        public string? Thumbprint { get; set; } = default;
        /// <summary>
        /// Количество сертификатов для поиска
        /// </summary>
        public CountSearchCertificatesCriteria? CountSearch { get; set; } = default;
        /// <summary>
        /// Не проверять цепочки найденных сертификатов
        /// </summary>
        public bool NoChain { get; set; }
        /// <summary>
        /// Не проверять сертификаты в цепочке на предмет отозванности
        /// </summary>
        public bool NoRev { get; set; }
        /// <summary>
        /// Завершать выполнение с ошибкой, если хотя бы один сертификат не прошел проверку
        /// </summary>
        public bool ErrChain { get; set; }

        internal string ExecuteParams()
        {
            var criteriasCollection = new List<string>();

            if (StringsSearchRDN is not null && StringsSearchRDN.Count > 0)
                criteriasCollection.Add(string.Join(",", StringsSearchRDN));

            if (IssuerRDN is not null)
                criteriasCollection.Add($"-issuer \"{IssuerRDN}\"");

            criteriasCollection.Add(
                MergeStoreNameAndLocation(StoreLocation, StoreName));

            if (CertFileName is not null)
                criteriasCollection.Add($"-f \"{CertFileName}\"");

            if (Thumbprint is not null)
                criteriasCollection.Add($"-thumbprint \"{Thumbprint}\"");

            if (CountSearch is not null)
                criteriasCollection.Add(CountSearch.ExecuteCriteria());

            if (NoChain)
                criteriasCollection.Add("-nochain");

            if (NoRev)
                criteriasCollection.Add("-norev");

            if (ErrChain)
                criteriasCollection.Add("-errchain");

            var cmd = string.Join(' ', criteriasCollection);

            return cmd;
        }

        static string MergeStoreNameAndLocation(StoreLocation storeLocation, StoreName storeName)
        {
            var location = storeLocation switch
            {
                StoreLocation.CurrentUser => "-u",
                StoreLocation.LocalMachine => "-m",
                _ => throw new InvalidEnumArgumentException("Invalid StoreLocation enum"),
            };

            var criteria = string.Join(
                separator: string.Empty,
                values: [location, storeName]);

            return criteria;
        }
    }
}
