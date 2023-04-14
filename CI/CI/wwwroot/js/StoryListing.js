function storyfilter(pg)
{
    $.ajax({
        url: "/Employee/StoryListing/StoryListing",
        type: "POST",
        data: { 'pg': pg },
        success: function (res) {
            $('#storycards').html($(res).find('#storycards').html());
        },
        error: function () {
            alert("some Error");
        }
    });
}