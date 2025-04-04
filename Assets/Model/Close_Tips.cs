using UnityEngine;

public class Close_Tips : MonoBehaviour
{
    public GameObject button;
    public GameObject panel;
    void Start()
    {

    }

    void Update()
    {

    }
    public void Close()
    {
        panel.gameObject.SetActive(false); // Deactivate the panel
        button.gameObject.SetActive(false); // Deactivate the button
    }
}
