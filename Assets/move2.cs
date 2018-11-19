using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move2 : MonoBehaviour {
    [SerializeField] GameObject ja;
    CapsuleCollider caps;
    BoxCollider box;
    public float jump=30f;
    public GameObject kamera;
    public float speed=0.1f;
    Rigidbody rgt;
    public bool grounded;
    public bool test;
    public float distToGround;
    public float cos;

    public bool chodzenie;
    public bool bieganie;
    public bool kucanie;

    void Start () {
        rgt = GetComponent<Rigidbody>();
        caps = GetComponent<CapsuleCollider>();
        box = GetComponent<BoxCollider>();

    }

    // Update is called once per frame
     void Update()
    {
        Skok();
        Chodzenie();
    }

    void FixedUpdate()
    {
        Run();
    }

    public void Chodzenie()                          
    {
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))

            {
                rgt.MovePosition(transform.position + new Vector3(Camera.main.transform.forward.x, 0.0f, Camera.main.transform.forward.z) * speed);
            }
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                rgt.MovePosition(transform.position + new Vector3(-Camera.main.transform.right.x, 0.0f, -Camera.main.transform.right.z) * speed);
            }
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                rgt.MovePosition(transform.position + new Vector3(Camera.main.transform.right.x, 0.0f, Camera.main.transform.right.z) * speed);
            }
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                rgt.MovePosition(transform.position + new Vector3(-Camera.main.transform.forward.x, 0.0f, -Camera.main.transform.forward.z) * speed);
            }
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            rgt.MovePosition(transform.position + new Vector3(Camera.main.transform.forward.x - Camera.main.transform.right.x, 0.0f, Camera.main.transform.forward.z- Camera.main.transform.right.z) * 0.7f*speed);
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
        {
            rgt.MovePosition(transform.position + new Vector3(Camera.main.transform.forward.x + Camera.main.transform.right.x, 0.0f, Camera.main.transform.forward.z + Camera.main.transform.right.z) * 0.7f * speed);
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            rgt.MovePosition(transform.position + new Vector3(-Camera.main.transform.forward.x + Camera.main.transform.right.x, 0.0f, -Camera.main.transform.forward.z + Camera.main.transform.right.z) * 0.7f * speed);
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            rgt.MovePosition(transform.position + new Vector3(-Camera.main.transform.forward.x - Camera.main.transform.right.x, 0.0f, -Camera.main.transform.forward.z - Camera.main.transform.right.z) * 0.7f * speed);
        }
    }
    void Run()                         //i kucanie
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (caps.height > 0.8f)
            {
                caps.height -= 0.08f;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.09f, this.transform.position.z);
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 0.07f, Camera.main.transform.position.z);
            }
            speed = 0.05f;
            kucanie = true;
            bieganie = false;
            chodzenie = false;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            speed = 0.18f;
            kucanie = false;
            bieganie = true;
            chodzenie = false;
        }
        else
        {
            speed = 0.1f;
            kucanie = false;
            bieganie = false;
            chodzenie = true;
        }
        if (caps.height <=1.35 && !kucanie)
        {
            caps.height += 0.08f;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.09f, this.transform.position.z);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 0.07f, Camera.main.transform.position.z);

        }
    }
    void Skok()
    {
        grounded = Physics.Raycast(transform.position, -Vector3.up, 2.8f);
        Debug.DrawRay(transform.position, -Vector3.up*2.8f, Color.yellow);
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rgt.velocity = new Vector3(0, jump, 0);
        }
    }
}
