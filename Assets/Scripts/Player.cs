using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform bulletHolder;
    public int testINT = 0;
    public int playerDmg = 0;
    public int playerHP = 10000;
    public int maxHP = 100;
    public int shield = 0;
    public int maxShield = 30;
    private Vector2 mousePosition;
    public float moveSpeed = 0.1f;
    public float dotDuration = 5;
    public Transform topBorder;
    public Transform bottomBorder;
    public GameObject bulletPrefab;
    public float atkSpeed = 5f;
    private float atkRate;
    public bool allowAtk = true;
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
    public bool FakeWater = false;
    public bool moveable = true;
    private Combat combat;

    void Start()
    {
        //StartCoroutine(fakeFire());
        midPoint = (topBorder.position.y + bottomBorder.position.y) / 2;
        combat = new Combat();
        loot = new OrbTypes[3];
        atkRate = 1 / atkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        attack();
        if (Input.GetMouseButton(0))
        {
            if (FakeStone == false && moveable == true)
            {
                FollowMouse();
            }
            else if (FakeStone == true && moveable == true)
            {
                ReverseMouse();
            }
            else
            {
                return;
            }
        }
        if (playerHP <= 0)
        {
            Destroy(gameObject);
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
            combat.takeDmg(ref playerHP, ref collision.GetComponent<AutoDestroy>().damage, ref shield);
            if (lightning == true)
            {
                Destroy(collision.transform.parent.gameObject);
            }
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Orb"))
        {
            if (collision.GetComponent<OrbTypes>().type == OrbTypes.OrbType.FakeFire)
            {
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
                foreach (Transform child in gameObject.transform)
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
                switchEffect(collision.GetComponent<OrbTypes>().type);
                Array.Clear(loot, 0, loot.Length);
            }
        }
        else if (collision.CompareTag("FakeOrb"))
        {
            switchEffect(collision.GetComponent<OrbTypes>().type);
            Destroy(collision.gameObject);
            Array.Clear(loot, 0, loot.Length);
        }

        else if (collision.CompareTag("BossBullet"))
        {
            combat.takeDmg(ref playerHP, ref collision.transform.parent.GetComponent<BossSpray>().bossDMG, ref shield);
            Destroy(collision.gameObject);
        }
    }

    IEnumerator lightningEffect()
    {
        lightning = true;
        yield return new WaitForSeconds(5);
        lightning = false;
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
        foreach (GameObject b in a)
        {
            Destroy(b);
        }
    }

    void stoneEffect()
    {
        shield += 30;
        if (shield > maxShield)
        {
            shield = maxShield;
        }
    }

    IEnumerator fakeFire()
    {
        FakeFire = true;
        for (int i = 0; i < 5; i++)
        {
            playerHP = playerHP - Mathf.RoundToInt((playerHP * 0.02f));
            //playerHP = Mathf.RoundToInt(playerHP - (playerHP * 0.01f));
            yield return new WaitForSeconds(1);
        }
        FakeFire = false;
    }

    IEnumerator fakeWater()
    {
        FakeWater = true;
        moveSpeed = 0.05f;
        yield return new WaitForSeconds(5);
        moveSpeed = 0.1f;
        FakeWater = false;
    }

    IEnumerator fakeEarth()
    {
        FakeStone = true;
        yield return new WaitForSeconds(5);
        FakeStone = false;
    }

    IEnumerator fakeLightning()
    {
        FakeLightning = true;
        moveable = false;
        allowAtk = false;
        yield return new WaitForSeconds(5);
        moveable = true;
        allowAtk = true;
        FakeLightning = false;

    }

    void switchEffect(OrbTypes.OrbType a)
    {
        switch (a)
        {
            case OrbTypes.OrbType.FakeEarth:
                {
                    StartCoroutine(fakeEarth());
                    break;
                }
            case OrbTypes.OrbType.FakeWater:
                {
                    StartCoroutine(fakeWater());
                    break;
                }
            case OrbTypes.OrbType.FakeFire:
                {
                    StartCoroutine(fakeFire());
                    break;
                }
            case OrbTypes.OrbType.FakeLightning:
                {
                    StartCoroutine(fakeLightning());
                    break;
                }
            case OrbTypes.OrbType.Earth:
                {
                    stone = true;
                    stoneEffect();
                    stone = false;
                    break;
                }
            case OrbTypes.OrbType.Fire:
                {
                    fire = true;
                    fireEffect();
                    fire = false;
                    break;
                }
            case OrbTypes.OrbType.Water:
                {
                    water = true;
                    waterEffect();
                    water = false;
                    break;
                }
            case OrbTypes.OrbType.Lightning:
                {
                    lightning = true;
                    lightningEffect();
                    lightning = false;
                    moveable = true;
                    break;
                }
        }
    }

    void attack()
    {
        if (Time.time >= atkRate && allowAtk == true)
        {
            Instantiate(bulletPrefab, new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity).transform.SetParent(bulletHolder);//.transform.SetParent(gameObject.transform);
            atkRate = atkRate + (1 / atkSpeed);
        }
        else
        {
            return;
        }
        atkRate = atkRate + (1 / atkSpeed);
    }

}
