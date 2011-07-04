using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OStronghold.GenericFolder;

namespace OStronghold.StrongholdFolder
{
    public enum subobject { Capacity, Existence };
    public enum behaviour { Full, Empty, Negative_slope, Positive_slope };
    public enum action { Build };
    public enum priority { Highest, High, Normal, Low, Lowest };

    public class Phenomenon
    {
        public int _ownerID;
        public int _objectID;
        public subobject _subobject;
        public behaviour _behaviour;

        public Phenomenon(int targetOwnerID, int targetObjectID, subobject targetSubObject, behaviour targetBehaviour)
        {
            _ownerID = targetOwnerID;
            _objectID = targetObjectID;
            _subobject = targetSubObject;
            _behaviour = targetBehaviour;
        }
    }

    public class ActionsToDo
    {        
        public action _action;
        public int _objectTypeID;
        public priority _priority;

        public ActionsToDo(action targetAction, int targetObjectTypeID, priority targetPriority)
        {
            _action = targetAction;
            _priority = targetPriority;
            _objectTypeID = targetObjectTypeID;
        }
    }

    public class DecisionMaker
    {
        public LinkedList<Phenomenon> listOfPhenomenons;
        public actionsToDoPriorityQueue listOfActionsToDo;

        public DecisionMaker()
        {
            listOfPhenomenons = new LinkedList<Phenomenon>();
            listOfActionsToDo = new actionsToDoPriorityQueue();
        }

        private ActionsToDo analyzePhenomenon(Phenomenon targetPhenomenon)
        {
            ActionsToDo result = null;
            
            if (targetPhenomenon._subobject == subobject.Existence && targetPhenomenon._behaviour == behaviour.Empty)
            {
                result = new ActionsToDo(action.Build, targetPhenomenon._objectID, priority.Highest);
            }//object does not exists, assuming need to build
            else if (targetPhenomenon._subobject == subobject.Capacity && targetPhenomenon._behaviour == behaviour.Full)
            {
                result = new ActionsToDo(action.Build, targetPhenomenon._objectID, priority.Highest);
            }//capacity is full, assuming need to build
            else if (targetPhenomenon._subobject == subobject.Capacity && targetPhenomenon._behaviour == behaviour.Empty)
            {
                if (targetPhenomenon._objectID == Consts.stronghold_treasury)
                {
                    Program._aStronghold.Treasury.depositGold(100); //solution in the meantime                      
                }//treasury is running out of money
            }//capacity is empty

            return result;
        }

        public void insertPhenomenon(int targetOwnerID, int targetObjectTypeID, subobject targetSubObject, behaviour targetBehaviour)
        {
            Phenomenon targetPhenomenon = new Phenomenon(targetOwnerID, targetObjectTypeID, targetSubObject, targetBehaviour);
            listOfPhenomenons.AddLast(targetPhenomenon);

            ActionsToDo toDo = analyzePhenomenon(targetPhenomenon);

            if (toDo != null)
            {
                listOfActionsToDo.insertItemIntoQueue(toDo);
            }//only if there is something to do.
        }

        public bool phenomenonExists(Phenomenon targetPhenomenon)
        {
            foreach (Phenomenon ph in this.listOfPhenomenons)
            {
                if (ph._behaviour == targetPhenomenon._behaviour &&
                    ph._objectID == targetPhenomenon._objectID &&
                    ph._ownerID == targetPhenomenon._ownerID &&
                    ph._subobject == targetPhenomenon._subobject)
                {
                    return true;
                }
            }
            return false;
        }//checks if phenomenon exists already in the list of phenomenons
    }
}
