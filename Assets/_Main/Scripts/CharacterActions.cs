using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions : MonoBehaviour
{

    EquipmentManager equipmentManager;

    void Start()
    {
        equipmentManager = GetComponent<EquipmentManager>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Equipment"))
            equipmentManager.Equip(other.gameObject);
    }
}
