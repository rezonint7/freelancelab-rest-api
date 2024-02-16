
function RedirectToView(redirect, type) {
    var token = localStorage.getItem('user');
    console.log(token);
    if (token != null) {
        $.ajax({
            url: redirect,
            type: type,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token);
            },
            success: function (response) {
                window.location.href = redirect;
            },
            error: function (xhr) {
                // Обработка ошибки
            }
        });
    }
}
