namespace FDBlog.Core.Entities
{
    public abstract class EntityBase:IEntityBase
    {
        public virtual int Id { get; set; }
        public virtual string CreatedBy { get; set; } = "Undefind";
        public virtual string? MadifiedBy { get; set; }
        public virtual string? DeleteddBy { get; set; }
        public virtual DateTime CreatedDate { get; set; } = DateTime.Now;
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual DateTime? DeletedDate { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
