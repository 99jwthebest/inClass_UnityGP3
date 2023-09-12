using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    // delegate - 
    public delegate void OnInputValueChanged(Vector2 inputValue);

    public event OnInputValueChanged onInputValueChanged;

    [SerializeField]
    RectTransform thumbstick;
    [SerializeField]
    RectTransform background;

    public void OnDrag(PointerEventData eventData)
    {
        //thumbstick.position = eventData.position;
        Vector3 touchPos = eventData.position;
        Vector3 thumbstickLocalOffset = Vector3.ClampMagnitude(touchPos - background.position, background.sizeDelta.x/2f);

        thumbstick.localPosition = thumbstickLocalOffset;
        onInputValueChanged?.Invoke(thumbstickLocalOffset / background.sizeDelta.y * 2);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        background.position = eventData.position;
        Debug.Log($"pointer down"); 

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        background.localPosition = Vector2.zero;
        thumbstick.localPosition = Vector2.zero;
        onInputValueChanged?.Invoke(Vector2.zero);

        Debug.Log($"pointer up");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
