using System;
using System.Collections.Generic;
using LGame.LBehaviour;
using LGame.LCommon;
using Game.LUtils;
using UnityEngine;

/***
 * 
 * 
 * 技能特效
 * 
 * 
 */

public class LYXSkillEffects : LABehaviour
{
    /// <summary>
    ///   args 
    ///  0， 当前技能对象
    ///  1, 侠客对象
    ///  2，目标对象
    ///  3，偏移
    /// </summary>
    /// <param name="args"></param>
    public delegate void SkillFun(params object[] args);

    /// <summary>
    /// 技能函数配置
    /// </summary>
    private static Dictionary<string, SkillFun> _skillFun = new Dictionary<string, SkillFun>();

    /// <summary>
    /// 初始化列表
    /// </summary>
    public static void AddSkillFun()
    {
        _skillFun.Add("Effect1001", Effect1001);
        _skillFun.Add("Effect1002", Effect1002);
    }

    /// <summary>
    /// 得到技能对应的处理函数
    /// </summary>
    /// <param name="funName"></param>
    /// <returns></returns>
    public static SkillFun GetSkillFun(string funName)
    {
        if (_skillFun.Count <= 0) AddSkillFun();
        return _skillFun.ContainsKey(funName) ? _skillFun[funName] : null;
    }

    /// <summary>
    /// 创建特效
    /// </summary>
    /// <param name="effName"> 所以特效都放到 Effects 下, 特效的名字</param>
    /// <param name="parent">特效的父节点</param>
    /// <returns></returns>
    public static LYXSkillEffects CreateEffects(string effName, Transform parent)
    {
        LYXSkillEffects effects = LCSCompHelper.LoadAndInstance<LYXSkillEffects>("Effects/" + effName, parent);
        if (effects == null) return null;
        return effects;
    }

    /// <summary>
    /// 设置特效的旋转
    /// </summary>
    /// <param name="rota"></param>
    public void SetEffectRotation(Vector3 rota)
    {
        transform.localRotation = Quaternion.Euler(rota);
    }

    /// <summary>
    /// 设置特效的大小
    /// </summary>
    /// <param name="doub">特效的倍数</param>
    public void SetEffectScale(float doub)
    {
        transform.localScale = transform.localScale * doub;
    }

    public void SetEffectScale(Vector3 vect)
    {
        transform.localScale = vect;
    }

    /// <summary>
    /// 设置特效的偏移
    /// </summary>
    public void SetEffectOffset(Vector3 offset)
    {
        transform.localPosition = transform.localPosition + offset;
    }

    /// <summary>
    /// 技能对象
    /// </summary>
    public LYXSkillEntity mSkillEntity;

    /// <summary>
    ///  当前发技能的侠客
    /// </summary>
    private LYXHeroObject mHeroObj;

    /// <summary>
    /// 目标侠客
    /// </summary>
    private LYXHeroObject mTargetObj;

    /// <summary>
    ///  特效偏移
    /// </summary>
    private Vector3 mOffset;

    /// <summary>
    /// 位置移动
    /// </summary>
    private TweenPosition mTweenPos;

    /// <summary>
    /// 销毁器
    /// </summary>
    private LCSelfDestroy mDestroy;

    /// <summary>
    /// 得到参数中的技能对象
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static LYXSkillEffects GetSkillForArgs(params object[] args)
    {
        LYXSkillEffects effects = null;
        if (args.Length > 0) effects = (LYXSkillEffects)args[0];
        if (effects == null) return null;
        effects.VerifySkillFunArgs(args);
        return effects;
    }

    /// <summary>
    /// 处理技能函数参数
    /// </summary>
    /// <param name="args"></param>
    public void VerifySkillFunArgs(params object[] args)
    {
        if (args.Length > 1)
            mHeroObj = (LYXHeroObject)args[1];
        if (args.Length > 2)
            mTargetObj = (LYXHeroObject)args[2];
        if (args.Length > 3)
            mOffset = (Vector3)args[3];
    }

    /// <summary>
    /// 清理特效
    /// </summary>
    public void ClearEffect()
    {
        mHeroObj.RemoveEffect(this);
        mHeroObj = null;
        mTargetObj = null;
        mOffset = Vector3.zero;
        mTweenPos = null;
        mSkillEntity = null;
        mDestroy = null;
        GameObject.DestroyObject(gameObject);
    }

    /// <summary>
    /// 暂停 
    /// </summary>
    public void OnPause(bool isPause)
    {
        if (mTweenPos != null) mTweenPos.enabled = !isPause;
        if (mDestroy != null) mDestroy.enabled = !isPause;
    }

    /// <summary>
    /// 技能 1001
    /// </summary>
    /// <param name="args"></param>
    public static void Effect1001(params object[] args)
    {
        LYXSkillEffects effects = GetSkillForArgs(args);
        if (effects == null) return;
        if (effects.mTargetObj == null)
        {
            effects.ClearEffect();
            return;
        }
        effects.SetEffectScale(100f);
        effects.mOffset = new Vector3(0, 15, 0);
        effects.transform.position = effects.mHeroObj.transform.position;
        effects.SetEffectOffset(effects.mOffset + new Vector3(0, 0, 5f));
        effects.mTweenPos = TweenPosition.Begin(effects.gameObject, 1f, Vector3.zero);
        effects.mTweenPos.to = effects.mTargetObj.transform.position + effects.mOffset + new Vector3(0, 0, -5f);
        effects.mTweenPos.AddOnFinished(effects.ClearEffect);
        effects.mTweenPos.Play();
        LCDelayAction.BeginAction(effects.gameObject, 0.6f, delegate()
        {
            if (effects.mSkillEntity != null || !effects.mTargetObj.mHeroEnity.IsHeroDie)
                effects.mTargetObj.CreateEffects(effects.mSkillEntity.NextSkillId, effects.mTargetObj.transform);
        });
    }

    /// <summary>
    /// 技能 1002
    /// </summary>
    /// <param name="args"></param>
    public static void Effect1002(params object[] args)
    {
        LYXSkillEffects effects = GetSkillForArgs(args);
        if (effects == null) return;
        effects.SetEffectRotation(new Vector3(0, 180f, 0));
        effects.SetEffectOffset(new Vector3(0, 4f, 4f));
        effects.SetEffectScale(3);
        effects.mDestroy = LCSelfDestroy.Begin(effects.gameObject, 2f);
    }

}

