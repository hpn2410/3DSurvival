using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public GameObject hover_Item_Info;
    public GameObject selectedObject;
    TextMeshProUGUI interaction_text;
    public bool cursorTarget = false;
    public Image centerDotIcon;
    public Image handIcon;

    // Design pattern (Singleton)
    public static SelectionManager Instance { get; set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        interaction_text = hover_Item_Info.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            InteractableObject interactableObject = selectionTransform.GetComponent<InteractableObject>();

            if (interactableObject && interactableObject.playerInRange)
            {

                cursorTarget = true;
                selectedObject = interactableObject.gameObject;

                interaction_text.text = interactableObject.GetItemName();
                hover_Item_Info.SetActive(true);

                if(interactableObject.CompareTag("Pickable"))
                {
                    handIcon.gameObject.SetActive(true);
                    centerDotIcon.gameObject.SetActive(false);
                }
                else
                {
                    handIcon.gameObject.SetActive(false);
                    centerDotIcon.gameObject.SetActive(true);
                }

            }
            else
            {
                hover_Item_Info.SetActive(false);
                cursorTarget = false;
                handIcon.gameObject.SetActive(false);
                centerDotIcon.gameObject.SetActive(true);
            }

        }
        else
        {
            cursorTarget = false;
            hover_Item_Info.SetActive(false);
            handIcon.gameObject.SetActive(false);
            centerDotIcon.gameObject.SetActive(true);
        }
    }

    public void DisableSelection()
    {
        handIcon.enabled = false;
        centerDotIcon.enabled = false;
        hover_Item_Info.SetActive(false);

        selectedObject = null;
    }

    public void EnableSelection()
    {
        handIcon.enabled = true;
        centerDotIcon.enabled = true;
        hover_Item_Info.SetActive(true);
    }
}
