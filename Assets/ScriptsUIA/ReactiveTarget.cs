using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    public void ReactToHit()
    {
        WanderingAI behaviour = GetComponent<WanderingAI>();
        if (behaviour != null)//ѕровер€ем, присоединен ли к персонажу сценарий WanderingAI; он может и отсутствовать.
        {
            behaviour.SetAlive(false);
            StartCoroutine(Die());
        }
    }

   private IEnumerator Die()
    {
        this.transform.Rotate(-75, 0, 0);
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
