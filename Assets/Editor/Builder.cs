using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class Builder
{
    private const char CommandStartCharacter = '-';
    private const string BuildPathCommand = "-buildPath";
    private const string KeystorePasswordCommand = "-keystorePass";
    private const string KeyAliasPasswordCommand = "-keyaliasPass";
    private const string ProvisionProfileId = "-provisionUUID";

    public static void BuildAndroid()
    {
        string buildPath, keystorePassword, keyAliasPassword, androidType;
        //Parse custom command line arguments
        Dictionary<string, string> commandToValueDictionary = GetCommandLineArguments();
        commandToValueDictionary.TryGetValue(BuildPathCommand, out buildPath);
        commandToValueDictionary.TryGetValue(KeystorePasswordCommand, out keystorePassword);
        commandToValueDictionary.TryGetValue(KeyAliasPasswordCommand, out keyAliasPassword);
        //Update Key Store and Alias password
        PlayerSettings.keyaliasPass = keyAliasPassword;
        PlayerSettings.keystorePass = keystorePassword;
        BuildPipeline.BuildPlayer(GetEnabledScenePaths(), buildPath, BuildTarget.Android, BuildOptions.None);
    }

    public static void BuildiOS()
    {
        string buildPath, provisionUUID;
        //Parse command line arguments
        Dictionary<string, string> commandToValueDictionary = GetCommandLineArguments();
        commandToValueDictionary.TryGetValue(BuildPathCommand, out buildPath);
        commandToValueDictionary.TryGetValue(ProvisionProfileId, out provisionUUID);
        //Update iOS Manual provisioning profile to Developer or App Store
        PlayerSettings.iOS.iOSManualProvisioningProfileID = provisionUUID;
        BuildPipeline.BuildPlayer(GetEnabledScenePaths(), buildPath, BuildTarget.iOS, BuildOptions.None);
    }

    private static string[] GetEnabledScenePaths()
    {
        return EditorBuildSettings.scenes.Select(e => e.path).ToArray();
    }

    private static Dictionary<string, string> GetCommandLineArguments()
    {
        Dictionary<string, string> commandToValueDictionary = new Dictionary<string, string>();
        string[] args = Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i].StartsWith(CommandStartCharacter.ToString(), StringComparison.Ordinal))
            {
                string command = args[i];
                string value = string.Empty;
                if (i < args.Length - 1 && !args[i + 1].StartsWith(CommandStartCharacter.ToString(), StringComparison.Ordinal))
                {
                    value = args[i + 1];
                    i++;
                }
                if (!commandToValueDictionary.ContainsKey(command))
                    commandToValueDictionary.Add(command, value);
            }
        }
        return commandToValueDictionary;
    }
}
