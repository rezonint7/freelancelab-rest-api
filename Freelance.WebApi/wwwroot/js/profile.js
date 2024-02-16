function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}

var user = parseJwt(localStorage.getItem('user'));
console.log(user);
var roleUser = user["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
var userId = user["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];

if (roleUser == "Implementer") {
    var settings = {
        "url": "https://localhost:7019/api/profile/implementer/" + userId,
        "method": "GET",
        "timeout": 0,
        "headers": {
            "Authorization": "Bearer " + localStorage.getItem('user')
        },
    };

    $.ajax(settings).done(function (response) {
        console.log(response);
        var skillsHtml = response.skills.map(function (skill) {
            return `<div class="skill-item border rounded p-1 mt-2 mb-3 mr-1">${skill}</div>`;
        }).join('');
        var card = `
                                <div class="card-body">
                <h4 class="card-title">
                            <strong id="lastName">${response.userInfo.lastName}</strong>
                            <strong id="firstName">${response.userInfo.firstName}</strong>
                            <strong id="midleName">${response.userInfo.middleName}</strong>
                </h4>
                        <p class="card-text">${response.specialization}</p>

                    <p class="card-text"><strong>Опыт работы: </strong>${response.workExperience.name}</p>
                        <p class="card-text">${response.about}</p>

                <div class="skills">
                    <p><strong>Ключевые навыки:</strong></p>
                    <div class="list-container">
                        <div class="d-flex flex-wrap">
                            ${skillsHtml}
                        </div>
                    </div>
                </div>
            </div>
        `
        var portfolioList = response.portfolio.map(function (portfolioItem) {
            return `
                         <div class="card">
            <div class="card-body">
                <h5 class="card-title">${portfolioItem.Title}</h5>
                    <p class="card-text">${portfolioItem.Description}</p>
            </div>
        </div>
                `
        }).join('');

        $('#main-info-card').append(card);
        $('user-info-portfolio').append(portfolioList);
    });
}
else if (roleUser == "Customer") {

}