using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LogicLevel.Model
{
    public class UserPartialModel : BaseModel, IEquatable<UserPartialModel>
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public Color Color { get; set; }
        public byte RoleId { get; set; }

        public bool Equals(UserPartialModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((UserPartialModel)obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static bool operator ==(UserPartialModel left, UserPartialModel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UserPartialModel left, UserPartialModel right)
        {
            return !Equals(left, right);
        }
    }
}
