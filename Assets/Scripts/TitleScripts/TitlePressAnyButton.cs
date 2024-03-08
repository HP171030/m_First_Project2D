using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings.Switch;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class TitlePressAnyButton : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] float lerpSpeed = 1f;
    [SerializeField] Image titleLogo;
    [SerializeField] float titleTargetTime;
    [SerializeField] Image SetUpMenu;
    [SerializeField] GameObject CursorIcon;
    [SerializeField] GameObject Menu;

    float time = 0f;

    bool isLerping = true;

    private void Start()
    {
        Instantiate(CursorIcon, transform.position, Quaternion.identity);
        text.color = Color.white;
        StartCoroutine(TextColor());
    }

    private IEnumerator TextColor()
    {
        while ( true )
        {

            Color start = isLerping ? Color.white : Color.black;
            Color end = isLerping ? Color.black : Color.white;
                
            Color lerpColor = Color.Lerp(start, end, time);
            text.color = lerpColor;
           time += Time.deltaTime * lerpSpeed;

            if ( time >= 1f )
            {
                isLerping = !isLerping;
                time = 0f;
            }

            yield return null;
        }
    }

    public void OnAnyKey(InputValue value)
    {
        if( value.isPressed )
        {
        StartCoroutine(PressAnyKey());
        

        }
    }

    
    public IEnumerator PressAnyKey()
    {
        float time = 0;
        
        Vector3 titlePos = titleLogo.transform.position;
        while (true)
        {
            float t = time / titleTargetTime;
            time += Time.deltaTime;
            titleLogo.transform.position = Vector3.Lerp(titlePos, titlePos + new Vector3(0, 100, 0), t);
           
            
            if(t >=1f )
            {
                text.gameObject.SetActive(false);
                SetUpMenu.gameObject.SetActive(true);
                Menu.SetActive(true);
            }

            yield return null;
        }
    }

    

}