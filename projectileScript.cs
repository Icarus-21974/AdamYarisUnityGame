using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    public float xmove;
    public Vector3 pMove;
    public float timerProjectile;
    public int dirForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pMove = new Vector3(xmove, 0);
        GetComponent<Transform>().position += pMove;
        if (timerProjectile > 1.5f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        timerProjectile+= Time.fixedDeltaTime;
    }
}
