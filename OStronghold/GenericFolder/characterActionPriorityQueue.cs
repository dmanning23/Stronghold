using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OStronghold.CharacterFolder;

namespace OStronghold.GenericFolder
{
    public class characterActionPriorityQueue : Queue<CharacterAction>
    {
        #region Members       
        #endregion

        #region Constructor

        public characterActionPriorityQueue()
        {               
        }

        #endregion

        #region Methods

        public void Clone(characterActionPriorityQueue target)
        {
            Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            this.Clear();
            foreach (CharacterAction val in target)
            {
                this.Enqueue(val);
            }
            Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
        
        public void insertItemIntoQueue(CharacterAction newItem)
        {
            Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            characterActionPriorityQueue temp = new characterActionPriorityQueue();            
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
            Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }//inserts item into queue based on priority

        public bool actionExistsInQueue(Consts.characterGeneralActions targetAction)
        {
            Consts.writeEnteringMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            foreach (CharacterAction action in this)
            {
                if (action.Action == targetAction)
                {
                    Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
                    return true;
                }
            }
            Consts.writeExitingMethodToDebugLog(System.Reflection.MethodBase.GetCurrentMethod().ReflectedType + "." + System.Reflection.MethodBase.GetCurrentMethod().Name);
            return false;
        }//returns if targetAction is already in queue

        #endregion
    }
}
