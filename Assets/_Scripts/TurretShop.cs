using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretShop : MonoBehaviour
{
    [SerializeField] Select selectedObject;
    public Turret[] allTurrets;
    [SerializeField] GameObject turretUIPrefab;
    string state;

    void RemoveCurrent()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
    }
    private void Update()
    {
        if(selectedObject.selected == null)
        {
            RemoveCurrent();
            state = "";
            return;
        }

        TurretManager tm = null;
        if (selectedObject.selected.TryGetComponent<TurretManager>(out tm))
        {
            // is a turret here
            if (tm.GetState() == TurretManager.State.Inactive)
            {
                if(state == tm.name + "I")
                {
                    return;
                }

                RemoveCurrent();
                foreach (Turret t in allTurrets)
                {
                    GameObject element = GameObject.Instantiate(turretUIPrefab, this.transform);
                    element.GetComponent<Image>().sprite = t.icon;
                    element.GetComponent<Button>().enabled = true;
                    element.GetComponent<Button>().onClick.AddListener(delegate () 
                    { 
                        tm.Construct(t);
                    });
                }
                state = tm.name + "I";
            }
            // is a turret here
            if (tm.GetState() == TurretManager.State.Construction)
            {
                if (state == tm.name + "C")
                {
                    return;
                }

                RemoveCurrent();
                // DO UI HERE


                // -----------
                state = tm.name + "C";
            }
            // is a turret here
            if (tm.GetState() == TurretManager.State.Active)
            {
                if (state == tm.name + "A")
                {
                    return;
                }

                RemoveCurrent();
                // DO UI HERE


                // -----------
                state = tm.name + "A";
            }
        }

        // MORE TO IMPLEMENT FOR OTHER SELECTABLES (:
    }
}
