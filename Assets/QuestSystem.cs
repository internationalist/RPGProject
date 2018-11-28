using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour {
    public GameObject interactText;
    public bool canInteract;
	// Use this for initialization
	void Start () {
        interactText.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        canInteract = true;
        interactText.SetActive(true);
    }

    public void OnTriggerExit(Collider other)
    {
        canInteract = false;
        interactText.SetActive(false);
    }
}
