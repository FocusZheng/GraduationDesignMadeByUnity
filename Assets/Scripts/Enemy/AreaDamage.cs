using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour {

    public float damage;
    private List<Enemy> enemies = new List<Enemy>();

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag==Tags.Enemy)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            //int index = enemies.IndexOf(enemy);
            //if (index == -1)
            //{
                enemy.GetDamage(damage);
                //enemies.Add(enemy);
            //}
        }
      

    }
}
