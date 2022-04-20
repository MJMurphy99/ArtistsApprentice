using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Combat_EntityManager : MonoBehaviour
{
    public TextMeshProUGUI splashText;
    public Button restart;
    public List<GameObject> playerParty;
    public List<GameObject> enemyParty;
    private List<List<GameObject>> entityList =  new List<List<GameObject>>();
    private Party p;

    // Start is called before the first frame update
    void Start()
    {
        p = GetComponent<Party>();
        playerParty = p.party;
        entityList.Add(playerParty);
        entityList.Add(enemyParty);
    }

    public void UpdatePartyCount(GameObject entity, int action)
    {
        int index = entity.CompareTag("Player") ? 0 : 1;

        if (action == 0)
        {
            if (index == 0) p.Remove(entity);
            else
            {
                entityList[index].Remove(entity);
                Destroy(entity);
            }

            if (entityList[index].Count == 0) EndGameState(index);
        }
        else if (action == 1) entityList[index].Add(entity);
        else
        {
            print("Unrecognised Action: Use 0 [REMOVE] or 1 [ADD]");
        }
    }

    private void EndGameState(int losingParty)
    {
        splashText.gameObject.SetActive(true);
        restart.gameObject.SetActive(true);
        splashText.text = losingParty == 0 ? "You Lose!" : "You Win!";
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scenes/CombatDevelopment");
    }
}
