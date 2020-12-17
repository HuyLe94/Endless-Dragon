using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    public GameObject bullet;
    public float fireRate = 5;
    public float timer = 0;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //fire();
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        
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

}
