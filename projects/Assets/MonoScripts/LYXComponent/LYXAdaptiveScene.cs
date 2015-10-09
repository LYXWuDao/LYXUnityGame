using System;
using UnityEngine;
using System.Collections;

/***
 * 
 * 
 * 屏幕自适应
 * 
 */

public class LYXAdaptiveScene : LYXBehaviour
{

    /// <summary>
    /// 跟新的方式
    /// </summary>
    public enum LYXAnchorUpdate
    {
        OnEnable,
        OnUpdate,
        OnStart,
    }

    /// <summary>
    ///  跟新方式
    /// </summary>
    public LYXAnchorUpdate anchorUpdate = LYXAnchorUpdate.OnStart;

    /// <summary>
    /// 当前是否存在 ngui widget 组件
    /// </summary>
    private UIWidget anchorWidget;

    /// <summary>
    /// 屏幕的宽
    /// </summary>
    public int mSceneWidth;

    /// <summary>
    /// 屏幕的高
    /// </summary>
    public int mSceneHeight;

    /// <summary>
    /// 物体的原始宽度
    /// </summary>
    public int mOriginalWidth;

    /// <summary>
    /// 物体的原始高度
    /// </summary>
    public int mOriginalHeight;

    protected override void Awake()
    {
        transform.localScale = Vector3.one;
        anchorWidget = gameObject.GetComponent<UIWidget>();
        bool isWidget = anchorWidget != null;
        Vector2 vect = LYXCompHelper.SceneWidthAndHeight(isWidget);
        mSceneWidth = (int)vect.x;
        mSceneHeight = (int)vect.y;
        if (isWidget)
        {
            anchorWidget.ResetAnchors();
            mOriginalWidth = (int)anchorWidget.localSize.x;
            mOriginalHeight = (int)anchorWidget.localSize.y;
        }
        else
        {
            MeshFilter meshFiter = gameObject.GetComponent<MeshFilter>();
            mOriginalWidth = (int)meshFiter.mesh.bounds.size.x;
            mOriginalHeight = (int)meshFiter.mesh.bounds.size.z;
        }
    }

    protected override void OnEnable()
    {
        if (anchorUpdate == LYXAnchorUpdate.OnEnable) AdaptiveScene();
    }

    protected override void Start()
    {
        if (anchorUpdate == LYXAnchorUpdate.OnStart) AdaptiveScene();
    }

    protected override void Update()
    {
        if (anchorUpdate == LYXAnchorUpdate.OnUpdate)
        {
            Vector2 vect = LYXCompHelper.SceneWidthAndHeight(anchorWidget != null);
            mSceneWidth = (int)vect.x;
            mSceneHeight = (int)vect.y;
            AdaptiveScene();
        }
    }

    /// <summary>
    /// 适应屏幕
    /// </summary>
    public void AdaptiveScene()
    {
        int w = 0, h = 0;
        if (anchorWidget != null)
        {
            float ratioW = mOriginalWidth / (float)mSceneWidth;
            float ratioH = mOriginalHeight / (float)mSceneHeight;
            if (ratioW < ratioH)
            {
                w = mSceneWidth;
                h = (int)(mOriginalHeight / ratioW);
            }
            else
            {
                h = mSceneHeight;
                w = (int)(mOriginalWidth / ratioH);
            }
            anchorWidget.SetDimensions(w, h);
        }
        else
        {
            float ratioW = mSceneWidth / (float)mOriginalWidth;
            float ratioH = mSceneHeight / (float)mOriginalHeight;
            transform.localScale = new Vector3(ratioW, 1, GetCameraFOV(10));
        }
    }

	public float GetCameraFOV(float currentFOV)
	{
        float scale = Convert.ToSingle(mSceneHeight / 640f);
		return currentFOV * scale;
	}

}
