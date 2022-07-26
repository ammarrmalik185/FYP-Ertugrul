using System.Collections.Generic;
using UnityEngine;

namespace Skill_Assets.Scripts.Skills{
    public interface ISkill{
        string Name{ get; }
        string Description{ get; }
        string Stats{ get; }
        Sprite SkillSprite{ get; }

        bool IsSkillable{ get; }
        bool Skilled{ get; set; }
        int Level{ get; set; }
        bool Equipped{ get; set; }
        string DisplayText{ get; }


        ISkill Parent{ get; set; }
        List<ISkill> Children{ get; set; }
        
        

        void activePressed();
        void Update();
        void AddChild(ISkill skill);
    }
}