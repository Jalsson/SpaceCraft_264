using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour {

    public Slider targetSlider;

    public Image targetImage;

    Color fullColor;
    public Color EmptyColor;
    public Color notificationColor;

    private void Start()
    {
        fullColor = targetImage.color;
    }

    public void ChangeSliderColor()
    {
        targetImage.color = Color.Lerp(EmptyColor, fullColor, targetSlider.value);
    }

    public IEnumerator BlinkColor()
    {
        targetImage.color = notificationColor;
        yield return new WaitForSecondsRealtime(0.3f);
        targetImage.color = fullColor;
        yield return new WaitForSecondsRealtime(0.3f);
        targetImage.color = notificationColor;
        yield return new WaitForSecondsRealtime(0.3f);
        targetImage.color = fullColor;
        yield return new WaitForSecondsRealtime(0.3f);
        targetImage.color = notificationColor;
        yield return new WaitForSecondsRealtime(0.3f);
        targetImage.color = fullColor;
        yield return new WaitForSecondsRealtime(0.3f);

    }
}
 