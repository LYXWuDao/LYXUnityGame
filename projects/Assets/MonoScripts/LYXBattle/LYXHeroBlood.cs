using UnityEngine;
using System.Collections;

/****
 * 
 * 
 *  战斗中侠客模型血条处理
 * 
 * 
 */

public class LYXHeroBlood : LYXBaseBehaviour
{
    /// <summary>
    /// 保存当前侠客数据
    /// </summary>
    [System.NonSerialized]
    [HideInInspector]
    public LYXHeroEntity mHeroEntity;

    /// <summary>
    ///  血条隐藏
    /// </summary>
    private TweenAlpha mTweenAlpha;

    /// <summary>
    /// 当前血量
    /// </summary>
    private UISlider bloodSlider;

    /// <summary>
    /// 当前怒气
    /// </summary>
    private UISlider angSlider;

    /// <summary>
    /// 血条显示时间
    /// </summary>
    private float mBloodTime = 0;

    /// <summary>
    /// 是否刷新血条
    /// </summary>
    private bool mIsRefresh = false;

    /// <summary>
    /// 创建血条
    /// </summary>
    /// <param name="tagert">血条模板 Prefab </param>
    /// <param name="parent">父节点</param>
    /// <returns></returns>
    public static LYXHeroBlood CreateBlood(GameObject tagert, Transform parent)
    {
        GameObject bgo = GameObject.Instantiate(tagert) as GameObject;
        if (bgo == null) return null;
        Transform btrans = bgo.transform;
        if (parent != null) btrans.parent = parent;
        btrans.localPosition = Vector3.zero;
        btrans.localRotation = Quaternion.identity;
        btrans.localScale = Vector3.one;
        LYXHeroBlood blood = LYXCompHelper.FindComponet<LYXHeroBlood>(bgo);
        // 血条
        blood.bloodSlider = btrans.Find("blood/bloodslider").GetComponent<UISlider>();
        //怒气
        blood.angSlider = btrans.Find("anger/angerslider").GetComponent<UISlider>();
        blood.mBloodTime = 0;
        blood.mIsRefresh = false;
        blood.gameObject.SetActive(false);
        return blood;
    }

    /// <summary>
    /// 刷新血条
    /// </summary>
    private void OnRefreshBlood()
    {
        if (bloodSlider == null || angSlider == null) return;
        bloodSlider.value = mHeroEntity.CurrentBlood / mHeroEntity.MaxBlood;
        angSlider.value = mHeroEntity.CurrentAnger / mHeroEntity.MaxAnger;
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (!mIsRefresh) return;
        if (mBloodTime <= 0)
        {
            mIsRefresh = false;
            mTweenAlpha = TweenAlpha.Begin(gameObject, 1f, 0);
            mTweenAlpha.from = 1;
            mTweenAlpha.to = 0;
            mTweenAlpha.Play();
            return;
        }
        mBloodTime -= deltaTime;
        OnRefreshBlood();
    }

    /// <summary>
    /// 设置血条的位置
    /// </summary>
    /// <param name="target">血条目标点</param>
    /// <param name="offect">偏移</param>
    public void SetBloodPostion(GameObject target, Vector3 offect)
    {
        if (target == null) return;
        LYXUIInset3D uiInset = LYXCompHelper.FindComponet<LYXUIInset3D>(gameObject);
        if (uiInset == null) return;
        uiInset.mOffset = offect;
        uiInset.mTarget = target;
    }

    /// <summary>
    /// 显示血条
    /// </summary>
    public void ShowBlood()
    {
        mIsRefresh = true;
        mBloodTime = 2;
        if (!gameObject.activeSelf) gameObject.SetActive(true);
        if (mTweenAlpha != null)
        {
            mTweenAlpha.enabled = false;
            UIRect rect = gameObject.GetComponent<UIRect>();
            rect.alpha = 1;
        }
    }

}
