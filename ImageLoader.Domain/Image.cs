
namespace ImageLoader.Domain
{
    public class Image : IIdentifiable
    {
        public long Id { get; set; }
        public string Domain { get; set; }
        public string Url { get; set; }       
        
        //TODO: Add concurency support later.
        //public byte[] RowVersion { get; set; }
    }
}
