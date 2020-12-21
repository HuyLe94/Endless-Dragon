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
    private GameObject target;
    [SerializeField]
    //private Vector2[][] moveTarget;
    private Vector2[] movePos;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //moveTarget = GameObject.Find("AI-Target").;
        //movePos = a.posGen();
        //Debug.Log(moveTarget);
        //movePos = new Vector2[10];
        //for (int i = 0; i < movePos.Length; i++)
        //{
        //    movePos[i] = moveTarget.GetComponent<AiTargetPos>().target[3][i];
        //    //Debug.Log(movePos[i]);
        //}
        //Debug.Log(moveTarget.GetComponent<AiTargetPos>().target[3][3]);
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

    //private void move()
    //{
    //    int b = Random.Range(0, 9);
    //    for (int i = 0; i < movePos.Length; i++)
    //    {
    //        movePos[i] = moveTarget.GetComponent<AiTargetPos>().target[b][i];
    //    }

    //    //    foreach (Vector2 c in moveTarget.GetComponent<AiTargetPos>().target[b])
    //    //{
    //    //    int i = 0;
    //    //    movePos[i] = c;
    //    //    i++;
    //    //}

    //    //for(int i = 0;i<movePos.Length;i++)
    //    //{
    //    //    movePos[i] = moveTarget.GetComponent<AiTargetPos>().target[b][i];
    //    //}

    //    //foreach(Vector2 c in movePos)
    //    //{
    //    //    transform.position = Vector2.MoveTowards(transform.position,c , 0.5f);
    //    //}

    //}

}
