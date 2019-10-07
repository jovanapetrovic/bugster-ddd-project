namespace Bugster.Business.Models
{
    public class UpdateTagRequest
    {
        public long TagId { get; set; }
        public string Name { get; set; }
        public string Bound { get; set; }
    }
}
