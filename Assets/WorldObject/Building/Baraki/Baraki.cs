using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Baraki : Building
    {
        protected override void Start()
        {
            base.Start();
            actions = new string[] { "Wojownik" , "Lucznik" ,"Mag" };
            //actions = new string[] { "Wojownik" , "Wojownik" , "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik", "Wojownik" };
        }

        public override void PerformAction(string actionToPerform)
        {
            base.PerformAction(actionToPerform);
            CreateUnit(actionToPerform);
        }
        public void SpawnWaveOfUnits()
        {
        CreateUnit("Wojownik");
        }
        protected override bool ShouldMakeDecision()
        {
            return false;
        }
    }

