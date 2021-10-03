using System;
using System.Collections.Generic;
using App1.Models;

namespace App1.AnswerChecker
{
    public class AnswerChecker
    {
        private const int PartsCount = 4;
        public AnswerCheckResult Check(string answer, QuestionModel questionModel)
        {
            var parts = answer.Split(',');
            if (parts.Length != PartsCount)
            {
                throw new ArgumentException();
            }

            var wasError = false;
            var summary = answer + "\n";
            for (var i = 0; i < PartsCount; i++)
            {
                if (!Check(parts[i], i, questionModel.Verb))
                {
                    wasError = true;
                    summary += $"{i+1}) {parts[i]}->{string.Join("/", GetById(questionModel.Verb, i))}\n";
                }
            }

            return new AnswerCheckResult
            {
                IsRight = !wasError,
                Summary = wasError ? summary : string.Empty
            };
        }

        private static List<string> GetById(VerbDataModel verb, int id)
        {
            switch (id)
            {
                case 0:
                    return verb.FirstForm;
                case 1:
                    return verb.SecondForm;
                case 2:
                    return verb.ThirdForm;
                case 3:
                    return verb.Translation;
            }

            return new List<string>();
        }
        private bool Check(string ans, int form, VerbDataModel verb)
        {
            var correctAns = GetById(verb, form);
            return correctAns?.Exists(x => x.Replace(" ", "") == ans.Replace(" ", "")) == true;
        }

        public AnswerCheckResult Skip(QuestionModel currentQuestion)
        {
            var summary = CreateCorrectAns(currentQuestion.Verb);
            return new AnswerCheckResult
            {
                IsRight = false,
                Summary = summary
            };
        }

        private static string CreateCorrectAns(VerbDataModel currentQuestionVerb)
        {
            var str = string.Empty;
            for (var i = 0; i < PartsCount; i++)
            {
                str += $"{string.Join("/", GetById(currentQuestionVerb, i))}";
                if (i != PartsCount -1)
                {
                    str += ", ";
                }
            }
            return str+"\n";
        }
    }
}