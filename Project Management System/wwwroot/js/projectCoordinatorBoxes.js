$(document).ready(function () {
    var currentExpandedBox = null; // Track the currently expanded box
    console.log("Loaded");

    $('.infoBox').click(function (event) {
        console.log("Clicked");
        event.stopPropagation(); // Prevent click from bubbling to document
        var $infoBox = $(this).closest('.infoBox');
        var wasActive = $infoBox.hasClass('active');

        // Collapse any currently expanded infoBox if it's not the one clicked
        if (currentExpandedBox && currentExpandedBox[0] !== $infoBox[0]) {
            collapseBox(currentExpandedBox);
        }

        // Toggle the clicked infoBox
        if (!wasActive) {
            expandBox($infoBox);
            currentExpandedBox = $infoBox;
        } else {
            collapseBox($infoBox);
            currentExpandedBox = null;
        }
    });

    $('.editInfo').click(function (event) {
        event.stopPropagation(); // Prevents the click event from bubbling up to the parent infoBox
    });

    $(document).click(function () {
        if (currentExpandedBox) {
            collapseBox(currentExpandedBox);
            currentExpandedBox = null;
        }
    });

    // Function to expand the infoBox
    function expandBox($box) {
        $box.addClass('active').css({
            'transform': 'scale(2)',
            'z-index': '998'
        }).find('.content').slideDown(200).css('opacity', '1');

        $box.find('.infobox--icon').css({
            'opacity': '0',
            'display': 'none'
        });
        $box.find('.infobox--title').css({
            'margin-top': '-180px'
        });
    }

    // Function to collapse the infoBox
    function collapseBox($box) {
        $box.removeClass('active').css({
            'transform': 'scale(1)',
            'z-index': '1'
        }).find('.content').slideUp(200).css('opacity', '0');

        $box.find('.infobox--icon').css({
            'opacity': '1',
            'display': 'block'
        });
        $box.find('.infobox--title').css({
            'margin-top': '0'
        });
    }

    // Toggle individual items within an infoBox
    $('.item-header').click(function () {
        var $itemContent = $(this).next('.item-content');
        var $icon = $(this).find('.toggle-icon');

        if ($itemContent.is(':visible')) {
            $itemContent.slideUp(300);
            $icon.text('+').removeClass('expand');
        } else {
            $itemContent.slideDown(300);
            $icon.text('-').addClass('expand');
        }
    });
});
