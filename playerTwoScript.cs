using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTwoScript : MonoBehaviour
{
    public Vector3 addLeft;
    public Vector3 addRight;
    public Vector3 addUp;

    public int dir;
    public GameObject lSpawner;
    public GameObject rSpawner;
    public GameObject projectile;
    public GameObject superProjectile;

    public bool extraPower;
    public bool doubleJump;
    public bool canJump;
    public bool inAir;

    public float yPos;
    public float pushTimer;
    public Vector3 projForce;

    public bool p2FellOff;
    public static bool p2Static;
    // Start is called before the first frame update
    void Start()
    {
        p2FellOff = false;
        p2Static = false;
    }

    // Update is called once per frame
    void Update()
    {


        yPos = GetComponent<Transform>().position.y;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody2D>().AddForce(addUp);
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<Rigidbody2D>().AddForce(addLeft);
            dir = -1;
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<Rigidbody2D>().AddForce(addRight);
            dir = 1;
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            projSpawn();
        }

        if (yPos <= -3)
        {
            p2FellOff=true;
            p2Static=true;
        }
    }

    void projSpawn()
    {
        if (extraPower == true)
        {
            if (dir == 1)
            {
                superProjectile.GetComponent<superProjectile>().xmove = .04f;
                Instantiate(superProjectile, rSpawner.GetComponent<Transform>().position, Quaternion.identity);
            }

            if (dir == -1)
            {
                superProjectile.GetComponent<superProjectile>().xmove = -.04f;
                Instantiate(superProjectile, lSpawner.GetComponent<Transform>().position, Quaternion.identity);
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
        if (collision.gameObject.tag == "ground")
        {
            canJump = true;
            inAir = false;

        }

        if (collision.gameObject.tag == "DoublePower")
        {
            extraPower = true;
        }

        if (collision.gameObject.tag == "JumpPower")
        {
            doubleJump = true;
        }

        if (collision.gameObject.tag == "Projectile")
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

            if (pushTimer > 2.5f)
            {
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.tag == "SuperProjectile")
        {
            pushTimer = 0;

            if (dir == 1)
            {
                GetComponent<Rigidbody2D>().AddForce(projForce*2);
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
