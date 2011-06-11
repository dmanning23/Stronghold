using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace OStronghold
{
    public class StrongholdClass
    {
        #region Members

        public struct strongholdBuildings
        {
            public int foodStorage;
        }//Stronghold buildings

        public struct strongholdStats 
        {
            public int currentPopulation;            
        } //Statistics for Stronghold

        public strongholdStats _stats; //number of people currently living in Stronghold
        public Hashtable _commoners; //hashtable to hold the commoners themselves
        public strongholdBuildings _buildings;

        #endregion

        #region Constructor

        public StrongholdClass()
        {
            _stats.currentPopulation = 0;            
            _commoners = new Hashtable();
            _buildings.foodStorage = 4;
        }//Constructor

        #endregion

        #region Methods

        public void populate(int numberofCommonersToProduce)
        {
            for (int i = 0; i < numberofCommonersToProduce; i++)
            {
                CharacterClass commoner = new CharacterClass();
                commoner._id = _stats.currentPopulation;
                commoner._name = "P#" + commoner._id;
                commoner._age = 18;
                commoner._hungerflowaction = Consts.characterHungerFlowActions.Idle;
                commoner._bodyneeds.HungerState = Consts.hungerState.Full;
                commoner._health.defineHP(20);
                commoner._health.defineStamina(10);
                _commoners.Add(_stats.currentPopulation, commoner);
                _stats.currentPopulation++;                
            }
        }//Populating by giving birth to x people

        public void printPopulation()
        {
            CharacterClass person = new CharacterClass();
            
            Console.WriteLine("Food storage: " + _buildings.foodStorage);            
            for (int i = 0; i < _stats.currentPopulation; i++)
            {
                person = ((CharacterClass)_commoners[i]);
                Console.WriteLine(person._name + " is " + person._bodyneeds.HungerState + " and " + person._hungerflowaction + " until " + person._currentActionFinishTime);                
            }
        }//Prints in output all the commoner information

        public void activateIdleCommoners()
        {                                                        
        }//Decide what idle commoners should be doing

        #endregion
    }
}
