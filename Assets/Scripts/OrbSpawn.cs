using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] orbs;
    public float TimeBetweenSpawn =3f;
    private int orbPerWave = 3;
    
    void Start()
    {
        StartCoroutine(SpawnOrbs());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
    }

    IEnumerator SpawnOrbs()
    {
        while (true)
        {
            
            for (int x = 0; x < orbPerWave; x++)
            {
                int i = Random.Range(0, orbs.Length);
                Rigidbody2D a = Instantiate(orbs[i], new Vector3(Random.Range(-5, 6), transform.position.y, 2), Quaternion.identity).GetComponent<Rigidbody2D>();
                a.gravityScale = Random.Range(0.1f, 0.3f);
                a.AddForce(new Vector2(Random.Range(-2, 2), 0), ForceMode2D.Impulse);
                //new Vector2(Random.Range(-1, 1), Random.Range(-5, 6)));

            }
            yield return new WaitForSeconds(TimeBetweenSpawn);
        }
    }
}
