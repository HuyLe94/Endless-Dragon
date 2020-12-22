using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovePattern : MonoBehaviour
{
    // Start is called before the first frame update
    private EnemySpawn spawner;
    int i = 0;
    [SerializeField]
    private Vector3[] ownPattern;
    void Start()
    {
        ownPattern = new Vector3[10];
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawn>();
        for(int x = 0; x<ownPattern.Length;x++)
        {
            ownPattern[x] = spawner.pos[x];
            Debug.Log("Copied");
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawner.move(gameObject, ownPattern[i]);
        if (Mathf.Abs(Vector3.Distance(transform.position, ownPattern[i])) < 0.01f)
        {
            i++;
        }
        
    }
}
