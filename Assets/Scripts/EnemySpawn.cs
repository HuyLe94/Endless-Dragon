using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform Spawner;
    public GameObject[] Mobs;
    public GameObject[] Bosses;
    [SerializeField]
    public Vector3[] pos;
    public float speed;
    public float timebtwSpawn = 1;
    public int waveCount = 0;
    public int enemyDeath=0;
    public bool spawnAllow = true;
    //public bool bossTime = false;
    private GameObject[] a;
    [SerializeField]
    private int mobPerWave = 10;
    void Start()
    {
        a = new GameObject[mobPerWave];
        StartCoroutine(Spawn());

    }
    private void OnEnable()
    {
        //StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (waveCount == 10 )//&& enemyDeath == 100)
        {
            spawnAllow = false;
            if(enemyDeath == 100)
            {
                //bossTime = true;
                spawnBoss();
                this.enabled = false;
              
            }
            
        }
    }
    public void newPos()
    {
        pos = new Vector3[10];
        for(int i = 0;i< pos.Length -1;i++)
        {
            pos[i] = new Vector3(Mathf.Round(Random.Range(-5f,5f)), Mathf.Round(Random.Range(0.5f, 6f)),0);
            pos[9] = new Vector3(Mathf.Round(Random.Range(-6f, 7f)), 12, 0);
        }
    }
    public IEnumerator Spawn()
    {
        
        while(spawnAllow == true)
        {
            int i = Random.Range(0, Mobs.Length);
            newPos();
            int b = Random.Range(-4, 5);
            for (int x = 0; x < mobPerWave; x++)
            {
                a[x] = Instantiate(Mobs[i], new Vector2(b, transform.position.y), Quaternion.identity);
                
                yield return new WaitForSeconds(1);
            }
            waveCount += 1;
            i = Random.Range(0, Mobs.Length);
            yield return new WaitForSeconds(timebtwSpawn);
        }
    }

    public void move(GameObject a, Vector3 i)
    {
        if (a != null)
        {
            a.transform.position = Vector3.MoveTowards(a.transform.position, i, speed * Time.deltaTime);
        }
    }

    public void spawnBoss()
    {
        int a = Random.Range(0,Bosses.Length);
        Instantiate(Bosses[a], transform.position,Quaternion.identity);
        enemyDeath = 0;
        waveCount = 0;
    }

    
}


