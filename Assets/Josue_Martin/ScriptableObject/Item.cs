using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JosueMartin
{

    [CreateAssetMenu(fileName = "New Item", menuName = "JosueMartin/Item", order = 1)]
    public class Item : ScriptableObject
    {
        public string name; // Nombre del objeto
        public Sprite icon; // Imagen que representa el objeto
        public string description; // Una breve descripcion del objeto
        public float value; // El valor del objeto dentro de el juego en base a la economia de este
        public GameObject prefab; // Es para saber a que objeto/mesh esta ligado el objeto
    }

}


