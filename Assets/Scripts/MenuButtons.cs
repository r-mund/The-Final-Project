using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void HardMode()
    {
        SceneManager.LoadScene("HardMode");
    }

    public void TimeAttack()
    {
        SceneManager.LoadScene("TimeAttack");
    }

    public void Quit()
    {
            Application.Quit();
    }
}
 
