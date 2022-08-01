using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProvaCandidato.Helper
{
    public static class MessageHelper
    {
        /// <summary>
        /// Exibe mensagem de Sucesso ou Erro
        /// Alterado Método para que se torne reutilizável tanto para mensagem de sucesso como erro.
        /// </summary>
        public static void DisplayMessage(Controller controller, string message, bool success)
        {
            var userMessage = new { CssClassName = "", Title = success ? "Sucesso" : "Erro", Message = message };

            //Transformado em Json para que tenhamos acesso ás propriedades dinamicamente.
            controller.TempData["UserMessage"] = JsonConvert.SerializeObject(userMessage, Formatting.Indented);
        }
    }
}