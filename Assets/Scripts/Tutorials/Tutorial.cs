using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public List<Stage> stages;
    public int activeStageIndex;
    public bool tutorialComplete => activeStageIndex == stages.Count;
    public bool exitMenuShown;
    public Canvas tutorialCompleteMenu;

    public void Start()
    {
        activeStageIndex = 0;
        stages[activeStageIndex].isActive = true;
        stages[activeStageIndex].gameObject.SetActive(true);
    }

    public void Update()
    {
        if (tutorialComplete)
        {
            if (!exitMenuShown)
            {
                tutorialCompleteMenu.gameObject.SetActive(true);
                exitMenuShown = true;
            }
            
            if (Controller.b)
                SceneManager.LoadScene("Menu");
            
            return;
        }

        if (stages[activeStageIndex].conditionResolved)
        {
            stages[activeStageIndex].isActive = false;
            stages[activeStageIndex].gameObject.SetActive(false);
            
            activeStageIndex++;

            if (tutorialComplete)
                return;
            
            stages[activeStageIndex].isActive = true;
            stages[activeStageIndex].gameObject.SetActive(true);
        }
    }
}
