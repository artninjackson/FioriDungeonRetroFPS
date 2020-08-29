using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public GameObject winScreen;
    public float waitTime = 5;
    public float counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        winScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0;

        if (collision.tag == "Player")
        {
            ///TO DO:
            ///hide player UI
            ///disable player control
            winScreen.SetActive(true);
            /*while(counter < waitTime)
            {
                counter += Time.deltaTime;
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);*/
            //Return to menu
        }
    }

}
