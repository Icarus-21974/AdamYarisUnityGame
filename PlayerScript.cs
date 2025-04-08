using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Vector3 addLeft;
    public Vector3 addRight;
    public Vector3 addUp;

    public Vector3 projForce;

    public int dir;

    public bool canJump;
    public bool doubleJump;
    public bool extraPower;
    public bool inAir;

    public GameObject lSpawner;
    public GameObject rSpawner;
    public GameObject projectile;
    public GameObject superProjectile;

    public float pushTimer;

    public float yPos;

    public bool p1FellOff;
    public static bool p1Static;
    // Start is called before the first frame update
    void Start()
    {
        p1FellOff = false;
        p1Static = false;
    }

    // Update is called once per frame
    void Update()
    {


        yPos = GetComponent<Transform>().position.y;

        if (yPos <= -3)
        {
            p1FellOff = true;
            p1Static = true;
            //123
        }

        if (Input.GetKeyDown(KeyCode.W) && canJump ==true)
        {
            GetComponent<Rigidbody2D>().AddForce(addUp);
            canJump = false;
            inAir = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().AddForce(addLeft);
            dir = -1;
        }

        if(Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().AddForce(addRight);
            dir = 1;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            projSpawn();
        }

        if( inAir==true && doubleJump == true)
        {
            if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
            {
                GetComponent<Rigidbody2D>().AddForce(addUp);
                doubleJump = false;
            }
        }
    }

    private void FixedUpdate()
    {
        //pushTimer += Time.fixedDeltaTime;
    }

    void projSpawn()
    {
        if (extraPower == true)
        {
            if (dir == 1)
            {
                superProjectile.GetComponent<superProjectile>().xmove =.04f;
                Instantiate(superProjectile, rSpawner.GetComponent<Transform>().position, Quaternion.identity);
                extraPower = false;
            }

            if(dir ==-1)
            {
                superProjectile.GetComponent<superProjectile>().xmove = -.04f;
                Instantiate(superProjectile, lSpawner.GetComponent<Transform>().position, Quaternion.identity);
                extraPower = false;
            }
        }

        else
        {
            if (dir == 1)
            {
                projectile.GetComponent<projectileScript>().xmove = .02f;
                Instantiate(projectile, rSpawner.GetComponent<Transform>().position, Quaternion.identity);

            }

            if (dir == -1)
            {
                projectile.GetComponent<projectileScript>().xmove = -.02f;
                Instantiate(projectile, lSpawner.GetComponent<Transform>().position, Quaternion.identity);

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Ground")
        {
            canJump = true;
            inAir = false;
            
        }

        if(collision.gameObject.tag == "DoublePower") 
        {
            extraPower = true;
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "JumpPower") 
        {
            doubleJump = true;
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag =="Projectile") 
        {
            pushTimer = 0;

            if (dir == 1)
            {
                GetComponent<Rigidbody2D>().AddForce(projForce);
            }

            if (dir == -1)
            {
                GetComponent<Rigidbody2D>().AddForce(-projForce);
            }

            if(pushTimer > 2.5f)
            {
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.tag == "SuperProjectile")
        {
            pushTimer = 0;

            if (dir == 1)
            {
                GetComponent<Rigidbody2D>().AddForce(projForce * 2);
            }

            if (dir == -1)
            {
                GetComponent<Rigidbody2D>().AddForce(-projForce * 2);
            }

            if (pushTimer > 2.5f)
            {
                Destroy(gameObject);
            }
        }
    }
}
