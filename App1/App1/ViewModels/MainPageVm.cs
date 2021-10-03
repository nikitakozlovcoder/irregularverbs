using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using App1.Contstants;
using App1.Models;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class MainPageVm : INotifyPropertyChanged
    {
        private QuestionModel _currentQuestion;
        private readonly VerbsTester.VerbsTester _verbsTester;
        private readonly AnswerChecker.AnswerChecker _ansChecker = new AnswerChecker.AnswerChecker();
        public Command CheckCommand { get; set; }
        public QuestionModel CurrentQuestion
        {
            get => _currentQuestion;
            set
            {
                _currentQuestion = value;
                OnPropertyChanged(nameof(CurrentQuestion));
            }
        }

        private string _answer;
        public string Answer
        {
            get => _answer;
            set
            {
                _answer = value;
                OnPropertyChanged(nameof(Answer));
            }
        }
        
        private AnswerCheckResult _checkResult;
        public AnswerCheckResult CheckResult
        {
            get => _checkResult;
            set
            {
                _checkResult = value;
                OnPropertyChanged(nameof(CheckResult));
            }
        }

        public Command SkipCommand { get; set; }
        public MainPageVm()
        {
            CheckCommand = new Command(x =>
            {
                try
                {
                    CheckResult = _ansChecker.Check(Answer, _currentQuestion);
                    CurrentQuestion = _verbsTester?.PickQuestion();
                    Answer = string.Empty;
                }
                catch (ArgumentException e)
                {
                    
                }
            });
            SkipCommand = new Command(x =>
            {
                CheckResult = _ansChecker.Skip(_currentQuestion);
                CurrentQuestion = _verbsTester?.PickQuestion();
                Answer = string.Empty;
            });
          
            var verbs = new VerbsParser.VerbsParser(VerbConstants.FilePath).ParseVerbs().ToList();
            _verbsTester = new VerbsTester.VerbsTester(verbs);
            CurrentQuestion = _verbsTester.PickQuestion();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}