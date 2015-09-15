using System.Collections.Generic;
using UnityEngine;
using System.Collections;

/****
 * 
 * 
 * 战斗 ui 界面处理
 * 
 * 
 */

public class LYXUIBattle : LYXBehaviour
{

    /// <summary>
    /// 战斗管理
    /// </summary>
    [System.NonSerialized]
    [HideInInspector]
    public LYXBattleManage mBattleMange;

    /// <summary>
    /// ui 界面侠客
    /// </summary>
    public LYXUIHeroObject[] mUIHero;

    /// <summary>
    /// 战斗侠客数据
    /// </summary>
    private List<LYXHeroEntity> _batHeroList;

    protected override void Start()
    {

    }

    /// <summary>
    /// 初始化 ui 界面
    /// </summary>
    /// <param name="manage">战斗管理</param>
    /// <param name="entitys">自己侠客的数据</param>
    public void InitBattleUI(LYXBattleManage manage, List<LYXHeroEntity> entitys)
    {
        mBattleMange = manage;
        _batHeroList = entitys;
        for (int i = 0, len = mUIHero.Length; i < len; i++)
        {
            mUIHero[i].InitUIPanel(manage, entitys[i]);
        }
    }

    /// <summary>
    ///  ui 界面暂停
    /// </summary>
    /// <param name="isPause"></param>
    public void OnUiPause(bool isPause)
    {
        for (int i = 0, len = mUIHero.Length; i < len; i++)
        {
            mUIHero[i].OnUiPause(isPause);
        }
    }

}
