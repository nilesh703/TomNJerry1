using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class MyBuilder
{

    //Example of creating Android apk by build execution 

    [UnityEditor.MenuItem("Tools/Build Project AllScene Android")]

    public static void BuildProjectAllSceneAndroid()
    {

        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);

        List<string> allScene = new List<string>();

        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {

            if (scene.enabled)
            {

                allScene.Add(scene.path);

            }

        }

        PlayerSettings.applicationIdentifier = "com.jio.appstore";

        PlayerSettings.statusBarHidden = true;

        BuildPipeline.BuildPlayer(

            allScene.ToArray(),

            "JioStoreJenkins.apk",

            BuildTarget.Android,

            BuildOptions.None

        );

    }
}
 