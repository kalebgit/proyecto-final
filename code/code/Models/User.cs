
using code.Handlers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code.Models
{
    internal class User : UserHandler
    {
        // delegados
        Verificator rightType = new Verificator(NonNullable);

        // instance variables
        private long _id;
        private string _name;
        private string _lastName;
        private string _userName;
        private string _password;
        private string _mail;

        private List<Product> _soldProducts;
        private List<Sale> _sales;


        // properties
        public long Id
        {
            get
            {
                return _id;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = (string)rightType(value);
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = (string)rightType(value);
            }
        }
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = (string)rightType(value);
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = (string)rightType(value);
            }
        }
        public string Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                string nonNullableMail;
                nonNullableMail = (string)rightType(value);
                rightType = new Verificator(IsMail);
                _mail = (string)rightType(nonNullableMail);
            }
        }

        public List<Product> SoldProducts
        {
            get
            {
                return _soldProducts;
            }
            set
            {
                _soldProducts = value;
            }
        }
        public List<Sale> Sales
        {
            get
            {
                return _sales;
            }
            set
            {
                _sales = value;
            }
        }

        //constructor
        public User(long id, string name, string lastName, string userName, 
            string password, string mail)
        {
            _id = id;
            _name = name;
            _lastName = lastName;
            _userName = userName;
            _password = password;
            _mail = mail;
            _soldProducts = UserHandler.GetSoldProducts(id);
            _sales = UserHandler.GetSales(id);
        }

        
        // metodo to string
        public override string ToString()
        {
            return String.Format("{0, -5}{1, -30}{2, -30}{3, -20}{4, -20}{5, -30}",
                _id, _name, _lastName, _userName, "privado", _mail);
        }

    }
}
