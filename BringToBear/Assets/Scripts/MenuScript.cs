using UnityEngine;
using UnityEngine.InputSystem;

public class MenuScript : MonoBehaviour
{
    Vector2 mousePos = Vector2.zero;
    public Camera mainCam;

    public void UpdateMousePos(InputAction.CallbackContext value)
    {
        mousePos = mainCam.ScreenToWorldPoint(mousePos);
        Debug.Log(mousePos);
    }
}
