using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace NetObserver.IpAdressUtility
{
    public class LocalIp
    {
        private static List<string> _listIp = new List<string>();
        private static List<Tuple<PrefixOrigin, string>> _listTuples = new List<Tuple<PrefixOrigin, string>>();

        public static string GetIpv4Localhost()
        {
            AddIpWithDhcpPrefix();
            if(_listIp.Count > 0) return _listIp.FirstOrDefault()?.ToString();
            AddIpWithManualPrefix(); 

            return _listIp.FirstOrDefault()?.ToString();
        }

        public static List<Tuple<PrefixOrigin, string>> GetAllIpv4NetInterface()
        {
            AddIpAndPrefixWithDhcpPrefix();
            AddIpaAndPrefixWithManualPrefix();
            return _listTuples;
        }

        private static void AddIpWithDhcpPrefix()
        {
            NetworkInterface[] netInterfaceMass = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface interfaceItem in netInterfaceMass)
            {
                if (interfaceItem.GetIPProperties().UnicastAddresses.LastOrDefault()?.PrefixOrigin == System.Net.NetworkInformation.PrefixOrigin.Dhcp)
                {
                    IPAddress ip = interfaceItem.GetIPProperties().UnicastAddresses.LastOrDefault()?.Address;
                    _listIp.Add(ip.ToString());
                    break;
                }
            }
        }

        private static void AddIpWithManualPrefix()
        {
            NetworkInterface[] netInterfaceMass = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface interfaceItem in netInterfaceMass)
            {
                if (interfaceItem.GetIPProperties().UnicastAddresses.LastOrDefault()?.PrefixOrigin == System.Net.NetworkInformation.PrefixOrigin.Manual)
                {
                    IPAddress ip = interfaceItem.GetIPProperties().UnicastAddresses.LastOrDefault()?.Address;
                    _listIp.Add(ip.ToString());
                    break;
                }
            }
        }

        private static void AddIpAndPrefixWithDhcpPrefix()
        {
            NetworkInterface[] netInterfaceMass = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface interfaceItem in netInterfaceMass)
            {
                if (interfaceItem.GetIPProperties().UnicastAddresses.LastOrDefault()?.PrefixOrigin == System.Net.NetworkInformation.PrefixOrigin.Dhcp)
                {
                    IPAddress ip = interfaceItem.GetIPProperties().UnicastAddresses.LastOrDefault()?.Address;
                    PrefixOrigin prefix = System.Net.NetworkInformation.PrefixOrigin.Dhcp;
                    _listTuples.Add((new Tuple<PrefixOrigin, string>(prefix, ip.ToString())));
                }
            }
        }

        private static void AddIpaAndPrefixWithManualPrefix()
        {
            NetworkInterface[] netInterfaceMass = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface interfaceItem in netInterfaceMass)
            {
                if (interfaceItem.GetIPProperties().UnicastAddresses.LastOrDefault()?.PrefixOrigin == System.Net.NetworkInformation.PrefixOrigin.Manual)
                {
                    IPAddress ip = interfaceItem.GetIPProperties().UnicastAddresses.LastOrDefault()?.Address;
                    PrefixOrigin prefix = System.Net.NetworkInformation.PrefixOrigin.Manual;
                    _listTuples.Add((new Tuple<PrefixOrigin, string>(prefix, ip.ToString())));
                }
            }
        }
    }
}
