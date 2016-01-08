using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace site_connectinfo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Witaj w programie wypisujacym informacje o polaczeniu z witryna\n");
            Console.WriteLine("Podaj adres witryny: \n");
            string adresStrony = Console.ReadLine();
            Console.WriteLine("\n");
            var adresy = Dns.GetHostAddresses(adresStrony);
            var port = 80;
            foreach (var adresIp in adresy)
            {
                var gniazdo = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    gniazdo.Connect(adresIp, port);
                    Console.WriteLine("Strona: " + adresStrony);
                    Console.WriteLine("IP serwera: " + ((IPEndPoint)gniazdo.RemoteEndPoint).Address.ToString());
                    Console.WriteLine("Numer portu zdalnego połączenia: " + ((IPEndPoint)gniazdo.RemoteEndPoint).Port.ToString());
                    Console.WriteLine("Numer portu lokalnego z którego dokonywane jest połączenie " + ((IPEndPoint)gniazdo.LocalEndPoint).Port.ToString());
                }
                catch (SocketException)
                {
                    Console.WriteLine("błąd połączenia z portem");
                }
                finally
                {
                    gniazdo.Dispose();
                }
            }

            Console.ReadKey();
        }
    }
}
