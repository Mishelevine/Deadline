
using UnityEngine;

public class FilePaths
{
    public static readonly string root = $"{Application.dataPath}/gameData/";

    //Resources Paths
    public static readonly string resources_graphics = "Graphics/";
    public static readonly string resources_backgroundImages = $"{resources_graphics}Backgrounds/";
    public static readonly string resources_backgroundVideos = $"{resources_graphics}Videos/";
    public static readonly string resources_blendTextures = $"{resources_graphics}Transition Effects/";
}
