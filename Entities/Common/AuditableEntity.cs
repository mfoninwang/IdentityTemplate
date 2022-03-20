namespace PermissionBasedTemplate.Entities.Commom
{
    public class AuditableEntity
    {
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; }

        public string? LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}
