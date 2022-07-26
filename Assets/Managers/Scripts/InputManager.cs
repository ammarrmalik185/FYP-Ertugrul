using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Managers.Scripts{
    internal enum playerControlOptions{
        forward, backward, left, right, run, walk, crouch, interact, pick, openInventory,openSkills, attack, altAttack, pause, ability1, ability2, ability3, showTeams, nextQuest
    }
    
    public class InputManager : MonoBehaviour{
        private Dictionary<playerControlOptions, List<KeyCode>> controlMapping;

        public void Start(){
            loadDefaults();
        }
        
        private void loadDefaults(){
            controlMapping = new Dictionary<playerControlOptions, List<KeyCode>>{
                { playerControlOptions.forward , new List<KeyCode>{ KeyCode.W }},
                { playerControlOptions.backward , new List<KeyCode>{ KeyCode.S }},
                { playerControlOptions.left , new List<KeyCode>{ KeyCode.A }},
                { playerControlOptions.right , new List<KeyCode>{ KeyCode.D }},
                { playerControlOptions.run , new List<KeyCode>{ KeyCode.LeftShift }},
                { playerControlOptions.walk , new List<KeyCode>{ KeyCode.LeftAlt }},
                { playerControlOptions.crouch , new List<KeyCode>{ KeyCode.LeftControl }},
                { playerControlOptions.interact , new List<KeyCode>{ KeyCode.E }},
                { playerControlOptions.pick , new List<KeyCode>{ KeyCode.F }},
                { playerControlOptions.openInventory , new List<KeyCode>{ KeyCode.I }},
                { playerControlOptions.openSkills, new List<KeyCode>{ KeyCode.T }},
                { playerControlOptions.attack , new List<KeyCode>{ KeyCode.Mouse0 }},
                { playerControlOptions.altAttack , new List<KeyCode>{ KeyCode.Mouse1 }},
                { playerControlOptions.pause , new List<KeyCode>{ KeyCode.Escape }},
                { playerControlOptions.ability1 , new List<KeyCode>{ KeyCode.Alpha1 }},
                { playerControlOptions.ability2 , new List<KeyCode>{ KeyCode.Alpha2 }},
                { playerControlOptions.ability3 , new List<KeyCode>{ KeyCode.Alpha3 }},
                { playerControlOptions.showTeams , new List<KeyCode>{ KeyCode.BackQuote }},
                { playerControlOptions.nextQuest , new List<KeyCode>{ KeyCode.RightBracket }},
            };
        }

        private void saveConfigToFile(){
            
        }

        private void getConfigFromFile(){
            
        }

        private void changeKey(playerControlOptions optionToChange, List<KeyCode> newConfig){
            controlMapping[optionToChange] = newConfig;
        }
        
        internal bool getKey(playerControlOptions option){
            return controlMapping[option].Any(Input.GetKey);
        }
        internal bool getKeyDown(playerControlOptions option){
            return controlMapping[option].Any(Input.GetKeyDown);
        }
        internal bool getKeyUp(playerControlOptions option){
            return controlMapping[option].Any(Input.GetKeyUp);
        }
    }
}