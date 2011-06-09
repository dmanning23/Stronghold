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
        public Hashtable _citizens; //hashtable to hold the citizens themselves

        #endregion

        #region Constructor

        public StrongholdClass()
        {
            _stats.currentPopulation = 0;            
            _citizens = new Hashtable();
        }//Constructor

        #endregion

        #region Methods

        public void populate(int numberofCitizensToProduce)
        {
            for (int i = 0; i < numberofCitizensToProduce; i++)
            {
                CharacterClass citizen = new CharacterClass();
                citizen._id = _stats.currentPopulation;
                citizen._name = "Citizen" + citizen._id;
                citizen._state = Consts.characterState.Idle;                
                _citizens.Add(_stats.currentPopulation, citizen);
                _stats.currentPopulation++;                
            }
        }//Populating by giving birth to x people

        public void printPopulation()
        {
            for (int i = 0; i < _stats.currentPopulation; i++)
            {
                Console.WriteLine(((CharacterClass)_citizens[i])._name + " (" + ((CharacterClass)_citizens[i])._gender + ") is " + ((CharacterClass)_citizens[i])._state);
            }
        }//Prints in output all the citizen information

        public void activateIdleCitizens()
        {
            //Randomly choosing a citizen and makes him Busy
            //TODO: Decide what each citizen should be doing.
            Random rand = new Random((int)DateTime.Now.Ticks);
            int x = rand.Next(0, _stats.currentPopulation);

            ((CharacterClass)_citizens[x])._state = Consts.characterState.Busy;
        }//Decide what idle citizens should be doing

        #endregion
    }
}
