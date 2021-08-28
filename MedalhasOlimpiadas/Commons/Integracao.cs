using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedalhasOlimpiadas.Models;
using Newtonsoft.Json;

namespace MedalhasOlimpiadas.Commons
{
    public class Integracao
    {
        public static EntPais PesquisaBandeira(EntPais obj)
        {
            var client = new RestClient("https://restcountries.eu/rest/v2/alpha/" + obj.Sigla);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            dynamic dynamicObject = JsonConvert.DeserializeObject(response.Content);
            obj.Bandeira = dynamicObject["flag"];

            return obj;
        }
    }
}
