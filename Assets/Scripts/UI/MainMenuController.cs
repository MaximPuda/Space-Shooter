using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public void PointerClick(int levelIndex)
    {
        GameManager.Instance.LoadLevel(levelIndex);
    }
}
