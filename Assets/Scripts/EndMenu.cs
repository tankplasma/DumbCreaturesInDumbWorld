using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        GameManager.Instance.ReturnToLobby();
    }

    public void GoNextLevel()
    {
        GameManager.Instance.GoNextLevel();
    }

    public void Retry()
    {
        GameManager.Instance.ReloadLevel();
    }
}
