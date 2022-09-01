using Agenda;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsultaCEP
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Boolean Consulta = true;
            Console.Clear();
            List<ViaCEP> ListCEP = new List<ViaCEP>();
            BancoCEP CriaBAse = new BancoCEP();
            ConfirmacaoSexo pessoa = new ConfirmacaoSexo();



            Console.WriteLine("Informe 1º nome:");
            String TextNome = Console.ReadLine();

            Console.WriteLine("Informe Sobrenome:");
            String TextFone = Console.ReadLine();

            Console.WriteLine("Informe Data Nascimento:");
            String TextEmail = Console.ReadLine();

         Rotulo_TextSexo:
            Console.WriteLine("Informe seu Sexo:");
            String TextSexo = Console.ReadLine();
//------------------------------------------------------------------------------------------------
//--------------- If que esta fazendo Confirmação de sexo -------------------------------------

            if (TextSexo == "F" || TextSexo == "M" || TextSexo == "f" || TextSexo == "m")
            {
                Console.WriteLine("------------------------");
                Console.WriteLine(" Sexo Não existente");
                Console.WriteLine("------------------------");
            }

            else
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("  Imforme Sexo Corretamente ");
                Console.WriteLine("  [F] Feminino ");
                Console.WriteLine("  [M] Masculino ");
                Console.WriteLine("-----------------------------------");
                goto Rotulo_TextSexo;
            }

//------------------------------------------------------------------------------------------------
//------------------------------Codigo que Caça Cep-----------------------------------------------


            while (Consulta)
            {
                try
                {
                    Console.WriteLine("--[ Consulta de CEP ]--");
                    Console.WriteLine("-->Informe CEP:");
                    String CEP = Console.ReadLine();

                    switch (CEP.ToUpper())
                    {
                        case "SAIR":
                            Consulta = false;
                            break;
                        case "EXIT":
                            Consulta = false;
                            break;
                        case "CARREGAR":
                            ListCEP = CriaBAse.BancoVIACEP();
                            Console.WriteLine("Dados carregados com sucesso !");
                            break;

                        case "SALVAR":
                            string filePath = @"c:\temp\ListaCEP.JSON";
                            var ObjetoDados = JsonConvert.SerializeObject(ListCEP);
                            await File.WriteAllTextAsync(filePath, ObjetoDados);
                            Console.WriteLine("Dados Salvos com Sucesso !!!");
                            break;
                        case "PESQUISAR":

                            Console.WriteLine("Informe Logradouro para pesquisa:");
                            string ValorAPesquisa = Console.ReadLine();
                            IEnumerable<ViaCEP>
                                PesquisarCEP = from e in ListCEP
                                               where e.logradouro == ValorAPesquisa
                                               select e;

                            foreach (var ResultadoPesquisa in PesquisarCEP)
                            {
                                Console.WriteLine(
                                ResultadoPesquisa.cep.ToString() + " " +
                                ResultadoPesquisa.logradouro.ToString() + " " +
                                ResultadoPesquisa.complemento.ToString() + " " +
                                ResultadoPesquisa.bairro.ToString() + " " +
                                ResultadoPesquisa.localidade.ToString() + " " +
                                ResultadoPesquisa.uf.ToString() + " " +
                                ResultadoPesquisa.ibge.ToString() + " " +
                                ResultadoPesquisa.gia.ToString() + " " +
                                ResultadoPesquisa.ddd.ToString() + " " +
                                ResultadoPesquisa.siafi.ToString());
                            }

                            break;
                        case "LISTAR":
                            int QuantidadeCEP = ListCEP.Count;
                            for (int i = 0; i < QuantidadeCEP; i++)
                            {
                                Console.WriteLine(
                                        i.ToString("D8") + " " +
                                        ListCEP[i].cep.ToString() + " " +
                                        ListCEP[i].logradouro.ToString() + " " +
                                        ListCEP[i].complemento.ToString() + " " +
                                        ListCEP[i].bairro.ToString() + " " +
                                        ListCEP[i].localidade.ToString() + " " +
                                        ListCEP[i].uf.ToString() + " " +
                                        ListCEP[i].ibge.ToString() + " " +
                                        ListCEP[i].gia.ToString() + " " +
                                        ListCEP[i].ddd.ToString() + " " +
                                        ListCEP[i].siafi.ToString()
                                    );
                            }
                            break;
                        default:
                            {
                                ConsultaVIACEP RetornoConsultaviaCEP = new ConsultaVIACEP();
                                ViaCEP oViacep = new ViaCEP();
                                oViacep = RetornoConsultaviaCEP.Consulta(CEP);
                                ListCEP.Add(oViacep);
                                Console.WriteLine("--[ Resultado de pesquisa ]----------------------------");
                                Console.WriteLine("cep........:" + oViacep.cep.ToString());
                                Console.WriteLine("logradouro.:" + oViacep.logradouro.ToString());
                                Console.WriteLine("complemento:" + oViacep.complemento.ToString());
                                Console.WriteLine("bairro.....:" + oViacep.bairro.ToString());
                                Console.WriteLine("localidade.:" + oViacep.localidade.ToString());
                                Console.WriteLine("uf.........:" + oViacep.uf.ToString());
                                Console.WriteLine("ibge.......:" + oViacep.ibge.ToString());
                                Console.WriteLine("gia........:" + oViacep.gia.ToString());
                                Console.WriteLine("ddd........:" + oViacep.ddd.ToString());
                                Console.WriteLine("siafi......:" + oViacep.siafi.ToString());
                                Console.WriteLine("-------------------------------------------------------");
                                Console.WriteLine(" Pressione <ENTER> para continuar ou Exit para sair");
                                Console.WriteLine("-------------------------------------------------------");
                                String Sair = Console.ReadLine();
                                if (Sair.ToUpper() == "EXIT")
                                {
                                    Consulta = false;
                                }
                            }
                            break;

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("-------------------------------------------------------");
                    Console.WriteLine("--- Atenção ocorreu um erro ---------------------------");
                    Console.WriteLine("-------------------------------------------------------");
                    Console.WriteLine(ex.Message.ToString());
                    Console.WriteLine("-------------------------------------------------------");
                    Console.WriteLine("--[pressione <Enter> para continuar ]------------------");
                    Console.WriteLine("-------------------------------------------------------");

                    Console.ReadLine();
                }

            }

        }
    }
}
