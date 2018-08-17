using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Higgs.Mbale.Interfaces
{
    public interface ICache
    {
        /** In memory **/

        bool Insert(string key, object value, int expirationSeconds);

        bool Remove(string key);

        object Get<T>(string key);

        bool Insert(string key, object value, string cacheFilePath);

        bool Remove(string key, string cacheFilePath);

        object Get<T>(string key, string cacheFilePath);
    }
}
