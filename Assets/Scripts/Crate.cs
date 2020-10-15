using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crate : MonoBehaviour
{
    public enum CrateType { whumpa, tnt, checkpoint, lives, multiplewhumpa, akuaku, nitro };
    public CrateType type = CrateType.whumpa;

    [System.Serializable]
    public struct CrateTexture {public CrateType type; public Texture TopTexture; public Texture SideTexture; };
    public List<CrateTexture> textures;
    public List<Texture> tntTextures;

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
    public GameObject checkpointPrefab;

    private bool HasBeenActivated = false;

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

        matList[0].SetTexture("_BaseMap", Ttx);
        matList[1].SetTexture("_BaseMap", Stx);
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
        Destroy(transform.parent.gameObject);
    }

    public IEnumerator TNT()
    {
        rend = GetComponent<Renderer>();
        List<Material> matList = new List<Material>();
        rend.GetMaterials(matList);

        matList[1].SetTexture("_BaseMap", tntTextures[0]);
        AudioSource.PlayClipAtPoint(sounds.tntCountdownSound, transform.position);
        yield return new WaitForSeconds(1f);
        matList[1].SetTexture("_BaseMap", tntTextures[1]);
        AudioSource.PlayClipAtPoint(sounds.tntCountdownSound, transform.position);
        yield return new WaitForSeconds(1f);
        matList[1].SetTexture("_BaseMap", tntTextures[2]);
        AudioSource.PlayClipAtPoint(sounds.tntCountdownSound, transform.position);
        yield return new WaitForSeconds(1f);
        AudioSource.PlayClipAtPoint(sounds.tntExplosionSounds[Random.Range(0, sounds.tntExplosionSounds.Count)], transform.position);

        Explode();

        StartCoroutine(SelfDestroy());
    }

    public void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 6f);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag == "Crate")
            {
                if (hitColliders[i].gameObject != this.gameObject)
                {
                    hitColliders[i].GetComponent<Crate>().CrateActivated(GameController.control.Player.GetComponent<CapsuleCollider>());
                }
            }
            else if (hitColliders[i].gameObject.tag == "Player")
            {
                hitColliders[i].GetComponent<DamageController>().Damage();
            }
        }
    }

    private void CrateActivated(Collider collision)
    {
        if (HasBeenActivated) return;
        HasBeenActivated = true;
        switch (type)
        {
            case CrateType.whumpa:
                StartCoroutine(SelfDestroy());
                GameObject temp = Instantiate(whumpaPrefab);
                temp.transform.position = transform.parent.position;
                temp.GetComponent<WhumpaFruit>().player = collision.gameObject.transform;
                temp.GetComponent<WhumpaFruit>().FollowPlayer();
                break;
            case CrateType.tnt:
                StartCoroutine(TNT());
                break;
            case CrateType.checkpoint:
                GameObject checkpoint = Instantiate(checkpointPrefab);
                checkpoint.transform.position = transform.position;
                GameController.control.Save(SceneManager.GetActiveScene().name);
                Destroy(transform.parent.gameObject);
                break;
            case CrateType.lives:
                break;
            case CrateType.multiplewhumpa:
                break;
            case CrateType.akuaku:
                break;
            case CrateType.nitro:
                Explode();
                StartCoroutine(SelfDestroy());
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "NPC")
        {
            CrateActivated(collision);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "NPC")
        {
            Debug.Log(collision.gameObject.name);
            if (type == CrateType.checkpoint || type == CrateType.nitro) CrateActivated(GameController.control.Player.GetComponent<CapsuleCollider>());
        }
    }
}
