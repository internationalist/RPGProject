using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCCharacter : MonoBehaviour {

    public GameObject questPanel;
    public GameObject interactText;
    public GameObject questConfirmPanel;
    public QuestSystem quest;
    public Button accept;
    public Button quit;
    public Button questConfirm;
    public Button questDecline;
    private bool questQuit;
    private bool acceptQuest;
    private bool npcPanelToggle;
    private bool msgPanel;
    private ApplicationState app;


	// Use this for initialization
	void Start () {
        questPanel.SetActive(false);
        accept.onClick.AddListener(onAccept);
        quit.onClick.AddListener(onQuit);
        questConfirm.onClick.AddListener(onQuestConfirm);
        questDecline.onClick.AddListener(onQuestDecline);
        questQuit = false;
        acceptQuest = false;
        npcPanelToggle = false;
        msgPanel = false;
        app = GameObject.FindGameObjectWithTag("Application").GetComponent<ApplicationState>();
    }

    private void onQuestDecline()
    {
        questConfirmPanel.SetActive(false);
        npcPanelToggle = false;
    }

    private void onQuestConfirm() {
        Debug.Log("quest confirmed");
        acceptQuest = true;
        questConfirmPanel.SetActive(false);
    }

    private void onAccept()
    {
        Debug.Log("Quest accepted");
        questPanel.SetActive(false);
        questConfirmPanel.SetActive(true);
    }

    private void onQuit()
    {
        Debug.Log("Quest quit");
        questQuit = true;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Tab) && quest.canInteract)
        {
            if (!acceptQuest)
            {
                interactText.SetActive(false);
                npcPanelToggle = !npcPanelToggle;
                questPanel.SetActive(npcPanelToggle);
                questConfirmPanel.SetActive(false);
            }
            else {
                interactText.SetActive(false);
                msgPanel = !msgPanel;
                app.message.SetActive(msgPanel);
            }
        }
        else if (!quest.canInteract || questQuit)
        {
            if (!acceptQuest)
            {
                npcPanelToggle = false;
                questPanel.SetActive(npcPanelToggle);
                questQuit = false;
                questConfirmPanel.SetActive(false);
            }
            else {
                msgPanel = false;
                app.message.SetActive(false);
            }
        }
        else if (acceptQuest) {
            npcPanelToggle = false;
            questPanel.SetActive(npcPanelToggle);
            Debug.Log("ALREADY ACCEPTED QUEST");
        }
	}
}
