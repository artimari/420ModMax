using System;
using System.Linq;
using System.Xml.Linq;

namespace ZeeDay
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = @"C:\Users\MartinScheer\Desktop\Mod Max 420\dayzOffline.chenarusplus.dsg\db\events.xml";
            var zDoc = XElement.Load(fileName);

            var modifier = 10.0;
            var items = zDoc.Descendants(@"event");

            items.Where(iE => iE.Attributes(@"name").First().Value.ToLower().Contains("infected"))
                .SelectMany(iE => iE.Descendants(@"children"))
                .ToList()
                .ForEach(el =>
                {
                    var minVal = Convert.ToInt32(el.Attributes(@"min").First().Value);
                    var maxVal = Convert.ToInt32(el.Attributes(@"max").First().Value);

                    el.SetAttributeValue("@min", Math.Ceiling(minVal * modifier));
                    el.SetAttributeValue("@max", Math.Ceiling(maxVal * modifier));

                });

            zDoc.Save(fileName);
        }
    }
}
