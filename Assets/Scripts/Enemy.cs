using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, 0), 1 * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHP <= 0)
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
        GameObject a = Instantiate(bullet, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
        a.transform.SetParent(transform);
        a.GetComponent<Rigidbody2D>().velocity = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hitted");
        combat.takeDmg(ref enemyHP, ref player.GetComponent<Player>().playerDmg, ref enemyShield);
        Destroy(collision.gameObject);
    }
}