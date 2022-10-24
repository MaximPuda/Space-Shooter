using UnityEngine;

public class UIController : MonoBehaviour
{
    public void RestartBTNClick()
    {
        GameManager.Instance.Restart();
    }

    public void MainMenuBTNClick()
    {
        GameManager.Instance.ExitToMainMenu();
    }

    public void PauseBTNClick()
    {

        GameManager.Instance.Pause();
    }    
}
