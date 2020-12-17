using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        void Start()
        {
            midPoint = (topBorder.position.y + bottomBorder.position.y) / 2;
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
            Debug.Log("Hitted");
            if (collision.CompareTag("Bullet"))
            {
                //Destroy(collision.transform.parent.gameObject);
                Destroy(collision);
            }
        }
    }
