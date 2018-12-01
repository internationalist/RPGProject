using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCCharacter : MonoBehaviour {

    public GameObject questPanel;
    public GameObject interactText;
    public GameObject questConfirmPanel;
    public GameObject questCompletePanel;
    public QuestSystem quest;
    public Button accept;
    public Button quit;
    public Button questConfirm;
    public Button questDecline;
    public Button questFinishButton;
    private bool questQuit;
    private bool acceptQuest;
    private bool npcPanelToggle;
    private bool msgPanelToggle;
    private ApplicationState app;

    public bool AcceptQuest
    {
        get
        {
            return acceptQuest;
        }

        set
        {
            acceptQuest = value;
        }
    }


    // Use this for initialization
    void Start () {
        questPanel.SetActive(false);
        accept.onClick.AddListener(onAccept);
        quit.onClick.AddListener(OnQuit);
        questConfirm.onClick.AddListener(onQuestConfirm);
        questDecline.onClick.AddListener(onQuestDecline);
        questFinishButton.onClick.AddListener(OnQuestComplete);
        questQuit = false;
        acceptQuest = false;
        npcPanelToggle = false;
        msgPanelToggle = false;
        app = GameObject.FindGameObjectWithTag("Application").GetComponent<ApplicationState>();
    }

    private void OnQuestComplete()
    {
        Debug.Log("Completing quest");
        questCompletePanel.SetActive(false);
        npcPanelToggle = false;
        acceptQuest = false;
        quest.questComplete = false;
        quest.GainXP(500);
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

    private void OnQuit()
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
            else if(quest.questComplete)
            {
                interactText.SetActive(false);
                questCompletePanel.SetActive(true);
            } else {
                interactText.SetActive(false);
                msgPanelToggle = !msgPanelToggle;
                app.message.SetActive(msgPanelToggle);
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
            else if (quest.questComplete)
            {
                interactText.SetActive(false);
                questCompletePanel.SetActive(false);
            } else {
                msgPanelToggle = false;
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
