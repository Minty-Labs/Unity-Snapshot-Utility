/*
 * "English", "日本語", "한국인", "Русский"
 * https://crowdin.com/project/unity-snapshot-utility/invite?h=71549c4b8a35fa43f2b93971d2c12dec1778145
 */

public static class LanguageModel {
    #region Page Titles

    public static string Main(int lang) {
        switch (lang) {
            case 1: return "主要";
            case 2: return "기본";
            case 3: return "Основной";
            default: return "Main";
        }
    }
    
    public static string About(int lang) {
        switch (lang) {
            case 1: return "だいたい";
            case 2: return "에 대한";
            case 3: return "О";
            default: return "About";
        }
    }

    #endregion

    #region General Options

    public static string GeneralOptions(int lang) {
        switch (lang) {
            case 1: return "一般的なオプション";
            case 2: return "일반 옵션";
            case 3: return "Общие настройки";
            default: return "General Options";
        }
    }
    
    public static string LoadSavedValues(int lang) {
        switch (lang) {
            case 1: return "保存された値をロードする";
            case 2: return "저장된 값 로드";
            case 3: return "Загрузить сохраненные значения";
            default: return "Load Saved Values";
        }
    }
    
    public static string SaveFileIssue(int lang) {
        switch (lang) {
            case 1: return "保存された値ファイルが破損しているか、何も保存されていません。 このボタンを押す前に値を保存してください";
            case 2: return "저장된 값 파일이 손상되었거나 아무것도 저장되지 않았습니다. 이 버튼을 누르기 전에 값을 저장하십시오";
            case 3: return "Файл сохраненных значений поврежден или ничего не было сохранено. Пожалуйста, сохраните значения, прежде чем нажимать эту кнопку";
            default: return "Saved values file is corrupted or nothing was saved. Please save values before pressing this button";
        }
    }
    
    public static string OldSaveFile(int lang) {
        switch (lang) {
            case 1: return "保存された値ファイルは古いバージョンのものであるため、壊れたり、まったく機能しなくなる可能性があります。 値をロードする前に新しい値を保存してください";
            case 2: return "저장된 값 파일은 이전 버전의 것이므로 문제가 발생하거나 전혀 작동하지 않을 수 있습니다. 값을 로드하기 전에 새 값을 저장하십시오.";
            case 3: return "Файл сохраненных значений из более старой версии, что-то может сломаться или вообще не работать. Пожалуйста, сохраните новые значения перед загрузкой значений";
            default: return "Saved values file is from an older version, things may break or not work at all. Please save new values before loading values";
        }
    }
    
    public static string SelectCamera(int lang) {
        switch (lang) {
            case 1: return "カメラの選択";
            case 2: return "카메라 선택";
            case 3: return "Выберите камеру";
            default: return "Select Camera";
        }
    }
    
    public static string HideSkybox(int lang) {
        switch (lang) {
            case 1: return "スカイボックスを非表示にしますか?";
            case 2: return "스카이박스를 숨기시겠습니까?";
            case 3: return "Скрыть скайбокс?";
            default: return "Hide Skybox?";
        }
    }
    
    public static string SkyboxTooltip(int lang) {
        switch (lang) {
            case 1: return "オブジェクトのみを表示";
            case 2: return "객체만 표시";
            case 3: return "Показывать только объекты";
            default: return "Only show object(s)";
        }
    }
    
    public static string OpenInExplorer(int lang) {
        switch (lang) {
            case 1: return "オープンディレクトリ?";
            case 2: return "오픈 디렉토리?";
            case 3: return "Открыть каталог?";
            default: return "Open Directory?";
        }
    }
    
    public static string OpenInViewer(int lang) {
        switch (lang) {
            case 1: return "画像ビューアを開きますか?";
            case 2: return "이미지 뷰어를 여시겠습니까?";
            case 3: return "Открыть средство просмотра изображений?";
            default: return "Open Image Viewer?";
        }
    }

    #endregion
    
    #region Resolution Options
    
    public static string ResolutionTypeSize(int lang) {
        switch (lang) {
            case 1: return "解像度の種類/サイズ";
            case 2: return "해상도 유형/크기";
            case 3: return "Тип разрешения/размер";
            default: return "Resolution Type/Size";
        }
    }

    public static string ResolutionType(int lang) {
        switch (lang) {
            case 1: return "解像度の種類";
            case 2: return "해상도 유형";
            case 3: return "Тип разрешения";
            default: return "Resolution Type";
        }
    }

