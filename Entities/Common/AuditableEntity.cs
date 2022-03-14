namespace WebApplication1.Entities.Commom
{
    public class AuditableEntity
    {
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; }
    }
}
