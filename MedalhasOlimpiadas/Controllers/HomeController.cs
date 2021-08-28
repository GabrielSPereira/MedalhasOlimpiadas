using System;
using System.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MedalhasOlimpiadas.Models;
using MedalhasOlimpiadas.Commons;
using Microsoft.AspNetCore.Http;

namespace MedalhasOlimpiadas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        #region Home
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public System.Web.Mvc.ContentResult HomeFiltro(IFormCollection collection) //Requisição POST do Método de pesquisa
        {
            try
            {
                String filtroModalidade = "";
                String filtroIsMulher = "2";
                if (collection != null)
                {
                    filtroModalidade = collection["ddlModalidade"]; //Campos utilizados no formulário de pesquisa
                    filtroIsMulher = collection["ddlIsMulher"];
                }

                List<EntMedalha> ListaGrid = new BllMedalha().ObterTodosPorMedalha(Utils.ToInt32(filtroModalidade), Utils.ToInt32(filtroIsMulher)); //Obtém o quadro de medalhas levando em consideração o grid Pesquisa
                foreach (EntMedalha obj in ListaGrid)
                {
                    obj.Atleta.Pais = new BllPais().ObterPorId(obj.Atleta.Pais.IdPais);
                    Integracao.PesquisaBandeira(obj.Atleta.Pais);
                }
                return HtmlCreator.CriaGrid("Home", ListaGrid);
            }
            catch (Exception ex)
            {
                throw;

            }
        }
        #endregion

        #region Detalhes
        public IActionResult Detalhes(int id)
        {
            EntPais ListaGrid = DetalhesPopula(id);
            return View(ListaGrid);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public EntPais DetalhesPopula(int id)
        {
            EntPais obj = new BllPais().ObterPorId(id);
            Integracao.PesquisaBandeira(obj);
            return obj;
        }
        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
