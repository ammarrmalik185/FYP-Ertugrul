using System.Collections.Generic;
using Managers.Scripts;
using UnityEngine;

namespace Player_Assets.Scripts{
    internal enum PlayerStates{
        Idle, Moving, Attacking, Falling, Jumping
    }

    internal enum StateModifiers{
        Run, FastRun, Walk, Crouch
    }

    public class AnimationController : MonoBehaviour{
        public GameDirector gameDirector;

        private InputManager inputManager;
        private Transform mainCamera;
    
        private Animator animator;
        private CharacterController characterController;
    
        private Vector3 lookVector;
        private Vector3 movementVector;

        private int turnRate;
        private const float jumpForce = 100f;

        private const float gravity = 0.3f;
        private const float resistancePercent = 0.99f;
        private float gravitationalVelocity;
        private const float terminalVelocity = 0.5f;

        //private PlayerStates currentPlayerState;
        private StateModifiers currentStateModifier;

        private static readonly int PlayerState = Animator.StringToHash("PlayerState");
        private static readonly int StateModifier = Animator.StringToHash("StateModifier");

        private readonly Dictionary<StateModifiers, int> movementSpeeds = new Dictionary<StateModifiers, int>(){
            {StateModifiers.Run, 28},
            {StateModifiers.Walk, 12},
            {StateModifiers.FastRun, 28},
            {StateModifiers.Crouch, 12}
        };

        // Start is called before the first frame update
        private void Start(){
            inputManager = gameDirector.inputManager;
            mainCamera = gameDirector.thirdPersonCamera.transform;
            turnRate = gameDirector.gameManager.turnRate;
            animator = GetComponent<Animator>();
            characterController = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        private void Update(){

            if (!gameDirector.gameManager.canPlayerMove)
                return;
            
            if (inputManager.getKey(playerControlOptions.run)){
                animator.SetInteger(StateModifier, (int)StateModifiers.FastRun);
                currentStateModifier = StateModifiers.FastRun;
            } else if (inputManager.getKey(playerControlOptions.walk)){
                animator.SetInteger(StateModifier, (int)StateModifiers.Walk);
                currentStateModifier = StateModifiers.Walk;
            } else if (inputManager.getKey(playerControlOptions.crouch)){
                animator.SetInteger(StateModifier, (int)StateModifiers.Crouch);
                currentStateModifier = StateModifiers.Crouch;
            } else {
                animator.SetInteger(StateModifier, (int)StateModifiers.Run);
                currentStateModifier = StateModifiers.Run;
            }

            if (characterController.isGrounded){
                handleGroundedMovement();
                gravitationalVelocity = 0;
            }
            else if (!characterController.isGrounded && gravitationalVelocity <= 0.05) {
                handleGroundedMovement();
                applyGravity();
            }
            else{
                handleInAirMovement();
            }

        }

        private void applyGravity(){
            if (gravitationalVelocity <= terminalVelocity){
                gravitationalVelocity += gravity * Time.deltaTime;
            }

            gravitationalVelocity *= resistancePercent;
            characterController.Move(Vector3.down * gravitationalVelocity);
        }
    
        private void handleGroundedMovement(){
            movementVector = new Vector3(
                Input.GetKey(KeyCode.D) ? 1 : (Input.GetKey(KeyCode.A) ? -1 : 0), 
                0f, 
                Input.GetKey(KeyCode.W) ? 1 : (Input.GetKey(KeyCode.S) ? -1 : 0)).normalized;
            
            if (movementVector.magnitude >= 0.1f) {
                lookVector = mainCamera.forward;
                lookVector.y = 0;

                var targetAngle = Mathf.Atan2(movementVector.x, movementVector.y) * Mathf.Rad2Deg
                    * mainCamera.rotation.eulerAngles.z;
                var movementRotation = Quaternion.LookRotation(lookVector).eulerAngles + Quaternion.LookRotation(movementVector).eulerAngles;
                
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(movementRotation), Time.deltaTime * turnRate);

                var movement = Quaternion.Euler(0f, targetAngle, 0f) * transform.forward * (Time.deltaTime * movementSpeeds[currentStateModifier]);
                movement.y = 0;
                characterController.Move(movement);

                animator.SetInteger(PlayerState, (int)PlayerStates.Moving);
                //currentPlayerState = PlayerStates.Moving;
            } else {
                animator.SetInteger(PlayerState, (int)PlayerStates.Idle);
                //currentPlayerState = PlayerStates.Idle;
            }
            // if (Input.GetKeyDown(KeyCode.Space)){
            //     characterController.Move(Vector3.up * (jumpForce * Time.deltaTime));
            //     animator.SetInteger(PlayerState, (int)PlayerStates.Jumping);
            // }
        }

        private void handleInAirMovement(){
            applyGravity();
            //currentPlayerState = PlayerStates.Falling;
            //animator.SetInteger(PlayerState, (int)PlayerStates.Falling);
        
        }
    }
}