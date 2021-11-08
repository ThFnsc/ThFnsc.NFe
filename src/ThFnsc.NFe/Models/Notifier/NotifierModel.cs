namespace ThFnsc.NFe.Models.Notifier
{
    public class NotifierModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string NotifierType { get; set; }

        public string JsonData { get; set; }

        public TimeSpan Delay { get; set; }
    }
}
