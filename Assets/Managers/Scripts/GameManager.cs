using UnityEngine;

namespace Managers.Scripts{
    public class GameManager : MonoBehaviour{
        public float distanceScale = 0.5f;
        public float interactDistance = 1f;
        public int turnRate = 10;
        public bool canPlayerMove{ get; set; } = true;
        
        public bool isInventoryOpen{ get; set; }
        public bool isPauseMenuOpen{ get; set; }
        public bool isSkillMenuOpen{ get; set; }
    }
}