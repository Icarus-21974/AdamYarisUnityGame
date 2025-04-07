using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class superProjectile : MonoBehaviour
{
    public float xmove;
    public Vector3 pMove;
    public float timerProjectile;
    // Start is called before the first frame update
    void Start()
    {
        //123
        //1234
        //1234
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
        timerProjectile += Time.fixedDeltaTime;
    }
}
