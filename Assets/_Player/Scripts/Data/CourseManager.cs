using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class CourseManager : MonoBehaviour
{
    public Course course;
    public CourseSections section;
    public string titleSearch = "Introducci�n.";

    private void Start()
    {
        Debug.Log("hello");
        //Llama el url de la api - por pruebas de momento esta asi 
        StartCoroutine(LoadCourseCoroutine("https://sophie-qa-api.azurewebsites.net/api/Courses/fac5b047-fbd8-4728-9a71-b316a241f2ac"));
    }

    //metodo para convertirn json en class course
    public void LoadCourseFromJSON(string jsonText)
    {
        course = JsonUtility.FromJson<Course>(jsonText);
    }

    //corrutina que llama a la api para buscar la data
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
                //metodo para json a class course
                LoadCourseFromJSON(jsonText);
                //captura en variable la seccion (un contenido del curso) 
                section = course.sections.FirstOrDefault(s => s.title == titleSearch);
                //captura el contenido de esa presentacion
                ParagraphsManager p = GetComponent<ParagraphsManager>();
                p.paragraphs = section.elements[0].elementLesson.paragraphs;
                //llena los items que se van a usar para la presentacion en el reproductor
                StartCoroutine(p.fill());
            }
        }
    }
}

//estructura de curso

[System.Serializable]
public class Course
{
    public string _id;
    public string code;
    public string organizationCode;
    public string author_code;
    public string approvalStatus;
    public string dateCreated;
    public string language;
    public string lessonTheme;
    public Details details;
    public CourseSections[] sections;
    public string languageName;
    public string voice;
    public string approvedBy;
    public string duration;
    public string createdBy;
}

[System.Serializable]
public class Details
{
    public string title;
    public string summary;
    public string[] categories;
    public string cover;
}

[System.Serializable]
public class CourseSections
{
    public string title;
    public MultiElement[] elements;
}

[System.Serializable]
public class MultiElement
{
    public string type;
    public string title;
    public string elementCode;
    public string quizCode;
    public ElementLesson elementLesson;
    public ElementVideo elementVideo;
    public ElementQuiz elementQuiz;
    public ElementHtml elementText;
    public ElementFile elementFile;
}

[System.Serializable]
public class ElementLesson
{
    public string lessonTheme;
    public Paragraphs[] paragraphs;
}

[System.Serializable]
public class ElementHtml
{
    public string title;
    public string cover;
    public string content;
}

[System.Serializable]
public class ElementVideo
{
    public string url;
}

[System.Serializable]
public class ElementQuiz
{
    public object[] quizz_list;
    public bool isAICreated;
}

[System.Serializable]
public class ItemQuizz
{
    public string code;
    public string question;
    public int order;
    public int answer;
    public string[] distractors;
}

[System.Serializable]
public class ElementFile
{
    public string name;
    public string url;
}

[System.Serializable]
public class Paragraphs
{
    public string content;
    public string audioUrl;
    public object[] splitAudioScript;
    public string audioScript;
    public Srt[] srt;
    public string[] keyPhrases;
    public string titleAI;
    public AlternativePronunciations[] alternativePronunciations;
    public ImageData imageData;
    public VideoData videoData;
}

[System.Serializable]
public class Srt
{
    public double start_time;
    public string text;
}

[System.Serializable]
public class AlternativePronunciations
{
    public string source;
    public string pronunciation;
}

[System.Serializable]
public class CourseDetails
{
    public string approvalStatus;
    public string approvedBy;
    public string author_code;
    public string buildingStatus;
    public string code;
    public string dateCreated;
    public Details details;
    public string organizationCode;
    public string organization_code;
    public object[] sections;
}

[System.Serializable]
public class CourseProgress
{
    public double averageScore;
    public int completedSections;
    public double percentage;
    public string status;
    public string totalSections;
}

[System.Serializable]
public class Progress
{
    public string _id;
    public string courseCode;
    public string userCode;
    public object progress;
}

[System.Serializable]
public class ImageData
{
    public Image2 image;
    public Thumb thumb;
    public FinalImage finalImage;
    public string urlBing;
}

[System.Serializable]
public class Image2
{
    public string url;
    public int width;
    public int height;
    public string imageId;
}

[System.Serializable]
public class Thumb
{
    public string url;
    public int width;
    public int height;
}

[System.Serializable]
public class FinalImage
{
    public string url;
}

[System.Serializable]
public class VideoData
{
    public FinalVideo finalVideo;
}

[System.Serializable]
public class FinalVideo
{
    public string url;
}

[System.Serializable]
public class ContentGeneratedByStructureInterface
{
    public string context;
    public string key;
    public string text;
    public string title;
    public int[] index;
}

[System.Serializable]
public class SimpleContentOutput
{
    public Content[] content;
    public int[] index;
}

[System.Serializable]
public class Content
{
    public string title;
    public string text;
}

[System.Serializable]
public class CourseTracking
{
    public int textsOk;
    public int textsErr;
    public int audiosOk;
    public int audiosErr;
    public int imagesOk;
    public int imagesErr;
    public int keysOk;
    public int keysErr;
    public Section section;
    public string[] paragraphs;
}

[System.Serializable]
public class Section
{
    public string code;
    public string title;
}

[System.Serializable]
public class TimelineObject
{
    public string keyword;
    public int delay;
    public int duration;
    public string fontColor;
    public int fontSize;
    public string inEffect;
    public string outEffect;
}

