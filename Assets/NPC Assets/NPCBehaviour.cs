using System;
using System.Collections.Generic;
using Managers.Scripts;
using Mirror;
using UnityEngine;
using UnityEngine.AI;

namespace NPC_Assets{
    public class NPCBehaviour : MonoBehaviour{
        // private SyncVar<NPCState> currentState = new SyncVar<NPCState>(NPCState.Idle);
        private NPCState currentState;

        private NPCAnimationController animationController;
        private GameDirector director;
        private NavMeshAgent agent;

        private float distanceScale;
        private float interactDistance;
        private bool openedPrompt;

        public List<Transform> patrolPath;
        private int currentPatrolIndex;
        private float currentPatrolWaitTime;
        private const float maxPatrolWaitTime = 1f;
        private const float checkPointHitDistance = 0.6f;

        public List<string> dialog;
        private int currentDialog;

        private float stateTimer;
        // Start is called before the first frame update
        private void Start(){
            director = GameDirector.getInstance();
            animationController = GetComponent<NPCAnimationController>();
            agent = GetComponent<NavMeshAgent>();
            
            distanceScale = director.gameManager.distanceScale;
            interactDistance = director.gameManager.interactDistance;
            // if (isServer)
            setDefaultState();
        }

        // Update is called once per frame
        private void Update(){
            stateTimer += Time.deltaTime;

            // switch (currentState.Value){
            switch (currentState){
                case NPCState.Idle:
                    HandleState_Idle();
                    break;
                case NPCState.Patrol:
                    HandleState_Patrol();
                    break;
                case NPCState.InDialog:
                    HandleState_InDialog();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleState_Idle(){
            agent.ResetPath();
            listenForDialog();
            animationController.CurrentAnimationState = NPCAnimationState.Standing;
        }

        private void HandleState_Patrol(){
            listenForDialog();
            
            // if (!isServer) return;
            
            if (patrolPath == null){
                ChangeState(NPCState.Idle);
                return;
            }
            if (Vector3.Distance(transform.position, patrolPath[currentPatrolIndex].position) < checkPointHitDistance){
                currentPatrolIndex = ++currentPatrolIndex % patrolPath.Count;
                currentPatrolWaitTime = 0;
            }

            if (currentPatrolWaitTime > maxPatrolWaitTime){
                animationController.CurrentAnimationState = NPCAnimationState.Walking;
                agent.SetDestination(patrolPath[currentPatrolIndex].position);
            }else{
                animationController.CurrentAnimationState = NPCAnimationState.Standing;
                currentPatrolWaitTime += Time.deltaTime;
            }
        }

        private void HandleState_InDialog(){
            agent.ResetPath();

            // if (!isClient) return;

            var lookOnLook = Quaternion.LookRotation(director.player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, 20 * Time.deltaTime);
            
            Debug.Log("Looking for input");
            animationController.CurrentAnimationState = NPCAnimationState.Standing;
            
            if (!director.inputManager.getKeyDown(playerControlOptions.interact) &&
                !director.inputManager.getKeyDown(playerControlOptions.attack)) return;
            
            Debug.Log("Input Received");
            currentDialog += 1;
            if (currentDialog >= dialog.Count){
                setDefaultState();
                director.uiManager.hideDialogText();
                director.gameManager.canPlayerMove = true;
                director.uiManager.showHUD();
                currentDialog = 0;
                Debug.Log("Dialog Ended");
            }else{
                director.uiManager.showDialogText(dialog[currentDialog]);
            }
            
        }

        private void listenForDialog(){
            if (dialog.Count < 1) return;
            if (director.player == null) return;
            
            if (Vector3.Distance(director.player.transform.position, transform.position) * distanceScale <=
                interactDistance){
                openedPrompt = true;
                director.uiManager.showInteractionPrompt();
                if (!director.inputManager.getKeyDown(playerControlOptions.interact)) return;
                ChangeState(NPCState.InDialog);
                director.gameManager.canPlayerMove = false;
                director.uiManager.hideHUD();
                director.uiManager.hideInteractionPrompt();
                director.uiManager.showDialogText(dialog[currentDialog]);
            } else{
                if (!openedPrompt) return;
                director.uiManager.hideInteractionPrompt();
                openedPrompt = false;
            }
        }

        private void setDefaultState(){
            if (patrolPath != null && patrolPath.Count > 1){
                ChangeState(NPCState.Patrol);
            }else{
                ChangeState(NPCState.Idle);
            }
        }

        // [ClientRpc]
        // private void ChangeStateClients(int newState){
        //     stateTimer = 0f;
        //     currentState = (NPCState) newState;
        // }

        // [Command (requiresAuthority = false)]
        private void ChangeState(int newState){
            currentState = (NPCState) newState;
            // ChangeStateClients(newState);
        }

        private void ChangeState(NPCState newState){
            ChangeState((int) newState);
        }
    }
}
