using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Stats
    public int curHealth;
    public int maxHealth = 3;

    void Start()
    {
        curHealth = maxHealth;

    }


    void Update()
    {

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0)
        {

            Die();
        }
    }

    void Die()
    {
        //Restart
        Application.LoadLevel(Application.loadedLevel);


    }


    public void Damage(int dmg)
    {
        curHealth -= dmg;

    }

}
