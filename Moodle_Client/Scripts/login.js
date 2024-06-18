window.onload=function()
{
	localStorage.clear();
}
async function login()
{
	
	var uName=document.getElementById('username').value;
	var pw=document.getElementById('pw').value
	if(isEmpty(uName) || isEmpty(pw)) alert('Felhasználónév és jelszó megadása kötelező');
	else
	{
		var data=
		{
			username: uName,
			password: pw
		};
		try {
            const response = await postData("Auth/login", data, false);
            if (response.username!="fail") {
                localStorage.setItem("data", JSON.stringify(response));
                console.log(response);
                window.location.href = "index.html";
            } else {
                alert(response.message || 'Login failed');
            }
        } catch (error) {
            console.error('Error during login:', error);
            alert('An error occurred during login. Please try again.');
        }
	}
}

function isEmpty(str)
{
	return (!str||0===str.length);
}