﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace OStronghold
{
    public class Stronghold
    {
        #region Members

        public struct strongholdBuildings
        {
            
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

        public Stronghold()
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
                Character commoner = new Character();
                commoner._id = _stats.currentPopulation;
                commoner._name = "P#" + commoner._id;
                commoner._age = 18;                
                commoner._bodyneeds.HungerState = Consts.hungerState.Full;
                commoner._bodyneeds.SleepState = Consts.sleepState.Awake;
                commoner._health.defineHP(20,0);
                commoner._health.defineStamina(100,1);
                commoner._characterActions.insertItemIntoQueue(new CharacterAction(Consts.characterGeneralActions.Idle, Consts.actionsData[(int)Consts.characterGeneralActions.Idle]._actionPriority, Program._gametime + Consts.actionsData[(int)Consts.characterGeneralActions.Idle]._actionDuration));
                commoner._characterinventory.putInInventory(new Generic.InventoryItem("Food", 1, 0.0, 1));                

                _commoners.Add(_stats.currentPopulation, commoner);                
                _stats.currentPopulation++;                
            }
        }//Populating by giving birth to x people

        public void printPopulation()
        {
            Character person = new Character();
                        
            for (int i = 0; i < _stats.currentPopulation; i++)
            {
                person = ((Character)_commoners[i]);
                //foreach (CharacterAction val in person._characterActions)
                //{
                //    Console.WriteLine(val.Action + " (" + val.Priority + ") ");
                //}
                Console.WriteLine(person._name + " (" + person._health.hp.Current + "|" + person._health.stamina.Current + ") is " + person._characterActions.Peek().Action + " and " + person._bodyneeds.HungerState + " has: " + person._characterinventory.ToString());
            }
        }//Prints in output all the commoner information

        public void activateIdleCommoners()
        {                                                        
        }//Decide what idle commoners should be doing

        #endregion
    }
}