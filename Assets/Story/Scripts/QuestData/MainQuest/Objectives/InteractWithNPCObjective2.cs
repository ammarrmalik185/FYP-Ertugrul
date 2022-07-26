using System;
using Managers.Scripts;
using Player_Assets.Scripts;
using Story.Scripts.Controllers;
using Story.Scripts.Interfaces_and_Enums;
using UnityEngine;

namespace Story.Scripts.QuestData.MainQuest.Objectives{
    public class InteractWithNPCObjective2 : IObjective{
        public ObjectiveData objectiveData{ get; }
        public bool Tracked{
            set => waypointToNpc.SetActive(value);
        }
        private GameDirector director;
        public bool isCompleted{ get; set; }

        private Transform npcLocation;
        private GameObject waypointToNpc;
        private float distanceScale;
        private float interactDistance;

        private bool inDialog;
        private readonly string[] dialog;
        private int currentDialog;

        

        public InteractWithNPCObjective2(){
            objectiveData = new ObjectiveData{
                text = "Go back to the blacksmith"
            };
            dialog = new []{
                "Blacksmith: Nice going",
                "Blacksmith: Now that you have learned the basics",
                "Blacksmith: You can start your journey for real",
            };
        }
        
        public void doInitialSetup(){
            if (isCompleted) return;
            
            director = GameDirector.getInstance();
            npcLocation = GameObject.Find("theBlacksmith").transform;
            waypointToNpc = director.mapManager.spawnWaypoint(npcLocation);
            distanceScale = director.gameManager.distanceScale;
            interactDistance = director.gameManager.interactDistance;
        }

        public void Update(){
            if (isCompleted) return;

            if (inDialog){
                if (!director.inputManager.getKeyDown(playerControlOptions.interact) &&
                    !director.inputManager.getKeyDown(playerControlOptions.attack)) return;
                currentDialog += 1;
                if (currentDialog >= dialog.Length){
                    inDialog = false;
                    director.uiManager.hideDialogText();
                    director.gameManager.canPlayerMove = true;
                    director.uiManager.showHUD();
                    isCompleted = true;
                }
                else{
                    director.uiManager.showDialogText(dialog[currentDialog]);
                }
            }
            else{
                if ((Vector3.Distance(director.player.transform.position, npcLocation.position) * distanceScale) <=
                    interactDistance){
                    director.uiManager.showInteractionPrompt();
                    if (!director.inputManager.getKeyDown(playerControlOptions.interact)) return;
                    inDialog = true;
                    director.gameManager.canPlayerMove = false;
                    director.uiManager.hideHUD();
                    director.uiManager.hideInteractionPrompt();
                    director.uiManager.showDialogText(dialog[currentDialog]);
                }
                else{
                    director.uiManager.hideInteractionPrompt();
                }
            }
        }

        public void doExitCondition(){
            waypointToNpc.SetActive(false);
            director.player.GetComponent<Player>().giveXp(20);
        }


    }
}