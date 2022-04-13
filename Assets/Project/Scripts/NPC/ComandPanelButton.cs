using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ComandPanelButton : MonoBehaviour,ISelectHandler,IDeselectHandler
{
    private Image Image;
    void Awake()
    {
        Image = transform.Find("Image").GetComponent<Image>();
    }
    private void OnEnable()
    {
        if(EventSystem.current.currentSelectedGameObject==this.gameObject)
        {
            Image.enabled = true;
        }
        else
        {
            Image.enabled = true;
        }
    }
    public void OnSelect(BaseEventData eventData)
    {
        Image.enabled = true;
    }
    public void OnDeselect(BaseEventData eventData)
    {
        Image.enabled = false;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
