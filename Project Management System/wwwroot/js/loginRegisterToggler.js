$(document).ready(function () {
    $('.studentBtn').css('border-bottom', '2px solid red');

    $('.body--register--inputToggle').click(function () {
        // Toggle between login and register forms based on current visibility
        if ($('.loginForm').is(':visible')) {
            toggleToRegister();
        } else {
            toggleToLogin();
        }
    });

    $('.studentBtn').click(function () {
        // Toggle to student registration form
        $('.registerFormStaff').slideUp(400);
        $('.registerForm').slideDown(400);
        $(this).css('border-bottom', '2px solid red');
        $('.staffBtn').css('border', 'none');
    });

    $('.staffBtn').click(function () {
        // Toggle to staff registration form
        $('.registerForm').slideUp(400);
        $('.registerFormStaff').slideDown(400);
        $(this).css('border-bottom', '2px solid red');
        $('.studentBtn').css('border', 'none');
    });

    function toggleToRegister() {
        $('.loginForm').slideUp(600, function () {
            $('.registerForm').slideDown(800);
            $('.body--register--staffToggler').show();
        });
        updateToggleText(true);
    }

    function toggleToLogin() {
        $('.registerForm').slideUp(600);
        $('.registerFormStaff').slideUp(600, function () {
            $('.loginForm').slideDown(600);
            $('.body--register--staffToggler').hide();
        });
        updateToggleText(false);
    }

    function updateToggleText(isRegistering) {
        if (isRegistering) {
            $('.body--login--newUser').text('Already have an account? Click here to login:');
            $('.body--login--container').css('min-height', '900px');
            $('.body--register--inputToggle').text('Login');
        } else {
            $('.body--login--newUser').text('New here? Register below:');
            $('.body--login--container').css('min-height', '650px');
            $('.body--register--inputToggle').text('Register');
        }
    }

    // Select dependency logic
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