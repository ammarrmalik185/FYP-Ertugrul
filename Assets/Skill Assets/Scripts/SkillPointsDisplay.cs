using Managers.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Skill_Assets.Scripts{
    public class SkillPointsDisplay : MonoBehaviour{
        public Text skillPointsView;
        void Update(){
            skillPointsView.text = GameDirector.getInstance().player.GetComponent<SkillManager>().freeSkillPoints.ToString();
        }
    }
}
