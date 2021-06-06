using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public int hearts;

    Animator animator;
    EquipmentManager equipmentManager;

    float fireRate = 1.3f;
    float nextFire;

    void Start()
    {
        animator = GetComponent<Animator>();
        equipmentManager = GetComponent<EquipmentManager>();
        nextFire = 0;
        hearts = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFire)
        {
            Attack();
            nextFire = Time.time + fireRate;
        }
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon") && other.gameObject != equipmentManager.currentWeapon)
        {
            hearts--;
            other.GetComponent<WeaponScript>().Hit();
        }
    }
}
