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

		await postData("Auth/login",data,false).then(async(data)=>
		{
			if(await data.token)
			{
				localStorage.setItem("data",JSON.stringify(data));
				console.log(data);
				window.location.href="main.html";
			}
			else alert(await data.Message);
		});
	}
}

function isEmpty(str)
{
	return (!str||0===str.length);
}