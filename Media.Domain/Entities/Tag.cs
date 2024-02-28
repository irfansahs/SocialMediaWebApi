namespace Media.Domain.Entities
{
     public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}