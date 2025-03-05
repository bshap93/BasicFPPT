using Lightbug.CharacterControllerPro.Implementation;
using Lightbug.Utilities;
using UnityEngine;

namespace ThirdParty.Character_Controller_Pro.Implementation.Scripts.Inputs.InputHandler
{
    [System.Serializable]
    public class InputHandlerSettings
    {
        [Tooltip("Input manager: Unity's old input manager\n\n" +
            "UI Mobile : It uses specific UI elements in the scene (InputButton and InputAxes component) as inputs. " +
                        "Make sure these elements \"action names\" match with the character actions you want to trigger.\n\n" + 
            "Custom: A custom implementation.")]
        [SerializeField]
        HumanInputType humanInputType = HumanInputType.InputManager;

        [SerializeField]
        [Condition("humanInputType", ConditionAttribute.ConditionType.IsEqualTo, ConditionAttribute.VisibilityType.Hidden, 2)]
        Lightbug.CharacterControllerPro.Implementation.InputHandler inputHandler = null;

        /// <summary>
        /// Gets/Sets the current InputHandler component.
        /// </summary>
        public Lightbug.CharacterControllerPro.Implementation.InputHandler InputHandler
        {
            get => inputHandler;
            set => inputHandler = value;
        }

        public void Initialize(GameObject gameObject)
        {
            if (inputHandler == null)
                inputHandler = Lightbug.CharacterControllerPro.Implementation.InputHandler.CreateInputHandler(gameObject, humanInputType);
        }
    }
}