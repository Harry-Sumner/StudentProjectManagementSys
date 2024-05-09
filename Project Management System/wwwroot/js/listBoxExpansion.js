$(document).ready(function () {
    console.log("Page loaded and script running"); // Check if the script runs at all
    $('.expandTopic').click(function (e) {
        e.preventDefault();
        console.log("Button clicked");  // Check if the event is captured
        $(this).closest('.topicBox').toggleClass('expanded');

        console.log("Toggle class applied: ", $(this).closest('.topicBox').hasClass('expanded'));  // Debugging

        if ($(this).closest('.topicBox').hasClass('expanded')) {
            $(this).attr('title', 'Collapse this topic for fewer details');
        } else {
            $(this).attr('title', 'Expand this topic for more details');
        }
    });
});
