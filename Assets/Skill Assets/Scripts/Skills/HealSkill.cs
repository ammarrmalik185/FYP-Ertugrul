using System.Collections.Generic;
using Invector.vMelee;
using Managers.Scripts;
using Player_Assets.Scripts;
using UnityEngine;

namespace Skill_Assets.Scripts.Skills{
    public class HealSkill: AbstractSkill{
        private const float heal = 20;
        private const float cooldown = 30;
        
        public override string Name => "Heal";
        public override string Description => "Activate this skill to heal a flat amount";
        public override string Stats => "Heal : " + heal + "%\nCooldown: " + cooldown;
        public override Sprite SkillSprite{ get; }

        public override bool Skilled{ get; set; }
        
        public override int Level{ get; set; }
        public override bool Equipped{ get; set; }

        public override string DisplayText{ get; set; } = "";
        public override ISkill Parent{ get; set; }
        public override List<ISkill> Children{ get; set; }
        
        private float currentCD;
        readonly GameDirector director;

        public HealSkill(){
            director = GameDirector.getInstance();
            SkillSprite = Resources.Load<Sprite>("skills/healSkill/sprite");
        }

        public override void activePressed(){
            if (!Equipped || !Skilled) return;
            director.player.GetComponent<Player>().giveHp(heal);
            currentCD = cooldown;
        }

        public override void Update(){
            if (!(currentCD > 0)) return; 
            currentCD -= Time.deltaTime;
            if (currentCD <= 0){
                currentCD = 0;
                DisplayText = "";
            }else{
                DisplayText = "CD\n" + Mathf.Round(currentCD) + "s";
            }
            
        }
    }
}