$(document).ready(function () {
    $('.body--register--inputToggle').click(function () {
        if ($('.loginForm').is(':visible')) {
            // Animate the login form height to 0
            $('.loginForm').animate({ height: 'toggle' }, 600, function () {
                // Once animation is complete, show the registration form
                $('.registerForm').animate({ height: 'toggle' }, 800);
                $('.body--login--container').css('min-height', '800px');
                $('.body--login--newUser').text('Already have an account? Click here to login: ');
                $('.body--register--inputToggle').text('Login');
            });
        } else {
            // Animate the registration form height to 0
            $('.registerForm').animate({ height: 'toggle' }, 800, function () {
                // Once animation is complete, show the login form
                $('.loginForm').animate({ height: 'toggle' }, 600);
                $('.body--login--container').css('min-height', '650px');
                $('.body--login--newUser').text('New here? Register below: ');
                $('.body--register--inputToggle').text('Register');
            });
        }
    });
});