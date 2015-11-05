using Game.LYX.Behaviour;
using Game.LYX.Common;
using Game.LYX.UI;
using UnityEngine;
using System.Collections;

/***
 * 
 * 
 * 框架内容测试文件
 * 
 */

public class LYXTestFramework : LYXBaseBehaviour
{

    protected override void Awake()
    {

        LYXUIManage.OpenPage("uiBattlePanel", "UI/uiBattlePanel.data");
        LYXUIManage.ClosePage("uiBattlePanel");

    }

}
