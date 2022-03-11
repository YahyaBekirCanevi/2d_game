using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public bool levelUp = false;
    [SerializeField] private Sprite[] btnImages = new Sprite[2];
    [SerializeField] private GameObject levelForm;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Button proceed;
    private void Update() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length == 0 && !levelUp) {
            levelUp = true;
            levelText.text = "Level\n Completed!";
            levelText.color = Color.green;
            proceed.GetComponent<Image>().sprite = btnImages[0];
            proceed.onClick.AddListener(() => {UnityEditor.EditorApplication.isPlaying = false;});
            levelForm.SetActive(true);
        }
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().isDeath && !levelUp){
            /* levelText.text = "Try Again?";
            levelText.color = Color.red;
            proceed.GetComponent<Image>().sprite = btnImages[1];
            proceed.gameObject.SetActive(false);
            levelForm.SetActive(true); */
            levelUp = true;
            StartCoroutine(Retry());
        }
        if(Input.GetKeyDown(KeyCode.R))
            StartCoroutine(Retry());
    }
    IEnumerator Retry(){
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
