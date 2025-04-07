using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class menuManager : MonoBehaviour
{
    public bool p2Text;
    public bool p1Text;
    public bool outOfTime;
    public TextMeshProUGUI gameStatus;
    // Start is called before the first frame update
    void Start()
    {
        p2Text = false;
        p1Text = false;
        outOfTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTwoScript.p2Static == true)
        {
            p2Text = true;
        }

        if (PlayerScript.p1Static == true)
        {
            p1Text = true;
            //1234
        }

        if(stupivisor.timeOut == true)
        {
            outOfTime = true;
        }

        if (p2Text == true)
        {
            gameStatus.text = "ur mom hella gay";
        }

        if (p1Text)
        {
            gameStatus.text = stupivisor.textStatic;
        }

        if(outOfTime == true)
        {
            gameStatus.text = stupivisor.textStatic;
        }
    }
}
