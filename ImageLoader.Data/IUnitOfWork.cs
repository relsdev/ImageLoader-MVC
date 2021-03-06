﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageLoader.Data
{
    public interface IUnitOfWork: IDisposable
    {
        int Save();
        IImageContext Context { get; }
    }
}
