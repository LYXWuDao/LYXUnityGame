using UnityEngine;
using System.Collections;

public class LYXBattleBuffer : LYXBaseBehaviour
{

    /// <summary>
    /// 创建伤害值
    /// </summary>
    /// <param name="parent">父节点</param>
    /// <param name="target">伤害值位置</param>
    /// <param name="hurtValue">伤害值</param>
    /// <param name="offect">便宜位置</param>
    /// <returns></returns>
    public static LYXBattleBuffer CreateHarm(GameObject parent, GameObject target, float hurtValue, Vector3 offect)
    {
        LYXBattleBuffer buffer = LYXCompHelper.LoadResource<LYXBattleBuffer>("Prefab/hurtvalue");
        UILabel lab = buffer.GetComponent<UILabel>();
        if (lab == null) return null;
        lab.text = "[ff0000] -" + hurtValue;
        Transform trans = lab.transform;
        trans.parent = parent.transform;
        trans.localPosition = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = Vector3.one;
        trans.position = target.transform.position;
        trans.localPosition = trans.localPosition + offect;
        return buffer;
    }

    /// <summary>
    /// 移动的位置
    /// </summary>
    public void PlayHarmAnimation()
    {
        TweenPosition post = TweenPosition.Begin(gameObject, 1f, transform.localPosition + new Vector3(0, 100, 0));
        post.AddOnFinished(delegate()
        {
            GameObject.DestroyObject(gameObject);
        });
        post.Play();
    }

}
