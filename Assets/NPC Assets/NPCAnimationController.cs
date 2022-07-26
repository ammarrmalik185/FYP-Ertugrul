using System;
using UnityEngine;

namespace NPC_Assets{
    public class NPCAnimationController : MonoBehaviour{
        private NPCAnimationState currentAnimationState;
        private Animator animator;
        
        private static readonly int State = Animator.StringToHash("State");

        private void Start(){
            animator = GetComponent<Animator>();
        }

        public NPCAnimationState CurrentAnimationState{
            get => currentAnimationState;
            set{
                currentAnimationState = value;
                GetComponent<Animator>().SetInteger(State, (int)currentAnimationState);
            }
        }
    }
}