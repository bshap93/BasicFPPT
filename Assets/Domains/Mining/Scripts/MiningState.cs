using Lightbug.CharacterControllerPro.Implementation;
using UnityEngine;

namespace Domains.Mining.Scripts
{
    public class MiningState : CharacterState 
    {
        [SerializeField] float miningRange = 5f;
        public Animator toolAnimator;
        static readonly int SwingMiningTool = Animator.StringToHash("SwingMiningTool");
        public Transform cameraTransform;

        // Write your transitions here
        public override bool CheckEnterTransition(CharacterState fromState)
        {
            if (toolAnimator != null)
            {
                toolAnimator.SetBool(SwingMiningTool, true);
            }
            return PerformMining();
        }
        
        private bool PerformMining()
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, miningRange ))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }

                return true;

            }
            
            return false;
        }

        // Write your transitions here
        public override void CheckExitTransition()
        {
            if (!CharacterActions.mine.value)
            {
                Debug.Log("Mining State will be exited");
                CharacterStateController.EnqueueTransition<MyNormalMovement>();
            }
        }


        
        public override void UpdateBehaviour(float dt)
        {
            
        }



    }
}
