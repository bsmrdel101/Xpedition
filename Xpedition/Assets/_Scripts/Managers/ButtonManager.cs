using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BEAN
{
    public class ButtonManager : MonoBehaviour
    {
        [SerializeField] private Button continueBtn;


        void Start()
        {
            if (continueBtn != null && PlayerPrefs.GetInt("GameCreated") == 1) continueBtn.interactable = true;
        }

        public void OnClickContinue()
        {
            SceneManager.LoadScene("Game");
        }

        public void OnClickNewGame()
        {
            PlayerPrefs.SetInt("GameCreated", 1);
            SceneManager.LoadScene("Game");
        }
    }
}