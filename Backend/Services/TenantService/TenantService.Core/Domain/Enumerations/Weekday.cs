using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantService.Core.Domain.Enumerations
{
    public class Weekday : Enumeration
    {
        public static Weekday Monday = new(0, "Monday");
        public static Weekday Tuesday = new(1, "Tuesday");
        public static Weekday Wednesday = new(2, "Wednesday");
        public static Weekday Thursday = new(3, "Thursday");
        public static Weekday Friday = new(4, "Friday");
        public static Weekday Saturday = new(5, "Saturday");
        public static Weekday Sunday = new(6, "Sunday");


        public Weekday(int value, string description)
            : base(value, description)
        {

        }

        public static Weekday Create(int value)
        {
            if (value == 0) return new Weekday(Monday.Value, Monday.Description);
            if (value == 1) return new Weekday(Tuesday.Value, Tuesday.Description);
            if (value == 2) return new Weekday(Wednesday.Value, Wednesday.Description);
            if (value == 3) return new Weekday(Thursday.Value, Thursday.Description);
            if (value == 4) return new Weekday(Friday.Value, Friday.Description);
            if (value == 5) return new Weekday(Saturday.Value, Saturday.Description);
            if (value == 6) return new Weekday(Sunday.Value, Sunday.Description);

            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return this.Description.ToString();
        }
    }
}
