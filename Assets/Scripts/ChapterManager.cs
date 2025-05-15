using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using UnityEditor;

[CreateAssetMenu(fileName = "NewChapter", menuName = "Chapters/chapter")]
public class ChapterManager : ScriptableObject
{

    public string Title;
    public string Description;

    public SceneAsset[] levels;



}
