using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    [SerializeField] Transform modelParent;
    [SerializeField] GameObject model;
    [SerializeField] State state;
    [SerializeField] float buildSpeed;
    [SerializeField] Transform bottomPos;
    [SerializeField] Turret currentTurret;
    [SerializeField] Turret buildingTurret;
    int moveInternal = 0; // 0 no move - 1 move up - 2 move down - 3 check

    public Turret GetCurrent() { return currentTurret; }
    public Turret GetBuilding() { return buildingTurret; }

    public GameObject GetTurretModel() { return model; }

    private void Awake()
    {
        state = State.Inactive;
    }

    public enum State
    {
        Active,
        Construction,
        Inactive
    }
    
    public State GetState() { return state; }

    private void Active()
    {

    }

    private void Construction()
    {
        // Building Complete
        if(moveInternal == 0)
        {
            if(buildingTurret == null)
            {
                currentTurret = null;
                state = State.Inactive; 
                return;
            }
            else
            {
                currentTurret = buildingTurret;
                buildingTurret = null;
                state = State.Active; 
                return;
            }
        }
        else
        {
            // if turret in slot
            if(currentTurret != null && model != null)
            {
                // check if reached goal position
                float distance = Vector3.Distance(model.transform.localPosition, bottomPos.position);
                if (distance < 0.05f)
                {
                    GameObject.Destroy(model);
                    currentTurret = null;
                    model = null;
                    moveInternal = 1; // time to move one up
                }
                else // move twrds goal position
                {
                    moveInternal = 2; // continue to move down
                    model.transform.Translate(new Vector3(0, buildSpeed * Time.deltaTime * -1, 0));
                }

            }
            else // if turret not in slot
            {
                // safe guard no new turret to build
                if(buildingTurret == null)
                {
                    moveInternal = 0;
                    return;
                }
                // create model of new turret
                if (model == null)
                {
                    // check if null
                    if(buildingTurret.modelPrefab == null)
                    {
                        buildingTurret = null;
                        currentTurret = null;
                        return;
                    }

                    model = GameObject.Instantiate(buildingTurret.modelPrefab, modelParent);
                    model.transform.localScale = Vector3.one * 0.5f;
                    model.transform.position = bottomPos.position;
                }

                // check if turret pos is close enough
                float distance = Vector3.Distance(model.transform.localPosition, Vector3.zero);
                if (distance < 0.05f)
                {
                    moveInternal = 0;
                }
                else // move twrds turret pos
                {
                    model.transform.Translate(new Vector3(0, buildSpeed * Time.deltaTime, 0));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (GetState())
        {
            case State.Active:
            {
                Active();
                break;
            }
                
            case State.Construction:
            {
                Construction();
                break;
            }
            
            default:
            case State.Inactive:
            {
                return; // return to stop unwasted processing power
            }

        }
    }

    public void Construct(Turret toConstruct)
    {
        state = State.Construction;
        moveInternal = 3;
        buildingTurret = toConstruct;
    }
    
}
