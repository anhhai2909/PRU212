using Assets.Scripts.DataPersistence.Data;
using narrenschlag.dialoguez;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    public TMP_Text characterName;

    public Image name;

    public TMP_Text characterText;

    public List<DialoguePlace> dialoguePlaces;

    public float textSpeed;

    public GameObject player;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        dialoguePlaces = new List<DialoguePlace>()
        {
            new DialoguePlace(
                -8,
                8,
                new List<Dialogue>()
                {
                    new Dialogue("Roger", "dddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd", true),
                    new Dialogue("Roger", "Hi niece and nephew", true),
                    new Dialogue("Roger", "Is Uncle Roger", true),
                    new Dialogue("Jamie Oliver", "Is Uncle Roger", false),
                    new Dialogue("Roger", "Is Uncle Roger", true),
                }
            ),
            
        };
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < dialoguePlaces.Count; i++)
        {
            if (dialoguePlaces[i]._xPosition - player.transform.position.x <= 10)
            {
                characterText.text = string.Empty;
                StartCoroutine(TypeLine(dialoguePlaces[i]._dialogues));
            }
        }
    }

    void ClickedOn(List<Dialogue> dialogues)
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (characterText.text == dialogues[index]._characterText)
            {
                NextLine(dialogues);
            }
            else
            {
                StopAllCoroutines();
                NextLine(dialogues);

            }
        }
    }

    void StartDialogue(List<Dialogue> dialogues)
    {
        index = -1;
        NextLine(dialogues);
    }

    IEnumerator TypeLine(List<Dialogue> dialogues)
    {
        foreach (char c in dialogues[index]._characterText.ToCharArray())
        {
            Debug.Log(dialogues[index]._characterText);
            characterText.text += c;
            CheckForOverflow(c.ToString());
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine(List<Dialogue> dialogues)
    {
        if (index < dialogues.Count - 1)
        {
            index++;
            if (dialogues[index]._isMainCharacter)
            {
                name.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1636, 1469);
            }
            else
            {
                name.GetComponent<RectTransform>().anchoredPosition = new Vector2(1740, 1469);
            }
            characterName.text = dialogues[index]._characterName;
            characterText.text = string.Empty;
            StartCoroutine(TypeLine(dialogues));
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void CheckForOverflow(string c)
    {
        if (characterText.preferredHeight > 300)
        {
            Debug.Log("a");
            characterText.text = c;
        }
    }


}
