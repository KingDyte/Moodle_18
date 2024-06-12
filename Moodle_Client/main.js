var user=localStorage.getItem('data');
var userData=JSON.parse(user);

window.onload=function()
{
    displayUserInfo();
    showCourses();
}
function displayUserInfo()
{
    
    if(user)
    {
        document.getElementById("profileData").innerHTML=
        '<h2>Profile Details</h2>'+
        `<p>Név: ${userData.name} </p>`+ 
        `<p>Neptun kód: ${userData.username} </p>`+
        `<p>Szak: ${userData.degree}</p>`;
    } 
}
function logout()
 {
    try
    {
        localStorage.clear();
        window.location.replace("login.html");
    }
    catch(error)
    {
        console.error("logout error: ",error);
    }
 }

function createList(data) {
    var list = "<ul>";
    data.forEach(item => {
        list += `<li>${item.Name}</li>`;
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

async function myCourses()
{
    var div = document.getElementById("courses");

    try {
        const data=await getData("user/"+userData.username+"/courses");
        //var courses=JSON.parse(data)
        console.log(data);
        div.innerHTML = "My Courses: "+createList(data);
    } catch (error) {
        console.log("Adatbekérési hiba: " + error);
        div.textContent = "Hiba történt az adatok lekérdezése során.";
    }

}

















