$(document).ready(function () {
    var draggedItem = null;
    $('.TopicsBtn').css('border-bottom', '2px solid red');
    $('.formUploadContainer').css('display', 'none');

    $('.TopicsBtn').click(function (e) {
        e.preventDefault();
        $('.formUploadContainer').slideUp(400, function () {
            $('.topicProposalContainer').slideDown(400);
        });
        $(this).css('border-bottom', '2px solid red');
        $('.FormUploadBtn').css('border', 'none');
    });

    $('.FormUploadBtn').click(function (e) {
        e.preventDefault();
        $('.topicProposalContainer').slideUp(400, function () {
            $('.formUploadContainer').slideDown(400);
        });
        $(this).css('border-bottom', '2px solid red');
        $('.TopicsBtn').css('border', 'none');
    });

    $('.incorrectDetails').click(function () {
        $('.additionalInfo').slideToggle();
    });

    $('.proposalBox').on('dragstart', function (e) {
        draggedItem = this;
        setTimeout(() => $(this).hide(), 0);
    });

    $('.proposalBox').on('dragend', function (e) {
        setTimeout(() => {
            $(this).show();
            draggedItem = null;
        }, 0);
    });

    $('.proposalBox').on('dragover', function (e) {
        e.preventDefault();  // Allow dropping by preventing default behavior
    });

    $('.proposalBox').on('dragenter', function (e) {
        e.preventDefault();  // Necessary for the drop event to fire correctly
        $(this).addClass('over');
    });

    $('.proposalBox').on('dragleave', function (e) {
        $(this).removeClass('over');
    });

    $('.proposalBox').on('drop', function (e) {
        $(this).removeClass('over');
        if (!draggedItem) return;

        // Determine whether to place the dragged item before or after
        const targetRect = this.getBoundingClientRect();
        const targetMidpoint = targetRect.top + targetRect.height / 2;
        const afterTarget = e.originalEvent.clientY > targetMidpoint;

        if (afterTarget && this.nextSibling) {
            $(draggedItem).insertAfter($(this));
        } else {
            $(draggedItem).insertBefore($(this));
        }
    });
});