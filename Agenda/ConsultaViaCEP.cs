using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;

namespace ConsultaCEP
{
    public class ConsultaVIACEP
    {
        public ViaCEP Consulta(string CEP)
        {
            string oCEP = CEP;
            string oURL = "https://viacep.com.br/ws/" + CEP + "/json/";

            HttpClient _httpClient = new HttpClient();
            HttpResponseMessage result =
                _httpClient.GetAsync(oURL).Result;

            String JsonRetorno =
                   result.Content.ReadAsStringAsync().Result;
                   ViaCEP oviaCEP = new ViaCEP();
                    oviaCEP = JsonConvert.DeserializeObject<ViaCEP>(JsonRetorno);
            return oviaCEP;
        }


    }
}