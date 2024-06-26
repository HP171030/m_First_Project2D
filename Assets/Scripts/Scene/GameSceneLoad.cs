using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameSceneLoad : BaseScene
{

    [SerializeField] Animator animator;
    [SerializeField] GameObject player;
    [SerializeField] ParticleSystem particlePrefab;
    [SerializeField] GameObject monsterMimic;
    [SerializeField] Transform mimicRegen1;


    [SerializeField] Image dialogOff;
    [SerializeField] UIManager uiManager;

    [SerializeField] QuestNPC questNPC;

    public override IEnumerator LoadingRoutine()
    {

        //게임 씬에 따른 내용 미리 구성(맵,풀드오브젝트..)

        Manager.Game.ScenePool();
        uiManager = FindObjectOfType<UIManager>();
        if ( uiManager != null )
        {
            uiManager.playerInput = player.GetComponent<PlayerInput>();
        }

        int ranSize = Random.Range(1, 4);

        for ( int i = 0; i < ranSize; i++ )
        {
            Vector2 newPosition = mimicRegen1.position + Random.insideUnitSphere * 3f;
            Instantiate(monsterMimic, newPosition, Quaternion.identity);

        }

        Manager.UICanvas.dialogue.gameObject.SetActive(true);
        Manager.UICanvas.dialogue.enabled = false;
        Manager.Game.titleOff = false;
        Manager.UICanvas.gameObject.SetActive(true);
        Manager.Game.CineInCam();
        yield return null;
    }

    public override IEnumerator OnStartScene()
    {
        Debug.Log("Onstart");
        Manager.Game.hitdam.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        animator.gameObject.SetActive(true);
        Instantiate(particlePrefab, player.transform.position, Quaternion.identity);
        player.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        animator.gameObject.SetActive(false);

    }
    public override void SceneLoad()
    {
        base.SceneLoad();
        player.transform.position = Manager.Data.GameData.playerPos;
      questNPC.InitQuest();
    }

}
