using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour
{
    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Vector2 cursorHotspot = new Vector2(96, 96);
    [SerializeField] Texture2D enemyCursor = null;
    [SerializeField] Texture2D UnknownCursor = null;

    CameraRaycaster cameraRaycaster;

	// Use this for initialization
	void Start ()
    {
        cameraRaycaster = GetComponent<CameraRaycaster>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch (cameraRaycaster.layerHit)
        {
            case Layer.Walkable:
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                break;

            case Layer.Enemy:
                Cursor.SetCursor(enemyCursor, cursorHotspot, CursorMode.Auto);
                break;

            case Layer.RaycastEndStop:
                Cursor.SetCursor(UnknownCursor, cursorHotspot, CursorMode.Auto);
                break;

            default:
                Debug.LogError("Don't know what cursor to show");
                return;
        }
	}
}
