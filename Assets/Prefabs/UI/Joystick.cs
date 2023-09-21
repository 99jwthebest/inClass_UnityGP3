using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    // delegate - 
    public delegate void OnInputValueChanged(Vector2 inputValue);
    public delegate void OnStickTapped();

    public event OnInputValueChanged onInputValueChanged;
    public event OnStickTapped onStickTapped;

    [SerializeField]
    RectTransform thumbstick;
    [SerializeField]
    RectTransform background;
    [SerializeField]
    float deadZone = 0.2f;

    Vector2 outputVal;
    bool wasDragging = false;

    public void OnDrag(PointerEventData eventData)
    {
        //thumbstick.position = eventData.position;
        Vector3 touchPos = eventData.position;
        Vector3 thumbstickLocalOffset = Vector3.ClampMagnitude(touchPos - background.position, background.sizeDelta.x/2f);

        thumbstick.localPosition = thumbstickLocalOffset;
         outputVal = thumbstickLocalOffset / background.sizeDelta.y * 2;
        if (outputVal.magnitude > deadZone) // out put value has to be bigger than the dead zone before triggering the input value delegate.
        {
            onInputValueChanged?.Invoke(outputVal);
            wasDragging = true;
        }
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
        if (wasDragging) // out put value has to be bigger than the dead zone before triggering the input value delegate.
        {
            wasDragging = false;
            return;
        }
        onStickTapped?.Invoke();
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