    public static string SetOwn(int lang) {
        switch (lang) {
            case 1: return "独自の値を設定する";
            case 2: return "나만의 가치 설정";
            case 3: return "Установите свои собственные значения";
            default: return "Set your own values";
        }
    }

    public static string Width(int lang) {
        switch (lang) {
            case 1: return "幅";
            case 2: return "너비";
            case 3: return "Ширина";
            default: return "Width";
        }
    }

    public static string Height(int lang) {
        switch (lang) {
            case 1: return "身長";
            case 2: return "키";
            case 3: return "Высота";
            default: return "Height";
        }
    }
    
    public static string HighResWarning(int lang) {
        switch (lang) {
            case 1: return "解像度が高いと、スナップショットのキャプチャ時に一時停止が発生し、ファイル サイズが大きくなる場合があります。";
            case 2: return "해상도가 높을수록 스냅샷을 캡처할 때 일시 중지가 발생할 수 있으며 더 큰 파일 크기가 생성됩니다.";
            case 3: return "Более высокие разрешения могут вызвать паузу при захвате моментального снимка и создать файлы большего размера.";
            default: return "Higher resolutions may cause a pause when capturing a snapshot and will create larger file sizes";
        }
    }
    
    public static string ResPresets(int lang) {
        switch (lang) {
            case 1: return "解像度のプリセット";
            case 2: return "해상도 사전 설정";
            case 3: return "Предустановки разрешения";
            default: return "Resolution Presets";
        }
    }
    
    public static string ResetValues(int lang) {
        switch (lang) {
            case 1: return "値のリセット";
            case 2: return "재설정 값";
            case 3: return "Сбросить значения";
            default: return "Reset Values";
        }
    }
    
    public static string SaveValues(int lang) {
        switch (lang) {
            case 1: return "値の保存";
            case 2: return "값 저장";
            case 3: return "Сохранить значения";
            default: return "Save Values";
        }
    }
    
    public static string Multiplier(int lang) {
        switch (lang) {
            case 1: return "乗数";
            case 2: return "승수";
            case 3: return "Множитель";
            default: return "Multiplier";
        }
    }
    
    #endregion

    #region How To use

    public static string HowToUse(int lang) {
        switch (lang) {
            case 1: return "使い方";
            case 2: return "사용하는 방법";
            case 3: return "Как использовать";
            default: return "How To Use";
        }
    }
    
    public static string AddCamera(int lang) {
        switch (lang) { 
            case 1: return "カメラをシーンに追加し、「カメラ」フィールドで参照します。";
            case 2: return "장면에 카메라를 추가하고 \"Camera\" 필드에서 참조하십시오.";
            case 3: return "Добавьте камеру в свою сцену и укажите ее в поле «Камера».";
            default: return "Add a camera to your scene and reference it in the \"Camera\" field";
        }
    }
    
    public static string BoxSetup(int lang) {
        switch (lang) { 
            case 1: return "オブジェクトとカメラを希望の場所と位置に設定します";
            case 2: return "물체와 카메라를 원하는 위치와 위치로 설정합니다.";
            case 3: return "Установите объект (ы) и камеру в нужное место и положение";
            default: return "Set up your object(s) and camera to desired location and position";
        }
    }
    
    public static string BoxSetRes(int lang) {
        switch (lang) { 
            case 1: return "解像度を設定する";
            case 2: return "해상도 설정";
            case 3: return "Установите свое разрешение";
            default: return "Set your resolution";
        }
    }
    
    public static string BoxPressButton(int lang) {
        switch (lang) { 
            case 1: return "下の青い「スナップショットをとる！」ボタンを押します。";
            case 2: return "아래의 파란색 \"스냅샷을 찍으세요!\" 버튼을 누르세요.";
            case 3: return "Нажмите синюю кнопку \"Моментальный снимок!\" ниже.";
            default: return "Press the Blue \"Take Snapshot!\" button below";
        }
    }
    
    public static string BoxFileExplorer(int lang) {
        switch (lang) { 
            case 1: return "ファイルエクスプローラーが開き、画像のファイルが含まれるフォルダーが表示されます";
            case 2: return "파일 탐색기는 사진 파일이 포함된 폴더를 엽니다.";
            case 3: return "Проводник откроет папку, содержащую файл изображения.";
            default: return "File Explorer will open to the folder containing the file of the picture";
        }
    }
    
