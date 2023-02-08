
using code.Handlers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace code.Models
{
    internal class Product : ProductHandler
    {
        // delegados
        Verificator rightType = new Verificator(NonNullable);

        // instance variables
        private long _id;
        private string _description;
        private decimal _cost;
        private decimal _sellingPrice;
        private int _stock;
        private long _userId;

        // properties
        public long Id
        {
            get
            {
                return _id;
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = (string)rightType(value);
            }
        }
        public decimal Cost
        {
            get
            {
                return _cost;
            }
            set
            {
                _cost = value;
            }
        }
        public decimal SellingPrice
        {
            get
            {
                return _sellingPrice;
            }

            set
            {
                rightType = new Verificator(IsDecimal);
                _sellingPrice = (decimal)rightType(value);
            }
        }
        public int Stock
        {
            get
            {
                return _stock;
            }
            set
            {
                rightType = new Verificator(IsInt);
                _stock = (int)rightType(value);
            }
        }
        public long UserId
        {
            get
            {
                return _userId;
            }
        }

        // constructor
        public Product(long id, string description, decimal cost, decimal sellingPrice,
            int stock, long userid)
        {
            _id = id;
            _description = description;
            _cost = cost;
            _sellingPrice = sellingPrice;
            _stock = stock;
            _userId = userid;
        }

        // metodo to string
        public override string ToString()
        {
            return String.Format("{0, -5}{1, -30}{2, -20}{3, -20}{4, -20}",
                _id, _description, _cost, _sellingPrice, _stock);
        }
    }
}
