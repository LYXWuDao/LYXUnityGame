using UnityEngine;

/****
 * 
 * 
 * ui  资源管理
 * 
 * 
 */

public class LYXUIManage
{

    /// <summary>
    /// 打开ui界面
    /// <param name="uiName"> ui 资源名字 </param>
    /// </summary>
    public static void OpenUI(string uiName)
    {
        string bundlePath = string.Format("UI/{0}.data", uiName);
        GameObject ui = LYXManageSource.LoadSource(uiName, bundlePath, typeof(GameObject));
        if (ui == null)
        {
            LYXLogHelper.Error("打开资源不存在!");
            return;
        }
        GameObject go = GameObject.Instantiate(ui) as GameObject;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
    }

}

