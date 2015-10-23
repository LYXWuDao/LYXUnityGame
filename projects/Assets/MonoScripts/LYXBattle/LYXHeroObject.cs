using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Collections;

/***
 * 
 * 
 * 侠客战斗对象
 * 
 * 
 */

public class LYXHeroObject : LYXBaseBehaviour
{

    /// <summary>
    /// 所有战斗侠客对象类型
    /// </summary>
    [System.NonSerialized]
    [HideInInspector]
    public LYXBattleManage mBatManage;

    /// <summary>
    /// 当前侠客数据
    /// </summary>
    [System.NonSerialized]
    [HideInInspector]
    public LYXHeroEntity mHeroEnity;

    /// <summary>
    /// 目标侠客
    /// </summary>
    public LYXHeroObject mTargetHero;

    /// <summary>
    /// 移动结束点
    /// </summary>
    public Transform mMoveEndPos;

    /// <summary>
    /// 移动速度
    /// </summary>
    public float mMoveSpeed = 20f;

    /// <summary>
    /// 动作控制
    /// </summary>
    private Animation mAnima;

    /// <summary>
    /// 特效集合
    /// </summary>
    private List<LYXSkillEffects> mSkillEffes;

    /// <summary>
    /// 事件列表
    /// </summary>
    private bool _startBattle = false;

    /// <summary>
    /// 当前血条
    /// </summary>
    private LYXHeroBlood mHeroBlood;

    /// <summary>
    /// 控制暂停
    /// </summary>
    private bool _isControlPause = false;

    /// <summary>
    /// 出手的时间
    /// </summary>
    private float _curShortSpeed = 0;

    protected override void Start()
    {
        _startBattle = false;
        _isControlPause = false;
        mSkillEffes = new List<LYXSkillEffects>();
        mAnima = transform.GetComponentInChildren<Animation>();
        InitHeroBloodInfo();
        // 默认播放跑的动作
        PlayAnimation("run");
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (_isControlPause) return;

        if (mHeroEnity.IsHeroDie)
        {
            OnPlayDie();
            return;
        }

        if (!_startBattle) return;

        if (MoveAction())
        {
            if (mAnima.IsPlaying("run")) mAnima.PlayQueued("idle1", QueueMode.PlayNow);
            OnHeroShortSpeed();
        }
        else
        {
            PlayAnimation("run");
        }
    }

    /// <summary>
    /// 计算侠客的出手速度
    /// </summary>
    public void OnHeroShortSpeed()
    {
        if (_curShortSpeed > 0)
        {
            _curShortSpeed -= Time.deltaTime;
            return;
        }
        _curShortSpeed = mHeroEnity.HeroShotSpeed;
        if (mHeroEnity.FriendAndFoe == 1 || mHeroEnity.CurrentAnger < mHeroEnity.MaxAnger)
            OnPlayAttack();
        else
            OnPlayAngerSkill();
    }

    /// <summary>
    /// 侠客移动到指定位置
    /// </summary>
    public void HeroMove()
    {
        _startBattle = true;
        _curShortSpeed = mHeroEnity.HeroShotSpeed;
    }

    /// <summary>
    /// 寻找目标
    /// </summary>
    /// <returns></returns>
    public bool MoveAction()
    {
        mTargetHero = mBatManage.GetNearHero(this);
        if (mTargetHero == null) return true;
        transform.LookAt(mTargetHero.transform);
        if (Vector3.Distance(mTargetHero.transform.position, transform.position) <= mHeroEnity.AttackDistance) return true;
        // 向目标对象移动
        Vector3 dirVect = (mTargetHero.transform.position - transform.position).normalized;
        transform.position = transform.position + dirVect * mHeroEnity.HeroMoveSpeed * Time.deltaTime;
        return false;
    }

    /// <summary>
    /// 侠客达到位置开始攻击
    /// </summary>
    /// <param name="aniName">播放的动作名字</param>
    /// <param name="idty">是否立即播放</param>
    public void PlayAnimation(string aniName, bool idty = true)
    {
        if (string.IsNullOrEmpty(aniName)) return;
        if (mAnima.IsPlaying(aniName) || !idty)
            mAnima.PlayQueued(aniName, QueueMode.CompleteOthers);
        else
            mAnima.PlayQueued(aniName, QueueMode.PlayNow);
    }

    /// <summary>
    /// 初始化血条的信息
    /// </summary>
    public void InitHeroBloodInfo()
    {
        if (mBatManage == null || mBatManage.m2DUIRoot == null || mBatManage.mBlood == null) return;
        LYXHeroBlood heroBlood = LYXHeroBlood.CreateBlood(mBatManage.mBlood, mBatManage.m2DUIRoot.transform);
        if (heroBlood == null) return;
        heroBlood.transform.localRotation = Quaternion.Euler(new Vector3(0, mHeroEnity.FriendAndFoe == 1 ? -90 : 90, 0));
        heroBlood.SetBloodPostion(gameObject, new Vector3(0, 200, 0));
        heroBlood.mHeroEntity = mHeroEnity;
        mHeroBlood = heroBlood;
    }

    /// <summary>
    /// 暂停当前侠客的操作
    /// </summary>
    /// <param name="isPause">
    /// 
    /// true  表示暂停操作
    /// false 表示停止暂停操作
    /// 
    /// </param>
    public void OnPause(bool isPause)
    {
        _isControlPause = isPause;
        // 暂停当前正在播放的动作
        mAnima.enabled = !isPause;

        for (int i = 0, len = mSkillEffes.Count; i < len; i++)
        {
            mSkillEffes[i].OnPause(isPause);
        }
    }

