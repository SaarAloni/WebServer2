async function getAll() {
    const r = await fetch('/api/contacts');
    const d = await r.json();
    console.log(d);
}

async function get() {
    const r = await fetch('/api/Login');
    const d = await r.json();
    console.log(d);
}

var token = '';
function login() {
    $.ajax({
        url: 'https://localhost:5254/api/Users?Id=e&password=e',
        type: 'POST',
        contentType: "application/json",
        success: function (data) { token = data },
        error: function () { },
    });
}
function getInfo() {
    $.ajax({
        url: 'https://localhost:5254/api/contacts',
        type: 'GET',
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', 'bearer' + token);
        },
        data: {},
                success: function (data) { console.log(data); },
        error: function () { },
    });
}


async function post() {
    const r = await fetch('/api/contacts', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({Id : '1', Name : 'saar', Server : 'a', Image : 'd', Password : '123'})
    });
    console.log(r);
}

async function put() {
    const r = await fetch('/api/contacts/1', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ Id: '1', Name: 'saar', Server: 'a', Image: 'e', Password: '456' })
    });
    console.log(r);
}

async function apiLogin() {
    const r = await fetch('/api/Login/e');
    const d = await r.json();
    console.log(d);
}
/*
async function apiInvite() {
    const r = await fetch('/api/invitations', {
    method: 'POST',
        headers: {
        'Content-Type': 'application/json'
    },
        body: JSON.stringify({ senderId: '1', reciverId: 'e'})
});
console.log(r);
}
*/
async function apiInvite() {
    const r = await fetch('/api/invitations', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ sender: '1', receiver: 'e' })
    });
    console.log(r);
}



async function del() {
    const r = await fetch('/api/contacts/1', {
        method: 'DELETE',
    });
    console.log(r);
}