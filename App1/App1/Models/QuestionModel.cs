namespace App1.Models
{
    public class QuestionModel
    {
        public int FormNumber { get; set; }
        public string VerbText { get; set; }
        public VerbDataModel Verb { get; set; }
    }
}