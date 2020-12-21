using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTargetPos : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2[][] target;

    void Start()
    {
        target = new Vector2[10][];
        for (int i = 0; i < target.Length; i++)
        {
            target[i] = new Vector2[10];
            for (int a = 0; a < 10; a++)
            {
                newPos();
                target[i][a] = transform.position;
                //Debug.Log(target[i][a]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newPos()
    {
        float X = Random.Range(-5f,5f);
        float Y = Random.Range(0.5f, 6.5f);
        transform.position = new Vector2(X,Y);
    }
}
