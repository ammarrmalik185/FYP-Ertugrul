using Managers.Scripts;
using Player_Assets.Scripts;
using Story.Scripts.Controllers;
using Story.Scripts.Interfaces_and_Enums;
using UnityEngine;

namespace Story.Scripts.QuestData.MapMaker_Quest.Objectives{
    public class GoToMapMakerArea3 : IObjective{
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

        public GoToMapMakerArea3(){
            objectiveData = new ObjectiveData{
                text = "Explore the Area (3 out of 5)"
            };
        }
        
        public void doInitialSetup(){
            if (isCompleted) return;
            
            director = GameDirector.getInstance();
            npcLocation = GameObject.Find("mapMakerArea3").transform;
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