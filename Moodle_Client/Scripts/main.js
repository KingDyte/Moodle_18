var user=localStorage.getItem('data');
var userData=JSON.parse(user);

window.onload=function()
{
    if(user===null)
        {
            logout();
        }

    displayUserInfo();
    showCourses();
}
function displayUserInfo()
{
    
    if(user)
    {
        document.getElementById("profileData").innerHTML=
        `</br>${userData.name} </br>`+ 
        `Neptun kód: ${userData.username} </br>`+
        `Szak: ${userData.degree}</br>`;
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
        list += `<li style="cursor: pointer;" onclick="showPopup('${item.Name}')">${item.Name}</li>`;
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

function showPopup(course) {
    const popup = document.getElementById('popup');
    const popupText = document.getElementById('popup-text');
    popupText.textContent = `Kurzus hallgatói: ${course}`;
    popup.style.display = 'block';
}

function closePopup() {
    const popup = document.getElementById('popup');
    popup.style.display = 'none';
}

async function sortByDepartment()
{
    var tanszek=document.getElementById("filter").value;
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

















