using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCharacter : MonoBehaviour {

    public GameObject questPanel;
    private bool tabActive = false;

	// Use this for initialization
	void Start () {
        questPanel.SetActive(tabActive);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            tabActive = !tabActive;
            questPanel.SetActive(tabActive);
        }
	}
}
