/* 
*   NatShare
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace NatShareU {

    using UnityEditor;
    using UnityEditor.Callbacks;
    using System.IO;
    #if UNITY_IOS
    using UnityEditor.iOS.Xcode;
    #endif

    public static class NatShareEditor {

        private const string
		LibraryUsageKey = @"NSPhotoLibraryUsageDescription",
        LibraryUsageDescription = @"Allow Riddle Mia This to load your selfie", // Change this as necessary
        LibraryUsageKey2 = @"NSPhotoLibraryAddUsageDescription",
        LibraryUsageDescription2 = @"Allow Riddle Mia This to save your selfie"; // Change this as necessary
#if UNITY_IOS

        [PostProcessBuild]
		static void LinkFrameworks (BuildTarget buildTarget, string path) {
			if (buildTarget != BuildTarget.iOS) return;
			string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
			PBXProject proj = new PBXProject();
			proj.ReadFromString(File.ReadAllText(projPath));
			string target = proj.TargetGuidByName("Unity-iPhone");
			proj.AddFrameworkToProject(target, "Photos.framework", true);
			File.WriteAllText(projPath, proj.WriteToString());
		}

        [PostProcessBuild]
		static void SetPermissions (BuildTarget buildTarget, string path) {
			if (buildTarget != BuildTarget.iOS) return;
			string plistPath = path + "/Info.plist";
			PlistDocument plist = new PlistDocument();
			plist.ReadFromString(File.ReadAllText(plistPath));
			PlistElementDict rootDictionary = plist.root;
			rootDictionary.SetString(LibraryUsageKey, LibraryUsageDescription);
            rootDictionary.SetString(LibraryUsageKey2, LibraryUsageDescription2);
			File.WriteAllText(plistPath, plist.WriteToString());
		}
#endif
    }
}