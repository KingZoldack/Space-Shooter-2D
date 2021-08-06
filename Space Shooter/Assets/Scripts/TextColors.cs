using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextColors : MonoBehaviour
{
    [SerializeField ]TextMeshProUGUI _colorText;
    bool changeColor = false;

    // Start is called before the first frame update
    void Start()
    {
        _colorText = gameObject.GetComponent<TextMeshProUGUI>();
        changeColor = true;
        StartCoroutine(ChangeColorRoutine());

    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator ChangeColorRoutine()
    {
        while (changeColor == true)
        {
            yield return new WaitForSeconds(1);
            _colorText.color = new Color32(15, 98, 230, 255); //Blue
            yield return new WaitForSeconds(1);
            _colorText.color = new Color32(235, 13, 13, 255); //Red
            yield return new WaitForSeconds(1);
            _colorText.color = new Color32(99, 235, 26, 255); //Green
            yield return new WaitForSeconds(1);
            _colorText.color = new Color32(235, 222, 1, 255); //Yellow
        }




    }
}
