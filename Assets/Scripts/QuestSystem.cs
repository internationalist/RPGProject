using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour {
    public GameObject interactText;
    private CharacterMovement cm;
    public NPCCharacter npcChar;
    public bool canInteract;
    public bool questComplete;
    // Use this for initialization
    void Start () {
        interactText.SetActive(false);
        questComplete = false;
        cm = GetComponent<CharacterMovement>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GainXP(int amount) {
        cm.GainXP(amount);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gold"))
        {
            if (npcChar.AcceptQuest)
            {
                Debug.Log("Found gold");
                questComplete = true;
                Destroy(other.gameObject);
            }
        }
        else {
            canInteract = true;
            interactText.SetActive(true);
        }

    }

    public void OnTriggerExit(Collider other)
    {
        canInteract = false;
        interactText.SetActive(false);
    }
}
