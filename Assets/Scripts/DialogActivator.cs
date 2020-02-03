using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    public string[] dialogLines;

    public bool isPerson = true;

    private bool activatable;

    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log("Active: " + !DialogManager.dialoguer.dialogBox.activeInHierarchy);

        if (activatable && Input.GetKeyDown(KeyCode.Return) && !DialogManager.dialoguer.dialogBox.activeInHierarchy)
        {
            Debug.Log("Dialog activated.");
            DialogManager.dialoguer.StartDialog(dialogLines, isPerson);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Activatable true");
            activatable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Activatable false");
            activatable = false;
        }
    }
}
