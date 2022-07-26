using System;
using Managers.Scripts;
using Player_Assets.Scripts;
using Story.Scripts.Controllers;
using Story.Scripts.Interfaces_and_Enums;
using UnityEngine;

namespace Story.Scripts.QuestData.MainQuest.Objectives{
    public class InteractWithWaleedObjective2 : IObjective{
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
        private bool openedPrompt;

        private bool inDialog;
        private readonly string[] dialog;
        private int currentDialog;

        

        public InteractWithWaleedObjective2(){
            objectiveData = new ObjectiveData{
                text = "Go to Waleed and Talk to him"
            };
            dialog = new []{
                "Waleed: So you got the food?",
                "You: Yes here you go",
                "(You give the food to Waleed)",
                "Waleed: 1 sec",
                "(1 second later)",
                "Waleed: Ok ate them all",
                "You: Uh what? that quick?",
                "Waleed: I was hungry",
                "You: OK ok, you didnt answer my question. Why 11?",
                "Waleed : That cause i wanted to eat 11.",
                "(You question your life choices)",
                "You: I am going to go now",
                "Waleed: ok"
            };
        }
        
        public void doInitialSetup(){
            if (isCompleted) return;
            
            director = GameDirector.getInstance();
            npcLocation = GameObject.Find("waleedUmar").transform;
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
                    isCompleted = true;
                }
                else{
                    director.uiManager.showDialogText(dialog[currentDialog]);
                }
            }
            else{
                try{
                    if (Vector3.Distance(director.player.transform.position, npcLocation.position) * distanceScale <=
                        interactDistance){
                        openedPrompt = true;
                        director.uiManager.showInteractionPrompt();
                        if (!director.inputManager.getKeyDown(playerControlOptions.interact)) return;
                        inDialog = true;
                        director.gameManager.canPlayerMove = false;
                        director.uiManager.hideHUD();
                        director.uiManager.hideInteractionPrompt();
                        director.uiManager.showDialogText(dialog[currentDialog]);
                    }
                    else{
                        if (!openedPrompt) return;
                        director.uiManager.hideInteractionPrompt();
                        openedPrompt = false;
                    }
                }
                catch (NullReferenceException){
                    //
                }
            }
        }

        public void doExitCondition(){
            waypointToNpc.SetActive(false);
            
            inDialog = false;
            director.uiManager.hideDialogText();
            director.gameManager.canPlayerMove = true;
            director.uiManager.showHUD();
            
            director.player.GetComponent<Player>().giveXp(20);
        }


    }
}