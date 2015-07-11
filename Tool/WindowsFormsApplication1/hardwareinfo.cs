using System;
using System.Collections.Generic;
using System.Management;
using System.Net;

namespace WindowsFormsApplication1
{
    public class hardwareInfo
    {
        public List<string> GetDiskInfo()
        {
            List<string> list = new List<string>();
            ManagementClass managementClass = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection instances = managementClass.GetInstances();
            using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ManagementObject managementObject = (ManagementObject)enumerator.Current;
                    string text = managementObject.Properties["Model"].Value.ToString();
                    if (!string.IsNullOrEmpty(text))
                    {
                        list.Add(text);
                    }
                }
            }
            return list;
        }
        public string GetHostName()
        {
            return Dns.GetHostName();
        }
        public string GetCpuID()
        {
            string result;
            try
            {
                ManagementClass managementClass = new ManagementClass("Win32_Processor");
                ManagementObjectCollection instances = managementClass.GetInstances();
                string text = null;
                using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        ManagementObject managementObject = (ManagementObject)enumerator.Current;
                        text = managementObject.Properties["ProcessorId"].Value.ToString();
                    }
                }
                result = text;
            }
            catch
            {
                result = "";
            }
            return result;
        }

        public List<string> GetMacInfo()
        {
            List<string> list = new List<string>();
            ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection instances = managementClass.GetInstances();
            using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ManagementObject managementObject = (ManagementObject)enumerator.Current;
                    if ((bool)managementObject["IPEnabled"])
                    {
                        string text = managementObject["MacAddress"].ToString();
                        if (!string.IsNullOrEmpty(text))
                        {
                            list.Add(text);
                        }
                    }
                }
            }
            return list;
        }
    }
}
