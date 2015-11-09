using System;
using Game.LYX.Behaviour;
using Game.LYX.Common;
using Game.LYX.Json;
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
        public string Names;

        public string sewe
        {
            get
            {
                return "";
            }
        }

        public int[] arrs = new[] { 1, 2, 3, 4 };

    }

    protected override void Awake()
    {

        Person p = new Person() { Names = "we" };

        string json = LYXJson.ToJson(new[] { 1, 2, 3, 4 });

        LYXLogHelper.Log(json);

        object[] objs = LYXJson.ToArray(json);

        for (int i = 0, len = objs.Length; i < len; i++)
        {
            LYXLogHelper.Log(objs[i]);
        }

    }

}
