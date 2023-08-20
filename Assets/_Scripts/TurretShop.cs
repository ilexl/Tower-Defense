using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretShop : MonoBehaviour
{
    [SerializeField] Select selectedObject;
    public Turret[] allTurrets;
    public Turret bin;
    [SerializeField] GameObject turretUIPrefab;
    string state;
    TurretManager tm = null;

    void RemoveCurrent()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
    }

    GameObject AddTurretShop(Turret t)
    {
        GameObject element = GameObject.Instantiate(turretUIPrefab, this.transform);
        element.GetComponent<Image>().sprite = t.icon;
        element.GetComponent<TurretUI>().turret = t;
        element.GetComponent<Button>().enabled = true;
        element.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            FindAnyObjectByType<Money>().ChangeBalance(-t.cost);
            tm.Construct(t);
        });
        return element;
    }

    void TurretShopMenu()
    {
        // is a turret here
        if (tm.GetState() == TurretManager.State.Inactive)
        {
            if (state == tm.name + "I")
            {
                return;
            }
            //Debug.Log("Inactive");
            RemoveCurrent();
            foreach (Turret t in allTurrets)
            {
                // do check to ensure all lvl 1 turrets
                if (t.name[2] == '1')
                {
                    AddTurretShop(t);
                }
            }
            state = tm.name + "I";
        }
        // is a turret here
        if (tm.GetState() == TurretManager.State.Construction)
        {
            if (state == tm.name + "C")
            {
                foreach (Transform t in transform)
                {
                    t.GetComponent<Button>().interactable = false;
                }
                return;
            }
            //Debug.Log("Construction");
            RemoveCurrent();
            foreach (Turret t in allTurrets)
            {
                // do check to ensure all lvl 1 turrets
                if (t.name[2] == '1' || tm.GetBuilding() == t)
                {
                    if (t.name[1] == tm.GetBuilding().name[1] && t.name[0] == tm.GetBuilding().name[0]) { continue; }
                    GameObject element = AddTurretShop(t);
                    element.GetComponent<Button>().interactable = false; // grey out while constructing
                }

                if ((t.name[0] == tm.GetBuilding().name[0] && t.name[1] == tm.GetBuilding().name[1]) && (t.name[2] == (tm.GetBuilding().name[2]) + 1))
                {
                    GameObject element = AddTurretShop(t);
                    element.GetComponent<Button>().interactable = false; // grey out while constructing
                }
            }
            
            state = tm.name + "C";
        }
        // is a turret here
        if (tm.GetState() == TurretManager.State.Active)
        {
            if (state == tm.name + "A")
            {
                
                return;
            }
            //Debug.Log("Active");

            

            RemoveCurrent();
            // DO UI HERE
            foreach (Turret t in allTurrets)
            {
                // do check to ensure all lvl 1 turrets
                if (t.name[2] == '1' || tm.GetCurrent() == t)
                {
                    if (t.name[1] == tm.GetCurrent().name[1] && t.name[0] == tm.GetCurrent().name[0]) { continue; }
                    GameObject element = AddTurretShop(t);
                    element.GetComponent<Button>().interactable = true; // grey out while constructing
                }

                if ((t.name[0] == tm.GetCurrent().name[0] && t.name[1] == tm.GetCurrent().name[1]) && (t.name[2] == (tm.GetCurrent().name[2]) + 1))
                {
                    GameObject element = AddTurretShop(t);
                    element.GetComponent<Button>().interactable = true; // grey out while constructing
                }
            }
            AddTurretShop(bin);

            // -----------
            state = tm.name + "A";
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

        tm = null;
        if (selectedObject.selected.TryGetComponent<TurretManager>(out tm))
        {
            TurretShopMenu();
        }

        // ------------------------------------------------------------------------------
        // THIS CODE IS FOR THE TURRET SHOP ONLY - OTHER SELECTABLE NEED THEIR OWN SCRIPT
        // ------------------------------------------------------------------------------
    }
}
