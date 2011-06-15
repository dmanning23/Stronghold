using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OStronghold.Generic
{
    public class MyPriorityQueue : LinkedList<CharacterAction>
    {
        #region Members       
        #endregion

        #region Constructor

        public MyPriorityQueue()
        {               
        }

        #endregion

        #region Methods

        public void insertBeginningofQueue(CharacterAction item)
        {            
            AddFirst(item);
        }

        public void insertItem(CharacterAction item)
        {
            //inserts the item according to priority into the queue. if same priority 

            if (Count == 0)
            {
                AddFirst(item);
            }
            else
            {

            }
        }//inserts item into queue and return the place where it was inserted

        #endregion
    }
}
