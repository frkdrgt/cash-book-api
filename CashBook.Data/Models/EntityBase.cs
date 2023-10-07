using System.ComponentModel.DataAnnotations.Schema;

namespace CashBook.Data
{
    public abstract class EntityBase<T>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
