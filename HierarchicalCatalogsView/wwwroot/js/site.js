var fileInput = document.getElementById('fileInput');
var submitButton = document.getElementById('submit');
var inputFileName = document.getElementById('inputFileName');

fileInput.addEventListener('change', function () {
    if (fileInput.files.length > 0) {
        submitButton.classList.remove('hidden');
        console.log(fileInput.files[0].name);
        inputFileName.classList.remove('hidden');
        inputFileName.textContent = "File to import: " + fileInput.files[0].name;
        
    } else {
        submitButton.classList.add('hidden');
        inputFileName.classList.add('hidden');
    }
});