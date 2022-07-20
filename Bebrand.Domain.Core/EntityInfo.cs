using System;
using System.ComponentModel.DataAnnotations;

namespace Bebrand.Domain.Core
{
    public abstract class EntityInfo
    {
        public Guid Id { get; set; }
        public DateTime ModifiedOn { get; set; }

        public DateTime Date { get; set; }
        [StringLength(450)]
        public string ModifiedBy { get; set; }


        public UserStatus Status { get; set; }

        public override bool Equals(object obj)
        {
            var compareTo = obj as EntityInfo;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(EntityInfo a, EntityInfo b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntityInfo a, EntityInfo b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";
        }
    }
}
