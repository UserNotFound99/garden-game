using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageButton : MonoBehaviour
{
    public void Start()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked!!");
    }

    public void TEST()
    {
        Debug.Log("test!");
    }
}
