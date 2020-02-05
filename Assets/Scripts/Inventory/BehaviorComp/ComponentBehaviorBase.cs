using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentBehaviorBase : MonoBehaviour {


    public ShipComponentDataHolder currentShipComponent;
    public Transform ship;
    public int equipmentSlot;
    public SlotType equipmentType;

    public void OnItemChanged(ShipComponentDataHolder ShipComp, int weaponPlace, SlotType inventorySlotType)
    {
        equipmentType = inventorySlotType;
        currentShipComponent = ShipComp;
        ship = PlayerStats.instance.transform;
        equipmentSlot = weaponPlace;
    }
     public virtual void Use()
    {
        
    }

}
