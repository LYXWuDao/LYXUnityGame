using System;
using System.Collections.Generic;

/***
 * 
 * 
 * 技能实体
 * 
 * 
 */

public class LYXSkillEntity
{

    /// <summary>
    /// 技能id
    /// </summary>
    public string SkillId = string.Empty;

    /// <summary>
    /// 技能的名字
    /// </summary>
    public string SkillName = string.Empty;

    /// <summary>
    /// 技能的路径
    /// </summary>
    public string SkillPath = string.Empty;

    /// <summary>
    /// 技能的类型
    /// 
    /// 1, 原地播放
    /// 2，弹道
    /// 
    /// </summary>
    public int SkillType = 0;

    /// <summary>
    /// 该技能释放时，目标释放技能
    /// </summary>
    public string NextSkillId = string.Empty;

    public LYXSkillEntity CopyEntity()
    {
        return new LYXSkillEntity()
        {
            SkillId = SkillId,
            SkillName = SkillName,
            SkillPath = SkillPath,
            SkillType = SkillType,
            NextSkillId = NextSkillId
        };
    }

}

