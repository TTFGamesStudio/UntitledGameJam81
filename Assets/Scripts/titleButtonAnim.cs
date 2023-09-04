using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class titleButtonAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject audioPrefab;
    public AudioClip onClip;
    public AudioClip offClip;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        //do stuff
        anim.SetBool("mouseOn",true);
        spawnSFX(onClip);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        //do stuff
        anim.SetBool("mouseOn",false);
        spawnSFX(offClip);
    }
    
    void spawnSFX(AudioClip soundToPlay)
    {
        GameObject G = Instantiate(audioPrefab);
        AudioSource a=G.GetComponent<AudioSource>();
        autoDelete d = G.GetComponent<autoDelete>();
        a.clip =soundToPlay;
        a.Play();
        d.setup(a.clip.length);
    }
}
