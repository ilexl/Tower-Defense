using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Game/Turret")]
public class Turret : ScriptableObject
{
    public GameObject modelPrefab;
    public GameObject projectilePrefab;
    public float damage;
    public Sprite icon;

    public Turret(GameObject _modelPrefab, GameObject _projectilePrefab, float _damage, Sprite _icon)
    {
        modelPrefab = _modelPrefab;
        projectilePrefab = _projectilePrefab;
        damage = _damage;
        icon = _icon;
    }
}
