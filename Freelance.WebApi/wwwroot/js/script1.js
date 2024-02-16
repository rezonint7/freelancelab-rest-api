function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}

var user = parseJwt(localStorage.getItem('user'))

if (user["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] == "Implementer") {
    var settings = {
        "url": "https://localhost:7019/api/profile/implementer/e4542e5e-4276-4261-b78d-ec734a6d6ea8",
        "method": "GET",
        "timeout": 0,
        "headers": {
            "Authorization": "Bearer " + localStorage.getItem('user')
        },
    };

    $.ajax(settings).done(function (response) {
        var card = `
                    <div class="card-body">
                <h4 class="card-title">
                    <strong id="firstName">${response}</strong>
                    <strong id="lastName">Doe</strong>
                    <strong id="midleName">Bebrovich</strong>
                </h4>
                <p class="card-text">C# разработчик</p>

                <p class="card-text"><strong>Опыт работы: </strong>менее года</p>
                <p class="card-text">Начинающий C# разработчик. Изучаю C# уже 2 года. Имеется небольшой опыт собственных проектов (сейчас разрабатываю собственное API на ASP NET 6). Также имеются знания основ программирования на JavaScript, Python, а также HTML и CSS </p>

                <div class="skills">
                    <p><strong>Ключевые навыки:</strong></p>
                    <div class="list-container">
                        <div class="d-flex flex-wrap">
                            <div class="skill-item border rounded p-1 mt-2 mb-3 mr-1">ООП (Object-Oriented Programming)</div>
                            <div class="skill-item border rounded p-1 mt-2 mb-3 mr-1">C#</div>
                            <div class="skill-item border rounded p-1 mt-2 mb-3 mr-1">ASP.NET</div>
                            <div class="skill-item border rounded p-1 mt-2 mb-3 mr-1">JavaScript</div>
                            <div class="skill-item border rounded p-1 mt-2 mb-3 mr-1">Python</div>
                            <div class="skill-item border rounded p-1 mt-2 mb-3 mr-1">HTML</div>
                            <div class="skill-item border rounded p-1 mt-2 mb-3 mr-1">CSS</div>
                        </div>
                    </div>
                </div>
            </div>
            `
    });
}
else if (user["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] == "Customer") {

}