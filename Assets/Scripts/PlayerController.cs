using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CreatureController {

    #region Events
    // Events here!
    #endregion

    public void OnPrimaryAction(InputAction.CallbackContext callbackContext) {
        if (!callbackContext.started)
            return;

        creature.UseCreatureAction(0);
    }

    public void OnSecondaryAction(InputAction.CallbackContext callbackContext) {
        if (!callbackContext.started)
            return;

        creature.UseCreatureAction(1);
    }

    public void OnNavigate(InputAction.CallbackContext callbackContext) {
        creature.movementDirection = callbackContext.ReadValue<Vector2>();
    }

    private void FixedUpdate() {
        var move = creature.movementDirection * Time.fixedDeltaTime * creature.movementSpeed;
        transform.position += (Vector3) move;
    }

}
