using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class PlayerStatus : MonoBehaviour
{
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text mpText;
    [SerializeField] TMP_Text goldText;
    [SerializeField] Image HpGauge;
    [SerializeField] Image MPGauge;
    int maxHpValue;
    int maxMpValue;
    [SerializeField] public UnityEvent Die;
    [SerializeField] Ease ease;

    private void Update()
    {


    }
    private void OnEnable()
    {
        Manager.Game.GoldUpdate += GoldUpdate;
        Manager.Game.playerHPevent += StatusHPUpdate;
        Manager.Game.playerMPevent += StatusMPUpdate;
        maxHpValue = Manager.Game.MaxHpEvent;
        maxMpValue = Manager.Game.MaxMpEvent;


    }
    private void OnDisable()
    {
        Manager.Game.GoldUpdate -= GoldUpdate;
        Manager.Game.playerHPevent -= StatusHPUpdate;
        Manager.Game.playerMPevent -= StatusMPUpdate;
    }

    public void GoldUpdate( int value )
    {

        goldText.text = ( "GOLD : " + Manager.Game.GoldEvent ).ToString();

        goldText.rectTransform.DOShakePosition(duration: 0.5f, strength: new Vector3(0, 10f, 0f), vibrato: 40, randomness: 150);


    }

    private void StatusHPUpdate( int curValue )
    {

        hpText.text = $"{Manager.Game.HpEvent} /{maxHpValue}".ToString();
        

        HpGauge.fillAmount = ( float )Manager.Game.HpEvent / maxHpValue;


        if ( Manager.Game.HpEvent <= 0 )
        {
            hpText.text = $"{Manager.Game.HpEvent}/{maxHpValue}".ToString();
            Manager.Game.DieEvent.Invoke();

        }
        if ( Manager.Game.HpEvent > Manager.Game.MaxHpEvent )
        {
            hpText.text = $"{maxHpValue}/{maxHpValue}".ToString();
            Debug.Log("HpMax");

        }
    }
    private void StatusMPUpdate( int curValue )
    {
        
         
         mpText.text = $"{Manager.Game.MpEvent}/{maxMpValue}".ToString();
        MPGauge.fillAmount = ( float )Manager.Game.MpEvent / maxMpValue;


        if ( Manager.Game.MpEvent <= 0 )
        {
            mpText.text = $"{Manager.Game.MpEvent}/{maxMpValue}".ToString();
            

        }
        if ( Manager.Game.MpEvent > Manager.Game.MaxMpEvent )
        {
            mpText.text = $"{maxMpValue}/{maxMpValue}".ToString();
            Debug.Log("MpMax");

        }
    }
}
