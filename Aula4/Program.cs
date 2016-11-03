using AluraTunesData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.SqlServer;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZXing;

namespace Aula4
{
    class Program
    {
        private const string Imagens = "Imagens";

        static void Main(string[] args)
        {
            var barcodWriter = new BarcodeWriter();
            barcodWriter.Format = BarcodeFormat.QR_CODE;
            barcodWriter.Options = new ZXing.Common.EncodingOptions
            {
                Width = 100,
                Height = 100
            };

            if (!Directory.Exists(Imagens))
                Directory.CreateDirectory(Imagens);

            using (var contexto = new AluraTunesEntities())
            {
                var queryFaixas =
                from f in contexto.Faixas
                select f;

                var listaFaixas = queryFaixas.ToList();

                Stopwatch stopwatch = Stopwatch.StartNew();

                var queryCodigos =
                listaFaixas
                .AsParallel()
                .Select(f => new
                {
                    Arquivo = string.Format("{0}\\{1}.jpg", Imagens, f.FaixaId),
                    Imagem = barcodWriter.Write(string.Format("aluratunes.com/faixa/{0}", f.FaixaId))
                });

                int contagem = queryCodigos.Count();

                stopwatch.Stop();

                Console.WriteLine("Códigos gerados: {0} em {1} segundos.", contagem,
                    stopwatch.ElapsedMilliseconds / 1000.0); //2.8s //1.563 segundos


                foreach (var item in queryCodigos)
                {
                    item.Imagem.Save(item.Arquivo, ImageFormat.Jpeg);
                }
            }
        }
    }
}
