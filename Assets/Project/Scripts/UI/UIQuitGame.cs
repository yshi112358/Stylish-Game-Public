using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class UIQuitGame : MonoBehaviour
{
    private bool _cursorOn = false;
    private void Start()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _cursorOn = true;
        }
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }
    public void Back()
    {
        if (_cursorOn)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
