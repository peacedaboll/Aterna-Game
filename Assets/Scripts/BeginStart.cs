using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginStart : MonoBehaviour
{
    public void PressStart()
    {
        SceneManager.LoadScene("ChoiseCharacter");
    }
}
