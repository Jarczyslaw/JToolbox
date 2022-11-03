namespace JToolbox.Core.Models.Results
{
    public abstract class Message
    {
        public int Code { get; set; }

        public string Content { get; set; }

        public object Tag { get; set; }
    }
}