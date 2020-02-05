using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ComponentInfoShower : ItemInfoShower, IPointerEnterHandler
{
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponent<DragMe>() != null)
        {
            var inventoryComponent = GetComponent<DragMe>().itemData;
            if (inventoryComponent != null)
            {
                InfoText = inventoryComponent.InfoText;
                base.OnPointerEnter(eventData);

            }
        }
    }
}
