namespace ImageWebAPIs.Models
{
    public class Image : EntityBase
    {
        public int Id { get; protected set; }
        public byte[] ImageContent { get; set; }
        public string ImagePath { get; set; }
        public bool Active { get; set; }
        public int UserId { get; set; }
        public virtual Client User { get; set; }
    }
}