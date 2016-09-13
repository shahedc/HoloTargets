using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider otherObject)
    {
        // detect object tap
        GameObject hitObject = otherObject.gameObject;
        SphereCollider hitObjectCollider = hitObject.GetComponent<SphereCollider>();

        if (hitObject != null)
        {
            Debug.Log("Matched: " + hitObject.GetComponent<Renderer>().material.color.ToString());
            hitObjectCollider.isTrigger = false;
            hitObjectCollider.enabled = true;
            EnableGravity(hitObject);
        }
        
        //
        this.gameObject.SetActive(false);
        
    }

    private static void EnableGravity(GameObject focusedObject)
    {
        var rb = focusedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
        }
    }
}
