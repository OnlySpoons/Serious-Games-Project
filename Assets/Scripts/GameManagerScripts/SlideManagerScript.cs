using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class SlideManagerScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI slideTitle, slideContents;

    [SerializeField]
    private Button nextButton, previousButton, finishButton;

    private int slideIndex = 0;

	void OnEnable ()
	{
        ObjectiveManagerScript.onObjectiveChanged += OnObjectiveChanged;
	}

    void OnDisable ()
    {
        ObjectiveManagerScript.onObjectiveChanged -= OnObjectiveChanged;
    }

    public void OnObjectiveChanged( ObjectiveData data )
    {
        slideIndex = 0;

        slideTitle.text = data.Slides[slideIndex].Title;
        slideContents.text = data.Slides[ slideIndex].Contents;

        previousButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
        finishButton.gameObject.SetActive(false);
    }

	public void LoadNextSlide ()
	{
		LoadToTextboxes( ObjectiveManagerScript.CurrentObjective.Slides[ ++slideIndex ] );

        if ( !previousButton.IsActive() )
			previousButton.gameObject.SetActive( true );

        if ( slideIndex == ObjectiveManagerScript.CurrentObjective.Slides.Count - 1 )
        {
            nextButton.gameObject.SetActive( false );
            finishButton.gameObject.SetActive( true );
        }
    }

	public void LoadPreviousSlide()
	{
		LoadToTextboxes( ObjectiveManagerScript.CurrentObjective.Slides[ --slideIndex ] );

        if ( !nextButton.IsActive() )
            nextButton.gameObject.SetActive( true );

        if ( finishButton.IsActive() )
            finishButton.gameObject.SetActive( false );

        if ( slideIndex == 0 )
            previousButton.gameObject.SetActive( false );
    }

	private void LoadToTextboxes( SlideData slide )
	{
        slideTitle.text = slide.Title;
        slideContents.text = slide.Contents;
    }
}
