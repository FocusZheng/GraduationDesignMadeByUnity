using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChange : MonoBehaviour {

    public  static CursorChange _instance;
    // Use this for initialization
    public Texture2D Attack;
    public Texture2D LockTarget;
    public Texture2D Normal;
    public Texture2D Npc;
    public Texture2D Pick;
    Vector2 hotspot = Vector2.zero;//定义一个热点 鼠标点击事件 默认为左上角
    CursorMode mode = CursorMode.Auto;//非硬件设置的

    private void Start()
    {
        _instance = this;
    }
    public void NpcCursor()
    {
        Cursor.SetCursor(Npc,hotspot,mode);
    }
    public void NormalCursor()
    {
        Cursor.SetCursor(Normal, hotspot, mode);
    }
    public void AttackCursor()
    {
        Cursor.SetCursor(Attack, hotspot, mode);
    }
    public void LockTargetCursor()
    {
        Cursor.SetCursor(LockTarget, hotspot, mode);
    }


}
