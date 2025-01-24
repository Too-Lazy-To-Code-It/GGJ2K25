using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Scripts.Characters.Bubble
{
    [RequireComponent(typeof(Rigidbody))]
    public class BubbleManager : CharacterManager
    { 
        [HideInInspector] public Rigidbody rb;
        protected override void Awake()
        {
            base.Awake();
            rb=GetComponent<Rigidbody>();
            
        }
    }
}