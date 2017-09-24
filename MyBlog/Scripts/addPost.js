var myData = new FormData();
var dataLeight = 0;

$('#upload').click(function (e) {
	console.log("Upload");
	e.preventDefault();
	var fileInputElement = document.getElementById('uploadFile');
	var uploadedView = document.getElementById('uploaded');
	var files = fileInputElement.files;
	if (files.length > 0) {
		if (window.FormData !== undefined) {

			for (var x = 0; x < files.length; x++) {
				if (String(files[x].type).indexOf("image") >= 0) {
					dataLeight++;
					myData.append("file" + dataLeight, files[x]);
					var htmlString = "<div><p>" + files[x].name + '<a href="#" class="deletePicture" value="file' + dataLeight + '">X</a></p></div>';
					uploadedView.innerHTML += htmlString;
				}
			}
		}
	}
	fileInputElement.value = "";
	addListenersForDelele();
});

function addListenersForDelele() {

	var pictures = document.getElementsByClassName("deletePicture");
	for (var i = 0; i < pictures.length; i++) {
		pictures[i].addEventListener("click", function (e) { delelePicture(e); });
	}
}

function delelePicture(e) {
	myData.delete(e.target.getAttribute("value"));
	e.target.parentElement.remove();
}

$('#submit').click(function (e) {
	console.log(document.getElementById("postName").value);
	console.log(document.getElementById("postContent").value);
	myData.append("postName", document.getElementById("postName").value);
	myData.append("postContent", document.getElementById("postContent").value);
	$.ajax({
		type: "POST",
		url: '/Home/AddPostAjax',
		contentType: false,
		processData: false,
		data: myData,
		success: function () {
			location.assign('/Home/Index');
		},
		error: function (xhr, status, p3) {
			myData.delete("postName");
			myData.delete("postContent");
			alert(xhr.responseText);
		}
	});
});