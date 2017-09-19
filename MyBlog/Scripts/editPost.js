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
				dataLeight++;
				myData.append("file" + dataLeight, files[x]);
				var htmlString = "<div><p>" + files[x].name + '<a href="#" class="deletePicture" value="file' + dataLeight + '">X</a></p></div>'
				uploadedView.innerHTML += htmlString;
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

$('.imageToDelete').click(function (e) {
	e.preventDefault();
	console.log("deleteImage");
	var reff = e.currentTarget;
	var image = document.getElementById(reff.getAttribute("value"));

	if (image.getAttribute("value") == "false") {
		reff.textContent = "don't delete"
		image.setAttribute("value", "true")
		return;
	}

	if (image.getAttribute("value") == "true") {
		reff.textContent = "delete"
		image.setAttribute("value", "false");
		return;
	}
})

$('#submit').click(function (e) {

	console.log("submit")
	var imagesToDelete = [];
	var images = document.getElementById("images").getElementsByTagName("img");
	console.log(images);

	for (var i = 0; i < images.length; i++) {
		console.log(images[i].getAttribute("value"));

		if (images[i].getAttribute("value") == "true") {
			imagesToDelete.push(images[i].getAttribute("id"));
		}
	}

	if (imagesToDelete.length > 0) {

		console.log(typeof (imageToDelete));
		myData.append("imagesToDelete", JSON.stringify(imagesToDelete));
	}
	myData.append("postId", document.getElementById("postId").value)
	myData.append("postName", document.getElementById("postName").value);
	myData.append("postContent", document.getElementById("postContent").value);
	$.ajax({
		type: "POST",
		url: '/Home/EditPostAjax',
		contentType: false,
		processData: false,
		data: myData,
		success: function () {
			location.assign('/Home/Index');
		}
	});
});