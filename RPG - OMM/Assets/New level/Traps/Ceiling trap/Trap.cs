using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject damagePlace;
    public float coolDown;
    private Animator anim;
    private Damage d;
    private ContactPoint2D[] contacts;

    private void Start()
    {
        anim = GetComponent<Animator>();
        d = GetComponent<Damage>();
    }
    private bool coroutineEnd;
    private void Update()
    {
        if (!coroutineEnd)
        {
            StartCoroutine(CoolDown());
            coroutineEnd = true;
        }
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDown);
        anim.SetTrigger("trigger");
    }

    public void DamageActiveTrue()
    {
        damagePlace.SetActive(true);
    }
    public void DamageActiveFalse()
    {
        damagePlace.SetActive(false);
        anim.ResetTrigger("trigger");
        coroutineEnd = false;
    }

    public void HitScan(Collider2D collision)
    {
        d.Hit(collision);
        damagePlace.SetActive(false);
        coroutineEnd = false;
    }
}
