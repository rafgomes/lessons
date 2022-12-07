using Leitor_ABCConfig.Entities;
using System.Collections.Generic;

namespace Leitor_ABCConfig.Libs
{
    public abstract class IDCPrinterBase
    {
        public abstract void PrintIDC(List<ImageDummyClass> idclasses);
    }
}