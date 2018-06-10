using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LibImg;
using Newtonsoft.Json;

namespace MyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var tm = new TimeoutTaskExample();

            Task.Run(() => { tm.StartTask(); });

            ////Resizer.PdfToImage();
            //var xDocument = XDocument.Parse(@"<xml><a myattribute='b'>c</a><tag class='myclass-one'>testo</tag></xml>");
            //var builder = new StringBuilder();

            //JsonSerializer.Create().
            //    Serialize(new CustomJsonWriter(new StringWriter(builder)), xDocument);
            //var serialized = builder.ToString();

            //Console.WriteLine(serialized);
            Console.ReadLine();
        }
    }

    public class CustomJsonWriter : JsonTextWriter
    {
        public CustomJsonWriter(TextWriter writer) : base(writer) { }

        public override void WritePropertyName(string name)
        {
            if (name.StartsWith("@") || name.StartsWith("#"))
            {
                base.WritePropertyName(name.Substring(1));
            }
            else
            {
                base.WritePropertyName(name);
            }
        }
    }
}
