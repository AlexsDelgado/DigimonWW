using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace Observer
    
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        public Sound[] background_Music;
        [SerializeField] private AudioSource sfx;
        [SerializeField] private AudioSource bgm;
        [SerializeField] private Transition transitionANIM;
        [SerializeField] private CombatManager CM_Manager;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(this.gameObject);

            if (transitionANIM != null)
            {
                transitionANIM.sfxTransition += PlaySound;
            }
            
            

        }

        private void Start()
        {
            PlayMusicLobby();
            if (transitionANIM == null)
            {
                transitionANIM = FindObjectOfType<Transition>();
                if (transitionANIM != null)
                {
                    transitionANIM.sfxTransition += PlaySound;
                }
            }
        }

        public void PlaySound(AudioClip clip)
        {
            sfx.PlayOneShot(clip);
        }

        public void StartCombat()
        {
            CM_Manager = FindObjectOfType<CombatManager>();
            if (CM_Manager != null)
            {
                CM_Manager.sfx += PlaySound;
                CM_Manager.music+= PlayMusicBattle;
            }
        }

        private void OnDestroy()
        {
            if (transitionANIM != null)
            {
                transitionANIM.sfxTransition -= PlaySound;
            }
            if (CM_Manager != null)
            {
                CM_Manager.sfx -= PlaySound;
                CM_Manager.music -= PlayMusicBattle;
            }
        }


        public void PlayMusicLobby()
        {
            int lobbyMusic = Random.Range(0, 2);
            AudioClip aux = background_Music[lobbyMusic].clip;
            bgm.clip = aux;
            bgm.Play();
            //bgm.PlayOneShot(aux);
        }

        public void PlayMusicBattle(AudioClip clip)
        {
            bgm.clip = clip;
            bgm.Play();
            //bgm.PlayOneShot(clip);
        }
        
        

    }
    
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;


    }
        
    
}