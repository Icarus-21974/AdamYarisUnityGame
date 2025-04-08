using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScreen : MonoBehaviour
{

    public AudioSource mainMenuAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            changeScene("AdamYarisCreate3");
        }
    }

    public void changeScene(string scenename)
    {
        SceneManager.GetSceneByName(scenename);
        SceneManager.LoadScene(scenename);
    }
}
