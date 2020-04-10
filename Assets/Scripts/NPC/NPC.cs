using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    private void OnMouseEnter()
    {
        CursorChange._instance.NpcCursor();
    }
    private void OnMouseExit()
    {
        CursorChange._instance.NormalCursor();
    }
}
