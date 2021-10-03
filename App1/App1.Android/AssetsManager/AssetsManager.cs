using System.Collections.Generic;
using System.IO;
using App1.AssetsManager;
using Xamarin.Forms;


[assembly: Dependency(typeof(App1.Android.AssetsManager.AssetsManagerAndroid))]
namespace App1.Android.AssetsManager
{
    public class AssetsManagerAndroid : IAssetsManager
    {
        
        public List<string> ReadAllLines(string assetName)
        {
            var context = global::Android.App.Application.Context;
            using (var input = context.Assets?.Open(assetName))
            {
                if (input == null) return new List<string>();
                using (var sr = new StreamReader(input))
                {
                    return new List<string>(sr.ReadToEnd().Split("\n"));
                }
            }
        }
    }
}