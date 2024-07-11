using System.Collections;
using UnityEngine;

public class SpawnSpike : MonoBehaviour
{
    [SerializeField]
    public GameObject spike;
    private Animator anim;
    [SerializeField] private float animationTime = 1.2f;
    [SerializeField] private float delayTime = 10f;
    protected Necromancer necromancer;
    private void Awake()
    {
        necromancer = GetComponent<Necromancer>();  
    }

    // Start is called before the first frame update
    void Start()
    {
        if (spike != null)
        {
            spike.SetActive(false);
            anim = spike.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Spike GameObject is not assigned.");
        }
    }

    public void setActiveSpike()
    {
 
    }

    public virtual void SpawnSpikes()
    {
        

        if (spike != null && anim != null)
        {
            spike.SetActive(true);
            StartCoroutine(ActivateAndPlay());
        }
        else
        {
            Debug.LogError("Spike GameObject or Animator is not assigned.");
        }
    }

    private IEnumerator ActivateAndPlay()
    {
        spike.SetActive(true);
        yield return null;

        anim.Play("LightBeam");

        yield return new WaitForSeconds(1.1f);

        spike.SetActive(false);
    }
    public void AnimationSpikeEnd()
    {
        necromancer.spawnSpikeState.isCastTimeOver = true;
    }
}
