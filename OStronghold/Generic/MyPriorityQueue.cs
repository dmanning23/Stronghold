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
            LinkedListNode<CharacterAction> existElement;            
            LinkedListNode<CharacterAction> newElement;
            //inserts the item according to priority into the queue. if same priority then it will be the first of all the same priority #
            if (Count == 0)
            {
                AddFirst(item);
            }
            else if (this.ElementAt(Count - 1).Priority < item.Priority)
            {
                AddLast(item);
            }
            else
            {
                for (int i = 0; i < this.Count; i++)
                {
                    existElement = new LinkedListNode<CharacterAction>(this.ElementAt(i));

                    if (this.ElementAt(i).Priority >= item.Priority)
                    {
                        this.AddBefore(existElement, item);

                    }
                }
            }
        }//inserts item into queue

        #endregion
    }
}
