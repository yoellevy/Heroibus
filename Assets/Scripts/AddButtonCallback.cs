using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class AddButtonCallback : MonoBehaviour
{
    // THIS WAS USED WHEN THE ATTACKS WERE BUTTONS AND NOT GAMEOBJECTS WITH COLLIDERS

    [SerializeField]
    private int index;

    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(() => AddCallback());
    }

    private void AddCallback()
    {
        // GameObject playerParty = GameObject.Find("PlayerParty");
        // playerParty.GetComponent<SelectUnit>().SelectAttack(this.index);
        SceneManager.LoadScene("Battle");
    }

}