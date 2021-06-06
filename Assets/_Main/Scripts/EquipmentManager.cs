using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum WeaponType
{
    Ranged,
    Melee
}
enum Weapon
{
    Unarmed,
    Pipe
}

public class EquipmentManager : MonoBehaviour
{
    WeaponType weaponType;

    [SerializeField] Weapon weapon;

    public GameObject currentWeapon;

    public GameObject[] playerWeapons;
    public GameObject[] weapons;


    private void Start()
    {
        Weapon = Weapon.Pipe;
    }

    public void Equip(GameObject equipment)
    {
        switch (equipment.name)
        {
            case "Bat":
                weaponType = WeaponType.Melee;
                Weapon = Weapon.Pipe;
                break;
        }
    }

    public void DropEquipment()
    {
        GameObject _droppedWeapon = GameObject.Instantiate(weapons[(int)weapon], playerWeapons[(int)weapon].transform.position, playerWeapons[(int)weapon].transform.rotation);
        _droppedWeapon.GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);

        Weapon = Weapon.Unarmed;
    }

    Weapon Weapon
    {
        get
        { return weapon; }
        set
        {
            weapon = value;
            for (int i = 0; i < playerWeapons.Length; i++)
                playerWeapons[i].SetActive(false);

            playerWeapons[(int)weapon].SetActive(true);
            currentWeapon = playerWeapons[(int)weapon];
        }
    }

}
