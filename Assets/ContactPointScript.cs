using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ContactPointScript : MonoBehaviour, IPointerClickHandler
{
    public Image indicatorImage;
    public Vector2 relativeContactPoint;
    private Selectable selectableComponent;
    private Image indicatorHolder;
    // Start is called before the first frame update
    void Start()
    {
        selectableComponent = GetComponent<Selectable>();
        indicatorHolder = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Detect if a click occurs - this is a callback, you do NOT have to call this
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        indicatorImage.rectTransform.position = pointerEventData.pressPosition;


        float RangeEndx = (indicatorHolder.rectTransform.rect.width * 0.5f) + transform.position.x; 
        float RangeStartx = (indicatorHolder.rectTransform.rect.width * -0.5f) + transform.position.x; 
        
        float RangeEndy = (indicatorHolder.rectTransform.rect.height * 0.5f) + transform.position.y;
        float RangeStarty = (indicatorHolder.rectTransform.rect.height * -0.5f) + transform.position.y;

        float tx = Mathf.InverseLerp(RangeStartx, RangeEndx, indicatorImage.rectTransform.position.x);
        float outputx = Mathf.Lerp(0, 1, tx);

        float ty = Mathf.InverseLerp(RangeStarty, RangeEndy, indicatorImage.rectTransform.position.y);
        float outputy = Mathf.Lerp(0, 1, ty);

        relativeContactPoint = new Vector2(outputx, outputy);
        Debug.Log(relativeContactPoint);
    }
    
}
