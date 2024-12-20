using UnityEngine;

namespace Script
{
    public class PlayerAnimEvents : MonoBehaviour
    {
        private Player player;
    
        // Start is called before the first frame update
        void Start()
        {
            player = GetComponentInParent<Player>();
        }

        private void AnimationTrigger()
        {
            player.AttackOver();
        }
    }
}
