$(document).ready(function () {
    $('.body--register--inputToggle').click(function () {
        toggleForms();
    });

    function toggleForms() {
        var isLoginFormVisible = $('.loginForm').is(':visible');
        if (isLoginFormVisible) {
            // Hide login form and show registration form
            $('.loginForm').slideUp(600, function () {
                $('.registerForm').slideDown(800);
                $('.body--register--staffToggler').show(); // Show the toggler for staff/student
                updateToggleTextAndHeight(true);
            });
        } else {
            // Hide registration form and show login form
            $('.registerForm').slideUp(800, function () {
                $('.loginForm').slideDown(600);
                $('.body--register--staffToggler').hide(); // Hide the toggler
                updateToggleTextAndHeight(false);
            });
        }
    }

    function updateToggleTextAndHeight(isRegisterVisible) {
        if (isRegisterVisible) {
            $('.body--login--container').css('min-height', '900px');
            $('.body--login--newUser').text('Already have an account? Click here to login:');
            $('.body--register--inputToggle').text('Login');
        } else {
            $('.body--login--container').css('min-height', '650px');
            $('.body--login--newUser').text('New here? Register below:');
            $('.body--register--inputToggle').text('Register');
        }
    }

    // Button events for toggling between student and staff registration forms
    $('.studentBtn').click(function () {
        $('.registerFormStaff').hide();
        $('.registerForm').show();
    });

    $('.staffBtn').click(function () {
        $('.registerForm').hide();
        $('.registerFormStaff').show();
    });
    $('#newDepartment').prop('disabled', true);

    // Listen for changes on the school select
    $('#newSchool').change(function () {
        var schoolValue = $(this).val();
        if (schoolValue !== 'null') {
            $('#newDepartment').prop('disabled', false);
        } else {
            $('#newDepartment').prop('disabled', true);
            $('#newDepartment').val('null');
        }
    });
});