using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartGameButtonController : MonoBehaviour
{
    bool gazedAt;
    float timer;
    public Image imgGaze;
    float gazeTime = 2f;
    GameMaster gm;

    void Start()
    {
        gm = GameMaster.GM;
    }

    void Update()
    {
        if (gazedAt)
        {
            timer += Time.deltaTime;
            imgGaze.fillAmount = (float)(timer / gazeTime);
            if (timer >= gazeTime)
            {
                timer = 0f;
                ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
            }
        }
    }

    public void OnPointerEnter()
    {
        gazedAt = true;
    }

    public void OnPointerExit()
    {
        gazedAt = false;
        timer = 0f;
        imgGaze.fillAmount = 0f;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1 );
    }

    public void ReplayGame()
    {
        gm.ResetGameMaster();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
