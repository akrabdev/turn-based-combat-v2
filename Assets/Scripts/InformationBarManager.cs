using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationBarManager : MonoBehaviour
{
    public static InformationBarManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    public Text informationText;
    public bool trainingMode;

    // Update is called once per frame
    public IEnumerator UpdateText(string inputText)
    {
        /*informationText.text = inputText + "\n"*/;
        if(trainingMode)
        {
            yield return new WaitForSeconds(0);
        }
        else
        {
            gameObject.SetActive(true);
            informationText.text += inputText + "\n";
            yield return new WaitForSeconds(3);
            informationText.text = "";
            gameObject.SetActive(false);
        }
        
        
    }
}
