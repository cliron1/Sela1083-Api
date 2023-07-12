/*
async function run() {
    const res = await fetch('https://localhost:7291/users');
    const data = await res.json();
    alert(data[0].fullName);
}
run();
*/

try {
    const res = await fetch('https://localhost:7291/users');
    const data = await res.json();
    alert(data[0].fullName);
} catch (err) {
    alert('Error: ' + err.message);
}