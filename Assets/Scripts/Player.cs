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
    public int playerHP = 100;
    public int maxHP = 100;
    public int shield = 0;
    public float dotDuration = 5;
    public Transform topBorder;
    public Transform bottomBorder;
    private Vector2 inversePos;
    private float midPoint;
    public OrbTypes[] loot;
    public bool lightning = true;
    public bool water = false;
    public bool fire = false;
    public bool stone = false;
    public bool FakeLightning = false;
    public bool FakeFire = false;
    public bool FakeStone = false;
    public bool FakeWater= false;


    void Start()
    {
        //StartCoroutine(fakeFire());
        //midPoint = (topBorder.position.y + bottomBorder.position.y) / 2;
        loot = new OrbTypes[3];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (FakeLightning == false)
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
            if( lightning == true)
            {
                lightningEffect(collision);
            }
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Orb"))
        {
            if (collision.GetComponent<OrbTypes>().type == OrbTypes.OrbType.FakeFire)
            {
                StartCoroutine(fakeFire());
                collision.gameObject.SetActive(false);
            }
            else if (loot[0] == null)
            {
                loot[0] = (collision.GetComponent<OrbTypes>());
                collision.transform.SetParent(transform);
                collision.gameObject.SetActive(false);
            }
            else if (loot[0] != null && loot[1] == null && loot[0].type == collision.GetComponent<OrbTypes>().type)
            {
                loot[1] = collision.GetComponent<OrbTypes>();
                collision.transform.SetParent(transform);
                collision.gameObject.SetActive(false);
            }
            else if (loot[0] != null && loot[0].type != collision.GetComponent<OrbTypes>().type)
            {
                Array.Clear(loot, 0, loot.Length);
                foreach(Transform child in gameObject.transform)
                {
                    Destroy(child.gameObject);
                }
                loot[0] = collision.GetComponent<OrbTypes>();
                collision.transform.SetParent(transform);
                collision.gameObject.SetActive(false);
            }

            else if ((loot[0] != null) && (loot[1] != null) && (loot[2] == null) && (loot[0].type == collision.GetComponent<OrbTypes>().type))
            {
                loot[2] = collision.GetComponent<OrbTypes>();
                collision.transform.SetParent(transform);
                collision.gameObject.SetActive(false);
            }
        }
    }

    void lightningEffect(Collider2D a)
    {
        Destroy(a.transform.parent.gameObject);
    }
    void waterEffect()
    {
        playerHP += 100;
        if (playerHP > maxHP)
        {
            playerHP = maxHP;
        }
    }
    void fireEffect()
    {
        var a = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject b in a)
        {
            Destroy(b);
        }
    }

    void stoneEffect()
    {
        shield += 30;
    }

    IEnumerator fakeFire()
    {
        int count = 5;
        while (count > 0)
        {
            playerHP -= 2;
            //playerHP = Mathf.RoundToInt(playerHP - (playerHP * 0.01f));
            yield return new WaitForSeconds(1);
            count--;
         }
        //for (float i=0 * Time.deltaTime; i < 5; i+=1)
        //{
        //    playerHP = Mathf.RoundToInt(playerHP - (playerHP * 0.01f* Time.deltaTime));
        //    Debug.Log(playerHP);
        //    Debug.Log("Here");
        //}
    }
}
