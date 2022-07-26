using System.Collections;
using System.Collections.Generic;
using Steamworks;
using TMPro;
using UnityEngine;

public class SteamUIDisplayer : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI uidDisplay;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (SteamManager.Initialized){
            uidDisplay.text = "UID: " + SteamUser.GetSteamID();
        }
        else{
            uidDisplay.text = "UID: Not Found(steam not connected)";
        }
    }
}
