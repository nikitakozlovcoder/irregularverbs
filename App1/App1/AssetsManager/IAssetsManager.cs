using System;
using System.Collections.Generic;

namespace App1.AssetsManager
{
    public interface IAssetsManager
    {
        List<string> ReadAllLines(string assetName);
    }
}