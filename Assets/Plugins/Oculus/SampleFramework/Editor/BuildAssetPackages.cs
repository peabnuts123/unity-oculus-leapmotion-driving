using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class BuildAssetPackages
{
	enum BuildConfiguration
	{
		Windows,
		Android
	}

	public static void Build()
	{
		Debug.Log("Building Deliverables");
		ExportPackages();
	}

	public static void ExportPackages()
	{
        string[] assets = AssetDatabase.FindAssets("t:Object", null).Select(s=>AssetDatabase.GUIDToAssetPath(s)).ToArray();
		assets = assets.Where(s=>
			s.StartsWith("Assets/Plugins/Oculus/Avatar/") ||
			s.StartsWith("Assets/Plugins/Oculus/AudioManager/") ||
			s.StartsWith("Assets/Plugins/Oculus/LipSync/") ||
			s.StartsWith("Assets/Plugins/Oculus/Platform/") ||
			s.StartsWith("Assets/Plugins/Oculus/Spatializer/") ||
			s.StartsWith("Assets/Plugins/Oculus/VoiceMod/") ||
			s.StartsWith("Assets/Plugins/Oculus/VR/") ||
			s.StartsWith("Assets/Plugins/Oculus/SampleFramework/")
		).ToArray();
		AssetDatabase.ExportPackage(assets, "OculusIntegration.unitypackage");
	}
}
