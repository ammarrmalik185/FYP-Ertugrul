using Skill_Assets.Scripts.Skills;

namespace Skill_Assets.Scripts.Builders{
    public class SkillFactory{
        public static ISkill GetSkill(Skills skillId){
            return skillId switch{
                Skills.LifeStealSkill => new LifeStealSkill(),
                Skills.HealSkill => new HealSkill(),
                Skills.VisionSkill => new LifeStealSkill(),
                _ => null
            };
        }
    }
}