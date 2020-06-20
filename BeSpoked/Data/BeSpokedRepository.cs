using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeSpoked.Data
{
    public class BeSpokedRepository
    {
        public BeSpokedContext Context { get; }
        public ILogger<BeSpokedRepository> Logger { get; }

        public BeSpokedRepository(BeSpokedContext context, ILogger<BeSpokedRepository> logger)
        {
            this.Context = context;
            this.Logger = logger;
        }


    }
}
