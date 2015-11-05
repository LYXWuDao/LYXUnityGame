using System.Collections.Generic;
using Game.LYX.Behaviour;
using Game.LYX.Common;
using UnityEngine;

/***
 * 
 * 
 * 战斗
 * 
 * 
 */

public class LYXBattleManage : LYXBaseBehaviour
{

    /// <summary>
    /// 战斗ui界面
    /// </summary>
    public LYXUIBattle mUIBattle;

    /// <summary>
    /// 移动结束位置
    /// </summary>
    public Transform mEndMovePos;

    /// <summary>
    /// 战斗 阵型位置
    /// </summary>
    public GameObject mBattleFormation;

    /// <summary>
    /// 自己方的战斗根节点
    /// </summary>
    public Transform mOwnBattleTrans;

    /// <summary>
    /// 敌方的战斗根节点
    /// </summary>
    public Transform mEnemyBattleTrans;

    /// <summary>
    /// 3d摄像机上ui根节点
    /// </summary>
    public GameObject m2DUIRoot;

    /// <summary>
    /// 血条和怒气
    /// </summary>
    public GameObject mBlood;

    /// <summary>
    /// 移动速度
    /// </summary>
    public float mMoveSpeed = 15f;

    /// <summary>
    /// 移动结束距离结束点的距离
    /// </summary>
    public float mMoveDistance = 100f;

    /// <summary>
    /// 自己侠客数据
    /// </summary>
    private List<LYXHeroEntity> _heroEntityList;

    /// <summary>
    /// 侠客战斗对象
    /// </summary>
    private List<LYXHeroObject> _heroObject;

    /// <summary>
    /// 开始移动进入战场
    /// </summary>
    private bool _isIntoBattle = false;

    protected override void Start()
    {
        _isIntoBattle = false;
        // 获得战斗的侠客
        List<LYXHeroEntity> ownList = LYXHeroConfig.GetOwnHeroEntity();
        _heroEntityList = ownList;
        _heroEntityList.AddRange(LYXHeroConfig.GetEnemyHeroEntity());
        CreateBattleModel();
        if (mUIBattle == null) mUIBattle = FindObjectOfType<LYXUIBattle>();
        mUIBattle.InitBattleUI(this, ownList);
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (_isIntoBattle)
        {
            bool isEnd = MoveIntoBattlefield();
            _isIntoBattle = !isEnd;
            if (isEnd)
            {
                for (int i = 0, len = _heroObject.Count; i < len; i++)
                {
                    _heroObject[i].HeroMove();
                }
            }
        }
    }

    /// <summary>
    /// 创建战斗角色模型
    /// </summary>
    private void CreateBattleModel()
    {
        _heroObject = new List<LYXHeroObject>();

        for (int i = 0, len = _heroEntityList.Count; i < len; i++)
        {
            LYXHeroEntity own = _heroEntityList[i];
            LYXHeroObject ownObject = LYXCompHelper.LoadResource<LYXHeroObject>("RoleModel/" + own.HeroModel);
            if (ownObject == null) continue;
            ownObject.mHeroEnity = own;
            ownObject.mMoveEndPos = mEndMovePos;
            string parPath = own.FriendAndFoe == 1 ? "own" : "enemy";
            InitHeroObjectPosition(ownObject.transform, parPath + "/" + own.HeroPos);
            if (own.FriendAndFoe == 1)
                ownObject.transform.localScale = new Vector3(-1, 1, 1);
            ownObject.mBatManage = this;
            _heroObject.Add(ownObject);
        }

        _isIntoBattle = true;
    }

    /// <summary>
    /// 初始化侠客的位置
    /// </summary>
    /// <param name="current">侠客</param>
    /// <param name="parentPath">侠客父节点路径</param>
    public void InitHeroObjectPosition(Transform current, string parentPath)
    {
        if (!string.IsNullOrEmpty(parentPath))
            current.parent = LYXCompHelper.FindTransform(mBattleFormation, parentPath);
        current.localPosition = Vector3.zero;
        current.localRotation = Quaternion.identity;
        current.localScale = Vector3.one;
    }

    /// <summary>
    /// 移动所以侠客进入战场
    /// </summary>
    public bool MoveIntoBattlefield()
    {
        Vector3 curOwnVect = mOwnBattleTrans.position;
        Vector3 curEnemyVect = mEnemyBattleTrans.position;
        Vector3 curEndVect = mEndMovePos.position;
        Vector3 ownDis = (curOwnVect - curEndVect).normalized;
        Vector3 enemyDis = (curEnemyVect - curEndVect).normalized;

        // 自己移动
        Vector3 ownTemp = ownDis * mMoveSpeed * Time.deltaTime;
        mOwnBattleTrans.position = curOwnVect - new Vector3(0, 0, ownTemp.z);
        // 敌人移动
        Vector3 enemyTemp = enemyDis * mMoveSpeed * Time.deltaTime;
        mEnemyBattleTrans.position = curEnemyVect - new Vector3(0, 0, enemyTemp.z);
        return Vector3.Distance(mOwnBattleTrans.position, curEndVect) <= mMoveDistance &&
               Vector3.Distance(mEnemyBattleTrans.position, curEndVect) <= mMoveDistance;
    }

    /// <summary>
    /// 得到最近的侠客
    /// </summary>
    /// <returns></returns>
    public LYXHeroObject GetNearHero(LYXHeroObject sourceHero)
    {
        List<LYXHeroObject> objs = _heroObject.FindAll(x => x.mHeroEnity.FriendAndFoe != sourceHero.mHeroEnity.FriendAndFoe && !x.mHeroEnity.IsHeroDie);
        if (objs.Count <= 0) return null;
        LYXHeroObject retObj = objs[0];
        float dis = Vector3.Distance(transform.position, retObj.transform.position);
        for (int i = 0, len = objs.Count; i < len; i++)
        {
            float temp = Vector3.Distance(transform.position, objs[i].transform.position);
            if (dis > temp)
            {
                retObj = objs[i];
                dis = temp;
            }
        }
        return retObj;
    }

    /// <summary>
    /// 移出死亡的侠客
    /// </summary>
    /// <param name="hobj">死亡的侠客</param>
    public void RemoveDieHero(LYXHeroObject hobj)
    {
        if (_heroObject == null || _heroObject.Count <= 0) return;
        _heroObject.Remove(hobj);
    }

    /// <summary>
    /// 暂停所以侠客的动作
    /// </summary>
    public void OnBattlePause(bool isPause)
    {
        for (int i = 0, len = _heroObject.Count; i < len; i++)
        {
            _heroObject[i].OnPause(isPause);
        }

        mUIBattle.OnUiPause(isPause);
    }

    /// <summary>
    /// 播放怒气技
    /// </summary>
    /// <param name="heroId">侠客id</param>
    public void OnPlayAngerSkill(string heroId)
    {
        for (int i = 0, len = _heroObject.Count; i < len; i++)
        {
            LYXHeroObject item = _heroObject[i];
            if (item.mHeroEnity.HeroId == heroId)
            {
                item.OnPlayAngerSkill();
                return;
            }
        }
    }

}
