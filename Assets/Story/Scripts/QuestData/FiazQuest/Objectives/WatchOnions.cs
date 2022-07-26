using System;
using Managers.Scripts;
using Player_Assets.Scripts;
using Story.Scripts.Controllers;
using Story.Scripts.Interfaces_and_Enums;
using UnityEngine;

namespace Story.Scripts.QuestData.FiazQuest.Objectives{
    public class WatchOnions : IObjective{
        public ObjectiveData objectiveData{ get; }
        public bool Tracked{
            set => waypointToNpc.SetActive(value);
        }
        public bool isCompleted{ get; set; }
        private GameDirector director;
        
        private Transform npcLocation;
        private GameObject waypointToNpc;
        private float distanceScale;
        private float interactDistance;

        private float timePassed;

        public WatchOnions(){
            objectiveData = new ObjectiveData{
                text = "Watch the onions for 5 seconds"
            };
        }
        
        public void doInitialSetup(){
            if (isCompleted) return;
            
            director = GameDirector.getInstance();
            npcLocation = GameObject.Find("fiazOnions").transform;
            waypointToNpc = director.mapManager.spawnWaypoint(npcLocation);
            distanceScale = director.gameManager.distanceScale;
            interactDistance = director.gameManager.interactDistance;
        }

        public void Update(){
            if (isCompleted) return;
            
            if (Vector3.Distance(director.player.transform.position, npcLocation.position) * distanceScale <= interactDistance * 5){
                timePassed += Time.deltaTime;
                if (timePassed > 5){
                    isCompleted = true;
                }

                objectiveData.text = $"Watch the onions for 5 seconds, ({Math.Round(timePassed)}/5)";
            }
        }

        public void doExitCondition(){
            director.player.GetComponent<Player>().giveXp(20);
            waypointToNpc.SetActive(false);
        }
    }
}