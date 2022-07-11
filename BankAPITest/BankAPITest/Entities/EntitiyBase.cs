using System.ComponentModel.DataAnnotations;

namespace BankAPITest.Entities
{
    public class EntityBase : IEntityBase
    {
        [Required]
        public virtual int Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is EntityBase eobj)
                return eobj.Id == Id;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }

}