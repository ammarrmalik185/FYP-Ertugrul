using System;
using System.Collections.Generic;
using System.Linq;
using Managers.Scripts;
using Player_Assets.Scripts;
using Story.Scripts.Controllers;
using Story.Scripts.Interfaces_and_Enums;
using UnityEngine;

namespace Story.Scripts.QuestData.MainQuest.Objectives{
    public class GetFood : IObjective{
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

        private bool inteactionShown;
        private int totalTargets;
        private int defeatedTargets;
        
        public GetFood(){
            totalTargets = 11;
            objectiveData = new ObjectiveData{
                text = $"Get The Food Containers ({0}/{totalTargets})"
            };
        }
        
        public void doInitialSetup(){
            if (isCompleted) return;
            
            director = GameDirector.getInstance();

            dummyTargets = new List<Tuple<Transform, bool, GameObject>>();
            
            var location = GameObject.Find("FoodContainer").transform;
            dummyTargets.Add(new Tuple<Transform, bool, GameObject>(location, false, director.mapManager.spawnWaypoint(location)));
            
            location = GameObject.Find("FoodContainer (1)").transform;
            dummyTargets.Add(new Tuple<Transform, bool, GameObject>(location, false, director.mapManager.spawnWaypoint(location)));
            
            location = GameObject.Find("FoodContainer (2)").transform;
            dummyTargets.Add(new Tuple<Transform, bool, GameObject>(location, false, director.mapManager.spawnWaypoint(location)));
            
            location = GameObject.Find("FoodContainer (3)").transform;
            dummyTargets.Add(new Tuple<Transform, bool, GameObject>(location, false, director.mapManager.spawnWaypoint(location)));
            
            location = GameObject.Find("FoodContainer (4)").transform;
            dummyTargets.Add(new Tuple<Transform, bool, GameObject>(location, false, director.mapManager.spawnWaypoint(location)));
            
            location = GameObject.Find("FoodContainer (5)").transform;
            dummyTargets.Add(new Tuple<Transform, bool, GameObject>(location, false, director.mapManager.spawnWaypoint(location)));
            
            location = GameObject.Find("FoodContainer (6)").transform;
            dummyTargets.Add(new Tuple<Transform, bool, GameObject>(location, false, director.mapManager.spawnWaypoint(location)));
            
            location = GameObject.Find("FoodContainer (7)").transform;
            dummyTargets.Add(new Tuple<Transform, bool, GameObject>(location, false, director.mapManager.spawnWaypoint(location)));
            
            location = GameObject.Find("FoodContainer (8)").transform;
            dummyTargets.Add(new Tuple<Transform, bool, GameObject>(location, false, director.mapManager.spawnWaypoint(location)));
            
            location = GameObject.Find("FoodContainer (9)").transform;
            dummyTargets.Add(new Tuple<Transform, bool, GameObject>(location, false, director.mapManager.spawnWaypoint(location)));
            
            location = GameObject.Find("FoodContainer (10)").transform;
            dummyTargets.Add(new Tuple<Transform, bool, GameObject>(location, false, director.mapManager.spawnWaypoint(location)));
            
            distanceScale = director.gameManager.distanceScale;
            interactDistance = director.gameManager.interactDistance;
        }

        public void Update(){
            if (isCompleted) return;

            var showInteraction = false;
            for (var i = 0; i < dummyTargets.Count; i++){
                if (!(Vector3.Distance(director.player.transform.position, dummyTargets[i].Item1.transform.position) *
                        distanceScale <= interactDistance)) continue;
                    showInteraction = true;
                if (!director.inputManager.getKeyDown(playerControlOptions.interact)) continue;
                dummyTargets[i].Item3.SetActive(false);
                dummyTargets[i].Item1.gameObject.SetActive(false);
                dummyTargets[i] = new Tuple<Transform, bool, GameObject>(dummyTargets[i].Item1, true, dummyTargets[i].Item3);
            }

            if (showInteraction){
                director.uiManager.showInteractionPrompt();
            }
            else{
                director.uiManager.hideInteractionPrompt();
            }

            defeatedTargets = dummyTargets.Count(dummyTarget => dummyTarget.Item2);
            objectiveData.text = $"Get The Food Containers ({defeatedTargets}/{totalTargets})";
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