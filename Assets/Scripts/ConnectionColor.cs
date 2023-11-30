using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UpdateConnectorColor();
    }



    public CableColor ConnectionColor_ = CableColor.White;

    [SerializeField] private MeshRenderer collorRenderer;


    private void UpdateConnectorColor()
    {
        if (collorRenderer == null)
            return;

        Color color = MaterialColor(ConnectionColor_);
        collorRenderer.material.color = color;
        /*
        MaterialPropertyBlock probs = new();
        collorRenderer.GetPropertyBlock(probs);
        probs.SetColor("_Color", color);
        collorRenderer.SetPropertyBlock(probs);
        */
    }

    public enum CableColor { White, Red, Green, Yellow, Blue, Cyan, Magenta }

    private Color MaterialColor(CableColor cableColor) => cableColor switch
    {
        CableColor.White => Color.white,
        CableColor.Red => Color.red,
        CableColor.Green => Color.green,
        CableColor.Yellow => Color.yellow,
        CableColor.Blue => Color.blue,
        CableColor.Cyan => Color.cyan,
        CableColor.Magenta => Color.magenta,
        _ => Color.clear
    };
}
