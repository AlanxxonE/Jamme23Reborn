using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothBehaviour : MonoBehaviour
{
    public enum ToothInteraction
    {
        PlayerToothInteraction,
        EnemyToothInteraction
    }

    public int toothEndurance { get; set; }

    private Renderer toothRendererComponent;

    private Color emitColor;

    // Start is called before the first frame update
    void Start()
    {
        toothEndurance = 3;

        toothRendererComponent = GetComponent<Renderer>();

        AdaptToothBasedOnEndurance(toothEndurance);
    }

    public void CheckForToothCollisionBasedOnType(ToothInteraction interactionType)
    {
        switch(interactionType)
        {
            case ToothInteraction.PlayerToothInteraction:
                if(toothEndurance <= 3)
                {
                    toothEndurance++;

                    AdaptToothBasedOnEndurance(toothEndurance);
                }
                break;
            case ToothInteraction.EnemyToothInteraction:
                if (toothEndurance > 0)
                {
                    toothEndurance--;

                    AdaptToothBasedOnEndurance(toothEndurance);
                }
                else
                {
                    PopOutTooth();
                }
                break;
        }
    }

    void AdaptToothBasedOnEndurance(int enduranceToAdapt)
    {
        switch(enduranceToAdapt)
        {
            case 3:
                emitColor = Color.white;
                break;
            case 2:
                emitColor = Color.yellow;
                break;
            case 1:
                emitColor = Color.black;
                break;
        }

        if(toothRendererComponent != null)
        {
            toothRendererComponent.material.SetColor("_EmissionColor", emitColor);
        }
    }

    void PopOutTooth()
    {
        if (this.gameObject.GetComponent<Rigidbody>() == null)
        {
            this.gameObject.AddComponent<Rigidbody>().AddForce(Vector3.up, ForceMode.Impulse);
        }
    }
}
