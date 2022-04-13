using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerTalk : MonoBehaviour
{
    [SerializeField]
    private GameObject NameUI;
    Ray ray;
    RaycastHit hit;
    public GameObject MessageUIPrefab;
    private RaycastHit previousHit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position + new Vector3(0, 0.5f, 0), transform.TransformDirection(Vector3.forward));
        if(Physics.Raycast(ray,out hit,10f)){
            if (hit.transform.tag == "Villager")
            {
                previousHit = hit;
                Message.allMessage = hit.transform.GetComponent<Receptionist>().name;
                GameObject nameUI = Instantiate<GameObject>(NameUI, hit.transform.position + Vector3.up * 1.5f, Camera.main.transform.rotation);
                if (Gamepad.current.buttonWest.wasPressedThisFrame)
                {
                    Message.allMessage = hit.transform.GetComponent<Receptionist>().Cont;
                    GameObject MessageUI = Instantiate(MessageUIPrefab);
                }
            }
            //else previousHit.transform.gameObject.GetComponent<NPC>;
        }
    }
}
