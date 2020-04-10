using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBabySpwan : MonoBehaviour {

    public Enemy enemy;
    public int MaxNum=5;//最大数量
    private int CurrentNum = 0;
    private float timer = 0;

    [Tooltip("刷新时间")]
    public float InstanceTime;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (CurrentNum<MaxNum)
        {
            if(timer>InstanceTime)
            {
                Vector3 pos = transform.position;
                pos.x = pos.x+Random.Range(-1, 1);//随机坐标生成
                pos.z = pos.z+Random.Range(-1, 1);
                GameObject go=Instantiate(enemy.gameObject, pos, Quaternion.identity);
                go.transform.parent = transform;
                timer = 0;
                CurrentNum++;
                //Debug.Log("当前怪物数量"+CurrentNum);
            }
          
        }
	}
    public void MinCurrentNum()
    {
        this.CurrentNum--;
    }
}
