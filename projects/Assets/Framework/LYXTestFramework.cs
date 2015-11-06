using System;
using Game.LYX.Behaviour;
using Game.LYX.Common;
using Game.LYX.Message;
using Game.LYX.UI;
using LitJson;
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

    public class Person
    {
        public string Name;
    }

    protected override void Awake()
    {

        Person p = new Person() { Name = "we" };

        string json = JsonMapper.ToJson(new object[] { p, p, p });

        JsonData data = JsonMapper.ToObject(json);

        LYXLogHelper.Log(data.Count);


    }

}
