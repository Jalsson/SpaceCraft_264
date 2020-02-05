using UnityEngine;

public class ShipComponentDataHolder : MonoBehaviour
{
    new public string name = "new attachment";
    public GameObject prefab = null;
    public bool IsDefaultAttacment = false;
    public string InfoText = "infoText";
    public SlotType AttachmentSlot;
    public int cost;


    public virtual void Equip()
    {

    }
    public virtual void Unequip()
    {

    }
}
public enum SlotType { Attachment, Thruster, Weapon, None,Market }



