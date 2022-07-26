using System.Collections.Generic;
using UnityEngine;

namespace Skill_Assets.Scripts.Skills{
    public abstract class AbstractSkill: ISkill{
        public abstract string Name{ get; }
        public abstract  string Description{ get; }
        public abstract  string Stats{ get; }
        public abstract  Sprite SkillSprite{ get; }
        public bool IsSkillable => Parent == null || Parent.Skilled; 
        public abstract  bool Skilled{ get; set; }
        public abstract  int Level{ get; set; }
        public abstract  bool Equipped{ get; set; }
        public abstract string DisplayText{ get; set; }

        public abstract  ISkill Parent{ get; set; }
        public abstract  List<ISkill> Children{ get; set; }
        public abstract void activePressed();
        public abstract void Update();

        public void AddChild(ISkill skill){
            skill.Parent = this;
            Children ??= new List<ISkill>();
            Children.Add(skill);
        }
    }
}