using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedalhasOlimpiadas.Models;

namespace MedalhasOlimpiadas.Commons
{
    public class HtmlHelperCreator
    {
        public StringBuilder CelulaInput(StringBuilder form, String entidade, String valor)
        {
            form.Append("<div class='col-md-4 mb-3'>");
            form.Append("<input type='text' class='form-control' id='txt" + entidade + "' name='txt" + entidade + "' placeholder='" + entidade + "' value='" + valor + "' required>");
            form.Append("</div>");

            return form;
        }

        public StringBuilder CelulaSelect<T>(StringBuilder form, String entidade, String nome, Int32 valor, List<T> ListaResultados)
        {
            form.Append("<div class='col-md-4 mb-3'>");
            form.Append("<div class='form-group'>");
            form.Append("<select id='dll" + entidade + "' name='dll" + entidade + "' class='custom-select' required>");
            form.Append("<option value=''>" + nome + "</option>");
            if (entidade == "Modalidade")
            {
                foreach (EntModalidade obj in (ListaResultados as List<EntModalidade>))
                {
                    if (valor == obj.IdModalidade)
                    {
                        form.Append("<option value='" + obj.IdModalidade + "' selected>" + obj.ModalidadeGenero + "</option>");
                    }
                    else
                    {
                        form.Append("<option value='" + obj.IdModalidade + "'>" + obj.ModalidadeGenero + "</option>");
                    }
                }
            }
            else if (entidade == "Pais")
            {
                foreach (EntPais obj in (ListaResultados as List<EntPais>))
                {
                    if (valor == obj.IdPais)
                    {
                        form.Append("<option value='" + obj.IdPais + "' selected>" + obj.Nome + "</option>");
                    }
                    else
                    {
                        form.Append("<option value='" + obj.IdPais + "'>" + obj.Nome + "</option>");
                    }
                }
            }
            else if (entidade == "TipoMedalha")
            {
                foreach (EntTipoMedalha obj in (ListaResultados as List<EntTipoMedalha>))
                {
                    if (valor == obj.IdTipoMedalha)
                    {
                        form.Append("<option value='" + obj.IdTipoMedalha + "' selected>" + obj.TipoMedalha + "</option>");
                    }
                    else
                    {
                        form.Append("<option value='" + obj.IdTipoMedalha + "'>" + obj.TipoMedalha + "</option>");
                    }
                }
            }
            else if (entidade == "Atleta")
            {
                foreach (EntAtleta obj in (ListaResultados as List<EntAtleta>))
                {
                    if (valor == obj.IdAtleta)
                    {
                        form.Append("<option value='" + obj.IdAtleta + "' selected>" + obj.NomePais + "</option>");
                    }
                    else
                    {
                        form.Append("<option value='" + obj.IdAtleta + "'>" + obj.NomePais + "</option>");
                    }
                }
            }
            form.Append("</select>");
            form.Append("</div>");
            form.Append("</div>");

            return form;
        }

        public StringBuilder BotaoGridPesquisa(StringBuilder grid, String entidade, Int32 id, Boolean ativo)
        {
            grid.Append("<td>");
            if (ativo)
            {
                grid.Append("<button type='button' class='btn btn-primary' id='botaoRemover" + id + "' value='Inativar' onClick='removeAjax(\"" + entidade + "\", " + id + ")'>Inativar</button>");
            }
            else
            {
                grid.Append("<button type='button' class='btn btn-primary' id='botaoRemover" + id + "' value='Ativar' onClick='removeAjax(\"" + entidade + "\", " + id + ")'>Ativar</button>");
            }
            grid.Append("<button type='button' class='btn btn-secondary botaoDetalhes' id='botaoDetalhes" + id + "' value='Detalhes' onClick='detalhesAjax(\"" + entidade + "\", " + id + ")'>Detalhes</button>");
            grid.Append("</td>");

            return grid;
        }

        public StringBuilder CelulaCabecalhoTabela(StringBuilder grid, String valor)
        {
            grid.Append("<th scope='col'>" + valor + "</th>");

            return grid;
        }

        public StringBuilder CelulaValorTabela(StringBuilder grid, String valor)
        {
            grid.Append("<td>" + valor + "</td>");

            return grid;
        }

        public StringBuilder CelulaImagemTabela(StringBuilder grid, String imagem)
        {
            grid.Append("<th scope='col'><img src='/Images/" + imagem + "' class='imgTopTable'></th>");

            return grid;
        }

        public StringBuilder CelulaValorImagemTabela(StringBuilder grid, String valor, String imagem, Int32 id)
        {
            grid.Append("<td><a href='Home/Detalhes/" + id + "'><img src='" + imagem + "' class='imgTable'>" + valor + "</a></td>");

            return grid;
        }
    }
}
