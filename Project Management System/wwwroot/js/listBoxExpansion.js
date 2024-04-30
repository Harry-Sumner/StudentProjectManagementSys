$(document).ready(function () {
    $('.listExpandButton').click(function () {
        $(this).closest('.listBox').toggleClass('expanded'); // Toggles the 'expanded' class on the parent .listBox      

        // Optional: Change the button text or appearance based on the state
        if ($(this).closest('.listBox').hasClass('expanded')) {
            $(this).attr('title', 'Collapse this topic for fewer details');
        } else {
            $(this).attr('title', 'Expand this topic for more details');
        }
    });
});