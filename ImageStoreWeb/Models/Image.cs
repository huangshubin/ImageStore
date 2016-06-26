namespace ImageStoreWeb.Models
{
    public class Image : EntityBase
    {
     
        public int Id { get; protected set; }
        public byte[] ImageContent { get; set; }
        public string ImagePath { get; set; }
        public string ImageType { get; set; }
        public bool Active { get; set; } = true;
        public int UserId { get; set; }
        public virtual Client User { get; set; }
      
    }
}