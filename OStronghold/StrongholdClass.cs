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
        public static strongholdBuildings _buildings;

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
                commoner._name = "P #" + commoner._id;
                commoner._age = 18;
                commoner._state = Consts.characterState.Idle;
                commoner._bodyneeds.HungerState = Consts.hungerState.Full;                
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
                Console.WriteLine(person._name + " is " + person._state + " (" + person._bodyneeds.HungerState + ") - " + person._bodyneeds.LastAteTime);

            }
        }//Prints in output all the commoner information

        public void activateIdleCommoners()
        {                                  
            CharacterClass person = new CharacterClass();
            for (int x = 0; x < _stats.currentPopulation; x++)
            {
                Console.WriteLine(x);
                person = ((CharacterClass)_commoners[x]);

                if (Program._gametime > (person._bodyneeds.LastAteTime + (int)person._bodyneeds.HungryTimer))
                {
                    person._bodyneeds.HungerState = Consts.hungerState.Hungry;
                }                
            }
        }//Decide what idle commoners should be doing

        #endregion
    }
}
