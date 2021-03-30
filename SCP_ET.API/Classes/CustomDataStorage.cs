using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_ET.API.Classes
{
    public class CustomDataStorage : IDisposable
    {
        private Dictionary<string, object> tempData = new Dictionary<string, object>();
        public void ClearData()
        {
            tempData.Clear();
        }

        public void RemoveData(string dataName)
        {
            if (ContainsData(dataName))
                tempData.Remove(dataName);
        }

        public void Set<T>(string dataName, T data)
        {
            tempData[dataName] = data;
        }

        public T Get<T>(string dataName)
        {
            return (T)tempData[dataName];
        }

        public bool ContainsData(string dataName)
        {
            return tempData.ContainsKey(dataName);
        }

        public void Dispose()
        {
            ClearData();
            GC.SuppressFinalize(this);
            PluginSystem.Logger.Info("CDS", "Clear data storage.");
        }
    }
}
