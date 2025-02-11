using System.Collections;
using System.Collections.Generic;
using Teo.AutoReference;
using UnityEngine;

public class TestGetComponent : BaseMonoBehaviour
{
    [SerializeField, FindInScene]
    private List<Test1> go;
}
