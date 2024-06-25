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
    Events();
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

function createList(data) { //popup
    var list = "<ul style='list-style-type: square;'>";
    data.forEach(item => {
        list += `<li style="cursor: pointer;" onclick="showPopup('`+ item.Code+`','`+ item.Name+`')">${item.Name}</li>`;
    });
    list += "</ul>";
    return list;
}
function createListStudents(data) {
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

function showPopup(course, name) {
    closePopupEvent();
    const popup = document.getElementById('popup');
    const popupText = document.getElementById('popup-text');
    console.log(course);
    console.log(name);
    popupText.innerHTML = '<div id="students"><h5 id="courseCode">'+name+'</h5><h7>'+course+'</h7><hr></div>'+"<br>";
    popupText.innerHTML = "<button id='attendCourse' style='border-radius: 12px' onclick='attendCourse("+"`"+course+"`"+")'>Kurzus felvétele</button>";
    popup.style.display = 'block';
    //console.log(course);
    StudentsOnCourse(course);
}

function closePopup() {
    const popup = document.getElementById('popup');
    popup.style.display = 'none';
}

async function StudentsOnCourse(course)
{
    const data =  await getData("user/"+course+"/enrolled");
    console.log(data);
    document.getElementById("students").innerHTML+=createListStudents(data);
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

async function attendCourse(courseCode)
{
    var a=await putData("user/enroll/"+courseCode,userData.username,false);
    alert("Sikeresen feliratkozva");
    
    console.log(a);
    closePopup();
    showPopup(courseCode);
}


//Event stuff
var events;

function eventList(data)
{
    var list = "<ul>";
    data.forEach(item => {
        const d=new Date(item.Date).toLocaleDateString('en-US', {
            day: '2-digit',
            month: '2-digit',
            year: 'numeric',
          })
        list += `<li style="cursor: pointer;" onclick="showPopupEvent('${item.Date}')">${d} - ${item.Course}</li>`;
    });
    list += "</ul>";
    return list;
}

async function Events()
{
    const data= await getData("user/"+userData.username+"/events");
    console.log(data);
    events=data;
    document.getElementById("events").innerHTML=eventList(data);
}

function showPopupEvent(eventDate) {
     closePopup();
    const popup = document.getElementById('popup-event');
    const popupText = document.getElementById('popup-text-event');
    var a;
    events.forEach(item =>{
        if(item.Date==eventDate)
        {
            a=item;
        }
    });
    const d=new Date(a.Date).toLocaleDateString('en-US', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric',
      });

    popupText.innerHTML = '<div id="eventsPopUp"><h5 >'+a.Course+`</h5><hr> <p>Időpont : ${d}<br>Megnevezés: ${a.Name}<br>Leírás: ${a.Description}</p></div>`;
    popup.style.display = 'block';
}

function closePopupEvent() {
    const popup = document.getElementById('popup-event');
    popup.style.display = 'none';
}

















