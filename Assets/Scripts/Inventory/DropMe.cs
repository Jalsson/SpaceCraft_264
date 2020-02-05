using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropMe : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Image containerImage;
	public Image InvenoryIcon;
    public bool isShipPart;
    private Color32 HiddenColor = new Color32(255,255,255,0);
	private Color normalColor = new Color(255,255,255);
	public Color highlightColor = Color.green;
    public Color WrongPartColor = Color.red;
    private DragMe dragMe;
    public int weaponSlot;
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
        else if(!isShipPart)
        containerImage.color = HiddenColor;


    }

    private void SetWeaponImageToInvenotryIcon()
    {
            containerImage.sprite = localDragMe.itemData.prefab.GetComponent<SpriteRenderer>().sprite;
            containerImage.color = normalColor;
        
    }

    public void OnDrop(PointerEventData data)
	{
        Sprite dropSprite = GetDropSprite(data);
        ShipComponentDataHolder shipAttachment = GetShipAttachment(data);

        if (dropSprite == null)
			return;

        if (isShipPart)
        {
            if (shipAttachment != null)
            {
                if (shipAttachment.AttachmentSlot != inventorySlotType)
                {
                    return;
                }
            }
        }


        var DropMe = dragMe.GetComponent<DropMe>();
        var localDragMe = GetComponent<DragMe>();


        if (DropMe == null)
        {
            if (shipAttachment.cost < PlayerStats.instance.score)
            {
                dragMe.GetComponent<BuyMe>().BuyItem(shipAttachment.cost);
            }
            else
            {
                AddTextToLog.instance.AddTextToEventLog("Not enough credits!");
                return;
            }
        }


        if (!isShipPart)
        {

            if (dropSprite != null)
            {

                if (localDragMe.itemData != null)
                    ChangeImageAndItem(dropSprite, true);
                else
                    ChangeImageAndItem(dropSprite, false);
            }
            
        }

        if(DropMe != null)
        if (DropMe.isShipPart || isShipPart || !isShipPart)
        {
            DestroyUnequippedComponents(DropMe);
        }

        if (isShipPart)
        {

            bool isNull = true;

            if (localDragMe.itemData != null)
            {
                isNull = false;
                if (!DropMe.isShipPart && isShipPart)
                {
                    SwichIconsAndAttachments(localDragMe);          //receiving slot shipPart and both slots have item.
                    CreateComponentForShip(shipAttachment, transform,weaponSlot);
                }
                if (DropMe.isShipPart && isShipPart)
                {
                    DestroyUnequippedComponents(this);
                    DestroyUnequippedComponents(DropMe);

                    CreateComponentForShip(localDragMe.itemData, dragMe.transform, DropMe.weaponSlot);
                    CreateComponentForShip(dragMe.itemData, transform, weaponSlot);
                    
                    SwichIconsAndAttachments(localDragMe);
                    // molemmat ovat aseita
                }

            }
            else
            isNull = true;

            if(isNull)
            {

                localDragMe.itemData = dragMe.itemData;
                CreateComponentForShip(shipAttachment, transform, weaponSlot);
                DropMe.containerImage.color = HiddenColor;
                 containerImage.sprite = GetDropSprite(data);

                //receiving slot is ShipPart and its empty
                dragMe.itemData = null;
                dragMe.GetComponent<Image>().sprite = null;
            }

            containerImage.color = normalColor;
            PlayerStats.instance.GetComponent<GetShipComponents>().GetComponents(shipAttachment);

        }
    }

    private void SwichIconsAndAttachments(DragMe localDragMe)
    {
        var localSprite = GetComponent<Image>().sprite;
        var localShipAttacment = localDragMe.itemData;

        localDragMe.itemData = dragMe.itemData;
        containerImage.sprite = dragMe.GetComponent<Image>().sprite;

        dragMe.itemData = localShipAttacment;
        dragMe.GetComponent<Image>().sprite = localSprite;
    }

    private static void DestroyUnequippedComponents(DropMe DropMe)
    {
        var currentComponents = PlayerStats.instance.GetComponentsInChildren<ComponentBehaviorBase>();
        for (int i = 0; i < currentComponents.Length; i++)
        {
            if (currentComponents[i].equipmentSlot == DropMe.weaponSlot && DropMe.inventorySlotType == currentComponents[i].equipmentType)
            {
                Destroy(currentComponents[i].gameObject);
            }
        }
    }

    private void CreateComponentForShip(ShipComponentDataHolder attachmentItem,Transform attachmentPos, int EquipSLot)
    {
        var newAtch = Instantiate(attachmentItem.prefab, attachmentPos.position, attachmentPos.rotation);
        newAtch.transform.SetParent(PlayerStats.instance.transform);
        newAtch.GetComponent<ComponentBehaviorBase>().OnItemChanged(attachmentItem, EquipSLot, inventorySlotType);
    }

    private void ChangeImageAndItem(Sprite dropSprite,bool nullLastImage)
    {
        var localDragMe = GetComponent<DragMe>();

        
        if (!nullLastImage)
        {
            
            //receiving slot is inventorySlot and there its empty
            containerImage.sprite = dropSprite;
            containerImage.color = normalColor;
            localDragMe.itemData = dragMe.itemData;

            dragMe.itemData = null;
            dragMe.GetComponent<Image>().sprite = null;
            dragMe.GetComponent<Image>().color = HiddenColor;
        }
        else
        {
            //receiving slot is inventorySlot and they are not empty

            SwichIconsAndAttachments(localDragMe);
        }

    }

    public void OnPointerEnter(PointerEventData data)
	{
        ShipComponentDataHolder shipAttachment = GetShipAttachment(data);


        if (InvenoryIcon == null)
			return;


        if (isShipPart)
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
        }
        else
        InvenoryIcon.color = highlightColor;
    }

	public void OnPointerExit(PointerEventData data)
	{
		if (InvenoryIcon == null)
			return;

        InvenoryIcon.color = normalColor;

    }
	
	private Sprite GetDropSprite(PointerEventData data)
	{
		var originalObj = data.pointerDrag;
		if (originalObj == null)
			return null;
		
		    dragMe = originalObj.GetComponent<DragMe>();
		if (dragMe == null)
			return null;
		
		var srcImage = originalObj.GetComponent<Image>();

		if (srcImage == null)
			return null;
		
		return srcImage.sprite;
	}

    private ShipComponentDataHolder GetShipAttachment(PointerEventData data)
    {
        var originalObj = data.pointerDrag;
        if (originalObj == null)
            return null;

        var dragMe = originalObj.GetComponent<DragMe>();
        if (dragMe == null)
            return null;

        var srcShipAttachment = dragMe.itemData;

        if (srcShipAttachment == null)
            return null;

        return srcShipAttachment;
    }
}
