using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform Spawner;
    public GameObject[] Mobs;
    [SerializeField]
    private Vector2[] pos;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void newPos()
    {
        pos = new Vector2[10];
        for(int i = 0;i< pos.Length;i++)
        {
            pos[i] = new Vector2(Random.Range(-5f,5f), Random.Range(0.5f, 6f));
        }
    }
    void spawn()
    {
        newPos();
        //Debug.Log(pos);
        int i = Random.Range(0, Mobs.Length);
        GameObject a = Instantiate(Mobs[i], transform.position,Quaternion.identity);
        foreach(Vector2 c in pos)
        {
            a.transform.position = Vector2.MoveTowards(a.transform.position, c, speed*Time.deltaTime);
        }
        Debug.Log(a.transform.position);
    }
}
