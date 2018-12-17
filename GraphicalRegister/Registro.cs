using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace GraphicalRegister
{
    internal class Registro
    {
        private RegistryKey regAddr;
        private string[][] regEntry;

        internal Registro(string addr)
        {
            regAddr = Registry.CurrentUser.OpenSubKey(@addr);
        }

        private RegistryKey SetRegBaseAddr() => regAddr = (Environment.Is64BitOperatingSystem) ?
                RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64) :
                RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

        private string[] GetEntryNames() => regAddr.GetValueNames();

        private void findData()
        {
            byte[] binaryData;

            if (regAddr != null)
            {
                int i = 0;
                regEntry = new string[regAddr.ValueCount][];

                foreach (string subKey in GetEntryNames())
                {
                    regEntry[i] = new string[5];
                    //Console.WriteLine(regAddr.ValueCount);
                    //Console.ReadKey();
                    binaryData = (byte[])regAddr.GetValue(subKey);

                    using (var stream = new MemoryStream(binaryData))
                    {
                        DateTime tsecond = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

                        using (var toRead = new BinaryReader(stream))
                        {
                            regEntry[i][0] = regAddr.Name;
                            regEntry[i][1] = subKey;
                            regEntry[i][2] = new Cesar(subKey, 13).getDescifrar();

                            stream.Seek(4, SeekOrigin.Begin);
                            var counter = toRead.ReadInt32();
                            regEntry[i][3] = counter.ToString();

                            stream.Seek(60, SeekOrigin.Begin); //Se convierte hora de windows a hora de UNIX, Se dividen nano segundos 
                            var lastDate = (toRead.ReadInt64() / 10000000) - 11644473600; //Y se restan segundos entre 1/1/1601 y 1/1/1970
                            regEntry[i][4] = tsecond.AddSeconds(lastDate).ToLocalTime().ToString();

                            i++;
                        }
                    }
                }
            }
        }

        internal string[][] getRegistryContent()
        {
            findData();
            return regEntry;
        }
    }
}
