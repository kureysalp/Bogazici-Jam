using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    EquipmentManager equipmentManager;

    private void Start()
    {
        equipmentManager = GetComponentInParent<EquipmentManager>();
    }
    public void Hit()
    {
        equipmentManager.DropEquipment();
    }
}
