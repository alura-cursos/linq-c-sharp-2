using AluraTunesData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var contexto = new AluraTunesEntities())
            {
                int faixaId = 1;

                Console.WriteLine("Quem comprou...");
                Console.WriteLine();

                var produtoProcurado = contexto.Faixas.Where(f => f.FaixaId == faixaId).Single();

                Console.WriteLine("{0}", produtoProcurado.Nome);
                Console.WriteLine();

                Console.WriteLine("...também comprou...");
                Console.WriteLine();

                var query =
                from esteItem in contexto.ItemsNotaFiscal
                join outroItem in contexto.ItemsNotaFiscal
                    on esteItem.NotaFiscalId equals outroItem.NotaFiscalId
                where esteItem.ItemNotaFiscalId != outroItem.ItemNotaFiscalId
                && esteItem.FaixaId == faixaId
                select new { esteItem, outroItem };

                Console.WriteLine("NF\tFaixa");
                foreach (var item in query)
                {
                    Console.WriteLine("{0}\t{1}", item.outroItem.NotaFiscalId, item.outroItem.Faixa.Nome);
                }
            }

            Console.ReadKey();
        }
    }
}

