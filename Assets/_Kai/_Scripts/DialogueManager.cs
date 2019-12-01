using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    GameObject Player;

    public Animator anim;
    public Sprite[] PortraitSprites;

    public Image Portrait;
    public Text DialogueText;
    public GameObject sound;
    private static DialogueManager _instance;

    public static DialogueManager Instance {
        get {
            return _instance;
        }
    }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    private void Start() {
        Player = GameObject.Find("Player");
        sound = GameObject.Find("SoundManager");
    }

    public Queue<string> sentence = new Queue<string>();

	public void StartDialogue(Dialogue dial) {
        sentence.Clear();
        anim.SetBool("isOpen", true);
        Player.GetComponent<Movement>().MovementOnPlayer = false;
        SwapPortrait((int)dial.portrait);

        foreach(string dialogue in dial.sentences) {
            sentence.Enqueue(dialogue);
        }

        NextDialogue();
    }

    public void NextDialogue() {
        if(sentence.Count <= 0) {
            EndDialogue();
            return;
        }
        SoundManager.Instance.OpenMenuSource.Play();
        string sentences = sentence.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentences));
    }

    IEnumerator TypeSentence(string sentence) {
        DialogueText.text = "";

        foreach(char letter in sentence.ToCharArray()) {
            DialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue() {
        anim.SetBool("isOpen", false);
        Player.GetComponent<Movement>().MovementOnPlayer = true;
    }

    void SwapPortrait(int tempDP) {
        Portrait.sprite = PortraitSprites[tempDP];
    }
}
