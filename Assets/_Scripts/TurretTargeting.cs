using Codice.CM.Client.Differences.Graphic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargeting : MonoBehaviour
{
    [SerializeField] TurretManager manager;
    [SerializeField] float idleRotateSpeed = 0;
    [SerializeField] GameObject currentTarget;
    [SerializeField] Transform EnemiesParent;

    // Update is called once per frame
    void Update()
    {
        if(manager == null) { return; }
        Turret turret = manager.GetCurrent();
        if(turret == null) { return; }
        currentTarget = null;

        foreach(Transform e in EnemiesParent)
        {
            if (Vector3.Distance(transform.position, e.position) > turret.range)
            {
                continue; // not in range
            }
            if (!e.TryGetComponent<Enemy>(out Enemy enemy)) { continue; }
            if(currentTarget == null)
            {
                currentTarget = e.gameObject;
                continue;
            }
            if (currentTarget.GetComponent<Enemy>().distanceToEnd > enemy.distanceToEnd)
            {
                // if the distance to the end is shorter then we will target that enemy instead
                currentTarget = e.gameObject;
                continue;
            }
        }

        if(currentTarget != null)
        {
            manager.GetTurretModel().transform.LookAt(currentTarget.transform);
            manager.GetTurretModel().transform.eulerAngles = new Vector3(0, manager.GetTurretModel().transform.eulerAngles.y, 0);
        }
        else
        {
            manager.GetTurretModel().transform.Rotate(new Vector3(0, Time.deltaTime * idleRotateSpeed, 0));
        }
    }


}
