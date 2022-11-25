﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Leitor_ABCConfig.Entities;

namespace Leitor_ABCConfig.Libs.Parsers
{
    public class TXTSaver
    {
        public void ToTXTFile(List<ImageDummyClass> idclasses)
        {
            //    StreamWriter sw = new StreamWriter(@"C:\temp\Resultado.txt", true, Encoding.UTF8);

            //    foreach (ImageDummyClass imageDummyClass in idclasses)
            //    {
            //        sw.WriteLine($"Name: {imageDummyClass.Name} // Width = {imageDummyClass.Width} // Height = {imageDummyClass.Height} // X = {imageDummyClass.X} // Y = {imageDummyClass.Y}");
            //    }
            //    sw.WriteLine("");
            //    sw.Close();
            //
            // o jeito abaixo não precisa colocar um close(); pq o using() vai fechar a conexão para vocÊ
            // geralmente quando fazemos leitura de arquivos, conexões com banco, tudo que é IO ( INPUT E OUTPUT EXTERNO)
            // É Necessário colocar um using para fechar conexao. E sempre bom colocar try catch pq sempre pode ter erro de conexao

            try
            {
                using (StreamWriter sw = new StreamWriter(@"C:\temp\Resultado.txt", true, Encoding.UTF8))
                {
                    foreach (ImageDummyClass imageDummyClass in idclasses)
                    {
                        sw.WriteLine($"Name: {imageDummyClass.Name} // Width = {imageDummyClass.Width} // Height = {imageDummyClass.Height} // X = {imageDummyClass.X} // Y = {imageDummyClass.Y}");
                    }
                    sw.WriteLine("");
                }

                Console.WriteLine("Resultado.txt Gerado!!!");

            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
