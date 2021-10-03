using System.Collections.Generic;
using System.IO;
using System.Linq;
using App1.AssetsManager;
using App1.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace App1.VerbsParser
{
    public class VerbsParser
    {
        private readonly string _path;
        public VerbsParser(string path)
        {
            _path = path;
        }

        public IEnumerable<VerbDataModel> ParseVerbs()
        {
            var assets = DependencyService.Get<IAssetsManager>();
            foreach (var line in assets.ReadAllLines(_path))
            {
                yield return ParseVerb(line.Trim());
            }
        }

        private static VerbDataModel ParseVerb(string line)
        {
            var strings = line.Split(' ', '\t');
            strings = strings.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            
            return new VerbDataModel
            {
                FirstForm = ParseField(strings[0]),
                SecondForm = ParseField(strings[1]),
                ThirdForm = ParseField(strings[2]),
                Translation = ParseField(strings[3])
            };
        }

        private static List<string> ParseField(string str)
        {
            return str.Replace('|', ' ').Split(';', '/', ',').ToList();
        }
    }
}