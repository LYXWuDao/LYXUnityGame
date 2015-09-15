using System;
using System.Collections.Generic;

/****
 * 
 * 
 * 侠客实体数据结构
 * 
 */


/// <summary>
/// 侠客战斗类型
/// </summary>
public enum LYXHeroBatType
{

    None = 0,

    /// <summary>
    /// 远程
    /// </summary>
    Remote = 1,

    /// <summary>
    /// 近战
    /// </summary>
    Infighting = 2

}

public class LYXHeroEntity
{
    /// <summary>
    /// 侠客类型
    /// </summary>
    public LYXHeroBatType HeroBattleType
    {
        get
        {
            LYXHeroBatType btype = LYXHeroBatType.None;
            if (AttackDistance <= 30)
            {
                btype = LYXHeroBatType.Infighting;
            }
            else if (AttackDistance <= 100)
            {
                btype = LYXHeroBatType.Remote;
            }
            return btype;
        }
    }

    /// <summary>
    /// 侠客是否死亡 
    /// 
    /// 血量小于0 侠客死亡
    /// </summary>
    public bool IsHeroDie
    {
        get
        {
            return CurrentBlood <= 0;
        }
    }

    /// <summary>
    /// 侠客 id
    /// </summary>
    public string HeroId = string.Empty;

    /// <summary>
    /// 普通攻击特效
    /// </summary>
    public string AttackSkillId = string.Empty;

    /// <summary>
    /// 怒气技攻击特效
    /// </summary>
    public string AngerSkillId = string.Empty;

    /// <summary>
    /// 侠客名字
    /// </summary>
    public string HeroName = string.Empty;

    /// <summary>
    /// 侠客模型
    /// </summary>
    public string HeroModel = string.Empty;

    /// <summary>
    /// 侠客头像
    /// </summary>
    public string HeroIcon = string.Empty;

    /// <summary>
    /// 最大血量
    /// </summary>
    public float MaxBlood = 0;

    /// <summary>
    /// 当前血量
    /// </summary>
    public float CurrentBlood = 0;

    /// <summary>
    /// 最大怒气
    /// </summary>
    public float MaxAnger = 0;

    /// <summary>
    /// 当前怒气
    /// </summary>
    public float CurrentAnger = 0;

    /// <summary>
    /// 战斗力
    /// </summary>
    public float BattlePower = 0;

    /// <summary>
    /// 攻击伤害
    /// </summary>
    public float AttackHit = 0;

    /// <summary>
    /// 距离停止中间的距离
    /// 
    /// 小于 30 近战
    /// 
    /// 小于100 远程
    /// </summary>
    public float AttackDistance = 0;

    /// <summary>
    /// 侠客位置
    /// 
    /// 从 1 - 6
    /// </summary>
    public float HeroPos = 0;

    /// <summary>
    ///  敌人还在自己
    /// </summary>
    public int FriendAndFoe = 1;

    /// <summary>
    /// 侠客移动速度
    /// </summary>
    public float HeroMoveSpeed = 0;

    /// <summary>
    /// 侠客出手速度
    /// </summary>
    public float HeroShotSpeed = 0;

    public LYXHeroEntity CopyEntity()
    {
        return new LYXHeroEntity()
        {
            HeroId = HeroId,
            AttackSkillId = AttackSkillId,
            AngerSkillId = AngerSkillId,
            HeroName = HeroName,
            HeroModel = HeroModel,
            HeroIcon = HeroIcon,
            MaxBlood = MaxBlood,
            CurrentBlood = CurrentBlood,
            MaxAnger = MaxAnger,
            CurrentAnger = CurrentAnger,
            BattlePower = BattlePower,
            AttackHit = AttackHit,
            AttackDistance = AttackDistance,
            HeroPos = HeroPos,
            FriendAndFoe = FriendAndFoe,
            HeroMoveSpeed = HeroMoveSpeed,
            HeroShotSpeed = HeroShotSpeed
        };
    }

}

