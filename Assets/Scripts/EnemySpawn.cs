using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform Spawner;
    public GameObject[] Mobs;
    int i = 0;
    //int x = 0;
    [SerializeField]
    private Vector3[] pos;
    public float speed;
    public float timebtwSpawn;
    //private float startingspawntime;
    //private bool reachTarget = false;
    private GameObject[] a;
    [SerializeField]
    private int mobPerWave = 10;
    // Start is called before the first frame update
    void Start()
    {
        a = new GameObject[mobPerWave];
        StartCoroutine(spawn());
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject b in a)
        {
            
            
                move(b, 0);
            if (reachTarget(b, pos[i]) == true)
            {
                i++;
            }


            //Debug.Log(i);
        }
    }
    public void newPos()
    {
        pos = new Vector3[10];
        for(int i = 0;i< pos.Length;i++)
        {
            pos[i] = new Vector3(Mathf.Round(Random.Range(-5f,5f)), Mathf.Round(Random.Range(0.5f, 6f)),0);
        }
    }
    IEnumerator spawn()
    {
        newPos();
        int i = Random.Range(0, Mobs.Length);
        for(int x = 0;x< mobPerWave;x++)
        {
                a[x] = Instantiate(Mobs[i], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timebtwSpawn);
        }
    }

    void move(GameObject a, int indexOfPos)
    {
        if(a!= null)
        {
            a.transform.position = Vector3.MoveTowards(a.transform.position, pos[indexOfPos], speed * Time.deltaTime);
        }
    }

    bool reachTarget(GameObject b, Vector3 location)
    {
        if (Mathf.Abs(Vector3.Distance(b.transform.position, location)) < 0.01f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


    //a.transform.position = Vector3.MoveTowards(a.transform.position, pos[indexOfPos], speed * Time.deltaTime);