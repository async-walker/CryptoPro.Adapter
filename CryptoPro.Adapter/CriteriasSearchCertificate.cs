using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace CryptoPro.Adapter.CryptCP
{
    /// <summary>
    /// Критерий поиска сертификатов (КПС) используется для задания сведений о
    /// субъектах, чьи сертификаты будут использоваться при выполнении команды(например,
    /// шифрование или подпись данных). 
    /// <para>Если команда такова, что КПС должен удовлетворять только
    /// один сертификат, то такой КПС будет обозначаться КПС1.</para>
    /// </summary>
    public class CriteriasSearchCertificate
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="CriteriasSearchCertificate"/>
        /// </summary>
        /// <param name="stringsSearchRDN">
        /// <para>Список строк, используемых для поиска сертификатов.</para>
        /// <para>Будут найдены сертификаты, в RDN субъекта/издателя которых присутствуют все эти строки.</para></param>
        /// <param name="issuerRDN">Используется RDN издателя для поиска</param>
        /// <param name="storeLocation">Расположение сертификата</param>
        /// <param name="storeName">Название хранилища</param>
        /// <param name="thumbprint">Отпечаток сертификата</param>
        /// <param name="nochain">Не проверять цепочки найденных сертификатов.</param>
        /// <param name="norev">Не проверять сертификаты в цепочке на предмет отозванности.</param>
        /// <param name="errchain"></param>
        public CriteriasSearchCertificate(
            List<string>? stringsSearchRDN = default,
            string? issuerRDN = default,
            StoreLocation storeLocation = StoreLocation.CurrentUser,
            StoreName storeName = StoreName.My,
            string? thumbprint = default,
            bool nochain = false,
            bool norev = false,
            bool errchain = false)
        {
            StringsSearchRDN = stringsSearchRDN;
            IssuerRDN = issuerRDN;
            StoreLocation = storeLocation;
            StoreName = storeName;
            Thumbprint = thumbprint;
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
        /// Отпечаток сертификата
        /// </summary>
        public string? Thumbprint { get; set; } = default;
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
            var cmd = string.Empty;

            var criteriasCollection = new List<string>();

            if (StringsSearchRDN is not null && StringsSearchRDN.Count > 0)
                criteriasCollection.Add(string.Join(",", StringsSearchRDN));

            if (IssuerRDN is not null)
                criteriasCollection.Add($"-issuer \"{IssuerRDN}\"");

            criteriasCollection.Add(
                MergeStoreNameAndLocation(StoreLocation, StoreName));
            
            if (Thumbprint is not null)
                criteriasCollection.Add($"-thumbprint \"{Thumbprint}\"");
            
            if (NoChain)
                criteriasCollection.Add("-nochain");
            
            if (NoRev)
                criteriasCollection.Add("-norev");
            
            if (ErrChain)
                criteriasCollection.Add("-errchain");

            cmd = string.Join(" ", criteriasCollection);

            return cmd;
        }

        static string MergeStoreNameAndLocation(StoreLocation storeLocation, StoreName storeName)
        {
            var location = storeLocation switch
            {
                StoreLocation.CurrentUser => "-u",
                StoreLocation.LocalMachine => "-m",
                _ => throw new InvalidEnumArgumentException(""),
            };

            var criteria = string.Join("", location, storeName);

            return criteria;
        }
    }
}
