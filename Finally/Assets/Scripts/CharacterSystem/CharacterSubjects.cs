using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CharacterSystem
{
    public class CharacterSubjects 
    {
        internal Subject actionStart { get; set; }
        internal Subject actionEnd { get; set; }
        internal Subject turnStart { get; set; }
        internal Subject turnEnd { get; set; }
        internal Subject battleStart { get; set; }
        internal Subject battleEnd { get; set; }
        internal Subject takeDamage { get; set; }
        internal Subject dealDamage { get; set; }
        internal Subject die { get; set; } 
        internal Subject move { get; set; }
        
        

        public CharacterSubjects()
        {
            actionStart = new Subject();
            actionEnd = new Subject();
            turnStart = new Subject();
            turnEnd = new Subject();
            battleStart = new Subject();
            battleEnd = new Subject();
            takeDamage = new Subject();
            dealDamage = new Subject();
            die = new Subject();
            move = new Subject();
        }
    }
}

