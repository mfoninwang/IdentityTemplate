namespace WebApplication1.Models
{
    public class AuditableEntity
    {
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
