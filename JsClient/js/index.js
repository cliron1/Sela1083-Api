// in JS Promise = Task in C#
fetch('https://localhost:7291/users')
    .then(res => res.json())
    .then(data => alert(data[0].fullName))
    .catch(err => console.log(err));
