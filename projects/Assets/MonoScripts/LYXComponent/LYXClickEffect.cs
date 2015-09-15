using UnityEngine;
using System.Collections;

/******
 * 
 * 
 *  鼠标按下时效果
 * 
 */

public class LYXClickEffect : LYXMouseEvent
{

    /// <summary>
    /// 点击模板特效
    /// </summary>
    public GameObject mTempEff;

    protected override void OnMouseLeftDown()
    {
        Debug.Log(Input.mousePosition);

        if (mTempEff == null) return;

        GameObject go = NGUITools.AddChild(gameObject, mTempEff);
        if (go == null) return;
        LYXDestroy dty = go.AddComponent<LYXDestroy>();
        dty.mDtyTime = 1f;
        dty.transform.localScale = Vector3.one * 0.5f;
        dty.transform.localRotation = Quaternion.Euler(new Vector3(-90f, 0, 0));
        Vector3 pos2 = UICamera.currentCamera.ScreenToWorldPoint(Input.mousePosition);
        dty.transform.position = pos2;
    }

}
