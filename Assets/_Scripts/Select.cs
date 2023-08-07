using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Select : MonoBehaviour
{
    [SerializeField] float maxDistance = 99999f;
    public Transform selected;

    //replace Update method in your class with this one
    void Update()
    {
        //if mouse button (left hand side) pressed instantiate a raycast
        if (Input.GetMouseButtonDown(0))
        {
            //create a ray cast and set it to the mouses cursor position in game
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (IsMouseOverUI())
                {
                    //Debug.Log("UI HIT");
                }
                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Selectable") && !IsMouseOverUI())
                {
                    selected = hit.transform;
                }
            }
            else if(IsMouseOverUI())
            {
                //Debug.Log("UI HIT");
            }
            else
            {
                selected = null;
            }


        }
    }

    bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
