
window.onload=function()
{
    displayUserInfo();
}
function displayUserInfo()
{
    var user=localStorage.getItem('data');
    if(user)
    {
        var userData=JSON.parse(user)
        document.getElementById("profile").innerHTML=
        '<h2>Profile Details</h2>'+
        `<p>Név: ${userData.name} </p>`+ 
        `<p>Neptun kód: ${userData.username} </p>`+
        `<p>Szak: ${userData.degree}</p>`
    } 
}
 async function logout()
 {
    localStorage.clear();
    window.location.replace("C:\Users\psely\Documents\GitHub\Moodle_18\Moodle_Client\index.html\index.html");
 }

function createList(data) {
    var list = "<ul>";
    data.forEach(item => {
        list += `<li>${item.name}</li>`;
    });
    list += "</ul>";
    return list;
}

async function showCourses() {
    var div = document.getElementById("courses");

    try {
        const data = await getData("course");
        console.log(data);
        div.innerHTML = createList(data);
    } catch (error) {
        console.log("Adatbekérési hiba: " + error);
        div.textContent = "Hiba történt az adatok lekérdezése során.";
    }
}

async function sortByDepartment(tanszek)
{
	var div = document.getElementById("courses");
    console.log("course/dep/"+tanszek);

    try {
        const data = await getData("course/dep/"+tanszek);
        console.log(data);
        div.innerHTML = createList(data);
    } catch (error) {
        console.log("Adatbekérési hiba: " + error);
        div.textContent = "Hiba történt az adatok lekérdezése során.";
    }
}

















