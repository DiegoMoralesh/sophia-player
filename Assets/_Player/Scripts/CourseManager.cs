using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class CourseManager : MonoBehaviour
{

    public Course course;

    private void Start()
    {
        StartCoroutine(LoadCourseCoroutine("https://sophie-qa-api.azurewebsites.net/api/Courses/fac5b047-fbd8-4728-9a71-b316a241f2ac"));
    }

    public void LoadCourseFromJSON(string jsonText)
    {
        course = JsonUtility.FromJson<Course>(jsonText);
    }

    private IEnumerator LoadCourseCoroutine(string apiUrl)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error al cargar la API: " + webRequest.error);
            }
            else
            {
                string jsonText = webRequest.downloadHandler.text;
                Debug.Log(jsonText);
                LoadCourseFromJSON(jsonText);
            }
        }
    }
}

[System.Serializable]
public class Course
{
    public string _id { get; set; }
    public string code { get; set; }
    public string organizationCode { get; set; }
    public string author_code { get; set; }
    public string approvalStatus { get; set; }
    public string dateCreated { get; set; }
    public string language { get; set; }
    public string languageName { get; set; }
    public string lessonTheme { get; set; }
    public string voice { get; set; }
    public Details details { get; set; }
    public CourseSections[] sections { get; set; }
    public string createdBy { get; set; }
    public string company { get; set; }
    public CourseDetails course { get; set; }
    public CourseProgress courseProgress { get; set; }
    public string email { get; set; }
    public object group { get; set; }
    public string groupCode { get; set; }
    public object groupUser { get; set; }
    public object last_access { get; set; }
    public string last_course_accessed { get; set; }
    public string name { get; set; }
    public string password { get; set; }
    public string phone { get; set; }
    public string position { get; set; }
    public Progress progress { get; set; }
    public string role { get; set; }
    public string status { get; set; }
    public string avatarIntroUrl { get; set; }
    public object[] quizScores { get; set; }
}

[System.Serializable]
public class Details
{
    public string title { get; set; }
    public string summary { get; set; }
    public string[] categories { get; set; }
    public string cover { get; set; }
    public string longSummary { get; set; }
}

[System.Serializable]
public class CourseSections
{
    public string title { get; set; }
    public MultiElement[] elements { get; set; }
}

[System.Serializable]
public class MultiElement
{
    public string type { get; set; }
    public string title { get; set; }
    public string elementCode { get; set; }
    public string quizCode { get; set; }
    public ElementLesson elementLesson { get; set; }
    public ElementVideo elementVideo { get; set; }
    public ElementQuiz elementQuiz { get; set; }
    public ElementHtml elementText { get; set; }
    public ElementFile elementFile { get; set; }
}

[System.Serializable]
public class ElementLesson
{
    public string lessonTheme { get; set; }
    public SlideInterface[] paragraphs { get; set; }
}

[System.Serializable]
public class ElementHtml
{
    public string title { get; set; }
    public string cover { get; set; }
    public string content { get; set; }
}

[System.Serializable]
public class ElementVideo
{
    public string url { get; set; }
}

[System.Serializable]
public class ElementQuiz
{
    public object[] quizz_list { get; set; }
    public bool isAICreated { get; set; }
}

[System.Serializable]
public class ItemQuizz
{
    public string code { get; set; }
    public string question { get; set; }
    public int order { get; set; }
    public int answer { get; set; }
    public string[] distractors { get; set; }
}

[System.Serializable]
public class ElementFile
{
    public string name { get; set; }
    public string url { get; set; }
}

[System.Serializable]
public class SlideInterface
{
    public string content { get; set; }
    public string audioUrl { get; set; }
    public object[] splitAudioScript { get; set; }
    public string audioScript { get; set; }
    public Srt[] srt { get; set; }
    public string[] keyPhrases { get; set; }
    public string titleAI { get; set; }
    public AlternativePronunciationInterface[] alternativePronunciations { get; set; }
    public ImageData imageData { get; set; }
    public VideoData videoData { get; set; }
}

[System.Serializable]
public class Srt
{
    public double start_time { get; set; }
    public string text { get; set; }
}

[System.Serializable]
public class AlternativePronunciationInterface
{
    public string source { get; set; }
    public string pronunciation { get; set; }
}

[System.Serializable]
public class CourseDetails
{
    public string approvalStatus { get; set; }
    public string approvedBy { get; set; }
    public string author_code { get; set; }
    public string buildingStatus { get; set; }
    public string code { get; set; }
    public string dateCreated { get; set; }
    public Details details { get; set; }
    public string organizationCode { get; set; }
    public string organization_code { get; set; }
    public object[] sections { get; set; }
}

[System.Serializable]
public class CourseProgress
{
    public double averageScore { get; set; }
    public int completedSections { get; set; }
    public double percentage { get; set; }
    public string status { get; set; }
    public string totalSections { get; set; }
}

[System.Serializable]
public class Progress
{
    public string _id { get; set; }
    public string courseCode { get; set; }
    public string userCode { get; set; }
    public object progress { get; set; }
}

[System.Serializable]
public class ImageData
{
    public Thumb thumb { get; set; }
    public FinalImage finalImage { get; set; }
    public string urlBing { get; set; }
}

[System.Serializable]
public class Thumb
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

[System.Serializable]
public class FinalImage
{
    public string url { get; set; }
}

[System.Serializable]
public class VideoData
{
    public FinalVideo finalVideo { get; set; }
}

[System.Serializable]
public class FinalVideo
{
    public string url { get; set; }
}

[System.Serializable]
public class ContentGeneratedByStructureInterface
{
    public string context { get; set; }
    public string key { get; set; }
    public string text { get; set; }
    public string title { get; set; }
    public int[] index { get; set; }
}

[System.Serializable]
public class SimpleContentOutput
{
    public Content[] content { get; set; }
    public int[] index { get; set; }
}

[System.Serializable]
public class Content
{
    public string title { get; set; }
    public string text { get; set; }
}

[System.Serializable]
public class CourseTracking
{
    public int textsOk { get; set; }
    public int textsErr { get; set; }
    public int audiosOk { get; set; }
    public int audiosErr { get; set; }
    public int imagesOk { get; set; }
    public int imagesErr { get; set; }
    public int keysOk { get; set; }
    public int keysErr { get; set; }
    public Section section { get; set; }
    public string[] paragraphs { get; set; }
}

[System.Serializable]
public class Section
{
    public string code { get; set; }
    public string title { get; set; }
}

[System.Serializable]
public class TimelineObject
{
    public string keyword { get; set; }
    public int delay { get; set; }
    public int duration { get; set; }
    public string fontColor { get; set; }
    public int fontSize { get; set; }
    public string inEffect { get; set; }
    public string outEffect { get; set; }
}

