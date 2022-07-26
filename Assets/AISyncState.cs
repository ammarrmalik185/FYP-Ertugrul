using System;
using System.Collections;
using System.Collections.Generic;
using Invector.vCharacterController.AI;
using Mirror;
using UnityEngine;

public class AISyncState : NetworkBehaviour{
    private v_AIController controller;

    private void Start(){
        controller = GetComponent<v_AIController>();
        controller.onReceiveDamage.AddListener(damage => {
            SyncHealth(controller.currentHealth);
        });
    }

    [Command]
    private void SyncHealth(float health){
        SyncHealthClient(health);
    }
    
    [ClientRpc]
    private void SyncHealthClient(float health){
        controller.currentHealth = health;
    }
    
}
