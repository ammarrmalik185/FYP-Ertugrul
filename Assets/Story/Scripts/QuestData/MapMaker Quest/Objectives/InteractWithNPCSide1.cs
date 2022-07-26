using System;
using Managers.Scripts;
using Player_Assets.Scripts;
using Story.Scripts.Controllers;
using Story.Scripts.Interfaces_and_Enums;
using UnityEngine;

namespace Story.Scripts.QuestData.MapMaker_Quest.Objectives{
    public class InteractWithNPCSideObjective1 : IObjective{
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

        

        public InteractWithNPCSideObjective1(){
            objectiveData = new ObjectiveData{
                text = "Go to the Map maker and Talk to him"
            };
            dialog = new []{
                "The Map Maker: Hello there!",
                "The Map Maker: How are you doing",
                "The Map Maker: So.. I need to make a map of the world, but i am too lazy to explore myself, so you need visit the places and tell me about them",
                "You: And why would i do that?",
                "The Map Maker: Ill give to 100 gold",
                "You: Deal"
            };
        }
        
        public void doInitialSetup(){
            if (isCompleted) return;
            
            director = GameDirector.getInstance();
            npcLocation = GameObject.Find("theMapMaker").transform;
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