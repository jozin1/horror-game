using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Assets
{
    class movement : MonoBehaviour
    {
        [SerializeField] GameObject ja;
        CapsuleCollider caps;
        BoxCollider box;
        Rigidbody rgt;
        public bool grounded = true;
        public float distToGround;
        float maxVel;
        float height;
        float acceleration = 80f;
        float jump = 6f;


        void Start()
        {
            rgt = GetComponent<Rigidbody>();
            caps = GetComponent<CapsuleCollider>();
            box = GetComponent<BoxCollider>();
            

        }

        void Update()
        {
            isGrounded();
            Chodzenie();
            Skok();
        }

        void FixedUpdate()
        {
           
        }

        bool isGrounded()
        {
            RaycastHit hit = new RaycastHit();
            Physics.Raycast(transform.position, -Vector3.up, out hit);
            distToGround = hit.distance;
                if(distToGround < 2.8f)
            {
                Debug.DrawRay(transform.position, -Vector3.up * 2.8f, Color.green);
                grounded = true;
                return true;
            }
            else
            {
                Debug.DrawRay(transform.position, -Vector3.up * 2.8f, Color.red);
                grounded = false;
                return false;
            }
            
        }

        



        int inputToDirection()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                maxVel = 7f;
                height = 2.7f;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                maxVel = 3f;
                height = 0.7f;
            }
            else
            {
                maxVel = 5f;
                height = 2.7f;
            }


            int w = Convert.ToInt16(Input.GetKey(KeyCode.W));   //1
            int s = Convert.ToInt16(Input.GetKey(KeyCode.S));   //5
            int a = Convert.ToInt16(Input.GetKey(KeyCode.A));   //7
            int d = Convert.ToInt16(Input.GetKey(KeyCode.D));   //3

            int caseSwitch = 8 * d + 4 * a + 2 * s + w;

            switch (caseSwitch)
            {
                case 0:
                    return 0;
                    
                case 1:
                    return 1;
                    
                case 2:
                    return 5;
                    
                case 3:
                    return 0;
                    
                case 4:
                    return 7;
                    
                case 5:
                    return 8;
                    
                case 6:
                    return 6;
                    
                case 7:
                    return 1;
                    
                case 8:
                    return 3;
                    
                case 9:
                    return 2;
                    
                case 10:
                    return 4;

                case 11:
                    return 3;

                case 12:
                    return 0;
                    
                case 13:
                    return 1;
                    
                case 14:
                    return 5;
                    
                case 15:
                    return 0;

                default:
                    return 0;
                    
            }
        }

        void Chodzenie()
        {
            if (inputToDirection() != 0 && rgt.velocity.magnitude < maxVel && grounded)
            {

                rgt.velocity = new Vector3(
                    rgt.velocity.x +
                    Camera.main.transform.forward.x * (float)Math.Cos(Math.PI * (inputToDirection() - 1) / 4) * Time.deltaTime * acceleration +
                    Camera.main.transform.right.x * (float)Math.Sin(Math.PI * (inputToDirection() - 1) / 4) * Time.deltaTime * acceleration,
                    rgt.velocity.y - (distToGround - height)*2f,
                    rgt.velocity.z +
                    Camera.main.transform.forward.z * (float)Math.Cos(Math.PI * (inputToDirection() - 1) / 4) * Time.deltaTime * acceleration +
                    Camera.main.transform.right.z * (float)Math.Sin(Math.PI * (inputToDirection() - 1) / 4) * Time.deltaTime * acceleration);
            }
            else if(grounded)
            {
                rgt.velocity = new Vector3(
                    rgt.velocity.x - Time.deltaTime * 10f * rgt.velocity.x,
                    rgt.velocity.y - (distToGround - height)*0.1f * rgt.velocity.y * rgt.velocity.y,
                    rgt.velocity.z - Time.deltaTime * 10f * rgt.velocity.z);
            }
           
        }

        void Skok()
        {
            
            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                rgt.velocity = new Vector3(rgt.velocity.x, jump, rgt.velocity.z);
            }
        }
    }
}
