using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public enum CrateType { whumpa, tnt, checkpoint, lives, multiplewhumpa, akuaku, nitro };
    public CrateType type = CrateType.whumpa;

    [System.Serializable]
    public struct CrateTexture {public CrateType type; public Texture TopTexture; public Texture SideTexture; };
    public List<CrateTexture> textures;

    [System.Serializable]
    public struct CrateSoundEffects { public List<AudioClip> bounceSounds; public AudioClip breakSound; public List<AudioClip> checkpointsSounds; public AudioClip lockedBounceSound; public AudioClip slotChangeSound; public AudioClip nitroBounceSound; public AudioClip nitroExplosionSound; public AudioClip tntCountdownSound; public List<AudioClip> tntExplosionSounds; };
    public CrateSoundEffects sounds;

    [System.Serializable]
    public struct CrateDestroyedSprites { public List<Sprite> sprites; public CrateType type; }
    public List<CrateDestroyedSprites> crateDestroyedSprites;

    private Renderer rend;
    private Texture Ttx;
    private Texture Stx;
    private Animation anim;
    private AudioSource audio;
    private List<AnimationState> states;

    public ParticleSystem destroyedFX;
    public GameObject whumpaPrefab;

    private void Awake()
    {
        anim = GetComponent<Animation>();
        audio = GetComponent<AudioSource>();
        states = new List<AnimationState>(anim.Cast<AnimationState>());
    }

    private void Start()
    {
        SetMaterials();
    }

    public void SetMaterials()
    {
        foreach (CrateTexture ct in textures)
        {
            if (ct.type == type)
            {
                Ttx = ct.TopTexture;
                Stx = ct.SideTexture;
            }
        }

        rend = GetComponent<Renderer>();
        List<Material> matList = new List<Material>();
        rend.GetMaterials(matList);

        matList[0].SetTexture("_MainTex", Ttx);
        matList[1].SetTexture("_MainTex", Stx);
    }

    public IEnumerator SelfDestroy()
    {
        anim.Play(states[0].name);
        AudioSource.PlayClipAtPoint(sounds.breakSound, transform.position);
        GetComponent<SkinnedMeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        List < Sprite > sprites = new List<Sprite>();
        foreach (CrateDestroyedSprites list in crateDestroyedSprites)
        {
            if (list.type == type) { sprites = list.sprites; break; }
        }


        for (int i = 0; i < 4; i++)
        {
            destroyedFX.textureSheetAnimation.SetSprite(i, sprites[i]);
        }
   
        destroyedFX.Play();
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != "Player") return;
        StartCoroutine(SelfDestroy());
        switch (type)
        {
            case CrateType.whumpa:
                GameObject temp = Instantiate(whumpaPrefab);
                temp.transform.position = transform.parent.position;
                temp.GetComponent<WhumpaFruit>().player = collision.gameObject.transform;
                temp.GetComponent<WhumpaFruit>().FollowPlayer();
                break;
            case CrateType.tnt:
                break;
            case CrateType.checkpoint:
                break;
            case CrateType.lives:
                break;
            case CrateType.multiplewhumpa:
                break;
            case CrateType.akuaku:
                break;
            case CrateType.nitro:
                break;
            default:
                break;
        }
    }
}
