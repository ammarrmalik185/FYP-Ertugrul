using Managers.Scripts;
using Skill_Assets.Scripts.Builders;
using Skill_Assets.Scripts.Enums;
using Skill_Assets.Scripts.Skills;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Skill_Assets.Scripts{
    public class SkillUIDisplay : MonoBehaviour, IPointerClickHandler{
        [SerializeField] private Builders.Skills skillId;
        [SerializeField] private SkillUIDisplay parent;
        private ISkill skillRef;
        private InputManager inputManager;
        private SkillManager skillManager;

        [SerializeField] private Image imageDisplay;
        [SerializeField] private Image backgroundImage;

        private void Awake(){
            skillRef = SkillFactory.GetSkill(skillId);
        }

        // Start is called before the first frame update
        private void Start(){
            if (parent != null)
                parent.skillRef.AddChild(skillRef);
            imageDisplay.sprite = skillRef.SkillSprite;
            var temp = imageDisplay.color;
            temp.a = 1;
            imageDisplay.color = temp;
            inputManager = GameDirector.getInstance().inputManager;
            skillManager = GameDirector.getInstance().player.GetComponent<SkillManager>();
        }

        // Update is called once per frame
        private void Update(){
            setColors();
        }
        
        public void OnPress(){
            if (skillRef == null) return;
            if (skillRef.Skilled){
                if (inputManager.getKey(playerControlOptions.ability1)){
                    skillManager.EquipSkill(skillRef, EquipSlots.Slot1);
                }else if (inputManager.getKey(playerControlOptions.ability2)){
                    skillManager.EquipSkill(skillRef, EquipSlots.Slot2);
                }else if (inputManager.getKey(playerControlOptions.ability3)){
                    skillManager.EquipSkill(skillRef, EquipSlots.Slot3);
                }
            } else if (skillRef.IsSkillable){
                skillManager.TakeSkill(skillRef);
            }
            
        }

        private readonly Color equipped = new Color32(125, 164, 102, 255);
        private readonly Color skilled = new Color32(39, 176, 230, 255);
        private readonly Color gray = new Color32(110, 110, 110, 255);
        private readonly Color normal = new Color32(164, 134, 102, 255);
        private void setColors(){
            if (skillRef != null){
                if (skillRef.Equipped){
                    backgroundImage.color = equipped;
                    imageDisplay.color = Color.white;
                    return;
                }

                if (skillRef.Skilled){
                    backgroundImage.color = skilled;
                    imageDisplay.color = Color.white;
                    return;
                }
                
                if (skillRef.IsSkillable){
                    backgroundImage.color = normal;
                    imageDisplay.color = Color.white;
                    return;
                }
                backgroundImage.color = gray;
                imageDisplay.color = gray;
            }
            else{
                backgroundImage.color = normal;
                imageDisplay.color = Color.white;
            }
        }

        public void OnPointerClick(PointerEventData eventData){
            if (eventData.button == PointerEventData.InputButton.Right){
                skillManager.ShowToolTip(skillRef);
            }
        }
    }
}
