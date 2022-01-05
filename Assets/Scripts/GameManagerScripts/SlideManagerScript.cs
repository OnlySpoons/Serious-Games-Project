using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class SlideManagerScript : MonoBehaviour
{
    [SerializeField]
    private List<SlideData> slides;

    [SerializeField]
    private Canvas slideCanvas;

    [SerializeField]
    private TextMeshProUGUI slideTitle, slideContents;

    [SerializeField]
    private Button nextButton, previousButton;

    private int slideIndex = 0;

	void Start ()
	{
        slideTitle.text = slides[ slideIndex ].Title;
        slideContents.text = slides[ slideIndex ].Contents;

        previousButton.gameObject.SetActive( false );
    }

	public void LoadNextSlide ()
	{
		LoadToTextboxes( slides[ ++slideIndex ] );

        if ( !previousButton.IsActive() )
			previousButton.gameObject.SetActive( true );

        if ( slideIndex == slides.Count - 1 )
            nextButton.gameObject.SetActive( false );
    }

	public void LoadPreviousSlide ()
	{
		LoadToTextboxes( slides[ --slideIndex ] );

        if ( !nextButton.IsActive() )
            nextButton.gameObject.SetActive( true );

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
