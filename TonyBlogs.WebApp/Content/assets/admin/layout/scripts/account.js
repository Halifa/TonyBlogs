var Account = function () {

    var login = function () {
        $('#errorDiv').css('display', 'none');
        Metronic.blockUI({
            animate: true
        });
        var data = $('#loginForm').serialize();
        $.post('/account/login', data, function (result) {
            Metronic.unblockUI();
            if (!result.IsSuccess) {
                $('#errorDiv').css('display', 'inline');
                $('#errorMsg').html(result.Msg);
            }
            else {
                window.location = '/home/index';
            }
        })
    }

    var keyDown = function (event) {
        if (event.keyCode == 13)
        {
            event.returnValue = false;
            event.cancel = true;
            login();
        }
    }

    return {
        //main
        init: function () {
            $('#btnLogin').on('click', login);
            $('#password').on('keydown', keyDown);
        }
    };
}