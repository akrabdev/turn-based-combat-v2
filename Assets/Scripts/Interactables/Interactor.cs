using UnityEngine;

public class Interactor : MonoBehaviour

{
    private IInteractable currentInteractable = null;
    private void Update()
    {
        CheckForInteraction();
    }

    private void CheckForInteraction()
    {
        if (currentInteractable == null) { return; }
        if (Input.GetKeyDown(KeyCode.E))
        {

            currentInteractable.Interact(transform.root.gameObject); // root of the current game object ie: Interactor
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //  interact with most recently entered collider:
        var interactable = other.GetComponent<IInteractable>();
        if (interactable == null) { return; }
        currentInteractable = interactable;


    }

    private void OnTriggerExit2D(Collider2D other)
    {

        var interactable = other.GetComponent<IInteractable>();
        if (interactable == null) { return; }
        if (interactable != currentInteractable) { return; }
        // we are sure now that this one is the currently selected
        currentInteractable = null;



    }
}
