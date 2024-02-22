namespace Domain.Exceptions
{
    public class VideoNotFoundException: Exception
    {
        public VideoNotFoundException() { }
        public VideoNotFoundException(string id) : base(string.Format($"Video not available for {id}")) { }
    }
}
