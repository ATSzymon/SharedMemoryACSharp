using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Data.Sqlite;

namespace SharedMemoryA
{
    class Program
    {
        static void Main(string[] args)
        {

            List<int> a = new List<int>();
            a.Add(1);
            a.Add(5);
            a.Add(4);
            a.Add(3);
            MemoryMappedFile dupa = MemoryMappedFile.CreateNew("14", 65000 * 4);
            byte[] tmp = new byte[65000 * 4];

            //using (MemoryMappedFile mmf = MemoryMappedFile.CreateNew("14", 10000))
            //{
                var binFormater = new BinaryFormatter();
                var mStream = new MemoryStream();
                binFormater.Serialize(mStream, a);
                Stopwatch w = Stopwatch.StartNew();
                using (MemoryMappedViewStream stream = dupa.CreateViewStream())
                {

                    BinaryWriter writer = new BinaryWriter(stream);
                    //Console.WriteLine(mStream.ToArray().Length);
                    //writer.Write(mStream.ToArray());

                    //writer.Write(tmp);
                    for (int i = 0; i < 65000; i++)
                    {
                        writer.Write(8 + i);

                    }
                    //writer.Write(1);
                    //writer.Write(222);

                }
                w.Stop();
            //}
            Console.ReadKey();
        }
    }
}
