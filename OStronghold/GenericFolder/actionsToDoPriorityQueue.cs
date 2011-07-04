using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OStronghold.StrongholdFolder;

namespace OStronghold.GenericFolder
{
    public class actionsToDoPriorityQueue:Queue<ActionsToDo>
    {
        #region Members       
        #endregion

        #region Constructor

        public actionsToDoPriorityQueue()
        {               
        }

        #endregion

        #region Methods

        public void Clone(actionsToDoPriorityQueue target)
        {
            this.Clear();
            foreach (ActionsToDo val in target)
            {
                this.Enqueue(val);
            }
        }

        public void insertItemIntoQueue(ActionsToDo newItem)
        {
            actionsToDoPriorityQueue temp = new actionsToDoPriorityQueue();            
            bool flag = false;
            
            if (this.Count == 0)
            {
                this.Enqueue(newItem);
            }
            else
            {
                foreach (ActionsToDo queueItem in this)
                {
                    if (!flag) //new item is not inserted yet
                    {
                        if (queueItem._priority >= newItem._priority)
                        {
                            temp.Enqueue(newItem);
                            temp.Enqueue(queueItem);
                            flag = true;
                        }
                        else temp.Enqueue(queueItem);
                    }
                    else
                    {
                        temp.Enqueue(queueItem);
                    }
                }
                if (!flag)
                {
                    temp.Enqueue(newItem); //if flag remains false then newItem has highest priority                    
                }
                this.Clone(temp);
            }            
        }//inserts item into queue based on priority

        #endregion
    }
}
