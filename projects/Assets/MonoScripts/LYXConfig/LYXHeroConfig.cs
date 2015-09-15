using System;
using System.Collections.Generic;

/***
 * 
 *  侠客配置
 * 
 */

public class LYXHeroConfig
{

    private static List<LYXHeroEntity> _heroEntitys = new List<LYXHeroEntity>
    {

        new LYXHeroEntity()
        {
            HeroId = Guid.NewGuid().ToString(),
            HeroName = "恶风",
            HeroModel = "annie",
            HeroPos = 1,
            CurrentBlood = 100,
            MaxBlood = 100,
            MaxAnger = 100,
            HeroMoveSpeed = 20,
            AttackDistance = 30,
            AttackHit = 1,
            HeroShotSpeed = 2
        },

        new LYXHeroEntity()
        {
            HeroId = Guid.NewGuid().ToString(),
            HeroName = "恶风",
            HeroModel = "ashe",
            HeroPos = 2,
            MaxBlood = 100,
            HeroMoveSpeed = 20,
            CurrentBlood = 100,
            MaxAnger = 100,
            AttackDistance = 90,
            AttackHit = 1,
            HeroShotSpeed = 4,
            AttackSkillId = "1001"
        },

        new LYXHeroEntity()
        {
            HeroId = Guid.NewGuid().ToString(),
            HeroName = "恶风",
            HeroModel = "annie",
            HeroPos = 3,
            MaxBlood = 100,
            HeroMoveSpeed = 20,
            CurrentBlood = 100,
            MaxAnger = 100,
            AttackHit = 1.5f,
            AttackDistance = 30,
            HeroShotSpeed = 2
        },

        new LYXHeroEntity()
        {
            HeroId = Guid.NewGuid().ToString(),
            HeroName = "恶风",
            HeroModel = "ashe",
            HeroPos = 4,
            MaxBlood = 100,
            HeroMoveSpeed = 20,
            CurrentBlood = 100,
            MaxAnger = 100,
            AttackDistance = 100,
            AttackHit = 1,
            HeroShotSpeed = 5
        },

        new LYXHeroEntity()
        {
            HeroId = Guid.NewGuid().ToString(),
            HeroName = "恶风",
            HeroModel = "Pantheon",
            HeroPos = 5,
            MaxBlood = 100,
            HeroMoveSpeed = 20,
            CurrentBlood = 100,
            MaxAnger = 100,
            AttackDistance = 20,
            AttackHit = 2,
            HeroShotSpeed = 1
        },

        new LYXHeroEntity()
        {
            HeroId = Guid.NewGuid().ToString(),
            HeroName = "恶风",
            HeroModel = "ashe",
            HeroPos = 6,
            MaxBlood = 100,
            HeroMoveSpeed = 20,
            CurrentBlood = 100,
            MaxAnger = 100,
            AttackDistance = 90,
            AttackHit = 1,
            HeroShotSpeed = 3
        }

    };

    /// <summary>
    /// 增加侠客
    /// </summary>
    /// <param name="entity">增加的侠客</param>
    public static void AddHeroEntity(LYXHeroEntity entity)
    {
        if (entity == null) return;
        if (_heroEntitys == null)
        {
            _heroEntitys = new List<LYXHeroEntity>();
        }
        _heroEntitys.Add(entity);
    }

    /// <summary>
    /// 删除侠客
    /// </summary>
    /// <param name="entity"></param>
    public static void DeleteHeroEntity(LYXHeroEntity entity)
    {
        if (entity == null) return;
        _heroEntitys.Remove(entity);
    }

    /// <summary>
    /// 删除侠客
    /// </summary>
    /// <param name="heroId"></param>
    public static void DeleteHeroEntity(string heroId)
    {
        if (string.IsNullOrEmpty(heroId)) return;
        _heroEntitys.RemoveAll(x => x.HeroId == heroId);
    }

    /// <summary>
    /// 修改侠客数据
    /// </summary>
    /// <param name="entity"></param>
    public static void ModifyHeroEntity(LYXHeroEntity entity)
    {
        if (entity == null || string.IsNullOrEmpty(entity.HeroId)) return;
        int index = _heroEntitys.FindIndex(x => x.HeroId == entity.HeroId);
        if (index < 0 || index >= _heroEntitys.Count) return;
        _heroEntitys[index] = entity;
    }

    /// <summary>
    /// 得到侠客的信息
    /// </summary>
    /// <param name="heroId">侠客id</param>
    /// <returns></returns>
    public static LYXHeroEntity GetHeroEntityById(string heroId)
    {
        if (string.IsNullOrEmpty(heroId)) return null;
        return _heroEntitys.Find(x => x.HeroId == heroId);
    }

    /// <summary>
    /// 获得自己方侠客数据
    /// </summary>
    /// <returns></returns>
    public static List<LYXHeroEntity> GetOwnHeroEntity()
    {
        List<LYXHeroEntity> result = new List<LYXHeroEntity>();

        for (int i = 0, len = _heroEntitys.Count; i < len && i < 6; i++)
        {
            LYXHeroEntity item = _heroEntitys[i].CopyEntity();
            item.FriendAndFoe = 1;
            result.Add(item);
        }
        return result;
    }

    /// <summary>
    /// 获得敌人的侠客数据
    /// </summary>
    /// <returns></returns>
    public static List<LYXHeroEntity> GetEnemyHeroEntity()
    {
        List<LYXHeroEntity> result = new List<LYXHeroEntity>();
        for (int i = 0, len = _heroEntitys.Count; i < len && i < 6; i++)
        {
            LYXHeroEntity item = _heroEntitys[i].CopyEntity();
            item.FriendAndFoe = 2;
            result.Add(item);
        }
        return result;
    }

}

