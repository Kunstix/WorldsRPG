using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager dialoguer;
    private const string NAME_PREFIX = "n-";

    [Header("Dialogtexts")]
    public Text dialogText;
    public Text nameText;
    public GameObject dialogBox;
    public GameObject nameBox;

    private string[] dialogLines;
    private int currentLine;
    private bool justStartedDialog;

    private 

    void Start()
    {
        if(dialoguer == null)
        {
            dialoguer = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                Debug.Log("Return key up");
                DisplayDialog();
            }
        }
    }

    private void DisplayDialog()
    {
        if (!justStartedDialog)
        {
            ShowNextPartOfDialog();
        }
        else
        {
            justStartedDialog = false;
        }
    }

    private void ShowNextPartOfDialog()
    {
        if (currentLine < dialogLines.Length - 1)
        {
            // Show next dialogline
            currentLine++;
            CheckForName();
            dialogText.text = dialogLines[currentLine];
        }
        else
        {
            // Dialog finished
            dialogBox.SetActive(false);
            PlayerController.player.canMove = true;
        }
    }

    public void StartDialog(string[] lines, bool isPerson)
    {
        PrepareDialog(lines);
        CheckForName();
        InitiateDialog(isPerson);
    }

    private void PrepareDialog(string[] lines)
    {
        dialogLines = lines;
        currentLine = 0;
    }

    private void InitiateDialog(bool isPerson)
    {
        dialogText.text = dialogLines[currentLine];
        dialogBox.SetActive(true);
        justStartedDialog = true;
        nameBox.SetActive(isPerson); 
        PlayerController.player.canMove = false;
    }

    public void CheckForName()
    {
        if (dialogLines[currentLine].StartsWith(NAME_PREFIX))
        {
            nameText.text = dialogLines[currentLine].Replace(NAME_PREFIX, "");
            currentLine++;
        }
    }
}
