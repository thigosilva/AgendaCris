using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultaCEP
{
    public class BancoCEP
    {

        public List<ViaCEP> BancoVIACEP()
        {
            List<ViaCEP> ListCEP = new List<ViaCEP>();
            ConsultaVIACEP RetornoConsultaviaCEP = new ConsultaVIACEP();
            ViaCEP oViacep = new ViaCEP();

            String ListaCEP = "06716530;05108040;06142050;06683170;06186073;06286020;06286010;06016004;06322200;06385830;06436360;05366220;" ;

            string[] CEP = ListaCEP.Split(";");

            foreach (var cepaPesquisar in CEP)
            {
                try
                {
                    oViacep = RetornoConsultaviaCEP.Consulta(cepaPesquisar);
                    ListCEP.Add(oViacep);
                    Console.Write(".");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocorreu um erro na pesquisa do cep:" + cepaPesquisar);
                    Console.WriteLine(ex.Message.ToString());

                }
            }

            return ListCEP;
        }


    }
}
