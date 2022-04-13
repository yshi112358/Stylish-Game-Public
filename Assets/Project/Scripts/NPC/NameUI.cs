using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameUI : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyTime());
    }
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
    private IEnumerator DestroyTime()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Destroy(gameObject);
    }
}
