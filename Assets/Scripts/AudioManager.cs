using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _luckySpinRewardCardSound;
        [SerializeField] private AudioClip _tckicketInsert;
    
        private AudioSource _audioSource;

        public void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        public void PlayInsertTicketSound()
        {
            _audioSource.clip = _tckicketInsert;
            _audioSource.Play();
        }
    
        public void PlayLuckySpinRewardCardSound()
        {
            var randomSound = Random.Range(0, _luckySpinRewardCardSound.Length);
            _audioSource.clip = _luckySpinRewardCardSound[randomSound];
            _audioSource.Play();
        }
    }
}