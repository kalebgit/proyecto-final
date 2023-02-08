using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code.Models
{
    internal class Sale
    {
        // instance variables
        private long _id;
        private string _comments;
        private long _userId;
        
        // properties
        public long Id
        {
            get
            {
                return _id;
            }
        }
        public string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
            }
        }
        public long UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }

        // constructor
        public Sale(long id, string comments, long userId)
        {
            _id = id;
            _comments = comments;
            _userId = userId;
        }

        public override string ToString()
        {
            return String.Format("{0, -10}{1, -50}{2, -10}", _id, _comments, _userId);
        }
    }
}
