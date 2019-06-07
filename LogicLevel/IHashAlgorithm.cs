using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel
{
    public interface IHashAlgorithm
    {
        byte[] GetHash(params string[] strings);
    }
}
