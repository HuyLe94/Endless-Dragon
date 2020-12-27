using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpray : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    private Combat combat;
    public int bossHP = 500;
    public int bossDMG = 10;
    public int numberOfShot;
    private float angle;
    public float bossSpeed;
    public int bossShield=0;
    public bool allowMove = false;
    public float BulletSpeed;
    private Transform player;
    public float fireRate;
    [SerializeField]
    private Vector2 newPos;
    void Start()
    {
        combat = new Combat();
        
        angle = Mathf.PI / -(numberOfShot + 1);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        InvokeRepeating("shoot", 2, fireRate);
        Pos();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, new Vector2(0f, 5.5f), 5);
        if (bossHP <=0)
        {
            Destroy(gameObject);
            
        }
    }

    private void FixedUpdate()
    {
        if(allowMove == true)
        {
            move();
            {
                Pos();
            }
        }
    }

    void shoot()
    {
        for (int i = 1; i <= numberOfShot; i++)
        {
            GameObject a = Instantiate(bullet, transform.position, Quaternion.identity);
            a.transform.SetParent(gameObject.transform);

            a.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle * i)* BulletSpeed, Mathf.Sin(angle * i) * BulletSpeed);

        }

    }

    void move()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, newPos,bossSpeed*Time.deltaTime);
    }
    void Pos()
    {
        newPos = new Vector2(Random.Range(-4, 5), transform.position.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerBullet"))
        {
            combat.takeDmg(ref bossHP, ref player.GetComponent<Player>().playerDmg, ref bossShield);
            Destroy(collision.gameObject);
        }
        
    }

    private void OnDestroy()
    {
        GameObject.Find("EnemySpawner").GetComponent<EnemySpawn>().enabled = true;
        GameObject.Find("EnemySpawner").GetComponent<EnemySpawn>().spawnAllow = true;
    }
}
