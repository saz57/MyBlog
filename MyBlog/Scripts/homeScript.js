function toggleVisible(tag)
{
    $(this).parent().children(tag).toggle();
}

toggleVisible.call($('.toggle-comments'),'.comments');
$('.toggle-comments').click(function (e) {
	e.preventDefault();
	toggleVisible.call($(this), '.comments');

	if (e.target.textContent == "Show comments")
	{
		e.target.textContent = "Hide comments";
		return;
	}

	if (e.target.textContent == "Hide comments") {
		e.target.textContent = "Show comments";
		return;
	}
});

