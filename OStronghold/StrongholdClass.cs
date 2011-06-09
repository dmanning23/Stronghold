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
            public int population; 
        } //Statistics for Stronghold

        public strongholdStats _stats; //number of people currently living in Stronghold
        public Hashtable _citizens;

        #endregion

        #region Constructor

        public StrongholdClass()
        {
            _stats.population = 0;
            _citizens = new Hashtable();
        }//Constructor

        #endregion

        #region Methods

        public void populate(int numberofCitizensToProduce)
        {
            for (int i = 0; i < numberofCitizensToProduce; i++)
            {
                CharacterClass citizen = new CharacterClass();
                citizen._name = "Citizen" + _stats.population;
                citizen._state = Consts.characterState.Idle;
                _citizens.Add(_stats.population, citizen);
                _stats.population++;
            }
        }//Populating by giving birth to x people

        #endregion
    }
}
