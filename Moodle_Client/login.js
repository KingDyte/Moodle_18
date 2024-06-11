async function login()
{
	var Uname=document.getElementById("username").value;
	var pw   =document.getElementById("pw").value;

	if(isEmpty(Uname) || isEmpty(pw)) alert('Felhasználónév és jelszó megadása kötelező');
	else
	{
		var data=
		{
			username: Uname,
			password: pw
		};

		/*await postData("Auth/login",data,false).then(async(data)=>
		{
			if(await data.token)
			{
				localStorage.setItem("data",JSON.stringify(data));
				console.log(data);
				window.location.href="main.html";
			}
			else console.log(data.token);
		});*/
		try {
            const response = await postData("Auth/login", data, false);
            if (response.username!="fail") {
                localStorage.setItem("data", JSON.stringify(response));
                console.log(response);
                window.location.href = "main.html";
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