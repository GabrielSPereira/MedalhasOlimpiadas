using System;
using System.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MedalhasOlimpiadas.Models;
using Microsoft.AspNetCore.Http;
using MedalhasOlimpiadas.Commons;

namespace MedalhasOlimpiadas.Controllers
{
    public class MedalhaController : Controller
    {
        private readonly ILogger<AtletaController> _logger;

        public MedalhaController(ILogger<AtletaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region medalha
        [HttpPost]
        [ValidateAntiForgeryToken]
        public System.Web.Mvc.ContentResult MedalhaFiltro(IFormCollection collection)
        {
            try
            {
                String filtroTipoMedalha = "";
                String filtroAtleta = "";
                String filtroAtivo = "1";
                if (collection != null)
                {
                    filtroTipoMedalha = collection["ddlTipoMedalha"];
                    filtroAtleta = collection["ddlAtleta"];
                    filtroAtivo = collection["ddlAtivo"];
                }

                List<EntMedalha> ListaGrid = new BllMedalha().ObterTodos(Utils.ToInt32(filtroTipoMedalha), Utils.ToInt32(filtroAtleta), Utils.ToInt32(filtroAtivo));

                return HtmlCreator.CriaGrid("Medalha", ListaGrid);
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        public System.Web.Mvc.ContentResult MedalhaAjaxForm(int id)
        {
            try
            {
                EntMedalha objMedalha = null;
                if (id > 0)
                {
                    objMedalha = new BllMedalha().ObterPorId(id);
                }
                else
                {
                    objMedalha = new EntMedalha();
                    objMedalha.Ativo = true;
                }

                return HtmlCreator.CriaForm("Medalha", objMedalha, objMedalha.IdMedalha);
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        [HttpPost]
        public ActionResult MedalhaSalvar(IFormCollection collection)
        {
            try
            {
                EntMedalha objMedalha = new EntMedalha();
                MedalhaPageToObject(objMedalha, collection);

                try
                {
                    if (objMedalha.IdMedalha == 0)
                    {
                        objMedalha = new BllMedalha().Inserir(objMedalha);
                    }
                    else
                    {
                        new BllMedalha().Alterar(objMedalha);
                    }
                }
                catch (Exception ex)
                {
                    return Content(ex.Message);
                }

                return Redirect("/Medalha");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        private void MedalhaPageToObject(EntMedalha objMedalha, IFormCollection collection)
        {
            objMedalha.IdMedalha = Utils.ToInt32(collection["hdnIdMedalha"]);
            objMedalha.Atleta.IdAtleta = Utils.ToInt32(collection["dllAtleta"]);
            objMedalha.TipoMedalha.IdTipoMedalha = Utils.ToInt32(collection["dllTipoMedalha"]);
            objMedalha.Ativo = true;
        }

        public ContentResult MedalhaAjaxRemover(int id)
        {
            try
            {
                EntMedalha objMedalha = new BllMedalha().ObterPorId(id);
                new BllMedalha().Remover(objMedalha);
                return Content(Convert.ToInt32(objMedalha.Ativo).ToString());
            }
            catch (Exception ex)
            {
                return Content(ex.Message);

            }
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
