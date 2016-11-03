using AluraTunesData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula1
{
    class Program
    {
        private const int TAMANHO_PAGINA = 10;

        static void Main(string[] args)
        {
            using (var contexto = new AluraTunesEntities())
            {
                var linhasRelatorio = contexto.NotasFiscais.Count();
                var paginasRelatorio = Math.Ceiling((double)linhasRelatorio / TAMANHO_PAGINA);

                for (var p = 1; p <= paginasRelatorio; p++)
                {
                    ImprimirPagina(contexto, p);
                }

                Console.ReadKey();
            }
        }

        private static void ImprimirPagina(AluraTunesEntities contexto, int pagina)
        {
            var query = from nf in contexto.NotasFiscais
                        orderby nf.NotaFiscalId
                        select new
                        {
                            Numero = nf.NotaFiscalId,
                            Data = nf.DataNotaFiscal,
                            Cliente = nf.Cliente.PrimeiroNome + " " + nf.Cliente.Sobrenome,
                            Total = nf.Total
                        };

            var tamanhoDoPulo = (pagina - 1) * TAMANHO_PAGINA;

            query = query.Skip(tamanhoDoPulo);

            query = query.Take(TAMANHO_PAGINA);

            Console.WriteLine();
            Console.WriteLine("Página: {0}", pagina);
            Console.WriteLine();

            foreach (var nf in query)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", nf.Numero, nf.Data, nf.Cliente, nf.Total);
            }
        }
    }

}
