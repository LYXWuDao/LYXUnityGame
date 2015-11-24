using LGame.LBehaviour;
using LGame.LCommon;
using LGame.LDebug;
using LGame.LSource;
using LGame.LUI;
using UnityEngine;

/***
 * 
 * 
 * 框架内容测试文件
 * 
 */

public class LCTestFramework : LABehaviour
{

    public override void Awake()
    {

        LCSUIManage.AsyncOpenWindow("uiBattlePanel", LCSPathHelper.UnityBuildRootPath() + "UI/uiBattlePanel.data");

    }

}
