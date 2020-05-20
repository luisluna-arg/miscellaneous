/* How to use 
 * Create a new instance passing a file input id as parameter, and an optional callback
 * 
 * var wrapper = FileReaderWrapper("input-id");
 *
 * Each time the user uploads a file, the file and its content will be stored in two local variables. 
 * Also, if a callback is provided, it will be executed
 *
 * This class also provides getters for the file and its content in a couple of formats
 */

var FileReaderWrapper = function (uploaderID, onLoad_Callback) {
	/* set up the FileReader and the variable that will hold the file's content */
	var reader = new FileReader();
	var _file = null;
	var _fileContent = "";

	/* when the file is passed to the FileReader, store its content in a local variable */
	reader.onload = function (e) {
		_file = document.getElementById(schedulingFile).files[0];
		_fileContent = reader.result;
		if (onLoad_Callback && typeof onLoad_Callback == "function"){
			onLoad_Callback();
		}
	};

	/* Read the content of the file each time that the user selects one 
	 *  This works for one file at a time
	 */
	var schedulingFile = uploaderID;
	document.
		getElementById(schedulingFile).
		addEventListener("change", function (e) {
			if (document.getElementById(schedulingFile).files.length === 0) {
				/* If there is no selected file abort */
				reader.abort();
				return;
			}
			
			reader.readAsText(document.getElementById(schedulingFile).files[0]);
		});

	this.getFileContent = function () { return _fileContent; };

	this.getFile = function () { return _file; };

	this.getFileAsBase64 = function () { return window.btoa(unescape(encodeURIComponent(this.getFileContent()))) };

	this.isFileLoaded = function () { return _fileContent != undefined && _fileContent != null && _fileContent.length > 0; }
};
