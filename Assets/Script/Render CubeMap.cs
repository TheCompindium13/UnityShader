using UnityEngine;
using UnityEditor;
using System.Collections;

public class RenderCubemapWizard : ScriptableWizard
{

    public Camera camera;

    void OnWizardUpdate()
    {
        helpString = "Select Camera position to render a cubemap from";
        isValid = (camera != null);
    }

    void OnWizardCreate()
    {
        // create Cubemap
        Cubemap cubemap = new Cubemap(512, TextureFormat.ARGB32, false);
        camera.RenderToCubemap(cubemap);
        AssetDatabase.CreateAsset(cubemap, $"Assets/Cubemap/{camera.name}.cubemap");
    }

    [MenuItem("ToolBox/Cubemap Wizard")]
    static void RenderCubemap()
    {
        ScriptableWizard.DisplayWizard<RenderCubemapWizard>(
            "Render cubemap", "Render");
    }
}