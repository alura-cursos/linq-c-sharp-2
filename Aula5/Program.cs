using AluraTunesData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula5
{
    class Program
    {
        static void Main(string[] args)
        {
            int clienteId = 17;

            using (var contexto = new AluraTunesEntities())
            {
                var vendasPorCliente =
                from v in contexto.ps_Itens_Por_Cliente(clienteId)
                group v by new { v.DataNotaFiscal.Year, v.DataNotaFiscal.Month }
                    into agrupado
                orderby agrupado.Key.Year, agrupado.Key.Month
                select new
                {
                    Ano = agrupado.Key.Year,
                    Mes = agrupado.Key.Month,
                    Total = agrupado.Sum(a => a.Total)
                };

                foreach (var item in vendasPorCliente)
                {
                    Console.WriteLine("{0}\t{1}\t{2}", item.Ano, item.Mes, item.Total);
                }
            }

            Console.ReadKey();
        }
    }
}
