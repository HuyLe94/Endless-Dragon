using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform parent;
    private float bulletSpeed;
    public int enemyDmg = 1;
    public int enemyHP = 10;
    public int enemyShield = 0;
    Transform player;
    public GameObject bullet;
    public float fireRate = 5;
    private float timer = 0;
    private GameObject target;
    private Combat combat;
    private Vector2[] movePos;
    void Start()
    {
        combat = new Combat();
        parent = GameObject.Find("BulletsHolder").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyDmg = 1*combat.EnemyStatMultiplierl;
        enemyHP = 10 * combat.EnemyStatMultiplierl;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, 0), 1 * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHP <= 0)
        {
            
            Destroy(gameObject);
        }
        if (transform.position.y >11)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        timer += 1 * Time.deltaTime;
        if (timer >= fireRate)
        {
            fire();
            timer = 0;
        }
    }

    void fire()
    {
        bulletSpeed = Random.Range(4f,7f);
        GameObject a = Instantiate(bullet, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
        a.transform.SetParent(parent);
        a.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-bulletSpeed);
        a.GetComponent<AutoDestroy>().parent = gameObject;
        a.GetComponent<AutoDestroy>().damage = enemyDmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerBullet"))
        {
            combat.takeDmg(ref enemyHP, ref player.GetComponent<Player>().playerDmg, ref enemyShield);
            Destroy(collision.gameObject);
        }
        
    }

    private void OnDestroy()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemySpawn>().enemyDeath += 1;
    }
}