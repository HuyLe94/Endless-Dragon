using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 mousePosition;
    public float moveSpeed = 0.1f;
    public static int playerHP = 100;
    public static int maxHP = 100;
    public Transform topBorder;
    public Transform bottomBorder;
    public bool fakeLighting = false;
    private Vector2 inversePos;
    private float midPoint;
    private OrbTypes[] loot;

    void Start()
    {
        midPoint = (topBorder.position.y + bottomBorder.position.y) / 2;
        loot = new OrbTypes[3];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (fakeLighting == false)
            {
                FollowMouse();
            }
            else
            {
                ReverseMouse();
            }
        }
        if (playerHP <= 0)
        {
            Destroy(gameObject);
            //Effect go here
            SceneManager.LoadScene("GameOver");
        }
    }
    void FollowMouse()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        inversePos.x = 0 - mousePosition.x;
        inversePos.y = 0 - mousePosition.y;
        if (mousePosition.x > -5 && mousePosition.x < 5 && mousePosition.y > bottomBorder.position.y && mousePosition.y < topBorder.position.y)
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }
    void ReverseMouse()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        inversePos = new Vector2(-mousePosition.x, midPoint + (midPoint - mousePosition.y));
        if (inversePos.x > -5 && inversePos.x < 5 && inversePos.y > bottomBorder.position.y && inversePos.y < topBorder.position.y)
            transform.position = Vector2.Lerp(transform.position, inversePos, moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            //Destroy(collision.transform.parent.gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Orb"))
        {
            if (loot[0] == null)
            {
                Debug.Log("1st slot");
                loot[0] = collision.GetComponent<OrbTypes>();
                Destroy(collision.gameObject);

            }
            else if(loot[1].type == 0)
            {
                Debug.Log("2nd slot");
                loot[1] = collision.GetComponent<OrbTypes>();
                Destroy(collision.gameObject);
            }
            else if(loot[2].type == 0)
            {
                Debug.Log("3rd slot");
                loot[2] = collision.GetComponent<OrbTypes>();
                Destroy(collision.gameObject);
            }
            //for (int i = 0; i < loot.Length; i++)
            //{
            //    if (i == 0) //&& loot[i] == null)
            //    {
            //        loot[i] = collision.GetComponent<OrbTypes>();
            //        Destroy(collision.gameObject);
            //    }
            //    else
            //    {
            //        if (loot[i] == null && collision.GetComponent<OrbTypes>().type == loot[i - 1].type)
            //        {
            //            loot[i] = collision.GetComponent<OrbTypes>();

            //            //var a = loot[i].type;
            //            //Debug.Log(loot[i].type);
            //            Destroy(collision.gameObject);
            //        }
            //else
            //{
            //    Array.Clear(loot, 0, loot.Length);
            //    loot[0] = collision.GetComponent<OrbTypes>();
            //}

            //    }
            //}

        }
    }
}
