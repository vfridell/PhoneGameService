using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneGameService.Models
{
    public class PhoneNumber : IComparable<PhoneNumber>, IComparable<string>
    {
        public int ID { get; set; }
        public string Number { get; set; }



        #region IComparable<PhoneNumber> Members

        public int CompareTo(PhoneNumber other)
        {
            if (Equals(other)) return 0;

            return Number.CompareTo(other.Number);
        }

        #endregion


        #region IComparable<string> Members

        public int CompareTo(string other)
        {
            if(Equals(other)) return 0;

            return Number.CompareTo(other);
        }

        #endregion
    }
}
