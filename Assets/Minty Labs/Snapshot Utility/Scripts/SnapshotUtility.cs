using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class SnapshotUtility : EditorWindow {
    private const string Version = "1.1.0";
    private const string SaveFileVersion = "2";
    private const string LogPrefix = "[<color=#9fffe3>MintySnapshot Utility</color>] ";
    private const string FakeUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:102.0) Gecko/20100101 Firefox/102.0";
    private static bool _updateAvailable;

    private int _pageNumber;
    
    private static int _languageSelected;
    private readonly string[] _languageOptions = new string[4] { "English", "日本語", "한국인", "Русский" };

    private int _resolutionSelected;
    private readonly string[] _resolutionOptions = new string[4] { "Custom", "Normal", "VRChat", "ChilloutVR" };

    private int _standardResSelected;
    private readonly string[] _standardResOptions = new string[6] { "480p", "720p", "1080p", "1440p (2K)", "2160p (4K)", "4320p (8K)" };

    private int _resolutionMultiplier = 1;

    private int _height = 1080, _width = 1920;
    private bool _isTransparent, _openFileDirectory, _openInDefaultImageViewer;
    
    private static string _japaneseContributors, _koreanContributors, _russianContributors;

    private static Camera _camera;
    private string _cameraNameFromScene;

    [MenuItem("Tools/Minty Labs/Snapshot Utility")]
    private static void ShowWindow() {
        CheckForUpdate();
        var eWindow = GetWindow<SnapshotUtility>();
        eWindow.titleContent = new GUIContent("Snapshot Utility");
        eWindow.minSize = new Vector2(500, 650);
        eWindow.autoRepaintOnSceneChange = true;
        eWindow.Show();

        try {
            if (_camera == null)
                _camera = GameObject.Find("Main Camera")?.GetComponent<Camera>();
        }
        catch {
            Debug.LogWarning(LogPrefix + "Could not find Main Camera in scene. Skipping...");
        }
    }

    private static void GetContributors() {
        var wc = new WebClient();
        wc.Headers.Add("User-Agent", FakeUserAgent);
        _japaneseContributors = wc.DownloadString("https://raw.githubusercontent.com/Minty-Labs/Unity-Snapshot-Utility/main/Remote/JapaneseContributors.txt");
        _koreanContributors = wc.DownloadString("https://raw.githubusercontent.com/Minty-Labs/Unity-Snapshot-Utility/main/Remote/KoreanContributors.txt");
        _russianContributors = wc.DownloadString("https://raw.githubusercontent.com/Minty-Labs/Unity-Snapshot-Utility/main/Remote/RussianContributors.txt");
        wc.Dispose();
    }

    private static void CheckForUpdate() {
        Debug.Log(LogPrefix + LanguageModel.CheckingForUpdate(_languageSelected));
        var wc = new WebClient();
        wc.Headers.Add("User-Agent", FakeUserAgent);
        var versionString = wc.DownloadString("https://raw.githubusercontent.com/Minty-Labs/Unity-Snapshot-Utility/main/Remote/version.txt");
        _updateAvailable = versionString != Version;
        Debug.Log(LogPrefix + (_updateAvailable ? LanguageModel.UpdateAvailable(_languageSelected) : LanguageModel.NoUpdateAvailable(_languageSelected)));
        wc.Dispose();
    }

    private void OnGUI() {
        EditorStyles.label.richText = true;
        GUILayout.Space(12f);
        var style = new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleCenter, richText = true, fixedHeight = 40f};

        EditorGUILayout.BeginHorizontal();
        var menuButtonStyle = new GUIStyle(GUI.skin.button) {fixedWidth = 100f};
        if (GUILayout.Button($"{(_pageNumber == 0 ? "> " : "")}" + LanguageModel.Main(_languageSelected) + $"{(_pageNumber == 0 ? " <" : "")}", style: menuButtonStyle)) 
            _pageNumber = 0;
        if (GUILayout.Button($"{(_pageNumber == 1 ? "> " : "")}" + LanguageModel.About(_languageSelected) + $"{(_pageNumber == 1 ? " <" : "")}" + 
                             $"{(_updateAvailable ? " (1)" : "")}", style: menuButtonStyle)) {
            _pageNumber = 1;
            GetContributors();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("<size=18><color=#9fffe3>Minty</color>Snapshot Utility</size>", style, GUILayout.ExpandWidth(true));
        EditorGUILayout.Separator();

        var savedValueDir = new DirectoryInfo("Assets/Minty Labs/Snapshot Utility/Saved Values/");
        if (!savedValueDir.Exists) savedValueDir.Create();
        var savedValueFile = new FileInfo(savedValueDir.FullName + "__savedValues.txt");
        if (!savedValueFile.Exists) savedValueFile.Create();

        #region Page 1 (0) - Main Menu

        if (_pageNumber == 0) {

            EditorGUILayout.LabelField($"<size=15><b>{LanguageModel.GeneralOptions(_languageSelected)}</b></size>");
            _languageSelected = EditorGUILayout.Popup(LanguageModel.Language(_languageSelected), _languageSelected, _languageOptions);
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            var savedValuesBtn = new GUIStyle(GUI.skin.button) {fixedWidth = _languageSelected == 3 ? 225f : 150f};
            if (GUILayout.Button(LanguageModel.LoadSavedValues(_languageSelected), style: savedValuesBtn)) {
                var savedValues = File.ReadAllLines(savedValueFile.FullName);
                if (savedValues == null || savedValues.Length == 0) {
                    Debug.LogError(LogPrefix + LanguageModel.SaveFileIssue(_languageSelected));
                    EditorGUILayout.HelpBox(LanguageModel.SaveFileIssue(_languageSelected), MessageType.Error);
                    return;
                }

                var versionLine = savedValues[11];
                if (!string.IsNullOrWhiteSpace(versionLine)) {
                    Debug.LogError(LogPrefix + LanguageModel.SaveFileIssue(_languageSelected));
                    EditorGUILayout.HelpBox(LanguageModel.SaveFileIssue(_languageSelected), MessageType.Error);

                    if (versionLine.Contains(SaveFileVersion)) return;
                    Debug.LogError(LogPrefix + LanguageModel.OldSaveFile(_languageSelected));
                    EditorGUILayout.HelpBox(LanguageModel.OldSaveFile(_languageSelected), MessageType.Error);
                    return;
                }
                
                _isTransparent = bool.Parse(savedValues[0]);
                _openFileDirectory = bool.Parse(savedValues[1]);
                _openInDefaultImageViewer = bool.Parse(savedValues[2]);
                _resolutionSelected = int.Parse(savedValues[3]);
                _standardResSelected = int.Parse(savedValues[4]);
                _resolutionMultiplier = int.Parse(savedValues[5]);
                _cameraNameFromScene = savedValues[6];
                _height = int.Parse(savedValues[7]);
                _width = int.Parse(savedValues[8]);
                var date = savedValues[10].Split(':');
                Debug.Log(LogPrefix + "Loaded Saved Values" + date[1].TrimStart(' ') + ":" + date[2] + ":" + date[3]);
            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(LanguageModel.SelectCamera(_languageSelected));
            _camera = EditorGUILayout.ObjectField(_camera, typeof(Camera), true, null) as Camera;
            EditorGUILayout.EndHorizontal();
            _cameraNameFromScene = _camera == null ? "null" : _camera.gameObject.name;
            
            _isTransparent = EditorGUILayout.ToggleLeft(LanguageModel.HideSkybox(_languageSelected), _isTransparent);
            var typeRect = GUILayoutUtility.GetLastRect();
            GUI.Label(new Rect(typeRect.x - 80, typeRect.y, typeRect.width, typeRect.height), new GUIContent("", LanguageModel.SkyboxTooltip(_languageSelected)));
        
            _openFileDirectory = EditorGUILayout.ToggleLeft(LanguageModel.OpenInExplorer(_languageSelected), _openFileDirectory);
            _openInDefaultImageViewer = EditorGUILayout.ToggleLeft(LanguageModel.OpenInViewer(_languageSelected), _openInDefaultImageViewer);
        
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField($"<size=15><b>{LanguageModel.ResolutionTypeSize(_languageSelected)}</b></size>");
            
            EditorGUI.BeginChangeCheck();
            _resolutionSelected = EditorGUILayout.Popup(LanguageModel.ResolutionType(_languageSelected), _resolutionSelected, _resolutionOptions);
            switch (_resolutionSelected) {
                case 0:
                    _resolutionMultiplier = 1;
                    EditorGUILayout.LabelField(LanguageModel.SetOwn(_languageSelected));
                    _width = EditorGUILayout.IntField(LanguageModel.Width(_languageSelected), _width);
                    _height = EditorGUILayout.IntField(LanguageModel.Height(_languageSelected), _height);
                    if (_width > 11999 || _height > 11999) {
                        EditorGUILayout.HelpBox(LanguageModel.HighResWarning(_languageSelected), MessageType.Warning);
                        return;
                    }
                    break;
                case 1:
                    _resolutionMultiplier = 1;
                    _standardResSelected = EditorGUILayout.Popup(LanguageModel.ResPresets(_languageSelected), _standardResSelected, _standardResOptions);
                    switch (_standardResSelected) {
                        case 0: // 480p
                            _width = 720;
                            _height = 480;
                            break;
                        case 1: // 720p
                            _width = 1280;
                            _height = 720;
                            break;
                        case 2: // 1080p
                            _width = 1920;
                            _height = 1080;
                            break;
                        case 3: // 1440p
                            _width = 2560;
                            _height = 1440;
                            break;
                        case 4: // 4K
                            _width = 3840;
                            _height = 2160;
                            break;
                        case 5: // 8K
                            _width = 7680;
                            _height = 4320;
                            break;
                    }
                    break;
                case 2: // VRChat
                    _width = 1200;
                    _height = 900;
                    break;
                case 3: // ChilloutVR
                    _width = 512;
                    _height = 512;
                    break;
            }
            
            EditorGUI.EndChangeCheck();

            if (_resolutionSelected != 0) {
                _width = EditorGUILayout.IntField(LanguageModel.Width(_languageSelected), _width) * _resolutionMultiplier;
                _height = EditorGUILayout.IntField(LanguageModel.Height(_languageSelected), _height) * _resolutionMultiplier;
            }

            if (_resolutionSelected == 2 || _resolutionSelected == 3) {
                EditorGUILayout.BeginHorizontal();
                _resolutionMultiplier = EditorGUILayout.IntSlider(label: LanguageModel.Multiplier(_languageSelected), _resolutionMultiplier, 1, 16);
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button(LanguageModel.ResetValues(_languageSelected))) {
                _isTransparent = false;
                _openFileDirectory = false;
                _openInDefaultImageViewer = false;
                _resolutionSelected = 1;
                _standardResSelected = 2;
                _resolutionMultiplier = 1;
                _cameraNameFromScene = "null";
                _height = 1920;
                _width = 1080;
            }

            if (GUILayout.Button(LanguageModel.SaveValues(_languageSelected))) {
                if (!savedValueFile.Exists) savedValueFile.Create();
                var sb = new StringBuilder();
                sb.AppendLine(_isTransparent.ToString());
                sb.AppendLine(_openFileDirectory.ToString());
                sb.AppendLine(_openInDefaultImageViewer.ToString());
                sb.AppendLine(_resolutionSelected.ToString());
                sb.AppendLine(_standardResSelected.ToString());
                sb.AppendLine(_resolutionMultiplier.ToString());
                sb.AppendLine(_cameraNameFromScene);
                sb.AppendLine(_height.ToString());
                sb.AppendLine(_width.ToString());
                sb.AppendLine("\nUpdated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sb.Append("Version: " + SaveFileVersion);
                File.WriteAllText(savedValueFile.FullName, sb.ToString());
                Debug.Log(LogPrefix + "Saved values to file");
            }

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField($"<size=15><b>{LanguageModel.HowToUse(_languageSelected)}</b></size>");
            EditorGUILayout.HelpBox($"  1. {LanguageModel.AddCamera(_languageSelected)}\n" +
                                    $"  2. {LanguageModel.BoxSetup(_languageSelected)}\n" +
                                    $"  3. {LanguageModel.BoxSetRes(_languageSelected)}\n" +
                                    $"  4. {LanguageModel.BoxPressButton(_languageSelected)}" +
                                    $"{(_openFileDirectory ? $"\n  5a. {LanguageModel.BoxFileExplorer(_languageSelected)}" : "")}" +
                                    $"{(_openInDefaultImageViewer ? $"\n  5b. {LanguageModel.BoxImageViewer(_languageSelected)}" : "")}", MessageType.Info);
            
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField($"<size=15>{LanguageModel.ImageResolutionOutcome(_languageSelected)} {_width}x{_height}</size>");
            if (_width > 11999 || _height > 11999) {
                EditorGUILayout.HelpBox(LanguageModel.HighResWarning(_languageSelected), MessageType.Warning);
            }
            var playModeButtons = new GUIStyle(GUI.skin.button) { fixedWidth = 150f, fixedHeight = 30f };
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            GUI.backgroundColor = new Color32(0, 0, 255, 255);
            if (GUILayout.Button(LanguageModel.TakeSnapshotButton(_languageSelected), playModeButtons)) {
                var bytes = CaptureSnapshot(_isTransparent, _width, _height, _camera);
                if (bytes == null) return;
                var filename = ScreenshotName(_width.ToString(), _height.ToString());
                var dInfo = new DirectoryInfo("Assets/Minty Labs/Snapshot Utility/Snapshots");
                if (!dInfo.Exists) dInfo.Create();

                File.WriteAllBytes(dInfo.FullName + "\\" + filename, bytes);
                Debug.Log(LogPrefix + $"Saved to \"{filename}\"");

                if (_openFileDirectory) {
                    Process.Start(dInfo.FullName);
                    Debug.Log(LogPrefix + $"Opening \"{dInfo.FullName}\"");
                }

                if (_openInDefaultImageViewer) {
                    Application.OpenURL("file://" + dInfo.FullName + "\\" + filename);
                    Debug.Log(LogPrefix + $"Opening \"{filename}\"");
                }
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return;
        }

        #endregion

        #region Page 2 (1) - About

        EditorGUILayout.LabelField($"<size=15><b>{LanguageModel.About(_languageSelected)}</b></size>");
        
        EditorGUILayout.LabelField($"<size=12><b>{LanguageModel.Version(_languageSelected)}:</b> <color=#EECCE0>{Version}</color>{(_updateAvailable ? $" - <color=red>{LanguageModel.UpdateAvailable(_languageSelected)}</color>" : "")}</size>");
        var aboutMenuButtonStyles = new GUIStyle(GUI.skin.button) {fixedWidth = 150f};
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(LanguageModel.CheckForUpdateButton(_languageSelected), style: aboutMenuButtonStyles)) 
            CheckForUpdate();

        if (_updateAvailable) {
            if (GUILayout.Button(LanguageModel.OpenBoothPage(_languageSelected), style: aboutMenuButtonStyles)) 
                Application.OpenURL("https://mintylabs.booth.pm/items/4949097");
        }
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField($"<size=12>{LanguageModel.Developer(_languageSelected)}: <color=#9fffe3>Mint</color>Lily</size>");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Minty Labs")) 
            Application.OpenURL("https://mintylabs.dev/");
        if (GUILayout.Button("Booth"))
            Application.OpenURL("https://mintylabs.booth.pm/");
        if (GUILayout.Button("X (Twitter)")) 
            Application.OpenURL("https://x.com/MintLiIy");
        if (GUILayout.Button("GitHub")) 
            Application.OpenURL("https://github.com/MintLily");
        if (GUILayout.Button("Ko-fi (Donate)")) 
            Application.OpenURL("https://ko-fi.com/MintLily");
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.Space(10);
        GUI.backgroundColor = new Color32(0, 0, 0, 55);
        EditorGUILayout.BeginVertical(new GUIStyle(GUI.skin.window) { padding = new RectOffset(10, 10, 10, 10)});
        EditorGUILayout.LabelField($"<size=15><b>{LanguageModel.LanguageContributors(_languageSelected)}</b></size>");
        EditorGUILayout.LabelField($"<size=12><b>{LanguageModel.English(_languageSelected)} (English)</b></size>");
        EditorGUILayout.LabelField("Lily");
        EditorGUILayout.LabelField($"<size=12><b>{LanguageModel.Japanese(_languageSelected)} (日本語)</b></size>");
        EditorGUILayout.LabelField(_japaneseContributors ?? "null");
        EditorGUILayout.LabelField($"<size=12><b>{LanguageModel.Korean(_languageSelected)} (한국인)</b></size>");
        EditorGUILayout.LabelField(_koreanContributors ?? "null");
        EditorGUILayout.LabelField($"<size=12><b>{LanguageModel.Russian(_languageSelected)} (Русский)</b></size>");
        EditorGUILayout.LabelField(_russianContributors ?? "null");
        EditorGUILayout.EndVertical();

        #endregion
    }

    private static byte[] CaptureSnapshot(bool isTransparent, int width, int height, Camera camera) {
        if (camera == null) {
            Debug.LogError(LogPrefix + LanguageModel.NoCameraError(_languageSelected));
            EditorGUILayout.HelpBox(LanguageModel.NoCameraError(_languageSelected), MessageType.Error);
            return null;
        }

        if (isTransparent) {
            var transRenderTex = new RenderTexture(width, height, (int)TextureFormat.ARGB32);
            var normalCamClearFlags = camera.clearFlags;
            var normalCamBgColor = camera.backgroundColor;
            camera.targetTexture = transRenderTex;
            camera.clearFlags = CameraClearFlags.Nothing;
            camera.backgroundColor = new Color(0, 0, 0, 0);
            camera.nearClipPlane = 0.01f;
            var transSnapshot = new Texture2D(width, height, TextureFormat.ARGB32, false);
            
            camera.Render();
            RenderTexture.active = transRenderTex;
            transSnapshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);

            camera.backgroundColor = normalCamBgColor;
            camera.clearFlags = normalCamClearFlags;
            camera.targetTexture = null;
            RenderTexture.active = null;
            DestroyImmediate(transRenderTex);
            
            return transSnapshot.EncodeToPNG();
        }
        
        var normRenderTex = new RenderTexture(width, height, (int)TextureFormat.ARGB32);
        camera.targetTexture = normRenderTex;
        camera.nearClipPlane = 0.01f;
        var normSnapshot = new Texture2D(width, height, TextureFormat.ARGB32, false);
        
        camera.Render();
        RenderTexture.active = normRenderTex;
        normSnapshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);

        camera.targetTexture = null;
        RenderTexture.active = null;
        DestroyImmediate(normRenderTex);
            
        return normSnapshot.EncodeToPNG();
    }
    
    private static string ScreenshotName(string width, string height) => $"{Application.productName}_snapshot_{width}x{height}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
}