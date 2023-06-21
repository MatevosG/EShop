﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Infrastructure.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}
