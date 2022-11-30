using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool key;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Door"))
        {
            if (key)
            {
                Vector3 rotation = other.gameObject.transform.eulerAngles;
                rotation.y = -90;
                other.gameObject.transform.eulerAngles = rotation;
            }
        }
        if (other.tag.Equals("Key"))
        {
            key = true;
            other.gameObject.SetActive(false);
        }
    }
}
