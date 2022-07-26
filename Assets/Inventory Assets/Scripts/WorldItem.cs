using System;
using Managers.Scripts;
using UnityEngine;

namespace Inventory_Assets.Scripts{
    public class WorldItem : MonoBehaviour{
        private GameDirector director;
        private float distanceScale;
        private float interactDistance;
        public void Start(){
            director = GameDirector.getInstance();
            distanceScale = director.gameManager.distanceScale;
            interactDistance = director.gameManager.interactDistance;
        }

        public void Update(){
            if(Vector3.Distance(transform.position, director.player.transform.position) * distanceScale <= interactDistance){
                director.uiManager.showPickupPrompt();
                if (director.inputManager.getKeyDown(playerControlOptions.pick)){
                    director.player.GetComponent<Inventory>().moveWeaponToBackpackFromWorldSpace(gameObject);
                    director.uiManager.hidePickupPrompt();
                }
            }else{
                director.uiManager.hidePickupPrompt();
            }
        }

        // private void OnCollisionStay(Collision collisionInfo){
        //     if (!collisionInfo.gameObject.CompareTag("Player")) return;
        //     director.uiManager.showPickupPrompt();
        //     if (director.inputManager.getKeyDown(playerControlOptions.pick)){
        //         collisionInfo.gameObject.GetComponent<Inventory>().moveWeaponToBackpackFromWorldSpace(gameObject);
        //     }
        // }
        //
        // private void OnCollisionExit(Collision other){
        //     if (other.gameObject.CompareTag("Player")){
        //         director.uiManager.hidePickupPrompt();
        //     }
        // }
    }
}
