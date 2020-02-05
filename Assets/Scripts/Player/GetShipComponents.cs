using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetShipComponents : MonoBehaviour
{
    public bool inventoryOn = false;
    public bool allowFire = true;
    public Rigidbody2D rigi2D;
    public List<CWeaponBehavior> weaponsList;
    public List<CThrusterBehavior> ThrusterList;
    public List<CAttachmentBehavior> AttachmentList;
    public int Ammo { get; private set; }

    bool[] messageCooldowns = { true, true, true };
        

    public void GetComponents(ShipComponentDataHolder shipComponent)
    {



        switch (shipComponent.AttachmentSlot)
        {
            case SlotType.Attachment:
                var attachments = GetComponentsInChildren<CAttachmentBehavior>();
                foreach (var item in attachments)
                {
                    AttachmentList.Add(item);
                }
                RemoveMissingItemsFromList(SlotType.Attachment);
                break;
            case SlotType.Thruster:
                var Thrusters = GetComponentsInChildren<CThrusterBehavior>();
                foreach (var item in Thrusters)
                {
                    ThrusterList.Add(item);
                }
                RemoveMissingItemsFromList(SlotType.Thruster);
                break;
            case SlotType.Weapon:

                var Weapons = GetComponentsInChildren<CWeaponBehavior>();
                foreach (var item in Weapons)
                {
                    weaponsList.Add(item);
                }
                RemoveMissingItemsFromList(SlotType.Weapon);
                break;

        }

    }

    public void RemoveMissingItemsFromList(SlotType removedItemEnum)
    {
        switch (removedItemEnum)
        {
            case SlotType.Attachment:
                for (int i = 0; i < AttachmentList.Count; i++)
                {
                    if (AttachmentList[i] == null)
                        AttachmentList.RemoveAt(i);
                }
                break;
            case SlotType.Thruster:
                for (int i = 0; i < ThrusterList.Count; i++)
                {
                    if (ThrusterList[i] == null)
                        ThrusterList.RemoveAt(i);
                }
                break;
            case SlotType.Weapon:
                for (int i = 0; i < weaponsList.Count; i++)
                {
                    if (weaponsList[i] == null)
                        weaponsList.RemoveAt(i);
                }
                break;
            case SlotType.None:
                break;
            default:
                break;
        }



    }

    void FixedUpdate()
    {
        

        if (Input.GetButton("Fire1") && allowFire  && !inventoryOn)
        {
            if (GameModeManager.instance.gameStarted)
            {
                RemoveMissingItemsFromList(SlotType.Weapon);
                if (weaponsList.Count > 0)
                {
                    if (weaponsList[0] != null)
                    {
                        weaponsList[0].Use();
                        Ammo -= 1;
                        allowFire = false;
                        StartCoroutine(Reset(weaponsList[0]));
                    }
                }
                else if (messageCooldowns[0])
                {
                    AddTextToLog.instance.AddTextToEventLog("No Weapon Installed");
                    messageCooldowns[0] = false;
                    StartCoroutine(ResetCountDown(0));
                }
            }
        }

        if (Input.GetButton("TrusterFwrd"))
        {
            if (ThrusterList.Count > 0)
                if (ThrusterList[0] != null && !PlayerStats.instance.noFuel)
                {
                    ThrusterList[0].Use();
                    PlayerStats.instance.UpdateFuelAmount(ThrusterList[0].thrusterPower * 0.1f);
                    if (PlayerStats.instance.thrusterPower == 0 && messageCooldowns[1])
                    {
                        AddTextToLog.instance.AddTextToEventLog("No power in engines use the Engine power slider to add power");
                        messageCooldowns[1] = false;
                        StartCoroutine(ResetCountDown(1));
                    }
                }
                else if(messageCooldowns[2])
                {
                    AddTextToLog.instance.AddTextToEventLog("No thruster Installed. Open Inventory with 'tab' to equip one");
                    messageCooldowns[2] = false;
                    StartCoroutine(ResetCountDown(2));
                }
        }

    }

    IEnumerator Reset(CWeaponBehavior weaponWichFired)
    {
        yield return new WaitForSecondsRealtime(weaponWichFired.FireRate);
        allowFire = true;
    }
    IEnumerator ResetCountDown(int boolToReset)
    {
        yield return new WaitForSecondsRealtime(3);
        messageCooldowns[boolToReset] = true;
    }
}
