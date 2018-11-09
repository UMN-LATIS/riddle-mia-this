using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class AlertContent
{
    public string alertText;
    public bool displayAlert;
}

public class CheckServerScript : MonoBehaviour
{

    private Animator anim;
    public GameObject AlertPopup;
    public TextMeshProUGUI SubjectBox;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(WaitForRequest("https://www.riddlemiathis.com/status.json"));
    }

    public void LaunchBrowser()
    {
        Application.OpenURL("https://www.riddlemiathis.com/");
    }


    IEnumerator WaitForRequest(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);
                AlertContent results = JsonUtility.FromJson<AlertContent>(www.downloadHandler.text);

                if (results.displayAlert)
                {
                    anim = AlertPopup.GetComponent<Animator>();
                    AlertPopup.SetActive(true);
                    SubjectBox.text = results.alertText;
                    anim.SetTrigger("Visible");
                }
            }
        }

    }
}
