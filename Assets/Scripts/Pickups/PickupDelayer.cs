using System.Collections;
using UnityEngine;

public class PickupDelayer : MonoBehaviour
{
    [SerializeField] float delayTime=0.5f;
    public bool canPickup=false;
    
    void Start()
    {
        StartCoroutine(PickupDelay());
    }

    IEnumerator PickupDelay()
    {
        yield return new WaitForSeconds(delayTime);
        canPickup=true;
    }
}
