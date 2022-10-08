using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Control : MonoBehaviour
{
    public static Control control;
    public float damage;

    public List<float> slot1probabilities;
    public List<float> slot2probabilities;
    public List<float> slot3probabilities;
    public List<float> slot4probabilities;
    public List<float> slot5probabilities;

    public List<Player> players;

    [SerializeField] GameObject skillCardUIPrefab;
    [SerializeField] GameObject skillUIPrefab;
    public RectTransform skillCardUIparent;

    public TextMeshProUGUI currentPlayerNameUItext;
    public TextMeshProUGUI currentPlayerEnduranceUItext;
    public TextMeshProUGUI currentPlayerResolveUItext;
    public TextMeshProUGUI currentPlayerWoundsUItext;

    // Start is called before the first frame update
    void Awake()
    {
        control = this;
        InitializePlayerTurn(players[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

    }

    void InitializePlayerTurn(Player player)
    {
        float proportion = skillCardUIPrefab.GetComponent<RectTransform>().rect.width / skillCardUIPrefab.GetComponent<RectTransform>().rect.height;

        currentPlayerNameUItext.text = player.playerName;
        currentPlayerEnduranceUItext.text = player.endurance.ToString();
        currentPlayerResolveUItext.text = player.resolve.ToString();
        currentPlayerWoundsUItext.text = player.wounds.ToString();

        for (int i =0;i<player.skillCards.Count;i++)
        {
            GameObject g = GameObject.Instantiate(skillCardUIPrefab, skillCardUIparent);
            RectTransform t = g.GetComponent<RectTransform>();
            t.sizeDelta = new Vector2(skillCardUIparent.rect.height*proportion, skillCardUIparent.rect.height);
        }
    }


}
