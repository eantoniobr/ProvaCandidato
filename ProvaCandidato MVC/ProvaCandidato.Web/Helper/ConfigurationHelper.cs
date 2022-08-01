using System.Configuration;

namespace ProvaCandidato.Helper
{
    /// <summary>
    /// Helper para auxiliar obter configurações do web.config
    /// Foi decidido desta forma pois poderá ser reutilizado em outras partes do sistema, além de ficar mais limpo a chamada
    /// </summary>
    public static class ConfigurationHelper
    {
        /// <summary>
        /// Obtém configuração AppSettings por nome.
        /// </summary>
        /// <param name="name">Nome (key) da configuração</param>
        /// <returns>Valor da configuração</returns>
        public static string GetAppSettingsByName(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }
    }
}