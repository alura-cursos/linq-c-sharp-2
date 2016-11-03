using AluraTunesData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula3
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var contexto = new AluraTunesEntities())
            {
                var mesAniversario = 1;

                while (mesAniversario <= 12)
                {
                    Console.WriteLine("Mês: {0}", mesAniversario);

                    var lista = (from f in contexto.Funcionarios
                                 where f.DataNascimento.Value.Month == mesAniversario
                                 orderby f.DataNascimento.Value.Month, f.DataNascimento.Value.Day
                                 select f).ToList();

                    mesAniversario++;

                    foreach (var f in lista)
                    {
                        Console.WriteLine("{0:dd/MM}\t{1} {2}", f.DataNascimento, f.PrimeiroNome, f.Sobrenome);
                    }
                }
                Console.ReadKey();
            }
        }
    }
}
