﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Caching;


namespace YG.SC.Weixin
{
    public class WeiXinCache
    {
        public static readonly WeiXinCache Instance = new WeiXinCache();

        public void Add(string key, string value)
        {
            if (HttpRuntime.Cache[key] != null)
            {
                HttpRuntime.Cache.Remove(key);
            }
            HttpRuntime.Cache.Add(key, value, null, DateTime.Now.AddSeconds(180), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
        }

        public string Get(string key)
        {
            var obj = HttpRuntime.Cache[key];
            if (obj == null)
            {
                return null;
            }
            return obj.ToString();
        }

    }
}