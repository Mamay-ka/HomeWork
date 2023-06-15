using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int health = 0;
    public float time = 3.0f;

    void Start()
    {
        ReceiveHealing();
    }

    public void ReceiveHealing()
    {
        StartCoroutine(Healing());
    }

    private IEnumerator Healing() 
    {
        
        while (health <= 100)
        {
            time -= 0.5f;
            health += 5;

            if (health > 100)
            {
                health = 100;
                Debug.Log("Health :" + health);
                break;
            }
            if (time <= 0)
                break;

            Debug.Log("Health :" + health);
            yield return new WaitForSeconds(0.5f);
         }
    }
}
