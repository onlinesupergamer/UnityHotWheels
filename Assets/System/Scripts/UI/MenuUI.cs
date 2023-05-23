using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void EnterRace()
    {
        SceneManager.LoadScene(1);
    }
    
}
