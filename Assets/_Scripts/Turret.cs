using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Game/Turret")]
public class Turret : ScriptableObject
{
    public GameObject modelPrefab;
    public GameObject projectilePrefab;
    public int damage;
    public Sprite icon;
    public float range;
    public float reloadTime;
    public float projectileSpeed;


    public Turret(GameObject _modelPrefab, GameObject _projectilePrefab, int _damage, Sprite _icon, float _range, float _reloadTime, float _projectileSpeed)
    {
        modelPrefab = _modelPrefab;
        projectilePrefab = _projectilePrefab;
        damage = _damage;
        icon = _icon;
        range = _range;
        reloadTime = _reloadTime;
        projectileSpeed = _projectileSpeed;
    }
}
