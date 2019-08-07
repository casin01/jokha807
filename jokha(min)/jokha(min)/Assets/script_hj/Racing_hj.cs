using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racing_hj : MonoBehaviour
{
    float moveSpeed = 0;
    public int a;
    int finish;
    public GameObject success;
    public GameObject fail;

    // Start is called before the first frame update
    void Awake()
    {
        success = GameObject.Find("Canvas").transform.Find("Success").gameObject;
        fail = GameObject.Find("Canvas").transform.Find("Fail").gameObject;
    }
    void Start()
    {
        moveSpeed = RandomSpeed_hj.speed[a];
    }

    // Update is called once per frame
    void Update()
    {
        if (BattingManager_hj.start == 1 && finish != 1)
            transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed + Time.deltaTime);
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Destination")
        {
            finish = 1;
            if (finish == 1 && BattingManager_hj.guess == RandomSpeed_hj.winner)
            {
                success.SetActive(true);
            }
            else
                fail.SetActive(true);
        }
    }
}
