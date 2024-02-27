namespace CryptoPro.Adapter.CryptCP.Types
{
    /// <summary>
    /// Критерий количества поиска сертификатов
    /// </summary>
    public class CountSearchCertificatesCriteria
    {
        /// <summary>
        /// Инициализация экземпляра <see cref="CountSearchCertificatesCriteria"/>
        /// </summary>
        /// <param name="allCerts">
        /// <para>Использовать все найденные сертификаты (для КПС)</para>
        /// <para>Если <see langword="true"/>, количество (<c>count</c>) не указывается</para>
        /// </param>
        /// <param name="count">
        /// <para>Количество сертификатов для поиска</para>
        /// <para>Если найдено менее N сертификатов, то вывести запрос для выбора нужного (по умолчанию N=10)</para>
        /// </param>
        public CountSearchCertificatesCriteria(bool allCerts, int? count = default)
        {
            if (allCerts && count is not null)
                throw new ArgumentException(
                    "Нельзя одновременно указывать для параметра 'allCerts'=[TRUE] и 'count' отличный от [NULL]");

            if (!allCerts && count is not null && count <= 0)
                throw new ArgumentException(
                    "При 'allCerts'=[FALSE] параметр 'count' должен быть положительным числом");

            AllCerts = allCerts;
            CountToSearch = count;
        }

        /// <summary>
        /// Использовать все найденные сертификаты (для КПС)
        /// </summary>
        public bool AllCerts { get; set; }
        /// <summary>
        /// Количество сертификатов для поиска
        /// </summary>
        public int? CountToSearch { get; set; }

        internal string ExecuteCriteria()
        {
            if (AllCerts)
                return "-all";
            else
            {
                if (CountToSearch > 1)
                    return $"-q{CountToSearch}";
                else 
                    return "-1";
            }
        }
    }
}
