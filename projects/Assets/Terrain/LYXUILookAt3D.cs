using Game.LYX.Behaviour;
using UnityEngine;
using System.Collections;


/**
 * 
 * 
 *  让 ui 位置 看向目标位置
 * 
 */

public class LYXUILookAt3D : LYXBaseBehaviour
{

    /// <summary>
    /// 3d 摄像机
    /// </summary>
    public Camera mainCamera;

    /// <summary>
    /// ui 摄像机
    /// </summary>
    public UICamera uiCamera;

    /// <summary>
    /// 看向的目标
    /// </summary>
    public GameObject mTarget;

    /// <summary>
    /// 保存目标当前位置
    /// </summary>
    [System.NonSerialized] private Vector3 mSaveTagPos;

    protected override void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        if (uiCamera == null) uiCamera = FindObjectOfType<UICamera>();
        mSaveTagPos = Vector3.one * -1000;
    }

    protected override void OnUpdate(float deltaTime)
    {
        if (mTarget == null) return;
        Vector3 tagPos = mTarget.transform.position;
        if (mSaveTagPos == tagPos) return;
        mSaveTagPos = tagPos;
        // 将目标点的位置转换成屏幕点
        Vector3 vet3 = mainCamera.WorldToScreenPoint(tagPos);
        Vector3 uiVect3 = uiCamera.camera.ScreenToWorldPoint(vet3);
        transform.position = new Vector3(uiVect3.x, uiVect3.y, 0);
    }

}
