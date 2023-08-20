using UnityEngine;
using UnityEngine.UI;

public class TurretUI : MonoBehaviour
{
    public Turret turret;
    [SerializeField] private Money money;

    void Awake() { if(money == null) { money = GameObject.FindAnyObjectByType<Money>(); } }

    private void Update()
    {
        // allow button to be pushed if balance is high enough
        GetComponent<Button>().interactable = (turret.cost <= money.GetBalance());
    }
}
