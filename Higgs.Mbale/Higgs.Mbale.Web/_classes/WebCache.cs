using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Higgs.Mbale.Interfaces;
using System.Web.Caching;

namespace Higgs.Mbale.Web._classes
{
    public class WebCache : ICache
    {
         /// <summary>
        /// Insert into cache with absolute expiration
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationSeconds"></param>
        /// <returns></returns>
        public bool Insert(string key, object value, int expirationSeconds)
        {
            try
            {
                HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.AddSeconds(expirationSeconds), Cache.NoSlidingExpiration);
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Remove item from cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            try
            {
                HttpRuntime.Cache.Remove(key);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get value from cache, value can be returned even if dependency associated with cached item has expired
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get<T>(string key)
        {
            return HttpRuntime.Cache[key];
        }

        public bool Insert(string key, object value, string cacheFilePath)
        {
            throw new NotSupportedException();
        }

        public bool Remove(string key, string cacheFilePath)
        {
            throw new NotSupportedException();
        }

        public object Get<T>(string key, string cacheFilePath)
        {
            throw new NotSupportedException();
        }
    }
    
}