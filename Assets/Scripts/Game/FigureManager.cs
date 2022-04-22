using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public struct Figure
    {
        public string path;
        public Vector3 rotation;
        public Vector3 scale;


        public Figure(string path, Vector3 rotation, Vector3 scale)
        {
            this.path = path;
            this.rotation = rotation;
            this.scale = scale;
        }
    }

    public static Figure GetFigureModel(string name)
    {
        switch (name)
        {
            case "avion":
                return new Figure("/Prefabs/Personajes/ ", new Vector3(180, 0, 90), new Vector3(0.15F, 0.15F, 0.15F));
            default:
                return new Figure("", new Vector3(), new Vector3());
        }
    }
}
