using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExampleProxyWebClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var encodedAddress = "Andrés Bello 2040, Santiago, Región Metropolitana";
                var key = ConfigWrapper.Instance.KeyGoogleMaps;
                using(var client = new WebClient())
                {
                    if (!string.IsNullOrWhiteSpace(ConfigWrapper.Instance.ProxyUrl))
                    {
                        var proxy = new WebProxy(ConfigWrapper.Instance.ProxyUrl, true);
                        Uri uri = new Uri("http://tempuri.org/");
                        ICredentials credentials = CredentialCache.DefaultCredentials;                        
                        proxy.Credentials = credentials;
                        client.Proxy = proxy;
                        Console.WriteLine($"Proxy configurated at '{ConfigWrapper.Instance.ProxyUrl}'");                        
                    }
                    var data = client.DownloadData($"https://maps.googleapis.com/maps/api/geocode/xml?sensor=false&key={key}&address={encodedAddress}");

                    using (Stream stream = new MemoryStream(data))
                    {
                        XDocument document = XDocument.Load(new StreamReader(stream));
                        XElement longitudeElement = document.Descendants("lng").FirstOrDefault();
                        XElement latitudeElement = document.Descendants("lat").FirstOrDefault();
                        double lat = 0;
                        double lon = 0;

                        if (latitudeElement != null && longitudeElement != null)
                            if (double.TryParse(latitudeElement.Value, out lat))
                                if (double.TryParse(longitudeElement.Value, out lon))
                                    Console.WriteLine($"latitude:{lat}, longitude:{lon}");
                    }
                }
            
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("Proceso finalizado. Presione cualquier tecla para cerrar.");
            Console.ReadKey();
        }
    }
}
