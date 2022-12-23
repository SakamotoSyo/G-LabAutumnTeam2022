using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpUI : MonoBehaviour
{
    [SerializeField] Sprite[] help_;
    [SerializeField] GameObject nextButton_;
    [SerializeField] GameObject backButton_;
    private Image img_;
    private int currentImg_ = 0;

    private void Start()
    {
        img_ = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (help_.Length != 2) { Debug.LogError("‘€ìà–¾‰æ‘œ‚ğ‚Q–‡İ’è‚µ‚Ä‚­‚¾‚³‚¢"); }
    }

    public void ChangeHelp()
    {
        AudioManager.Instance.PlaySound(SoundPlayType.SE_select);

        if (currentImg_ == 0)
        {
            nextButton_.SetActive(false);
            backButton_.SetActive(true);
            currentImg_ = 1;
        }
        else
        {
            nextButton_.SetActive(true);
            backButton_.SetActive(false);
            currentImg_ = 0;
        }
        img_.sprite = help_[currentImg_];
    }
}
