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
        
        public struct strongholdStats 
        {
            public int currentPopulation;            
        } //Statistics for Stronghold

        public strongholdStats _stats; //number of people currently living in Stronghold
        public Hashtable _commoners; //hashtable to hold the commoners themselves

        #endregion

        #region Constructor

        public StrongholdClass()
        {
            _stats.currentPopulation = 0;            
            _commoners = new Hashtable();
        }//Constructor

        #endregion

        #region Methods

        public void populate(int numberofCommonersToProduce)
        {
            for (int i = 0; i < numberofCommonersToProduce; i++)
            {
                CharacterClass commoner = new CharacterClass();
                commoner._id = _stats.currentPopulation;
                commoner._name = "Citizen" + commoner._id;
                commoner._age = 18;
                commoner._state = Consts.characterState.Idle;                
                _commoners.Add(_stats.currentPopulation, commoner);
                _stats.currentPopulation++;                
            }
        }//Populating by giving birth to x people

        public void printPopulation()
        {
            CharacterClass person = new CharacterClass(); 
            for (int i = 0; i < _stats.currentPopulation; i++)
            {
                person = ((CharacterClass)_commoners[i]);
                Console.WriteLine(person._name + " (" + person._gender + ") is " + person._state + "(" + person._mindset._moneyScale + "," + person._mindset._fameScale + "," + person._mindset._xpScale + ")");
            }
        }//Prints in output all the commoner information

        public void activateIdleCommoners()
        {
            //Randomly choosing a commoner and makes him Busy
            //TODO: Decide what each commoner should be doing.
            Random rand = new Random((int)DateTime.Now.Ticks);
            int x = rand.Next(0, _stats.currentPopulation);

            ((CharacterClass)_commoners[x])._state = Consts.characterState.Busy;
        }//Decide what idle commoners should be doing

        #endregion
    }
}
