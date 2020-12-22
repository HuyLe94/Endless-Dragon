using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform Spawner;
    public GameObject[] Mobs;
    [SerializeField]
    public Vector3[] pos;
    public float speed;
    public float timebtwSpawn = 1;
    public bool spawnAllow = true;
    //private float startingspawntime;
    private GameObject[] a;
    [SerializeField]
    private int mobPerWave = 10;
    // Start is called before the first frame update
    void Start()
    {
        a = new GameObject[mobPerWave];
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        //for(int t = 0; t< a.Length;t++)
        //{
        //    for(int x = 0; x< pos.Length;x++)
        //    {
        //        move(a[t], pos[t]);
        //    }
            
        //}
    }
    public void newPos()
    {
        pos = new Vector3[10];
        for(int i = 0;i< pos.Length;i++)
        {
            pos[i] = new Vector3(Mathf.Round(Random.Range(-5f,5f)), Mathf.Round(Random.Range(0.5f, 6f)),0);
        }
    }
    IEnumerator Spawn()
    {
        
        while(spawnAllow == true)
        {
            int i = Random.Range(0, Mobs.Length);
            newPos();
            for (int x = 0; x < mobPerWave; x++)
            {
                a[x] = Instantiate(Mobs[i], transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1);
            }
            i = Random.Range(0, Mobs.Length);
            yield return new WaitForSeconds(timebtwSpawn);
        }
    }

    public void move(GameObject a, Vector3 i)
    {
        if(a!=null)
        {
            a.transform.position = Vector3.MoveTowards(a.transform.position, i, speed * Time.deltaTime);
        }   
    }

    //bool reachTarget(Vector3 current, Vector3 finalPos)
    //{
        
    //    if (Mathf.Abs(Vector3.Distance(current, finalPos)) < 0.01f)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
}


    //a.transform.position = Vector3.MoveTowards(a.transform.position, pos[indexOfPos], speed * Time.deltaTime);