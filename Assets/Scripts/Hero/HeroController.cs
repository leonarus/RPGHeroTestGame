using Hero.Settings;
using UnityEngine;

namespace Hero
{
    public class HeroController : MonoBehaviour
    {
        [field: SerializeField] public HeroSettings HeroSettings { get; set; }
    }
}