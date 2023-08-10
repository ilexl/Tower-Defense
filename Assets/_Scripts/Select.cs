using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Select : MonoBehaviour
{
    [SerializeField] float maxDistance = 99999f;
    public Transform selected;
    [SerializeField] GraphicRaycaster raycaster;

    //replace Update method in your class with this one
    void Update()
    {
        //if mouse button (left hand side) pressed instantiate a raycast
        if (Input.GetMouseButtonDown(0))
        {
            //create a ray cast and set it to the mouses cursor position in game
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
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
        else
        {
            PointerEventData pointerEventData = new(EventSystem.current)
            {
                position = Input.mousePosition
            };
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            foreach (var result in raycastResults)
            {
                
                bool valid = result.gameObject.TryGetComponent<TurretUI>(out TurretUI tUI);
                if (valid)
                {
                    
                    if (selected.TryGetComponent<TurretManager>(out TurretManager tm))
                    {
                        tm.ShowRangeCustom(true, tUI.turret.range);
                    }
                }
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                if (IsMouseOverUI())
                {
                    // Do nothing
                }
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Selectable") && !IsMouseOverUI())
                {
                    Transform possTurret = hit.transform;
                    if (possTurret.TryGetComponent<TurretManager>(out TurretManager tm))
                    {
                        tm.ShowRange(true);
                    }
                }
            }
        }
    }

    bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
