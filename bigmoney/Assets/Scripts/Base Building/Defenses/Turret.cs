using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Placeable
{
    public int range;
    public int dmg;
    public float fireCD;
    float curCD;
    // Start is called before the first frame update
    void Start()
    {
        float curCD = fireCD; 
    }

    // Update is called once per frame
    void Update()
    {
        if (curCD <= 0)
        {
            List<Enemy> elist = FindObjectOfType<EnemyDirector>().getEnemies();
            Enemy closest = null;
            float cdist = 10000;
            if (elist.Count != 0)
            {
                foreach (Enemy e in elist)
                {
                    float dist = Vector2.Distance(e.transform.position, this.gameObject.transform.position);
                    if (dist <= range)
                    {
                        if (closest == null)
                        {
                            closest = e;
                            cdist = dist;
                        }
                        else
                        {
                            if (cdist > dist)
                            {
                                closest = e;
                                cdist = dist;
                            }
                        }
                    }
                }
            }
            if (closest != null)
            {
                FindObjectOfType<GameManager>().spawnAttackLine(this.gameObject.transform.position, closest.transform.position);
                closest.takeDamage(dmg);
            }
            curCD = fireCD;

        }
        else
            curCD -= Time.deltaTime;
    }
}
