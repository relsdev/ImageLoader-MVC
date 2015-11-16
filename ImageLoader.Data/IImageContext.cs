﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageLoader.Data
{
    public interface IImageContext : IDisposable
    {
        int SaveChanges();
    }
}
