using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour
{
    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D enemyCursor = null;
    [SerializeField] Texture2D UnknownCursor = null;
    [SerializeField] Vector2 cursorHotspot = new Vector2(0, 0);

    CameraRaycaster cameraRaycaster;

	// Use this for initialization
	void Start ()
    {
        cameraRaycaster = GetComponent<CameraRaycaster>();
        cameraRaycaster.onLayerChange += OnLayerChanged; // Registering
	}
	
	void OnLayerChanged (Layer newLayer)
    {
        switch (newLayer)
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

// TODO Consider De-registing OnlayerChanged on leaving all game scenes