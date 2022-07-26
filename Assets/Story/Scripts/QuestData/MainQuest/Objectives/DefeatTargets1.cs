using System;
using System.Collections.Generic;
using System.Linq;
using Managers.Scripts;
using Player_Assets.Scripts;
using Story.Scripts.Controllers;
using Story.Scripts.Interfaces_and_Enums;
using UnityEngine;

namespace Story.Scripts.QuestData.MainQuest.Objectives{
    public class DefeatTargets1 : IObjective{
        public ObjectiveData objectiveData{ get; }

        public bool Tracked{
            set{
                if (value){
                    dummyTargets.ForEach(dt => {
                        dt.Item3.SetActive(!dt.Item2);
                    });
                }
                else{
                    dummyTargets.ForEach(dt => {
                        dt.Item3.SetActive(false);
                    });
                }
            }
        }

        public bool isCompleted{ get; set; }
        private bool complete;
        private GameDirector director;
        private List<Tuple<Transform, bool, GameObject>> dummyTargets;
        
        private float distanceScale;
        private float interactDistance;

        private int totalTargets;
        private int defeatedTargets;
        public DefeatTargets1(){
            totalTargets = 8;
            objectiveData = new ObjectiveData{
                text = $"Attack the targets ({0}/{totalTargets})"
            };
        }
        
        public void doInitialSetup(){
            if (isCompleted) return;
            
            director = GameDirector.getInstance();

            dummyTargets = new List<Tuple<Transform, bool, GameObject>>();
            
            var location = GameObject.Find("Dummy").transform;
            dummyTargets.Add(new Tuple<Transform, bool, GameObject>(location, false, director.mapManager.spawnWaypoint(location)));
            
            location = GameObject.Find("Dummy (1)").transform;
            dummyTargets.Add(new Tuple<Transform, bool, GameObject>(location, false, director.mapManager.spawnWaypoint(location)));
            
            location = GameObject.Find("Dummy (2)").transform;
            dummyTargets.Add(new Tuple<Transform, bool, GameObject>(location, false, director.mapManager.spawnWaypoint(location)));
            
            location = GameObject.Find("Dummy (3)").transform;
            dummyTargets.Add(new Tuple<Transform, bool, GameObject>(location, false, director.mapManager.spawnWaypoint(location)));
            
            location = GameObject.Find("Dummy (4)").transform;
            dummyTargets.Add(new Tuple<Transform, bool, GameObject>(location, false, director.mapManager.spawnWaypoint(location)));
            
            location = GameObject.Find("Dummy (5)").transform;
            dummyTargets.Add(new Tuple<Transform, bool, GameObject>(location, false, director.mapManager.spawnWaypoint(location)));
            
            location = GameObject.Find("Dummy (6)").transform;
            dummyTargets.Add(new Tuple<Transform, bool, GameObject>(location, false, director.mapManager.spawnWaypoint(location)));
            
            location = GameObject.Find("Dummy (7)").transform;
            dummyTargets.Add(new Tuple<Transform, bool, GameObject>(location, false, director.mapManager.spawnWaypoint(location)));
            
            distanceScale = director.gameManager.distanceScale;
            interactDistance = director.gameManager.interactDistance;
        }

        public void Update(){
            if (isCompleted) return;
            
            for (var i = 0; i < dummyTargets.Count; i++){
                if (!(Vector3.Distance(director.player.transform.position, dummyTargets[i].Item1.transform.position) *
                        distanceScale <= interactDistance)) continue;
                if (!director.inputManager.getKeyDown(playerControlOptions.attack)) continue;
                dummyTargets[i].Item3.SetActive(false);
                dummyTargets[i] = new Tuple<Transform, bool, GameObject>(dummyTargets[i].Item1, true, dummyTargets[i].Item3);
            }

            defeatedTargets = dummyTargets.Count(dummyTarget => dummyTarget.Item2);
            objectiveData.text = $"Attack the targets ({defeatedTargets}/{totalTargets})";
            if (dummyTargets.All(dummyTarget => dummyTarget.Item2)){
                isCompleted = true;
            }
        }
        

        public void doExitCondition(){
            dummyTargets.ForEach(dt => {
                dt.Item3.SetActive(false);
            });
            director.player.GetComponent<Player>().giveXp(20);
        }
    }
}