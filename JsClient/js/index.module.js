async function refreshList() {
    try {
        const res = await fetch('https://localhost:7291/users');
        const data = await res.json();
        //alert(data[0].fullName);

        let trs = '';
        for (const user of data) {
            trs += `<tr>
        <td>${user.fullName}</td>
        <td>${user.email}</td>
        </tr>`;
            document.querySelector('#tbody')
                .innerHTML = trs;
        }

    } catch (err) {
        alert('Error: ' + err.message);
    }
}
refreshList();

const debug = true;
if (debug) {
    document.querySelector('#name').value = 'Mirit';
    document.querySelector('#email').value = 'mirit@gmail.com';
}

const btn = document.querySelector('#btnAdd');
btn.addEventListener('click', async function () {
    const name = document.querySelector('#name');
    const email = document.querySelector('#email');

    // validation comes here..

    const payload = {
        fullName: name.value,
        email: email.value
    };
    const req = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json' // MIME
        },
        body: JSON.stringify(payload)
    };
    const res = await fetch('https://localhost:7291/users', req);
    result = await res.json();
    await refreshList();
});
