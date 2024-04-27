function goToLoginPage() {
	document.getElementById("logoutButton").addEventListener("click", function() {window.location.href = "index.html";});
		
}

async function showCourses() {
    var textArea = document.getElementById("courses");

    try{        
        const data = await getData("course");
        console.log(data);
        textArea.textContent = JSON.stringify(data, null, 2);
    }
    catch(error){
        console.log("Adatbekérési hiba: " + error);
    }
}


























