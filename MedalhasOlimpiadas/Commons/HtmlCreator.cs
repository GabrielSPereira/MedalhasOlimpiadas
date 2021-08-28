using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using MedalhasOlimpiadas.Models;
using System.Web.Mvc;

namespace MedalhasOlimpiadas.Commons
{
    public class HtmlCreator
    {
        public static ContentResult CriaGrid<T>(String entidade, List<T> ListaResultados)
        {
            HtmlHelperCreator htmlCreator = new HtmlHelperCreator();
            StringBuilder grid = new StringBuilder("");
            grid.Append("<table class='table' style='text-align:center;'>");
            grid.Append("<thead>");
            grid.Append("<tr>");
            if (entidade == "Atleta")
            {
                htmlCreator.CelulaCabecalhoTabela(grid, "Atleta");
                htmlCreator.CelulaCabecalhoTabela(grid, "Modalidade");
                htmlCreator.CelulaCabecalhoTabela(grid, "País");
            }
            else if (entidade == "Medalha")
            {
                htmlCreator.CelulaCabecalhoTabela(grid, "Atleta");
                htmlCreator.CelulaCabecalhoTabela(grid, "Medalha");
            }
            if (entidade == "Home")
            {
                htmlCreator.CelulaCabecalhoTabela(grid, "Posição");
                htmlCreator.CelulaCabecalhoTabela(grid, "País");
                htmlCreator.CelulaImagemTabela(grid, "gold-medal.png");
                htmlCreator.CelulaImagemTabela(grid, "silver-medal.png");
                htmlCreator.CelulaImagemTabela(grid, "bronze-medal.png");
                htmlCreator.CelulaCabecalhoTabela(grid, "TOTAL");
            }
            else
            {
                htmlCreator.CelulaCabecalhoTabela(grid, "Opções");
            }
            grid.Append("</thead>");
            grid.Append("<tbody>");
            if (entidade == "Atleta")
            {
                foreach (EntAtleta obj in (ListaResultados as List<EntAtleta>))
                {
                    grid.Append("<tr>");
                    htmlCreator.CelulaValorTabela(grid, obj.Nome);
                    htmlCreator.CelulaValorTabela(grid, obj.Modalidade.ModalidadeGenero);
                    htmlCreator.CelulaValorTabela(grid, obj.Pais.Nome);
                    htmlCreator.BotaoGridPesquisa(grid, entidade, obj.IdAtleta, obj.Ativo);
                    grid.Append("</tr>");
                }
            }
            else if (entidade == "Medalha")
            {
                foreach (EntMedalha obj in (ListaResultados as List<EntMedalha>))
                {
                    grid.Append("<tr>");
                    htmlCreator.CelulaValorTabela(grid, obj.Atleta.Nome);
                    htmlCreator.CelulaValorTabela(grid, obj.TipoMedalha.TipoMedalha);
                    htmlCreator.BotaoGridPesquisa(grid, entidade, obj.IdMedalha, obj.Ativo);
                    grid.Append("</tr>");
                }
            }
            else if (entidade == "Home")
            {
                foreach (EntMedalha obj in (ListaResultados as List<EntMedalha>))
                {
                    grid.Append("<tr>");
                    htmlCreator.CelulaValorTabela(grid, obj.posicaoQuadro.ToString());
                    htmlCreator.CelulaValorImagemTabela(grid, obj.Atleta.Pais.Nome, obj.Atleta.Pais.Bandeira, obj.Atleta.Pais.IdPais);
                    htmlCreator.CelulaValorTabela(grid, obj.quantidadeOuro.ToString());
                    htmlCreator.CelulaValorTabela(grid, obj.quantidadePrata.ToString());
                    htmlCreator.CelulaValorTabela(grid, obj.quantidadeBronze .ToString());
                    htmlCreator.CelulaValorTabela(grid, obj.quantidadeMedalha .ToString());
                    grid.Append("</tr>");
                }
            }
            grid.Append("</tbody>");
            grid.Append("</table>");

            return new ContentResult
            {
                ContentType = "text/html",
                Content = grid.ToString()
            };
        }

        public static ContentResult CriaForm<T>(String entidade, T obj, Int32 Id)
        {
            HtmlHelperCreator htmlCreator = new HtmlHelperCreator();
            List<EntModalidade> listaModalidade = new BllModalidade().ObterTodos("", 2, 1);
            List<EntPais> listaPais = new BllPais().ObterTodos("", 1);
            List<EntAtleta> listaAtleta = new BllAtleta().ObterTodos(0, 0, "", 1);
            List<EntTipoMedalha> listaTipoMedalha = new BllTipoMedalha().ObterTodos(1);
            StringBuilder form = new StringBuilder("<form action='/" + entidade + "/" + entidade + "Salvar' method='post' id='formDetalhes'>");
            form.Append("<h2>Cadastro de " + entidade);
            form.Append("<hr/>");
            form.Append("<input type='hidden' id='hdnId" + entidade + "' name='hdnId" + entidade + "' value='" + Id + "'>");

            if (entidade == "Atleta")
            {
                form.Append("<div class='form-row'>");
                htmlCreator.CelulaInput(form, "Nome", (obj as EntAtleta).Nome);
                htmlCreator.CelulaSelect(form, "Modalidade", "Modalidade", (obj as EntAtleta).Modalidade.IdModalidade, listaModalidade);
                htmlCreator.CelulaSelect(form, "Pais", "País", (obj as EntAtleta).Pais.IdPais, listaPais);
                form.Append("</div>");
            }
            else if (entidade == "Medalha")
            {
                form.Append("<div class='form-row'>");
                htmlCreator.CelulaSelect(form, "TipoMedalha", "Tipo de Medalha", (obj as EntMedalha).TipoMedalha.IdTipoMedalha, listaTipoMedalha);
                htmlCreator.CelulaSelect(form, "Atleta", "Atleta", (obj as EntMedalha).Atleta.IdAtleta, listaAtleta);
                form.Append("</div>");
            }
            form.Append("<button type='submit' class='btn btn-primary'>Enviar</button>");
            form.Append("</form>");

            return new ContentResult
            {
                ContentType = "text/html",
                Content = form.ToString()
            };
        }

    }
}
