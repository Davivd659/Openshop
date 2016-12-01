﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pm25.WebCrawler
{
    public interface IPageParser<out T>
    {
        T Extract();
    }
}