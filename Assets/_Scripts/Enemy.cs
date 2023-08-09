using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float distanceToEnd = 0;
    [SerializeField] float speed;
    [SerializeField] private int health;
    private int maxHealth;
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject healthBar;

    public float GetSpeed() { return speed; }
    public int GetHealth() { return health; }

    private void Awake()
    {
        maxHealth = health;
        Canvas.GetComponent<Canvas>().worldCamera = Camera.main;
        healthBar.GetComponent<UnityEngine.UI.Slider>().minValue = 0;
        healthBar.GetComponent<UnityEngine.UI.Slider>().maxValue = maxHealth;    
    }

    private void Update()
    {
        distanceToEnd = GetComponent<FollowPath>().RemainingDistance();
        Canvas.transform.LookAt(Camera.main.transform.position);
        Canvas.transform.Rotate(new Vector3(0, 180, 0));


        healthBar.gameObject.GetComponent<UnityEngine.UI.Slider>().value = health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