    public static string BoxImageViewer(int lang) {
        switch (lang) { 
            case 1: return "デフォルトの画像ビューア (PNG 用) が開き、スナップショットが表示されます";
            case 2: return "기본 이미지 뷰어(PNG용)가 스냅샷과 함께 열립니다.";
            case 3: return "Ваша программа просмотра изображений по умолчанию (для PNG) откроется со снимком";
            default: return "Your default image viewer (for PNGs) will open with the snapshot";
        }
    }
    
    public static string ImageResolutionOutcome(int lang) {
        switch (lang) { 
            case 1: return "画像解像度:";
            case 2: return "이미지 해상도:";
            case 3: return "Разрешение изображения:";
            default: return "Image Resolution:";
        }
    }
    
    public static string TakeSnapshotButton(int lang) {
        switch (lang) { 
            case 1: return "スナップショットをとる！";
            case 2: return "스냅샷을 찍으세요!";
            case 3: return "Моментальный снимок!";
            default: return "Take Snapshot!";
        }
    }
    
    public static string NoCameraError(int lang) {
        switch (lang) { 
            case 1: return "カメラが見つかりません。 カメラをシーンに追加し、「カメラ」フィールドで参照してください。";
            case 2: return "카메라를 찾을 수 없습니다. 장면에 카메라를 추가하고 \"Camera\" 필드에서 참조하십시오.";
            case 3: return "Камера не найдена. Пожалуйста, добавьте камеру в свою сцену и укажите ее в поле \"Камера\".";
            default: return "No camera found. Please add a camera to your scene and reference it in the \"Camera\" field";
        }
    }

    #endregion

    #region Version

    public static string Version(int lang) {
        switch (lang) { 
            case 1: return "バージョン";
            case 2: return "버전";
            case 3: return "Версия";
            default: return "Version";
        }
    }
    
    public static string CheckForUpdateButton(int lang) {
        switch (lang) { 
            case 1: return "更新を確認";
            case 2: return "업데이트를 확인";
            case 3: return "Проверить обновления";
            default: return "Check for update";
        }
    }
    
    public static string OpenBoothPage(int lang) {
        switch (lang) { 
            case 1: return "ブースページを開く";
            case 2: return "부스 페이지 열기";
            case 3: return "Открыть страницу \"Booth\"";
            default: return "Open Booth Page";
        }
    }
    
    public static string CheckingForUpdate(int lang) {
        switch (lang) { 
            case 1: return "アップデートをチェックしています...";
            case 2: return "업데이트 확인 중...";
            case 3: return "Проверка обновлений...";
            default: return "Checking for update...";
        }
    }
    
    public static string UpdateAvailable(int lang) {
        switch (lang) { 
            case 1: return "アップデートが利用可能です!";
            case 2: return "업데이트 가능!";
            case 3: return "Доступно обновление!";
            default: return "Update available!";
        }
    }
    
    public static string NoUpdateAvailable(int lang) {
        switch (lang) { 
            case 1: return "利用可能なアップデートはありません";
            case 2: return "사용 가능한 업데이트 없음";
            case 3: return "Нет доступных обновлений";
            default: return "No update available";
        }
    }

    public static string Developer(int lang) {
        switch (lang) {
            case 1: return "デベロッパー";
            case 2: return "개발자";
            case 3: return "Разработчик";
            default: return "Developer";
        }
    }

    public static string Language(int lang) {
        switch (lang) { 
            case 1: return "言語";
            case 2: return "언어";
            case 3: return "Язык";
            default: return "Language";
        }
    }
    
    public static string LanguageContributors(int lang) {
        switch (lang) { 
            case 1: return "言語貢献者";
            case 2: return "언어 기여자";
            case 3: return "Авторы языка";
            default: return "Language Contributors";
        }
    }
    
    public static string English(int lang) {
        switch (lang) { 
            case 1: return "英語";
            case 2: return "영어";
            case 3: return "Английский";
            default: return "English";
        }
    }
    
    public static string Japanese(int lang) {
        switch (lang) { 
            case 1: return "日本";
            case 2: return "일본어";
            case 3: return "Японский";
            default: return "Japanese";
        }
    }
    
    public static string Korean(int lang) {
        switch (lang) { 
            case 1: return "韓国語";
            case 2: return "한국인";
            case 3: return "Корейский";
            default: return "Korean";
        }
    }
    
    public static string Russian(int lang) {
        switch (lang) { 
            case 1: return "ロシア";
            case 2: return "러시아인";
            case 3: return "Русский";
            default: return "Russian";
        }
    }

    #endregion
}