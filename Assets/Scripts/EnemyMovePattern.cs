using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovePattern : MonoBehaviour
{
    // Start is called before the first frame update
    private EnemySpawn spawner;
    int i = 0;
    void Start()
    {
        
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        spawner.move(gameObject, spawner.pos[i]);
        if (Mathf.Abs(Vector3.Distance(transform.position, spawner.pos[i])) < 0.01f)
        {
            i++;
        }
        
    }
}
