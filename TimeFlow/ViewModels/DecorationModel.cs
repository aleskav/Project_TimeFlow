using TimeFlow.Models;

namespace TimeFlow.ViewModels
{
    public class DecorationModel
    {
        public Tache Tache { get; set; }
        public int Span { get; set; }
        public int StartHourIndex { get; set; }
        public int EndHourIndex { get; set; }
        public int StartQuarterIndex { get; set; }
        public int EndQuarterIndex { get; set; }

        public string FeuilleJourId { get; set; }
    }
}
