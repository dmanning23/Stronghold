using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OStronghold.CharacterFolder;

namespace OStronghold.GenericFolder
{
    public class MyPriorityQueue : Queue<CharacterAction>
    {
        #region Members       
        #endregion

        #region Constructor

        public MyPriorityQueue()
        {               
        }

        #endregion

        #region Methods

        public void Clone(MyPriorityQueue target)
        {
            this.Clear();
            foreach (CharacterAction val in target)
            {
                this.Enqueue(val);
            }
        }
        
        public void insertItemIntoQueue(CharacterAction newItem)
        {
            MyPriorityQueue temp = new MyPriorityQueue();            
            bool flag = false;

            if (this.Count == 0)
            {
                this.Enqueue(newItem);
            }
            else
            {
                foreach (CharacterAction queueItem in this)
                {
                    if (!flag) //new item is not inserted yet
                    {
                        if (queueItem.Priority >= newItem.Priority)
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

        public bool actionExistsInQueue(Consts.characterGeneralActions targetAction)
        {
            foreach (CharacterAction action in this)
            {
                if (action.Action == targetAction)
                {
                    return true;
                }
            }
            return false;
        }//returns if targetAction is already in queue

        #endregion
    }
}
