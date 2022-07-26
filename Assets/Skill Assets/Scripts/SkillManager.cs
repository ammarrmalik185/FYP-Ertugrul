using System;
using System.Collections.Generic;
using System.Linq;
using Managers.Scripts;
using Skill_Assets.Scripts.Enums;
using Skill_Assets.Scripts.Skills;
using UnityEngine;

namespace Skill_Assets.Scripts{
    public class SkillManager : MonoBehaviour{
        private Dictionary<EquipSlots, ISkill> equippedSkills;
        private InputManager inputManager;
        public int freeSkillPoints;

        public SkillTooltip skillTooltip;

        private List<EquippedSkillManager> equippedSkillUIManagers;
        public List<ISkill> takenSkills;

        private void Start(){
            freeSkillPoints = 3;
            inputManager = GameDirector.getInstance().inputManager;
            equippedSkills = new Dictionary<EquipSlots, ISkill>();
            takenSkills = new List<ISkill>();
            equippedSkillUIManagers = GameDirector.getInstance().uiManager.equippedSkillUIManagers;
            skillTooltip = GameDirector.getInstance().uiManager.skillTooltip.GetComponent<SkillTooltip>();
            // EquipSkill(new LifeStealSkill(), EquipSlots.Slot1);
            // EquipSkill(new HealSkill(), EquipSlots.Slot2);
        }

        private void Update(){
            foreach (var equippedSkill in equippedSkills){
                equippedSkill.Value.Update();
                if (inputManager.getKeyDown(mapSlotToOption(equippedSkill.Key))){
                    equippedSkill.Value.activePressed();
                }
            }
        }

        public void ShowToolTip(ISkill skill){
            skillTooltip.gameObject.SetActive(true);
            skillTooltip.Show();
            skillTooltip.SkillRef = skill;
        }
        
        public void EquipSkill(ISkill skill, EquipSlots slot){
            if (skill.Equipped){
                var oldSlot = equippedSkills.First(item => item.Value == skill);
                if (equippedSkills.ContainsKey(oldSlot.Key))
                    equippedSkills.Remove(oldSlot.Key);
            }
            
            if (equippedSkills.ContainsKey(slot)){
                equippedSkills[slot].Equipped = false;
                equippedSkills.Remove(slot);
            }
            equippedSkills.Add(slot, skill);
            skill.Equipped = true;
            skill.Skilled = true;
            equippedSkillUIManagers[mapSlotToIndex(slot)].SkillRef = skill;
        }

        public void TakeSkill(ISkill skill){
            if (freeSkillPoints > 0){
                skill.Skilled = true;
                freeSkillPoints--;
                takenSkills.Add(skill);
            }
        }
        
        private static playerControlOptions mapSlotToOption(EquipSlots equipSlot){
            return equipSlot switch{
                EquipSlots.Slot1 => playerControlOptions.ability1,
                EquipSlots.Slot2 => playerControlOptions.ability2,
                EquipSlots.Slot3 => playerControlOptions.ability3,
                _ => throw new ArgumentOutOfRangeException(nameof(equipSlot), equipSlot, null)
            };
        }
        private static int mapSlotToIndex(EquipSlots equipSlot){
            return equipSlot switch{
                EquipSlots.Slot1 => 0,
                EquipSlots.Slot2 => 1,
                EquipSlots.Slot3 => 2,
                _ => throw new ArgumentOutOfRangeException(nameof(equipSlot), equipSlot, null)
            };
        }
    }
}