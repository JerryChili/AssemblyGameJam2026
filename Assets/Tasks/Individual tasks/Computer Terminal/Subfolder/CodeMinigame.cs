using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CodeMinigame : MonoBehaviour
{
    public TMP_Text codeText;
    public TextAsset codeFile;

    public float typingSpeed = 200f;

    public Action OnCompleted;


    private string fullCode;
    private string displayedCode;

    public ScrollRect scrollRect;

    private int currentCharacter;


    private void Awake()
    {
        fullCode = codeFile.text;
    }


    private void Update()
    {
        if (currentCharacter >= fullCode.Length)
            return;


        if (Input.anyKeyDown)
        {
            RevealCharacters();
        }
    }

    public void ResetMinigame()
    {
        currentCharacter = 0;
        displayedCode = "";

        codeText.text = "";
    }


    private void RevealCharacters()
    {
        int amount = UnityEngine.Random.Range(5, 20);

        currentCharacter += amount;

        currentCharacter = Mathf.Clamp(
            currentCharacter,
            0,
            fullCode.Length
        );


        displayedCode = fullCode.Substring(
            0,
            currentCharacter
        );


        codeText.text = displayedCode;


        Canvas.ForceUpdateCanvases();

        scrollRect.verticalNormalizedPosition = 0f;


        if (currentCharacter >= fullCode.Length)
        {
            Complete();
        }
    }


    private void Complete()
    {
        Debug.Log("System repaired!");

        OnCompleted?.Invoke();

        gameObject.SetActive(false);
    }
}