using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class SlideManagerScript : MonoBehaviour
{
    [SerializeField]
    private Canvas slideCanvas;

    [SerializeField]
    private TextMeshProUGUI slideTitle, slideContents;

    [SerializeField]
    private Button nextButton, previousButton, finishButton;

    private int slideIndex = 0;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        slideIndex = 0;

        slideTitle.text = GameManagerScript.CurrentObjective.Slides[slideIndex].Title;
        slideContents.text = GameManagerScript.CurrentObjective.Slides[slideIndex].Contents;

        previousButton.gameObject.SetActive(false);
        finishButton.gameObject.SetActive(false);
    }

    public void OpenSlides()
    {
        slideCanvas.enabled = true;
    }

    public void CloseSlides()
    {
        slideCanvas.enabled = false;
        nextButton.gameObject.SetActive( true );
    }

	public void LoadNextSlide ()
	{
		LoadToTextboxes(GameManagerScript.CurrentObjective.Slides[ ++slideIndex ] );

        if ( !previousButton.IsActive() )
			previousButton.gameObject.SetActive( true );

        if ( slideIndex == GameManagerScript.CurrentObjective.Slides.Count - 1 )
        {
            nextButton.gameObject.SetActive( false );
            finishButton.gameObject.SetActive( true );
        }
    }

	public void LoadPreviousSlide ()
	{
		LoadToTextboxes(GameManagerScript.CurrentObjective.Slides[ --slideIndex ] );

        if ( !nextButton.IsActive() )
            nextButton.gameObject.SetActive( true );
        if ( finishButton.IsActive() )
            finishButton.gameObject.SetActive( false );

        if ( slideIndex == 0 )
            previousButton.gameObject.SetActive( false );
    }

	private void LoadToTextboxes( SlideData slide )
	{
        slideCanvas.enabled = true;

        slideTitle.text = slide.Title;
        slideContents.text = slide.Contents;
    }
}
