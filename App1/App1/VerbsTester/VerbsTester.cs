using System;
using System.Collections.Generic;
using System.Linq;
using App1.Models;

namespace App1.VerbsTester
{
    public class VerbsTester
    {
        private const int PartsCount = 4;
        private List<VerbDataModel> _verbs; 
        private int _idx = 0;
        public VerbsTester(IEnumerable<VerbDataModel> verbs)
        {
            ShuffleAndSetVerbs(verbs);
        }
        
        private void ShuffleAndSetVerbs(IEnumerable<VerbDataModel> verbs)
        {
            var rnd = new Random();
            _verbs = verbs.OrderBy(_ => rnd.Next()).ToList();
        }
        public QuestionModel PickQuestion()
        {
            if (_idx >= _verbs.Count)
            {
                _idx = 0;
                ShuffleAndSetVerbs(_verbs);
            }

            var verb = _verbs[_idx++];
            var rnd = new Random();
            var form = rnd.Next(0, 3);
            var verbText = string.Empty;
            switch (form)
            {
                case 0:
                    verbText = string.Join("/", verb.FirstForm);
                    break;
                case 1:
                    verbText = string.Join("/", verb.SecondForm);
                    break;
                case 2:
                    verbText = string.Join("/", verb.ThirdForm);
                    break;
            }
            
            return new QuestionModel
            {
                FormNumber = form,
                VerbText = verbText,
                Verb = verb
              
            };
        }
    }
}