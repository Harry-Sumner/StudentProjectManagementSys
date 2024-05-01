$(document).ready(function () {
    // Assuming '#toggle' is the ID of the button that will be clicked to open and close the sidebar
    const sidebar = $('#sidebar');
    const toggle = $('#toggle'); // Single button for toggling the sidebar
    const divider = $('.header--divider--desktop');
    const uocLogo = $('.header--uocLogo--standard')

    toggle.click(function () {
        // Check if sidebar is open by checking its left style property
        if (sidebar.css('left') === '0px') {
            // If it's open, close it
            sidebar.css('left', '-306px');
            toggle.css('left', '10px');
            divider.css('left', '90px');
            uocLogo.css('left', '110px');
        } else {
            // If it's closed, open it
            sidebar.css('left', '0');
            toggle.css('left', '316px');
            divider.css('left', '406px');
            uocLogo.css('left', '426px');
        }
        // Toggle the button's appearance as well
        toggle.toggleClass('button-open button-close');
    });

    $('.header--accountBtn').click(function () {
        var $dropdownMenu = $('.dropdown-menu');
        if ($dropdownMenu.is(':visible')) {
            $dropdownMenu.slideUp(); // Animates the hiding of the dropdown
        } else {
            $dropdownMenu.slideDown(); // Animates the showing of the dropdown
        }
    });
    document.getElementById('logout').addEventListener('click', function (event) {
        event.preventDefault(); // Prevent the default action of following the link
        document.getElementById('logoutForm').submit(); // Submit the form
        // Show a popup notification
        alert("You have been logged out.");
    });

});