    /// <summary>
    /// 是否需要镜像
    /// </summary>
    /// <returns></returns>
    public float GetHeroMirror()
    {
        if (mHeroEnity == null) return 1;
        return mHeroEnity.FriendAndFoe == 1 ? -1 : 1;
    }

    /// <summary>
    /// 播放普通攻击
    /// </summary>
    public void OnPlayAttack()
    {
        PlayAnimation("attack1");
        PlayAnimation("idle1", false);
        if (mTargetHero == null) return;
        mHeroEnity.CurrentAnger += mHeroEnity.AttackHit * 5;
        mTargetHero.OnPlayHit(mHeroEnity.AttackHit);
        LYXDelayAction.BeginAction(gameObject, 0.3f, delegate()
        {
            CreateEffects(mHeroEnity.AttackSkillId, null);
        });
    }

    /// <summary>
    /// 播放怒气技
    /// </summary>
    public void OnPlayAngerSkill()
    {
        mBatManage.OnBattlePause(true);
        // 播放怒气技
        TweenScale scale = TweenScale.Begin(gameObject, 1f, new Vector3(1.5f * GetHeroMirror(), 1.5f, 1.5f));
        scale.method = UITweener.Method.EaseInOut;
        scale.style = UITweener.Style.Once;
        scale.AddOnFinished(OnTweenFinished);
        scale.Play();
    }

    /// <summary>
    /// 放大结束
    /// </summary>
    public void OnTweenFinished()
    {
        mAnima.enabled = true;
        // 播放大招动画
        PlayAnimation("anger");
        PlayAnimation("idle1", false);
        // 停止暂停
        mHeroEnity.CurrentAnger = 0;
        mBatManage.OnBattlePause(false);
        if (!string.IsNullOrEmpty(mHeroEnity.AngerSkillId))
        {
            LYXSkillEntity entity = LYXSkillConfig.GetSkillEntity(mHeroEnity.AngerSkillId);
            LYXSkillEffects effects = LYXSkillEffects.CreateEffects(entity.SkillPath, transform);
            effects.SetEffectOffset(new Vector3(0, 15, 0));
            //  播放特效
            LYXSkillEffects.SkillFun fun = LYXSkillEffects.GetSkillFun("Effect" + mHeroEnity.AngerSkillId);
            if (fun != null)
            {
                fun(effects, this, mTargetHero, new Vector3(0, 15, 0));
            }
        }
        TweenScale scale = TweenScale.Begin(gameObject, 1f, new Vector3(1f * GetHeroMirror(), 1f, 1f));
        scale.method = UITweener.Method.EaseInOut;
        scale.style = UITweener.Style.Once;
        // 移出完成事件
        EventDelegate evt = new EventDelegate(OnTweenFinished);
        scale.RemoveOnFinished(evt);
        scale.Play();
    }

    /// <summary>
    /// 播放受击动作
    /// </summary>
    /// <param name="hurt">伤害值</param>
    public void OnPlayHit(float hurt)
    {
        mHeroBlood.ShowBlood();
        mHeroEnity.CurrentBlood -= hurt;
        if (mHeroEnity.CurrentBlood <= 0) mHeroEnity.CurrentBlood = 0;
        _curShortSpeed = mHeroEnity.HeroShotSpeed;
        LYXBattleBuffer buffer = LYXBattleBuffer.CreateHarm(mBatManage.m2DUIRoot, gameObject, hurt, new Vector3(0, 150, 0));
        if (buffer != null)
        {
            buffer.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
            buffer.PlayHarmAnimation();
        }
    }

    /// <summary>
    /// 侠客死亡
    /// </summary>
    public void OnPlayDie()
    {
        _isControlPause = true;
        mBatManage.RemoveDieHero(this);
        PlayAnimation("death");
        mHeroBlood.ShowBlood();
        SkinnedMeshRenderer render = transform.GetComponentInChildren<SkinnedMeshRenderer>();
        if (render == null) return;
        AnimationState state = mAnima["death"];
        TweenColor tcol = TweenColor.Begin(render.gameObject, state.clip.length + 0.1f, new Color(120 / 255f, 120 / 255f, 120 / 255f, 0));
        tcol.method = UITweener.Method.EaseIn;
        tcol.style = UITweener.Style.Once;
        tcol.AddOnFinished(delegate()
        {
            GameObject.DestroyObject(gameObject);
        });
        tcol.Play();
    }

    /// <summary>
    /// 创建特效
    /// </summary>
    /// <param name="effectId">特效id</param>
    /// <param name="parent">特效的父节点</param>
    public void CreateEffects(string effectId, Transform parent)
    {
        if (string.IsNullOrEmpty(effectId)) return;
        LYXSkillEntity entity = LYXSkillConfig.GetSkillEntity(effectId);
        if (entity == null)
        {
            Debug.LogError("你创建的特效不存在！！！");
            return;
        }
        LYXSkillEffects effects = LYXSkillEffects.CreateEffects(entity.SkillPath, parent);
        if (mHeroEnity.FriendAndFoe == 2)
            effects.SetEffectRotation(new Vector3(0, -180, 0));
        effects.mSkillEntity = entity;
        // 得到播放特效的函数
        LYXSkillEffects.SkillFun fun = LYXSkillEffects.GetSkillFun("Effect" + effectId);
        if (fun != null)
        {
            // 特效对象，当前播放特效的侠客，目标侠客，
            fun(effects, this, mTargetHero);
        }
        // 增加到特效管理
        mSkillEffes.Add(effects);
    }

    /// <summary>
    /// 移出特效
    /// </summary>
    /// <param name="eff"></param>
    public void RemoveEffect(LYXSkillEffects eff)
    {
        if (mSkillEffes == null || mSkillEffes.Count <= 0) return;
        mSkillEffes.Remove(eff);
    }
}
