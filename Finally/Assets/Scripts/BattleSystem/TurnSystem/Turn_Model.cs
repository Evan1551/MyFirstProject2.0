using CommandSystem;
using CharacterSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using CardSystem;
using BattleSystem;
using System.Text;

namespace TurnSystem
{
    public class Turn_Model
    {
        Turn_Model()
        {

        }
        private static Turn_Model turn_Model = null;
        public static Turn_Model Instance
        {
            get
            {
                if (turn_Model == null)
                {
                    turn_Model = new Turn_Model();
                }
                return turn_Model;
            }
        }


        public List<Character> actionSeqence;
        public int turnCounter;
        public int actionCounter;
        public int actionPerTurn;//每回合的action数

       public void ini()
        {
            actionSeqence.Clear();
            actionPerTurn = 0;
            actionCounter = 0;
            turnCounter = 0;
        }
    }
}
