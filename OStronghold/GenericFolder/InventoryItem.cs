using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold.GenericFolder
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

        public InventoryItem(InventoryItem target)
        {            
            this._name = String.Copy(target.Name);
            this._id = target.ID;
            this._weight = target.Weight;
            this._quantity = target.Quantity;            
        }

        #region Methods

        public void DeductQuantity(int amount)
        {
            Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name);
            if (_quantity - amount < 0)
            {
                Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name);
                throw new System.Exception("Trying to taking amount bigger than quantity.");
            }
            _quantity -= amount;
            Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        public string getInventoryItemString()
        {
            Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name);
            string result = "";                       
            result += "Item ID: " + this.ID + "\n";
            result += "Item name: " + this.Name + "\n";
            result += "Item weight: " + this.Weight + "\n";
            result += "Item quantity: " + this.Quantity + "\n";
            Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().Name);
            return result;
        }

        #endregion

        #endregion
    }
}
