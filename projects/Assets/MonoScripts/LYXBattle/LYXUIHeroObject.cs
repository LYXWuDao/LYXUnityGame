using UnityEngine;
using System.Collections;

/***
 * 
 * 
 *  ui 战斗侠客
 * 
 * 
 */

public class LYXUIHeroObject : LYXBaseBehaviour
{
    /// <summary>
    /// 战斗管理
    /// </summary>
    private LYXBattleManage _manage;

    /// <summary>
    /// 数据
    /// </summary>
    private LYXHeroEntity _heroEntity;

    private bool _isStartRefresh = false;

    /// <summary>
    /// 当前血量
    /// </summary>
    private UISlider bloodSlider;

    /// <summary>
    /// 当前怒气
    /// </summary>
    private UISlider angSlider;

    /// <summary>
    /// 死亡图片
    /// </summary>
    private UISprite heroDieSpr;

    /// <summary>
    /// 怒气满时效果
    /// </summary>
    private TweenRotation mTweenRota;

    /// <summary>
    /// 是否在播放怒气特效
    /// </summary>
    private bool isHasTween = false;

    /// <summary>
    /// 界面是否暂停
    /// </summary>
    private bool isUiPasue = false;

    /// <summary>
    /// 初始化ui 界面
    /// </summary>
    public void InitUIPanel(LYXBattleManage mage, LYXHeroEntity entity)
    {
        _manage = mage;
        _heroEntity = entity;
        _isStartRefresh = true;

        // 血条
        bloodSlider = LYXCompHelper.FindComponet<UISlider>(gameObject, "roleblood/blood/bloodslider");
        //怒气
        angSlider = LYXCompHelper.FindComponet<UISlider>(gameObject, "roleblood/anger/angerslider");
        heroDieSpr = LYXCompHelper.FindComponet<UISprite>(gameObject, "die");

        heroDieSpr.gameObject.SetActive(false);
        mTweenRota = TweenRotation.Begin(gameObject, 0.1f, Quaternion.identity);
        mTweenRota.enabled = false;
        isHasTween = false;
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (isUiPasue) return;
        if (!_isStartRefresh) return;
        if (_heroEntity == null) return;
        if (_heroEntity.IsHeroDie)
        {
            //侠客死亡
            _isStartRefresh = false;
            heroDieSpr.gameObject.SetActive(true);
            transform.localRotation = Quaternion.identity;
            mTweenRota.enabled = false;
            bloodSlider.value = 0;
            angSlider.value = 0;
            return;
        }
        OnRefreshBlood();
        if (_heroEntity.CurrentAnger >= _heroEntity.MaxAnger && !isHasTween)
        {
            isHasTween = true;
            mTweenRota.enabled = true;
            mTweenRota.from = new Vector3(0, 0, -5);
            mTweenRota.to = new Vector3(0, 0, 5);
            mTweenRota.style = UITweener.Style.PingPong;
            mTweenRota.Play();
        }
    }

    /// <summary>
    /// 刷新血条
    /// </summary>
    private void OnRefreshBlood()
    {
        if (bloodSlider == null || angSlider == null || _heroEntity == null) return;
        bloodSlider.value = _heroEntity.CurrentBlood / _heroEntity.MaxBlood;
        angSlider.value = _heroEntity.CurrentAnger / _heroEntity.MaxAnger;
    }

    /// <summary>
    /// 是否怒气技
    /// </summary>
    public void OnClickAngerSkill()
    {
        if (isUiPasue) return;
        if (_heroEntity == null || _heroEntity.IsHeroDie) return;
        if (_heroEntity.CurrentAnger < _heroEntity.MaxAnger) return;
        transform.localRotation = Quaternion.identity;
        mTweenRota.enabled = false;
        isHasTween = false;
        _manage.OnPlayAngerSkill(_heroEntity.HeroId);
    }

    /// <summary>
    /// 是否暂停
    /// </summary>
    /// <param name="isPause"></param>
    public void OnUiPause(bool isPause)
    {
        if (_heroEntity == null || _heroEntity.IsHeroDie) return;
        if (mTweenRota != null && isHasTween) mTweenRota.enabled = !isPause;
        isUiPasue = isPause;
    }
}
