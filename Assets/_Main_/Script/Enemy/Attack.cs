using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider2D col;
    public int attackDamage = 10;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable != null)
        {
            damagable.Hit(attackDamage); // Example damage value
            Debug.Log("Enemy hit player for " + attackDamage + " damage!");
        }
    }
}
