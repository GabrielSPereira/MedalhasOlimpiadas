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
    public class AtletaController : Controller
    {
        private readonly ILogger<AtletaController> _logger;

        public AtletaController(ILogger<AtletaController> logger)
        {
            _logger = logger;
        }

        #region Atleta
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public System.Web.Mvc.ContentResult AtletaFiltro(IFormCollection collection)
        {
            try
            {
                String filtroAtleta = "";
                String filtroModalidade = "";
                String filtroPais = "";
                String filtroAtivo = "1";
                if (collection != null)
                {
                    filtroAtleta = collection["txtNome"];
                    filtroModalidade = collection["ddlModalidade"];
                    filtroPais = collection["ddlPais"];
                    filtroAtivo = collection["ddlAtivo"];
                }

                List<EntAtleta> ListaGrid = new BllAtleta().ObterTodos(Utils.ToInt32(filtroModalidade), Utils.ToInt32(filtroPais), filtroAtleta, Utils.ToInt32(filtroAtivo));
                foreach (EntAtleta obj in ListaGrid)
                {
                    obj.Modalidade = new BllModalidade().ObterPorId(obj.Modalidade.IdModalidade);
                }

                return HtmlCreator.CriaGrid("Atleta", ListaGrid);
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        public System.Web.Mvc.ContentResult AtletaAjaxForm(int id) //Requisição Post do cria e detalhes do Atleta
        {
            try
            {
                EntAtleta objAtleta = null;
                if (id > 0) //Faz a verificação para ver se já no banco (preenche os campos do formulário de cadastro)
                {
                    objAtleta = new BllAtleta().ObterPorId(id);
                }
                else
                {
                    objAtleta = new EntAtleta();
                    objAtleta.Ativo = true;
                }

                return HtmlCreator.CriaForm("Atleta", objAtleta, objAtleta.IdAtleta); //Chama o método de criar o forumlário
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        [HttpPost]
        public ActionResult AtletaSalvar(IFormCollection collection) //Através da collection ele recebe os dados do formulário
        {
            try
            {
                EntAtleta objAtleta = new EntAtleta();
                AtletaPageToObject(objAtleta, collection); //Chama o método para alimentar o objeto Atleta

                try
                {
                    if (objAtleta.IdAtleta == 0) //Verifica se é um cadastro ou edição pelo IdAtleta
                    {
                        objAtleta = new BllAtleta().Inserir(objAtleta);
                    }
                    else
                    {
                        new BllAtleta().Alterar(objAtleta);
                    }
                }
                catch (Exception ex)
                {
                    return Content(ex.Message);
                }

                return Redirect("/Atleta"); //Redireciona para a Padrão após a execução do inserir ou alterar
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        private void AtletaPageToObject(EntAtleta objAtleta, IFormCollection collection)
        {
            objAtleta.IdAtleta = Utils.ToInt32(collection["hdnIdAtleta"]); //Dados vindos do formulário, nesse caso se não tivermos IdAtleta, ou seja, é um novo cadastro
            objAtleta.Modalidade.IdModalidade = Utils.ToInt32(collection["dllModalidade"]);
            objAtleta.Pais.IdPais = Utils.ToInt32(collection["dllPais"]);
            objAtleta.Nome = collection["txtNome"];
            objAtleta.Ativo = true;
        }

        public ContentResult AtletaAjaxRemover(int id)
        {
            try
            {
                EntAtleta objAtleta = new BllAtleta().ObterPorId(id);
                new BllAtleta().Remover(objAtleta);
                return Content(Convert.ToInt32(objAtleta.Ativo).ToString());
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
