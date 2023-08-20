using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hover : MonoBehaviour
{
    public Transform hover;
    [SerializeField] Select select;
    [SerializeField] GraphicRaycaster raycaster;

    //replace Update method in your class with this one
    void Update()
    {
        hover = null;
        PointerEventData pointerEventData = new(EventSystem.current)
        {
            position = Input.mousePosition
        };
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        foreach (var result in raycastResults)
        {
            bool valid = result.gameObject.TryGetComponent<TurretUI>(out TurretUI tUI);
            if(valid) { hover = tUI.transform; }
            
            if (select.selected == null) { break; }
            select.selected.TryGetComponent<TurretManager>(out TurretManager tm);
            if(tm == null) { break; }
            if (valid)
            {
                tm.ShowRangeCustom(true, tUI.turret.range);
            }
        }
    }
}
