using Managers.Scripts;
using Player_Assets.Scripts;
using Story.Scripts.Controllers;
using Story.Scripts.Interfaces_and_Enums;
using Story.Scripts.Others.Waypoints;
using UnityEngine;

namespace Story.Scripts.QuestData.MainQuest.Objectives{
    public class GoToCastle : IObjective{
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

        public GoToCastle(){
            objectiveData = new ObjectiveData{
                text = "Go to the Castle"
            };
        }
        
        public void doInitialSetup(){
            if (isCompleted) return;
            
            director = GameDirector.getInstance();
            npcLocation = GameObject.Find("CastleOfKnightsPointer").transform;
            waypointToNpc = director.mapManager.spawnWaypoint(npcLocation);
            distanceScale = director.gameManager.distanceScale;
            interactDistance = director.gameManager.interactDistance;
        }

        public void Update(){
            if (isCompleted) return;
            
            if (Vector3.Distance(director.player.transform.position, npcLocation.position) * distanceScale <= interactDistance * 5){
                isCompleted = true;
            }
        }

        public void doExitCondition(){
            director.player.GetComponent<Player>().giveXp(20);
            waypointToNpc.SetActive(false);
        }
    }
}