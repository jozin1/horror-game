using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {
    [SerializeField] public GameObject ja;
    public GameObject reka;
    public GameObject kulka;
    public bool isholding;
    AudioSource audio;
    public float odleglosc;
    public AudioClip step;
    Camera cam;
    public GameObject obiekt;
    public bool test;
    public bool test2;
    public bool test3;
    public float speedH = 4f;
    public float speedV = 4f;
    public float pozycja;
    public float timer = 0;
    public move2 mv2;
    public float yaw=0.0f;
    public float pitch=0.0f;
    public bool noszenie;

    public float moving =0f;      //ruch kamery w ten sposób, by wyglądała jak przy prawdziwym chodzeniu
    public float limit = 1;
    float cos = 0.04f;
    float cos2 = 0.009f;
    float speed;
    void Start()
    {
        cam = GetComponent<Camera>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        Sounds();
        CameraRotate();
        CameraMove();
        Holding();
    }

    void CameraRotate()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");
        if (pitch > 60)
        {
            pitch = 60;
        }
        if (pitch < -60)
        {
            pitch = -60;
        }
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    void CameraMove()
    {
       
        pozycja = this.transform.position.y - ja.transform.position.y;
        if (mv2.bieganie)
        {
            test = true;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + cos, cam.transform.position.z);
                moving += limit;
                if (moving > 7)
                {
                    limit = -limit;
                    cos = -cos;
                }
                if (moving < -7)
                {
                    limit = -limit;
                    cos = -cos;
                }
            }
        }

        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl) && pozycja>1.9f)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - 0.04f, cam.transform.position.z);
        }
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl) && pozycja < 1.7f)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + 0.04f, cam.transform.position.z);
        }

    }

    void Sounds()
    {
        if(mv2.chodzenie && mv2.grounded)
        {
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
            {
                timer += Time.deltaTime;
                if (timer >= 0.5f)
                {
                    audio.PlayOneShot(step);
                    timer = 0;
                }
            }
        }
    }
    void Holding()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));
        Debug.DrawRay(ray.origin, ray.direction*4, Color.blue);
        if (Input.GetMouseButtonDown(1))
        {
            obiekt.GetComponent<Rigidbody>().useGravity = true;
            obiekt = null;
            isholding = false;
        }
        if (Physics.Raycast(ray, out hit,4) && hit.collider.tag=="moving")
        {
            reka.SetActive(true);
            if (Input.GetMouseButton(0))
            {
                isholding = true;
                obiekt = hit.collider.gameObject;
                for (int i = 0; i < 1; i++)
                {
                    kulka.transform.position = obiekt.transform.position;
                    odleglosc= Vector3.Distance(kulka.transform.position, Camera.main.transform.position);
                }
                obiekt.GetComponent<Rigidbody>().useGravity = false;
                obiekt.GetComponent<Rigidbody>().detectCollisions = true;
                obiekt.transform.position = kulka.transform.position;
                reka.SetActive(false);
            }
        }
        else
        {
            reka.SetActive(false);
        }
        if(isholding)
        {
            obiekt.transform.position = kulka.transform.position;
        }

    }
}

