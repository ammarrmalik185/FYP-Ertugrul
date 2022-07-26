using System;
using Skill_Assets.Scripts.Enums;
using Skill_Assets.Scripts.Skills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Skill_Assets.Scripts{
    public class EquippedSkillManager : MonoBehaviour{
        public Image skillIconContainer;
        public EquipSlots equipSlot;

        private ISkill _skillRef;
        public ISkill SkillRef{
            get => _skillRef;
            set{
                _skillRef = value;
                if (_skillRef != null){
                    skillIconContainer.sprite = _skillRef.SkillSprite;
                }
            }
        }
        public TextMeshProUGUI hotkeyText;
        public TextMeshProUGUI displayText;

        public void Update(){
            if (SkillRef != null) displayText.text = SkillRef.DisplayText;
        }
    }
}