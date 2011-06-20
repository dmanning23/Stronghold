using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold.Generic
{
    public class InventoryItem
    {
        #region Members

        private string _name;
        private int _id;
        private double _weight;
        private int _quantity;

        public string Name
        {
            get { return _name; }
        }

        public int ID
        {
            get { return _id; }
        }

        public double Weight
        {
            get { return _weight; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        #endregion

        #region Constructor

        public InventoryItem()
        {
            _name = "";
            _id = -1;
            _quantity = -1;
            _weight = 0.0;
        }

        public InventoryItem(string nameValue, int idValue, double weightValue, int quantityValue)
        {
            _name = String.Copy(nameValue);
            _id = idValue;
            _weight = weightValue;
            _quantity = quantityValue;
        }

        public void DeductQuantity(int amount)
        {
            if (_quantity - amount < 0)
            {
                throw new System.Exception("Trying to taking amount bigger than quantity.");
            }
            _quantity -= amount;
        }

        #region Methods

        #endregion

        #endregion
    }
}
