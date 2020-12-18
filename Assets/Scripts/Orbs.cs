using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbs : MonoBehaviour
{
    // Start is called before the first frame update
    private OrbTypes orb;
    void Start()
    {
        
        orb = gameObject.GetComponent<OrbTypes>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x < -10 || gameObject.transform.position.x > 10 || gameObject.transform.position.y < -10)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Wall"))
        {
            //Debug.Log("Bounced");
            Vector2 velocity = new Vector2(-(gameObject.GetComponent<Rigidbody2D>().velocity.x), gameObject.GetComponent<Rigidbody2D>().velocity.y) ;
            gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
        }
    }
}
