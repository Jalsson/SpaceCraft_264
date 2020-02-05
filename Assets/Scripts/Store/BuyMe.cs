using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyMe : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Image containerImage;
    public Image InvenoryIcon;
    private Color32 HiddenColor = new Color32(255, 255, 255, 0);
    private Color normalColor = new Color(255, 255, 255);
    public Color highlightColor = Color.green;
    public Color WrongPartColor = Color.red;
    private DragMe dragMe;
    public SlotType inventorySlotType;
    public Sprite SlothIcon;
    private DragMe localDragMe;


    public void OnEnable()
    {
        localDragMe = GetComponent<DragMe>();

        if (localDragMe.itemData != null)
        {
            containerImage.color = HiddenColor;
            SetWeaponImageToInvenotryIcon();
        }

    }

    private void SetWeaponImageToInvenotryIcon()
    {
        containerImage.sprite = localDragMe.itemData.prefab.GetComponent<SpriteRenderer>().sprite;
        containerImage.color = normalColor;

    }

    public void OnPointerEnter(PointerEventData data)
    {

        ShipComponentDataHolder shipAttachment = GetShipAttachment(data);


        if (localDragMe != null)
        {


                if (shipAttachment != null)
                {
                    if (shipAttachment.AttachmentSlot == inventorySlotType)
                    {
                        InvenoryIcon.color = highlightColor;
                    }

                    else if (shipAttachment.AttachmentSlot != inventorySlotType)
                    {
                        InvenoryIcon.color = WrongPartColor;
                    }
                }
            else
                InvenoryIcon.color = highlightColor;

            if (shipAttachment == null && localDragMe.itemData == null)
            {
                if (localDragMe.itemData == null)
                    InvenoryIcon.color = normalColor;
            }

        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        GetShipAttachment(data);

        if (InvenoryIcon == null)
            return;

        InvenoryIcon.color = normalColor;
    }

    private ShipComponentDataHolder GetShipAttachment(PointerEventData data)
    {
        var originalObj = data.pointerDrag;
        if (originalObj == null)
            return null;

            dragMe = originalObj.GetComponent<DragMe>();
        if (dragMe == null)
            return null;

        var srcShipAttachment = dragMe.itemData;

        if (srcShipAttachment == null)
            return null;

        return srcShipAttachment;
    }
    public void BuyItem(int itemCost)
    {
        PlayerStats.instance.ChangeScore(-itemCost);
    }
}
