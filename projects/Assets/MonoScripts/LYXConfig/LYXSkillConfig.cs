
using System;
using System.Collections.Generic;

public class LYXSkillConfig
{

    private static List<LYXSkillEntity> _skillEntitys = new List<LYXSkillEntity>
    {
        new LYXSkillEntity()
        {
            SkillId = "1001",
            SkillPath = "bullet 14",
            NextSkillId = "1002"
        },
         
        new LYXSkillEntity(){
            SkillId = "1002",
            SkillPath = "rfx_Straight_IceWall2"
        },
    };

    /// <summary>
    /// 得到技能
    /// </summary>
    /// <param name="skillId">技能id</param>
    /// <returns></returns>
    public static LYXSkillEntity GetSkillEntity(string skillId)
    {
        return _skillEntitys.Find(x => x.SkillId == skillId);
    }

}

