using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private Vector3 screenPoint;
    public static bool attivo = true;

    private void OnMouseDown()
    {
        if (attivo)
        {
            screenPoint = UnityEngine.Camera.main.WorldToScreenPoint(gameObject.transform.position);
        }
        //print(screenPoint);
    }

    private void OnMouseDrag()
    {
        if (attivo)
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 currentPos = UnityEngine.Camera.main.ScreenToWorldPoint(currentScreenPoint);
            transform.position = currentPos;
        }
        //print(currentScreenPoint);
        //print(currentPos);
    }
}
