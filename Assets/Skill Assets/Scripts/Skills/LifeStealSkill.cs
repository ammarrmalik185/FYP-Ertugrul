using System.Collections.Generic;
using Invector.vMelee;
using Managers.Scripts;
using Player_Assets.Scripts;
using UnityEngine;

namespace Skill_Assets.Scripts.Skills{
    public class LifeStealSkill : AbstractSkill{
        
        private const float duration = 5;
        private const float healPercent = 20;
        private const float cooldown = 30;
        
        public override string Name => "Life Steal";
        public override string Description => "Activate this skill to heal when you attack an enemy for a percentage of the damage you deal";
        public override string Stats => "Heal Percent: " + healPercent + "%\nDuration: " + duration + "s\nCooldown: " + cooldown;
        public override Sprite SkillSprite{ get; }

        public override bool Skilled{ get; set; }
        public override int Level{ get; set; }
        public override bool Equipped{ get; set; }

        public override string DisplayText{ get; set; } = "";
        public override ISkill Parent{ get; set; }
        public override List<ISkill> Children{ get; set; }

        private readonly vMeleeManager meleeManager;
        private bool activeEnabled;
        private float remainingTime;
        private float currentCD;
        readonly GameDirector director;

        public LifeStealSkill(){
            director = GameDirector.getInstance();
            meleeManager = director.player.GetComponent<vMeleeManager>();
            SkillSprite = Resources.Load<Sprite>("skills/lifeStealSkill/sprite");
        }

        public override void activePressed(){
            if (!Equipped || !Skilled) return;
            if (activeEnabled) return;
            activeEnabled = true;
            remainingTime = duration;
            meleeManager.onDamageHit.AddListener(DamageDealt);
        }

        public override void Update(){
            if (activeEnabled){
                DisplayText = "AC\n" + Mathf.Round(remainingTime) + "s";
                remainingTime -= Time.deltaTime;
                if (remainingTime <= 0){
                    remainingTime = 0;
                    activeEnabled = false;
                    currentCD = cooldown;
                    meleeManager.onDamageHit.RemoveListener(DamageDealt);
                }
            }

            if (!(currentCD > 0)) return; 
            currentCD -= Time.deltaTime;
            if (currentCD <= 0){
                currentCD = 0;
                DisplayText = "";
            }else{
                DisplayText = "CD\n" + Mathf.Round(currentCD) + "s";
            }
            
        }

        private void DamageDealt(vHitInfo hitInfo){
            director.player.GetComponent<Player>().giveHp(hitInfo.attackObject.damage.damageValue * healPercent / 100);
        }
    }
}