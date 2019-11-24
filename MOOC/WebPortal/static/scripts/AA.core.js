var BASE_URL = "http://localhost:6328/";
//var BASE_URL = "http://adminapps.in/course/";
//var BASE_URL = "http://moocacademy.in/"; //for mooc
//var BASE_URL = "http://myclasstest.com/mooc/"; //for godady


// creating ChapterIndex
var Chapters = [];
var Secions = [];


var chapterId = 0;
var sectionId = 0;
var courseId = 0;

//-----------------populating Chapters and Sections accourding to the course-------------------
//function PopulateChaptersByCourse(courseId) {
function PopulateChaptersByCourse(courseId, CallFunctionAfterChaptersSectionsLoaded) {
    var result = { "Status": "Ok", "Message": "GetChaptersByCourse-Success", "Chapters": [{"Id":1,"CourseId":1,"Title":"Computer Basics","PageName":"DomyFile_0.html","Link":"","Time":0,"IsDB":false,"DateCreated":"\/Date(1383598800000)\/","Language":"En","ContentVersion":"1","CourseName":null,"MigratedChapterId":0,"OrignalName":""},{"Id":2,"CourseId":1,"Title":"Computer Hardware & Storage","PageName":"DomyFile_2.html","Link":"","Time":0,"IsDB":false,"DateCreated":"\/Date(1384722000000)\/","Language":"En","ContentVersion":"1","CourseName":null,"MigratedChapterId":0,"OrignalName":""},{"Id":3,"CourseId":1,"Title":"Operating Systems & File Management","PageName":"DomyFile_0.html","Link":"","Time":0,"IsDB":false,"DateCreated":"\/Date(1384722000000)\/","Language":"En","ContentVersion":"1","CourseName":null,"MigratedChapterId":0,"OrignalName":""},{"Id":4,"CourseId":1,"Title":"Microsoft Paint and Notepad","PageName":"DomyFile_1.html","Link":"","Time":0,"IsDB":false,"DateCreated":"\/Date(1384722000000)\/","Language":"En","ContentVersion":"1","CourseName":null,"MigratedChapterId":0,"OrignalName":""},{"Id":5,"CourseId":1,"Title":"Microsoft Word","PageName":"DomyFile_3.html","Link":"","Time":0,"IsDB":false,"DateCreated":"\/Date(1384722000000)\/","Language":"En","ContentVersion":"1","CourseName":null,"MigratedChapterId":0,"OrignalName":""},{"Id":6,"CourseId":1,"Title":"Microsoft Excel","PageName":"DomyFile_0.html","Link":"","Time":0,"IsDB":false,"DateCreated":"\/Date(1384808400000)\/","Language":"En","ContentVersion":"1","CourseName":null,"MigratedChapterId":0,"OrignalName":""},{"Id":7,"CourseId":1,"Title":"Microsoft PowerPoint","PageName":"DomyFile_2.html","Link":"","Time":0,"IsDB":false,"DateCreated":"\/Date(1384808400000)\/","Language":"En","ContentVersion":"1","CourseName":null,"MigratedChapterId":0,"OrignalName":""},{"Id":8,"CourseId":1,"Title":"Computer Networks","PageName":"DomyFile_2.html","Link":"","Time":0,"IsDB":false,"DateCreated":"\/Date(1384808400000)\/","Language":"En","ContentVersion":"1","CourseName":null,"MigratedChapterId":0,"OrignalName":""},{"Id":9,"CourseId":1,"Title":"Computer Security","PageName":"DomyFile_2.html","Link":"","Time":0,"IsDB":false,"DateCreated":"\/Date(1384808400000)\/","Language":"En","ContentVersion":"1","CourseName":null,"MigratedChapterId":0,"OrignalName":""},{"Id":10,"CourseId":1,"Title":"Social Media","PageName":"DomyFile_2.html","Link":"","Time":0,"IsDB":false,"DateCreated":"\/Date(1384808400000)\/","Language":"En","ContentVersion":"1","CourseName":null,"MigratedChapterId":0,"OrignalName":""}] };
    onSuccessPopulateChaptersByCourse(result, courseId, CallFunctionAfterChaptersSectionsLoaded);
    //var path = "Handler/GetChaptersByCourse.ashx?courseid=" + courseId;
    //CallHandler(path, function (result) { onSuccessPopulateChaptersByCourse(result, courseId, CallFunctionAfterChaptersSectionsLoaded); });
}

function onSuccessPopulateChaptersByCourse(result, courseId, CallFunctionAfterChaptersSectionsLoaded) {
    if (result.Status == "Ok") {
        //console.log(result);
        Chapters = result.Chapters;

        PopulateSecionsByCourse(courseId, CallFunctionAfterChaptersSectionsLoaded);
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            //parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            //alert(result.Message);
            console.log(result.Message);
        }
    }
}

function PopulateSecionsByCourse(courseId, CallFunctionAfterChaptersSectionsLoaded) {
    var result = { "Status": "Ok", "Message": "GetSectionsByCourse-Success", "Sections": [{"Id":1,"ChapterId":1,"Title":"Introduction","PageName":"Introduction.htm","Description":"","Link":"Course/FOC/1/Introduction.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1383578801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":2,"ChapterId":1,"Title":"Computer Structure ","PageName":"computer-structure.htm","Description":"","Link":"Course/FOC/1/computer-structure.htm","Time":3600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1383578801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":3,"ChapterId":1,"Title":"Hardware","PageName":"Hardware.htm","Description":"","Link":"Course/FOC/1/Hardware.htm","Time":2000,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1383578801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":4,"ChapterId":1,"Title":"Software","PageName":"software.htm","Description":"","Link":"Course/FOC/1/software.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1383578801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":5,"ChapterId":1,"Title":"System Software","PageName":"system-software.htm","Description":"","Link":"Course/FOC/1/system-software.htm","Time":3600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1383578801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":6,"ChapterId":1,"Title":"Application software","PageName":"application-software.htm","Description":"","Link":"Course/FOC/1/application-software.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1383578801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":7,"ChapterId":1,"Title":"Generalized Packages","PageName":"generalized-packages.htm","Description":"","Link":"Course/FOC/1/generalized-packages.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1383578801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":8,"ChapterId":1,"Title":"Customized Packages","PageName":"customized-packeges.htm","Description":"","Link":"Course/FOC/1/customized-packeges.htm","Time":3000,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1383578801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":9,"ChapterId":2,"Title":"Introduction","PageName":"introduction.htm","Description":"","Link":"Course/FOC/2/introduction.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713374000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":10,"ChapterId":2,"Title":"Input Devices","PageName":"input-devices.htm","Description":"","Link":"Course/FOC/2/input-devices.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713374000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":11,"ChapterId":2,"Title":"keyboard","PageName":"keyboard.htm","Description":"","Link":"Course/FOC/2/keyboard.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713374000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":12,"ChapterId":2,"Title":"Mouse","PageName":"mouse.htm","Description":"","Link":"Course/FOC/2/mouse.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713374000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":13,"ChapterId":2,"Title":"Optical Scanner","PageName":"opticals-scanner.htm","Description":"","Link":"Course/FOC/2/opticals-scanner.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713374000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":14,"ChapterId":2,"Title":"Touch Screen","PageName":"touch-screen.htm","Description":"","Link":"Course/FOC/2/touch-screen.htm","Time":300,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713374000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":15,"ChapterId":2,"Title":"Microphone","PageName":"microphone.htm","Description":"","Link":"Course/FOC/2/microphone.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713374000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":16,"ChapterId":2,"Title":"Web Camera","PageName":"web-camera.htm","Description":"","Link":"Course/FOC/2/web-camera.htm","Time":300,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713374000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":17,"ChapterId":2,"Title":"Output Devices","PageName":"output-devices.htm","Description":"","Link":"Course/FOC/2/output-devices.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713374000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":18,"ChapterId":2,"Title":"Monitor","PageName":"monitor.htm","Description":"","Link":"Course/FOC/2/monitor.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713374000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":19,"ChapterId":2,"Title":"Display Resolution","PageName":"display-resolution.htm","Description":"","Link":"Course/FOC/2/display-resolution.htm","Time":300,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713374000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":20,"ChapterId":2,"Title":"Printers","PageName":"printers.htm","Description":"","Link":"Course/FOC/2/printers.htm","Time":2700,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713374000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":21,"ChapterId":2,"Title":"Speaker","PageName":"speaker.htm","Description":"","Link":"Course/FOC/2/speaker.htm","Time":300,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713374000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":22,"ChapterId":2,"Title":"Projector","PageName":"projector.htm","Description":"","Link":"Course/FOC/2/projector.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713374000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":23,"ChapterId":2,"Title":"Fundamentals of CPU","PageName":"fundamentals-of-cpu.htm","Description":"","Link":"Course/FOC/2/fundamentals-of-cpu.htm","Time":300,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713375000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":24,"ChapterId":2,"Title":"Introduction to Storage","PageName":"Introduction.htm","Description":"","Link":"Course/FOC/2/Introduction-storage.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713719000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":25,"ChapterId":2,"Title":"Storage Type","PageName":"storage-type.htm","Description":"","Link":"Course/FOC/2/storage-type.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713720000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":26,"ChapterId":2,"Title":"Primary Storage","PageName":"primary-storage.htm","Description":"","Link":"Course/FOC/2/primary-storage.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713720000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":27,"ChapterId":2,"Title":"RAM Memory","PageName":"ram-memory.htm","Description":"","Link":"Course/FOC/2/ram-memory.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713720000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":28,"ChapterId":2,"Title":"ROM Memory","PageName":"rom-memory.htm","Description":"","Link":"Course/FOC/2/rom-memory.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713720000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":29,"ChapterId":2,"Title":"Secondary Storage","PageName":"secondary-storage.htm","Description":"","Link":"Course/FOC/2/secondary-storage.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713720000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":30,"ChapterId":2,"Title":"Hard Disk Drive","PageName":"hard-disk-drive.htm","Description":"","Link":"Course/FOC/2/hard-disk-drive.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713720000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":31,"ChapterId":2,"Title":"CDs and- DVDs","PageName":"cds-and-dvds.htm","Description":"","Link":"Course/FOC/2/cds-and-dvds.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713720000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":32,"ChapterId":2,"Title":"USB Flash Drive","PageName":"usb-flash-drive.htm","Description":"","Link":"Course/FOC/2/usb-flash-drive.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384713720000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":33,"ChapterId":3,"Title":"Introduction","PageName":"Introduction.htm","Description":"","Link":"Course/FOC/3/Introduction.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714229000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":34,"ChapterId":3,"Title":"Functions of an Operating System","PageName":"functions-of-an-operating-system.htm","Description":"","Link":"Course/FOC/3/functions-of-an-operating-system.htm","Time":2700,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714230000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":35,"ChapterId":3,"Title":"Operating System Vendors","PageName":"operating-system-vendors.htm","Description":"","Link":"Course/FOC/3/operating-system-vendors.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714230000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":36,"ChapterId":3,"Title":"Logging On","PageName":"logging-on.htm","Description":"","Link":"Course/FOC/3/logging-on.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714230000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":37,"ChapterId":3,"Title":"Start Menu","PageName":"start-menu.htm","Description":"","Link":"Course/FOC/3/start-menu.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714230000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":38,"ChapterId":3,"Title":"Overview of the Options","PageName":"overview-of-the-options.htm","Description":"","Link":"Course/FOC/3/overview-of-the-options.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714230000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":39,"ChapterId":3,"Title":"Task Bar","PageName":"task-bar.htm","Description":"","Link":"Course/FOC/3/task-bar.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714230000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":40,"ChapterId":3,"Title":"Start Program","PageName":"start-program.htm","Description":"","Link":"Course/FOC/3/start-program.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714230000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":41,"ChapterId":3,"Title":"Quitting Program","PageName":"quitting-program.htm","Description":"","Link":"Course/FOC/3/quitting-program.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714230000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":42,"ChapterId":3,"Title":"Getting Help","PageName":"getting-help.htm","Description":"","Link":"Course/FOC/3/getting-help.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714230000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":43,"ChapterId":3,"Title":"Locating Files and Folders","PageName":"locating-files-and-folders.htm","Description":"","Link":"Course/FOC/3/locating-files-and-folders.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714230000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":44,"ChapterId":3,"Title":"Changing System Settings","PageName":"changing-system-settings.htm","Description":"","Link":"Course/FOC/3/changing-system-settings.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714230000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":45,"ChapterId":3,"Title":"Using My Computer","PageName":"using-my-computer.htm","Description":"","Link":"Course/FOC/3/using-my-computer.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714230000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":46,"ChapterId":3,"Title":"Display the Storage Contents","PageName":"display-the-storage-contents.htm","Description":"","Link":"Course/FOC/3/display-the-storage-contents.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714230000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":47,"ChapterId":3,"Title":"Start Lock and Shutdown Windows","PageName":"start-lock-and-shutdown-windows.htm","Description":"","Link":"Course/FOC/3/start-lock-and-shutdown-windows.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714230000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":48,"ChapterId":3,"Title":"File Management in Windows","PageName":"file-management-in-windows.htm","Description":"","Link":"Course/FOC/3/file-management-in-windows.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714528000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":49,"ChapterId":3,"Title":"Using Windows Explorer","PageName":"using-windows-explorer.htm","Description":"","Link":"Course/FOC/3/using-windows-explorer.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714528000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":50,"ChapterId":3,"Title":"Copying or Moving a File or Folder","PageName":"copying-or-moving-a-file-or-folder.htm","Description":"","Link":"Course/FOC/3/copying-or-moving-a-file-or-folder.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714528000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":51,"ChapterId":3,"Title":"View File Details","PageName":"view-file-details.htm","Description":"","Link":"Course/FOC/3/view-file-details.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714528000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":52,"ChapterId":3,"Title":"Create New Folder","PageName":"create-new-folder.htm","Description":"","Link":"Course/FOC/3/create-new-folder.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714528000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":53,"ChapterId":3,"Title":"Rename a File or Folder","PageName":"rename-a-file-or-folder.htm","Description":"","Link":"Course/FOC/3/rename-a-file-or-folder.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714528000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":54,"ChapterId":3,"Title":"Delete a File or Folder","PageName":"delete-a-file-or-folder.htm","Description":"","Link":"Course/FOC/3/delete-a-file-or-folder.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714528000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":55,"ChapterId":3,"Title":"Hidden Files and Folders","PageName":"hidden-files-and-folders.htm","Description":"","Link":"Course/FOC/3/hidden-files-and-folders.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714528000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":56,"ChapterId":3,"Title":"Install Software Hardware","PageName":"install-software-hardware.htm","Description":"","Link":"Course/FOC/3/install-software-hardware.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714528000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":57,"ChapterId":3,"Title":"Install Software","PageName":"install-software.htm","Description":"","Link":"Course/FOC/3/install-software.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714528000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":58,"ChapterId":3,"Title":"Change or Remove Software","PageName":"change-or-remove-software.htm","Description":"","Link":"Course/FOC/3/change-or-remove-software.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714528000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":59,"ChapterId":3,"Title":"Add New Features","PageName":"add-new-features.htm","Description":"","Link":"Course/FOC/3/add-new-features.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714528000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":60,"ChapterId":3,"Title":"Install Hardware","PageName":"install-hardware.htm","Description":"","Link":"Course/FOC/3/install-hardware.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714528000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":61,"ChapterId":3,"Title":"Search in Windows","PageName":"search-in-windows.htm","Description":"","Link":"Course/FOC/3/search-in-windows.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714528000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":62,"ChapterId":3,"Title":"Device Manager","PageName":"device-manager.htm","Description":"","Link":"Course/FOC/3/device-manager.htm","Time":2700,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384714528000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":63,"ChapterId":4,"Title":"Introduction to Microsoft Paint","PageName":"introduction-paint.htm","Description":"","Link":"Course/FOC/4/introduction-paint.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384810934000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":64,"ChapterId":4,"Title":"Application Software","PageName":"application-software.htm","Description":"","Link":"Course/FOC/4/application-software.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384810934000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":65,"ChapterId":4,"Title":"The Toolbar","PageName":"toolbar.htm","Description":"","Link":"Course/FOC/4/toolbar.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384810934000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":66,"ChapterId":4,"Title":"Color Palette","PageName":"color-palette.htm","Description":"","Link":"Course/FOC/4/color-palette.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384810934000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":67,"ChapterId":4,"Title":"The Option Tool","PageName":"the-option-tool.htm","Description":"","Link":"Course/FOC/4/the-option-tool.htm","Time":7200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384810934000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":68,"ChapterId":4,"Title":"Save Image","PageName":"save-image.htm","Description":"","Link":"Course/FOC/4/save-image.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384810934000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":69,"ChapterId":4,"Title":"Introduction to Notepad","PageName":"introduction-notepad.htm","Description":"","Link":"Course/FOC/4/introduction-notepad.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384811065000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":70,"ChapterId":4,"Title":"Open Notepad","PageName":"open-notepad.htm","Description":"","Link":"Course/FOC/4/open-notepad.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384811065000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":71,"ChapterId":4,"Title":"Save","PageName":"save.htm","Description":"","Link":"Course/FOC/4/save.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384811065000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":72,"ChapterId":4,"Title":"Print","PageName":"print.htm","Description":"","Link":"Course/FOC/4/print.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384811065000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":73,"ChapterId":4,"Title":"Open","PageName":"open.htm","Description":"","Link":"Course/FOC/4/open.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384811065000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":74,"ChapterId":4,"Title":"Font","PageName":"font.htm","Description":"","Link":"Course/FOC/4/font.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384811065000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":75,"ChapterId":4,"Title":"Word Wrap","PageName":"word-wrap.htm","Description":"","Link":"Course/FOC/4/word-wrap.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384811066000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":76,"ChapterId":5,"Title":"Introduction","PageName":"introduction.htm","Description":"","Link":"Course/FOC/5/introduction.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768953000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":77,"ChapterId":5,"Title":"Features","PageName":"features.htm","Description":"","Link":"Course/FOC/5/features.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768953000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":78,"ChapterId":5,"Title":"Word","PageName":"word-2007.htm","Description":"","Link":"Course/FOC/5/word-2007.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768953000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":79,"ChapterId":5,"Title":"Screen Layout","PageName":"screen-layout.htm","Description":"","Link":"Course/FOC/5/screen-layout.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768953000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":80,"ChapterId":5,"Title":"Menus","PageName":"menus.htm","Description":"","Link":"Course/FOC/5/menus.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768953000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":81,"ChapterId":5,"Title":"Toolbars","PageName":"toolbars.htm","Description":"","Link":"Course/FOC/5/toolbars.htm","Time":3600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768953000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":82,"ChapterId":5,"Title":"Rulers","PageName":"rulers.htm","Description":"","Link":"Course/FOC/5/rulers.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768953000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":83,"ChapterId":5,"Title":"Typing Screen Objects","PageName":"typing-screen-objects.htm","Description":"","Link":"Course/FOC/5/typing-screen-objects.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768953000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":84,"ChapterId":5,"Title":"Scrollbars","PageName":"scrollbars.htm","Description":"","Link":"Course/FOC/5/scrollbars.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768953000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":85,"ChapterId":5,"Title":"Managing Documents","PageName":"managing-documents.htm","Description":"","Link":"Course/FOC/5/managing-documents.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768953000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":86,"ChapterId":5,"Title":"Create New Document","PageName":"create-new-document.htm","Description":"","Link":"Course/FOC/5/create-new-document.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768953000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":87,"ChapterId":5,"Title":"Open an Existing Document","PageName":"open-an-existing-document.htm","Description":"","Link":"Course/FOC/5/open-an-existing-document.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768953000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":88,"ChapterId":5,"Title":"Save a New or Existing Document","PageName":"save-new-or-existing-document.htm","Description":"","Link":"Course/FOC/5/save-new-or-existing-document.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768953000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":89,"ChapterId":5,"Title":"Find a Document","PageName":"find-a-document.htm","Description":"","Link":"Course/FOC/5/find-a-document.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":90,"ChapterId":5,"Title":"Close a Document","PageName":"close-a-document.htm","Description":"","Link":"Course/FOC/5/close-a-document.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":91,"ChapterId":5,"Title":"Print a Document","PageName":"print-a-document.htm","Description":"","Link":"Course/FOC/5/print-a-document.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":92,"ChapterId":5,"Title":"Exit Word Program","PageName":"exit-word-program.htm","Description":"","Link":"Course/FOC/5/exit-word-program.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":93,"ChapterId":5,"Title":"Keyboard Shortcuts","PageName":"keyboard-shortcuts.htm","Description":"","Link":"Course/FOC/5/keyboard-shortcuts.htm","Time":4800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":94,"ChapterId":5,"Title":"Working with Text","PageName":"working-with-text.htm","Description":"","Link":"Course/FOC/5/working-with-text.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":95,"ChapterId":5,"Title":"Typing Text","PageName":"typing-text.htm","Description":"","Link":"Course/FOC/5/typing-text.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":96,"ChapterId":5,"Title":"Inserting Text","PageName":"inserting-text.htm","Description":"","Link":"Course/FOC/5/inserting-text.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":97,"ChapterId":5,"Title":"Spacebar and Tabs","PageName":"spacebar-and-tabs.htm","Description":"","Link":"Course/FOC/5/spacebar-and-tabs.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":98,"ChapterId":5,"Title":"Selecting Text","PageName":"selecting-text.htm","Description":"","Link":"Course/FOC/5/selecting-text.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":99,"ChapterId":5,"Title":"Deleting Text","PageName":"deleting-text.htm","Description":"","Link":"Course/FOC/5/deleting-text.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":100,"ChapterId":5,"Title":"Replacing Text","PageName":"replacing-text.htm","Description":"","Link":"Course/FOC/5/replacing-text.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":101,"ChapterId":5,"Title":"Formatting Text","PageName":"formatting-text.htm","Description":"","Link":"Course/FOC/5/formatting-text.htm","Time":7200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":102,"ChapterId":5,"Title":"Format Painter","PageName":"format-painter.htm","Description":"","Link":"Course/FOC/5/format-painter.htm","Time":3600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":103,"ChapterId":5,"Title":"Format Paragraphs","PageName":"format-paragraphs.htm","Description":"","Link":"Course/FOC/5/format-paragraphs.htm","Time":7200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":104,"ChapterId":5,"Title":"Line Markers","PageName":"line-markers.htm","Description":"","Link":"Course/FOC/5/line-markers.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":105,"ChapterId":5,"Title":"Line Spacing","PageName":"line-spacing.htm","Description":"","Link":"Course/FOC/5/line-spacing.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":106,"ChapterId":5,"Title":"Paragraph Spacing","PageName":"paragraph-spacing.htm","Description":"","Link":"Course/FOC/5/paragraph-spacing.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":107,"ChapterId":5,"Title":"Borders and Shading","PageName":"borders-and-shading.htm","Description":"","Link":"Course/FOC/5/borders-and-shading.htm","Time":3600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":108,"ChapterId":5,"Title":"Bulleted and Numbered Lists","PageName":"bulleted-and-numberd-lists.htm","Description":"","Link":"Course/FOC/5/bulleted-and-numberd-lists.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":109,"ChapterId":5,"Title":"Copying Text and Moveing Text","PageName":"copying-text-and-moving-text.htm","Description":"","Link":"Course/FOC/5/copying-text-and-moving-text.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":110,"ChapterId":5,"Title":"Spelling and Grammar","PageName":"spelling-and-grammer.htm","Description":"","Link":"Course/FOC/5/spelling-and-grammer.htm","Time":3600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":111,"ChapterId":5,"Title":"Page Formatting","PageName":"page-formatting.htm","Description":"","Link":"Course/FOC/5/page-formatting.htm","Time":5400,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":112,"ChapterId":5,"Title":"Page Margins","PageName":"page-margins.htm","Description":"","Link":"Course/FOC/5/page-margins.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":113,"ChapterId":5,"Title":"Page Size and Orientation","PageName":"page-size-and-orientation.htm","Description":"","Link":"Course/FOC/5/page-size-and-orientation.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":114,"ChapterId":5,"Title":"Zoom in to the Page","PageName":"zoom-in-to-the-page.htm","Description":"","Link":"Course/FOC/5/zoom-in-to-the-page.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":115,"ChapterId":5,"Title":"Headers and Footers","PageName":"headers-and-footers.htm","Description":"","Link":"Course/FOC/5/headers-and-footers.htm","Time":3600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":116,"ChapterId":5,"Title":"Page Numbers","PageName":"page-numbers.htm","Description":"","Link":"Course/FOC/5/page-numbers.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":117,"ChapterId":5,"Title":"Inserting a Page Break","PageName":"inserting-a-page-break.htm","Description":"","Link":"Course/FOC/5/inserting-a-page-break.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":118,"ChapterId":5,"Title":"Deleting a page break","PageName":"deleting-a-page-break.htm","Description":"","Link":"Course/FOC/5/deleting-a-page-break.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384768954000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":119,"ChapterId":6,"Title":"Introduction","PageName":"introduction.htm","Description":"","Link":"Course/FOC/6/introduction.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769846000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":120,"ChapterId":6,"Title":"Features os Spreadsheets","PageName":"features-of-spreadsheets.htm","Description":"","Link":"Course/FOC/6/features-of-spreadsheets.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769846000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":121,"ChapterId":6,"Title":"Features of MS-Excel","PageName":"features-of-ms-excel-2007.htm","Description":"","Link":"Course/FOC/6/features-of-ms-excel-2007.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769846000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":122,"ChapterId":6,"Title":"Office themes and Excel styles","PageName":"office-themes-and-excel-styles.htm","Description":"","Link":"Course/FOC/6/office-themes-and-excel-styles.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769846000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":123,"ChapterId":6,"Title":"Formulas","PageName":"formulas.htm","Description":"","Link":"Course/FOC/6/formulas.htm","Time":300,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769846000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":124,"ChapterId":6,"Title":"Function AutoComplete","PageName":"function-autocomplete.htm","Description":"","Link":"Course/FOC/6/function-autocomplete.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769846000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":125,"ChapterId":6,"Title":"Sorting and Filtering","PageName":"sorting-and-filtering.htm","Description":"","Link":"Course/FOC/6/sorting-and-filtering.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769846000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":126,"ChapterId":6,"Title":"Starting Excel","PageName":"starting-excel.htm","Description":"","Link":"Course/FOC/6/starting-excel.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769846000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":127,"ChapterId":6,"Title":"Excel Worksheet","PageName":"excel-worksheet.htm","Description":"","Link":"Course/FOC/6/excel-worksheet.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769846000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":128,"ChapterId":6,"Title":"Selecting, Adding and Renaming Worksheets","PageName":"selecting-adding-and-renaming-worksheets.htm","Description":"","Link":"Course/FOC/6/selecting-adding-and-renaming-worksheets.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769846000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":129,"ChapterId":6,"Title":"Selecting Cells and Ranges","PageName":"selecting-cells-and-ranges.htm","Description":"","Link":"Course/FOC/6/selecting-cells-and-ranges.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769846000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":130,"ChapterId":6,"Title":"Navigating the Worksheet","PageName":"navigating-the-worksheet.htm","Description":"","Link":"Course/FOC/6/navigating-the-worksheet.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":131,"ChapterId":6,"Title":"Data Entry","PageName":"data-entry.htm","Description":"","Link":"Course/FOC/6/data-entry.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":132,"ChapterId":6,"Title":"Editing Data","PageName":"editing-data.htm","Description":"","Link":"Course/FOC/6/editing-data.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":133,"ChapterId":6,"Title":"Cell References","PageName":"cell-references.htm","Description":"","Link":"Course/FOC/6/cell-references.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":134,"ChapterId":6,"Title":"Find and Replace","PageName":"find-and-replace.htm","Description":"","Link":"Course/FOC/6/find-and-replace.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":135,"ChapterId":6,"Title":"Modifying a Worksheet","PageName":"modifying-a-worksheet.htm","Description":"","Link":"Course/FOC/6/modifying-a-worksheet.htm","Time":2400,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":136,"ChapterId":6,"Title":"Resizing Rows and Columns","PageName":"modifying-a-worksheet.htm","Description":"","Link":"Course/FOC/6/resizing-rows-and-columns.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":137,"ChapterId":6,"Title":"Insert moved or copied cells","PageName":"resizing-rows-and-columns.htm","Description":"","Link":"Course/FOC/6/insert-moved-or-copied-cells.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":138,"ChapterId":6,"Title":"Move or copy the contents of a cell","PageName":"insert-moved-or-copied-cells.htm","Description":"","Link":"Course/FOC/6/move-or-copy-the-contents-of-cell.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":139,"ChapterId":6,"Title":"Copy cell values, cell formats, or formulas only","PageName":"move-or-copy-the-contents-of-cell.htm","Description":"","Link":"Course/FOC/6/copy-cell-values-formats-formulas.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":140,"ChapterId":6,"Title":"Drag and Drop","PageName":"drag-and-drop.htm","Description":"","Link":"Course/FOC/6/drag-and-drop.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":141,"ChapterId":6,"Title":"Freez Pangs","PageName":"freeze-pans.htm","Description":"","Link":"Course/FOC/6/freeze-pans.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":142,"ChapterId":6,"Title":"Page Breaks","PageName":"page-breaks.htm","Description":"","Link":"Course/FOC/6/page-breaks.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":143,"ChapterId":6,"Title":"Page Setup","PageName":"page-setup.htm","Description":"","Link":"Course/FOC/6/page-setup.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":144,"ChapterId":6,"Title":"Print Preview","PageName":"print-preview.htm","Description":"","Link":"Course/FOC/6/print-preview.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":145,"ChapterId":6,"Title":"Print","PageName":"print.htm","Description":"","Link":"Course/FOC/6/print.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":146,"ChapterId":6,"Title":"File Open, Save and Close","PageName":"file-open-save-and-close.htm","Description":"","Link":"Course/FOC/6/file-open-save-and-close.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":147,"ChapterId":6,"Title":"Format Cells","PageName":"format-cells.htm","Description":"","Link":"Course/FOC/6/format-cells.htm","Time":2400,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":148,"ChapterId":6,"Title":"Format Cell Dialog Box","PageName":"format-cell-dialog-box.htm","Description":"","Link":"Course/FOC/6/format-cell-dialog-box.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":149,"ChapterId":6,"Title":"Date and Time","PageName":"date-and-time.htm","Description":"","Link":"Course/FOC/6/date-and-time.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":150,"ChapterId":6,"Title":"Format Coulumns and Rows","PageName":"format-columns-and-rows.htm","Description":"","Link":"Course/FOC/6/format-columns-and-rows.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":151,"ChapterId":6,"Title":"AutoFit Columns","PageName":"autofit-columns.htm","Description":"","Link":"Course/FOC/6/autofit-columns.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":152,"ChapterId":6,"Title":"Hide Column or Row","PageName":"hide-column-or-row.htm","Description":"","Link":"Course/FOC/6/hide-column-or-row.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":153,"ChapterId":6,"Title":"Unhide Column or Row","PageName":"unhide-column-or-row.htm","Description":"","Link":"Course/FOC/6/unhide-column-or-row.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":154,"ChapterId":6,"Title":"Formulas and Functions","PageName":"formulas-and-functions.htm","Description":"","Link":"Course/FOC/6/formulas-and-functions.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":155,"ChapterId":6,"Title":"Copy a Formula","PageName":"copy-a-formula.htm","Description":"","Link":"Course/FOC/6/copy-a-formula.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":156,"ChapterId":6,"Title":"Auto sum feature","PageName":"auto-sum-features.htm","Description":"","Link":"Course/FOC/6/auto-sum-features.htm","Time":3600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":157,"ChapterId":6,"Title":"Charts","PageName":"charts.htm","Description":"","Link":"Course/FOC/6/charts.htm","Time":300,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":158,"ChapterId":6,"Title":"Types of Chart","PageName":"types-of-charts.htm","Description":"","Link":"Course/FOC/6/types-of-charts.htm","Time":2400,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":159,"ChapterId":6,"Title":"Components of a Chart","PageName":"components-of-a-chart.htm","Description":"","Link":"Course/FOC/6/components-of-a-chart.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":160,"ChapterId":6,"Title":"Draw a Chart","PageName":"draw-a-chart.htm","Description":"","Link":"Course/FOC/6/draw-a-chart.htm","Time":5400,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":161,"ChapterId":6,"Title":"Editing of a Chart","PageName":"editing-of-a-chart.htm","Description":"","Link":"Course/FOC/6/editing-of-a-chart.htm","Time":2400,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":162,"ChapterId":6,"Title":"Resizing the Chart","PageName":"resizing-the-chart.htm","Description":"","Link":"Course/FOC/6/resizing-the-chart.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769847000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":163,"ChapterId":6,"Title":"Moving the Chart","PageName":"moving-the-chart.htm","Description":"","Link":"Course/FOC/6/moving-the-chart.htm","Time":300,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769848000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":164,"ChapterId":6,"Title":"Copying the Chart to Microsoft word","PageName":"copying-the-chart.htm","Description":"","Link":"Course/FOC/6/copying-the-chart.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769848000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":165,"ChapterId":6,"Title":"Graphics - Autoshapes and Smart Art","PageName":"graphics-autoshapes-and-smart-art.htm","Description":"","Link":"Course/FOC/6/graphics-autoshapes-and-smart-art.htm","Time":2400,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769848000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":166,"ChapterId":6,"Title":"Smart Art Graphics","PageName":"smart-art-graphics.htm","Description":"","Link":"Course/FOC/6/smart-art-graphics.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769848000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":167,"ChapterId":6,"Title":"Adding Clip Art","PageName":"adding-clip-art.htm","Description":"","Link":"Course/FOC/6/adding-clip-art.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769848000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":168,"ChapterId":6,"Title":"Inserting and Editing a Picture from a File","PageName":"inserting-and-editing-picture.htm","Description":"","Link":"Course/FOC/6/inserting-and-editing-picture.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384769848000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":169,"ChapterId":7,"Title":"Microsoft Powerpoint","PageName":"microsoft-powerpoint.htm","Description":"","Link":"Course/FOC/7/microsoft-powerpoint.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":170,"ChapterId":7,"Title":"Introduction","PageName":"introduction.htm","Description":"","Link":"Course/FOC/7/introduction.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":171,"ChapterId":7,"Title":"Starting a Powerpoint","PageName":"starting-a-powerpoint.htm","Description":"","Link":"Course/FOC/7/starting-a-powerpoint.htm","Time":3600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":172,"ChapterId":7,"Title":"Installed Templates","PageName":"installed-templates.htm","Description":"","Link":"Course/FOC/7/installed-templates.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":173,"ChapterId":7,"Title":"Design Template","PageName":"design-template.htm","Description":"","Link":"Course/FOC/7/design-template.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":174,"ChapterId":7,"Title":"Blank Presentations","PageName":"blank-presentations.htm","Description":"","Link":"Course/FOC/7/blank-presentations.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":175,"ChapterId":7,"Title":"Slide Layouts","PageName":"slide-layouts.htm","Description":"","Link":"Course/FOC/7/slide-layouts.htm","Time":3600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":176,"ChapterId":7,"Title":"Selecting Content","PageName":"selecting-content.htm","Description":"","Link":"Course/FOC/7/selecting-content.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":177,"ChapterId":7,"Title":"Open and Existing Presentations","PageName":"open-an-existing-presentation.htm","Description":"","Link":"Course/FOC/7/open-an-existing-presentation.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":178,"ChapterId":7,"Title":"Viewing slides","PageName":"viewing-slides.htm","Description":"","Link":"Course/FOC/7/viewing-slides.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":179,"ChapterId":7,"Title":"Normal View","PageName":"normal-view.htm","Description":"","Link":"Course/FOC/7/normal-view.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":180,"ChapterId":7,"Title":"Slide Sorter View","PageName":"slide-sorter-view.htm","Description":"","Link":"Course/FOC/7/slide-sorter-view.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":181,"ChapterId":7,"Title":"Slide Show View","PageName":"slide-show-view.htm","Description":"","Link":"Course/FOC/7/slide-show-view.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":182,"ChapterId":7,"Title":"Design Tips","PageName":"design-tips.htm","Description":"","Link":"Course/FOC/7/design-tips.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":183,"ChapterId":7,"Title":"Working with Slides","PageName":"working-with-slides.htm","Description":"","Link":"Course/FOC/7/working-with-slides.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":184,"ChapterId":7,"Title":"Applying a Design Templates","PageName":"applying-design-template.htm","Description":"","Link":"Course/FOC/7/applying-design-template.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":185,"ChapterId":7,"Title":"Changing Slide Layouts","PageName":"changing-slide-layouts.htm","Description":"","Link":"Course/FOC/7/changing-slide-layouts.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":186,"ChapterId":7,"Title":"Insert/Edit Existing Slides as Your New Slides","PageName":"insert-or-edit-existing-slides.htm","Description":"","Link":"Course/FOC/7/insert-or-edit-existing-slides.htm","Time":3600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":187,"ChapterId":7,"Title":"Reordering Slides","PageName":"reordering-slides.htm","Description":"","Link":"Course/FOC/7/reordering-slides.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":188,"ChapterId":7,"Title":"Hide Slides","PageName":"hide-slides.htm","Description":"","Link":"Course/FOC/7/hide-slides.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":189,"ChapterId":7,"Title":"Moving Between Slides","PageName":"moving-between-slides.htm","Description":"","Link":"Course/FOC/7/moving-between-slides.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770492000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":190,"ChapterId":7,"Title":"Working with Text","PageName":"working-with-text.htm","Description":"","Link":"Course/FOC/7/working-with-text.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770493000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":191,"ChapterId":7,"Title":"Inserting Text","PageName":"inserting-text.htm","Description":"","Link":"Course/FOC/7/inserting-text.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770493000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":192,"ChapterId":7,"Title":"Formatting Text","PageName":"formatting-text.htm","Description":"","Link":"Course/FOC/7/formatting-text.htm","Time":7200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770493000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":193,"ChapterId":7,"Title":"Text Box Properties","PageName":"text-box-properties.htm","Description":"","Link":"Course/FOC/7/text-box-properties.htm","Time":2400,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770493000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":194,"ChapterId":7,"Title":"Adding Notes","PageName":"adding-notes.htm","Description":"","Link":"Course/FOC/7/adding-notes.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770493000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":195,"ChapterId":7,"Title":"Spell Check","PageName":"spell-check.htm","Description":"","Link":"Course/FOC/7/spell-check.htm","Time":3600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770493000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":196,"ChapterId":7,"Title":"Saving and Printing","PageName":"saving-and-printing.htm","Description":"","Link":"Course/FOC/7/saving-and-printing.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770493000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":197,"ChapterId":7,"Title":"Page Setup","PageName":"page-setup.htm","Description":"","Link":"Course/FOC/7/page-setup.htm","Time":2400,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770493000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":198,"ChapterId":7,"Title":"Save as File","PageName":"save-as-file.htm","Description":"","Link":"Course/FOC/7/save-as-file.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770493000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":199,"ChapterId":7,"Title":"Save as Web Page","PageName":"save-as-web-page.htm","Description":"","Link":"Course/FOC/7/save-as-web-page.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770493000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":200,"ChapterId":7,"Title":"Print","PageName":"print.htm","Description":"","Link":"Course/FOC/7/print.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770493000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":201,"ChapterId":7,"Title":"Close Document","PageName":"close-document.htm","Description":"","Link":"Course/FOC/7/close-document.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770493000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":202,"ChapterId":7,"Title":"Exit Powerpoint Program","PageName":"exit-powerpoint-program.htm","Description":"","Link":"Course/FOC/7/exit-powerpoint-program.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770493000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":203,"ChapterId":7,"Title":"Keyboard Shortcuts","PageName":"keyboard-shortcuts.htm","Description":"","Link":"Course/FOC/7/keyboard-shortcuts.htm","Time":3600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770493000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":204,"ChapterId":8,"Title":"Introduction to Computer Networks","PageName":"computer-networks.htm","Description":"","Link":"Course/FOC/8/computer-networks.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":205,"ChapterId":8,"Title":"Local Area Network","PageName":"local-area-network.htm","Description":"","Link":"Course/FOC/8/local-area-network.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":206,"ChapterId":8,"Title":"Metropolitan Area Network","PageName":"metropolitan-area-network.htm","Description":"","Link":"Course/FOC/8/metropolitan-area-network.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":207,"ChapterId":8,"Title":"Wide Area Network","PageName":"wide-area-network.htm","Description":"","Link":"Course/FOC/8/wide-area-network.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":208,"ChapterId":8,"Title":"Protocols","PageName":"protocols.htm","Description":"","Link":"Course/FOC/8/protocols.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":209,"ChapterId":8,"Title":"Internet Protocol","PageName":"nternet-protocol.htm","Description":"","Link":"Course/FOC/8/internet-protocol.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":210,"ChapterId":8,"Title":"Post Office Protocol","PageName":"postoffice-protocol.htm","Description":"","Link":"Course/FOC/8/postoffice-protocol.htm","Time":2100,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":211,"ChapterId":8,"Title":"Hyper Text Transfer Protocol","PageName":"hyper-text-transfer-protocol.htm","Description":"","Link":"Course/FOC/8/hyper-text-transfer-protocol.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":212,"ChapterId":8,"Title":"File Transfer Protocol","PageName":"file-transfer-protocol.htm","Description":"","Link":"Course/FOC/8/file-transfer-protocol.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":213,"ChapterId":8,"Title":"IP Address","PageName":"ip-address.htm","Description":"","Link":"Course/FOC/8/ip-address.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":214,"ChapterId":8,"Title":"Share a Printer","PageName":"share-a-printer.htm","Description":"","Link":"Course/FOC/8/share-a-printer.htm","Time":300,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":215,"ChapterId":8,"Title":"File and Printer Sharing","PageName":"file-and-printer-sharing.htm","Description":"","Link":"Course/FOC/8/file-and-printer-sharing.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":216,"ChapterId":8,"Title":"Sharing Printers","PageName":"sharing-printers.htm","Description":"","Link":"Course/FOC/8/sharing-printers.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":217,"ChapterId":8,"Title":"Stop Printer Sharing","PageName":"stop-printer-sharing.htm","Description":"","Link":"Course/FOC/8/stop-printer-sharing.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":218,"ChapterId":8,"Title":"Connect to Printer","PageName":"connect-to-printer.htm","Description":"","Link":"Course/FOC/8/connect-to-printer.htm","Time":3600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":219,"ChapterId":8,"Title":"Setting or Removing Permissions","PageName":"setting-or-removing-permissions.htm","Description":"","Link":"Course/FOC/8/setting-or-removing-permissions.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384770801000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":220,"ChapterId":9,"Title":"Introduction","PageName":"introduction.htm","Description":"","Link":"Course/FOC/9/introduction.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384781739000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":221,"ChapterId":9,"Title":"Antivirus","PageName":"antivirus.htm","Description":"","Link":"Course/FOC/9/antivirus.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384781739000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":222,"ChapterId":9,"Title":"Popular Antivirus Software","PageName":"popular-antivirus-software.htm","Description":"","Link":"Course/FOC/9/popular-antivirus-software.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384781739000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":223,"ChapterId":9,"Title":"Best Practices","PageName":"best-practices.htm","Description":"","Link":"Course/FOC/9/best-practices.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384781740000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":224,"ChapterId":9,"Title":"Firewall","PageName":"firewall.htm","Description":"","Link":"Course/FOC/9/firewall.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384781740000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":225,"ChapterId":9,"Title":"Configure Windows Firewall","PageName":"configure-windows-xp-firewall.htm","Description":"","Link":"Course/FOC/9/configure-windows-xp-firewall.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384781740000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":226,"ChapterId":9,"Title":"Best Practices","PageName":"best-practices-firewall.htm","Description":"","Link":"Course/FOC/9/best-practices-firewall.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384781740000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":227,"ChapterId":9,"Title":"Security Essentials","PageName":"security-essentials.htm","Description":"","Link":"Course/FOC/9/security-essentials.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384781740000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":228,"ChapterId":9,"Title":"Safe Mode","PageName":"safe-mode.htm","Description":"","Link":"Course/FOC/9/safe-mode.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384781740000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":229,"ChapterId":9,"Title":"Start the Computer in Safe Mode","PageName":"start-the-computer-in-safe-mode.htm","Description":"","Link":"Course/FOC/9/start-the-computer-in-safe-mode.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384781740000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":230,"ChapterId":9,"Title":"MSConfig Utility","PageName":"msconfig-utility.htm","Description":"","Link":"Course/FOC/9/msconfig-utility.htm","Time":2700,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384781740000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":231,"ChapterId":9,"Title":"Introduction to Internet","PageName":"introduction-internet.htm","Description":"","Link":"Course/FOC/9/introduction-internet.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782060000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":232,"ChapterId":9,"Title":"Browsers","PageName":"browsers.htm","Description":"","Link":"Course/FOC/9/browsers.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782060000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":233,"ChapterId":9,"Title":"Popular Web Browsers","PageName":"popular-web-browsers.htm","Description":"","Link":"Course/FOC/9/popular-web-browsers.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782060000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":234,"ChapterId":9,"Title":"Web Browser User Interface","PageName":"web-browser-user-interface.htm","Description":"","Link":"Course/FOC/9/web-browser-user-interface.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782060000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":235,"ChapterId":9,"Title":"Internet Options","PageName":"internet-options.htm","Description":"","Link":"Course/FOC/9/internet-options.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782060000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":236,"ChapterId":9,"Title":"Cleanup History","PageName":"cleanup-history.htm","Description":"","Link":"Course/FOC/9/cleanup-history.htm","Time":3600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782060000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":237,"ChapterId":9,"Title":"Protocol and Security","PageName":"protocol-and-security.htm","Description":"","Link":"Course/FOC/9/protocol-and-security.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782060000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":238,"ChapterId":9,"Title":"Email System","PageName":"email-system.htm","Description":"","Link":"Course/FOC/9/email-system.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782060000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":239,"ChapterId":9,"Title":"Popular Email Service Providers","PageName":"popular-email-service-providers.htm","Description":"","Link":"Course/FOC/9/popular-email-service-providers.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782060000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":240,"ChapterId":9,"Title":"Password Strength","PageName":"password-strength.htm","Description":"","Link":"Course/FOC/9/password-strength.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782060000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":241,"ChapterId":9,"Title":"SPAM Emails","PageName":"spam-emails.htm","Description":"","Link":"Course/FOC/9/spam-emails.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782060000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":242,"ChapterId":9,"Title":"Social Engineering Emails","PageName":"social-engineering-emails.htm","Description":"","Link":"Course/FOC/9/social-engineering-emails.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782060000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":243,"ChapterId":9,"Title":"Email Best Practices","PageName":"email-best-practices.htm","Description":"","Link":"Course/FOC/9/email-best-practices.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782060000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":244,"ChapterId":9,"Title":"Search Engines","PageName":"search-engines.htm","Description":"","Link":"Course/FOC/9/search-engines.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782060000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":245,"ChapterId":9,"Title":"Popular Search Engines","PageName":"popular-search-engines.htm","Description":"","Link":"Course/FOC/9/popular-search-engines.htm","Time":600,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782060000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":246,"ChapterId":9,"Title":"Google Tricks","PageName":"google-tricks.htm","Description":"","Link":"Course/FOC/9/google-tricks.htm","Time":1200,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782060000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":247,"ChapterId":9,"Title":"Downloads and Installations","PageName":"downloads-and-installations.htm","Description":"","Link":"Course/FOC/9/downloads-and-installations.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782060000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":248,"ChapterId":10,"Title":"Introduction","PageName":"introduction.htm","Description":"","Link":"Course/FOC/10/introduction.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782208000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":249,"ChapterId":10,"Title":"Social Media Websites","PageName":"social-media-websites.htm","Description":"","Link":"Course/FOC/10/social-media-websites.htm","Time":900,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782208000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":250,"ChapterId":10,"Title":"Popular Social Media","PageName":"popular-social-media.htm","Description":"","Link":"Course/FOC/10/popular-social-media.htm","Time":1800,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782208000)\/","MigratedSectionId":0,"OrignalName":""},{"Id":251,"ChapterId":10,"Title":"Best Practices","PageName":"best-practices.htm","Description":"","Link":"Course/FOC/10/best-practices.htm","Time":2700,"IsDB":false,"PageContent":"","DateCreated":"\/Date(1384782209000)\/","MigratedSectionId":0,"OrignalName":""}]};
    onSuccessPopulateSecionsByCourse(result, CallFunctionAfterChaptersSectionsLoaded);
    //var path = "Handler/GetSectionsByCourse.ashx?courseid=" + courseId;
    //CallHandler(path, function (result) { onSuccessPopulateSecionsByCourse(result, CallFunctionAfterChaptersSectionsLoaded) });
}

function onSuccessPopulateSecionsByCourse(result, CallFunctionAfterChaptersSectionsLoaded) {
    if (result.Status == "Ok") {
        console.log(result);
        Secions = result.Sections;
        //Init1();
        CallFunctionAfterChaptersSectionsLoaded();
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            //parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            //alert(result.Message);
            console.log(result.Message);
        }
    }
}
//---------------------------------------end---------------------------------------------------

//-------------------------Manupulating Chapters and Sections----------------------------------
function GetChapterObject(chapterId) {
    if (chapterId == 1 || chapterId == 0) {
        return Chapters[0];
    }

    for (var i = 0; i < Chapters.length; i++) {
        if (Chapters[i].Id == chapterId) {
            return Chapters[i];
        }
    }
}

function GetChapterSectionObject(chapterId, sectionId) {
    var sectionObject = GetChapterSectionList(chapterId);

    if (sectionId == 1 || sectionId == 0) {
        return sectionObject[0];
    }

    for (var i = 0; i < sectionObject.length; i++) {
        if (sectionObject[i].Id == sectionId) {
            return sectionObject[i];
        }
    }
}

function GetChapterSectionList(chapterId) {
    var ChapterSection = [];

    if (chapterId == 1) {
        for (var i = 0; i < Secions.length; i++) {
            if (Secions[i].ChapterId == Chapters[0].Id) {
                ChapterSection.push(Secions[i]);
            }
        }
        return ChapterSection;
    }

    for (var i = 0; i < Secions.length; i++) {
        if (Secions[i].ChapterId == chapterId) {
            ChapterSection.push(Secions[i]);
        }
    }

    return ChapterSection;
}
//---------------------------------End---------------------------------------------------------------

//buildBASE_URL();

function buildBASE_URL() {
    var baseURL = BASE_URL.split('/');

    var protocol = document.location.protocol;
    var hostName = document.location.hostname;

    var newURL = protocol + "//" + hostName + "/" + baseURL[3];
    if (baseURL[3] != "") {
        newURL += "/";
    }

    BASE_URL = newURL;
}

function Init() {
    $(".content-container").css("display", "none");

    //Scroll page to top when loding image is show    
    parent.window.scrollTo(0, 0);

    //Add Loader image while page is rendered
    var loaderContain = $("<div/>");
    $(loaderContain).attr("id", "loaderMainContain");
    $(loaderContain).attr("style", "width: 100%;float: left;text-align: center;font-size:24px;margin-top: 20%;");
    $(loaderContain).html("<img src='" + BASE_URL + "static/images/loading.gif' /> Loading... Please wait ");
    var ele = $(".content-container").parent();
    $(ele).append(loaderContain);

    getQueryStrings();
    var qs = getQueryStrings();
    //chapterId = qs["chapterId"];
    //sectionId = qs["sectionId"];
    courseId = qs["courseid"];
    PopulateChaptersByCourse(courseId, Init1);
}

function Init1() {
    // Hide Main-Container till completely rendered    
    $(".content-main").find("a").each(function () {
        $(this).attr("target", "_blank");
    });

    $(".content-container").css("display", "none");

    //Add Loader image while page is rendered
    var loaderContain = $("<div/>");
    $(loaderContain).attr("id", "loaderMainContain");
    $(loaderContain).attr("style", "width: 100%;float: left;text-align: center;font-size:24px;margin-top: 20%;");
    $(loaderContain).html("<img src='" + BASE_URL + "static/images/loading.gif' /> Loading... Please wait ");
    var ele = $(".content-container").parent();
    //$(ele).append(loaderContain);

    var i = 0;
    $(".content-main").find(".content-navigation").each(function () {
        i++;
        $(this).attr("id", "content_navigation_" + i);
    });

    // adding Quiz loader image
    var loaderQuiz = $("<div/>");
    $(loaderQuiz).attr("id", "loaderQuiz");
    $(loaderQuiz).attr("style", "width: 100%;float: left;text-align: center;")
    $(loaderQuiz).html("<img src='" + BASE_URL + "static/images/loading.gif' /> Loading Quiz... Please wait");
    $(".content-main").append(loaderQuiz);

    // adding Modul-Popup for Report-Issue
    var divModal = $("<div/>");
    $(divModal).html('<div class="modal fade" id="ReportIssue" tabindex="-2" role="dialog" aria-labelledby="myModalLabel2" aria-hidden="true" style="display:none;"> ' +
        '<div class="modal-dialog"> <div class="modal-content"> <div class="modal-header"> <button type="button" onclick="HideReportIssuePopup();" class="close" data-dismiss="modal" aria-hidden="true"> ' +
         ' &times;</button> <h4 class="modal-title">Report an issue</h4></div><div class="modal-body">' +
         '<div id="reportIssueStatus" class="ErrorContainer" style="display:none"></div>' +
         '<div class="Record"><div class="Column2"><input type="text" id="txtTitle" placeholder="Title" /><span style="color: Red">*</span></div></div><div class="Record"><div class="Column2"> ' +
         '<input type="text" id="txtDescription" placeholder="Description" /><span style="color: Red">*</span></div></div><div class="Record"><div class="Column2"> ' +
         '<input type="text" id="txtEmail" placeholder="E-mail" /><span style="color: Red">*</span></div></div><div class="Record"><div class="Column2"> ' +
         '<textarea id="txtExpectedContent" placeholder="Expected content" cols="20" rows="2" style="max-height: 100px;margin-bottom: 3%; padding: 6px;"></textarea><span style="color: Red">*</span></div></div><div class="Record"> ' +
         '<div class="Column2"></div></div></div><div class="modal-footer"><a href="Javascript:void(0);" class="btn" data-dismiss="modal" onclick="HideReportIssuePopup();" ' +
         'title="click to close popup">Close</a><input id="btnSave" type="button"  value="Submit" class="btn btn-primary" OnClick="SaveReportIssue();" />' +
         '</div></div><!-- /.modal-content --></div><!-- /.modal-dialog --></div> <div id="ReportModelBack" style="display:none;" class="modal-backdrop fade in"></div>');

    //$(".content-main").append(divModal);

    //adding link-button for report issue
    var reportIssue = $("<div/>");
    $(reportIssue).attr("style", "float: left;");
    $(reportIssue).attr("id", "reportissuelink");
    $(reportIssue).html("<a style='color: white' target='_top' data-toggle='modal' onClick='ShowReportIssuePopup();' href='javascript:void(0);'>Report Issue</a>");
    $("#content_navigation_1").append(reportIssue);

    /*var facebookShare = $("<div/>");
    $(facebookShare).attr("style", "float: left;margin-left:5px;");
    $(facebookShare).attr("id", "fbShare");
    $(facebookShare).html("<a style='color: white' href='https://www.facebook.com/sharer/sharer.php?u=http://adminapps.in/course/Course/fundamental-of-computers/1/Introduction.htm' target='_blank'>Share on Facebook </a>");
    $("#content_navigation_1").append(facebookShare);*/


    getQueryStrings();
    var qs = getQueryStrings();
    chapterId = qs["chapterId"];
    sectionId = qs["sectionId"];
    courseId = qs["courseid"];

    //PopulateChaptersByCourse(courseId);
    //console.log(window.navigator);

    //SaveSiteAnalyticsDetails
    //();

    var ref = document.location.href.indexOf(BASE_URL);
    if (ref >= 0) {
        try {
            var values = parent.document.location.href.substring(BASE_URL.length).split("/");
            var value = values[0].split("?");

            if (value[0].toLowerCase() == "hintpage.aspx") {
                //BuilLeftNavigation();
                BuilLeftNavigationForAnnonymusUser();
                BuildBreadcrumbs(chapterId, sectionId);

                $(".content-navigation").find("div").each(function () {
                    if ($(this).html().trim() == "Previous") {
                        $(this).hide();
                    }
                    $(this).hide();
                });

                $(".content-navigation").find("div").each(function () {
                    if ($(this).html().trim() == "Next") {
                        //$(this).attr("onClick", "nextClick();");next_previusClick
                        $(this).hide();
                    }

                });

                $(".content-container").css("display", "block");
                $("#loaderMainContain").remove();
                $("#loaderQuiz").remove();
            }
            else if (value[0].toLowerCase() == "coursecontent.aspx") {

                $(".content-navigation").find("div").each(function () {
                    if ($(this).html().trim() == "Previous") {
                        $(this).html("Next");
                        $(this).attr("onClick", "next_previusClick(true);");
                        $(this).attr("style", "float:right");
                    }
                    else if ($(this).html().trim() == "Next") {
                        $(this).html("Previous");
                        $(this).attr("onClick", "preClick();");
                        $(this).attr("style", "float:right;margin-right:10px");
                    }

                    $(this).attr("class", "btn btn-primary");
                });

                CallHandler("Handler/GetUserChapterStatus.ashx?courseid=" + courseId, OnComplete);
            }
            else {
                document.location = BASE_URL + "CourseContent.aspx";
            }

            var hostName = document.location.hostname;
            if (hostName == "moocacademy.in" || hostName == "www.moocacademy.in") {
                (function (i, s, o, g, r, a, m) {
                    i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                        (i[r].q = i[r].q || []).push(arguments)
                    }, i[r].l = 1 * new Date(); a = s.createElement(o), m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
                })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

                ga('create', 'UA-46034211-1', 'moocacademy.in');
                ga('send', 'pageview');
            }
        }
        catch (e) {
            //alert(e);
        }
    }

}


function getQueryStrings() {
    var assoc = {};
    var decode = function (s) { return decodeURIComponent(s.replace(/\+/g, " ")); };
    var queryString = parent.location.search.substring(1);

    var keyValues = queryString.split('&');

    for (var i in keyValues) {
        var key = keyValues[i].split('=');
        if (key.length > 1) {
            assoc[decode(key[0])] = decode(key[1]);
        }
    }

    return assoc;
}

function GetQueryStringsForHtmPage() {

    var assoc = {};
    var decode = function (s) { return decodeURIComponent(s.replace(/\+/g, " ")); };
    var queryString = document.location.search.substring(1);

    var keyValues = queryString.split('&');

    for (var i in keyValues) {
        var key = keyValues[i].split('=');
        if (key.length > 1) {
            assoc[decode(key[0])] = decode(key[1]);
        }
    }

    return assoc;
}

//var path = BASE_URL + "Handler/GetUserChapterStatus.ashx";

var currentId;

function CallHandler(queryString, OnComp) {
    if (BASE_URL == "") {
        BASE_URL = "http://localhost:6328/";
        //BASE_URL = "http://adminapps.in/course/";
        //BASE_URL = "http://moocacademy.in/"; //for mooc
        //BASE_URL = "http://myclasstest.com/mooc/";
    }

    var path = BASE_URL + queryString;

    $.ajax({
        url: path,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        responseType: "json",
        cache: false,
        //success: OnComplete,
        success: OnComp,
        error: OnFail
    });
    return false;
}

function OnComplete(result) {
    if (result.Status == "Ok") {
        //PopulateChaptersByCourse(courseId);
        chapterId = result.ChapterId;
        sectionId = result.SectionId;

        var chapterobj = GetChapterObject(chapterId);
        var section = GetChapterSectionObject(chapterId, sectionId);

        /*while (section == null) {
        sectionId++;
        section = GetChapterSectionObject(chapterId, sectionId);
        }*/

        var qs = GetQueryStringsForHtmPage();
        chapterId = qs["chpid"];
        sectionId = qs["secid"];
        //chapterId = section.ChapterId;
        //sectionId = section.Id;

        /*var ref = document.location.href.indexOf(BASE_URL);
        if (ref >= 0) {
        try {
        var values = document.location.href.substring(BASE_URL.length).split("/");
        chapterId = values[2];
        sectionId = GetChapterSectionID(chapterId, values[3]);
        //alert(sectionId);
        }
        catch (e) {
        alert(e);
        }
        }*/

        // creating left navigation
        BuilLeftNavigation();
        //creating breadcrumbs
        BuildBreadcrumbs(chapterId, sectionId);
        // creating and assinging next previus button functionality
        BuildNextPreviusButtonFunctional(chapterId, sectionId);
        // get Chapter and section Quiz
        GetQuizFromDatabase();
        // update chapter time e.g. how much time a user spend on a perticuler chapter
        UpdateChapterTime(chapterId, sectionId);
        // udpate chapter status
        UpdateUserChapterStatus(chapterId, sectionId);
        // check if user browser support csss3 and html5
        CheckUserBrowerSupportCSS3HTML5();

        if (chapterId == Chapters[Chapters.length - 1].Id) {
            secionList = GetChapterSectionList(chapterId);
            if (sectionId == secionList[secionList.length - 1].Id) {
                $(".content-navigation").find("div").each(function () {
                    if ($(this).html().trim() == "Next") {
                        $(this).html("Finish");
                        $(this).attr("onClick", "next_previusClick(true);");
                    }
                });
            }
        }

        $(".content-container").css("display", "block");
        $("#loaderMainContain").remove();
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            //alert(result.Message); 
            console.log(result.Message);
        }
    }
}

function SaveSiteAnalyticsDetails() {
    var intverval = setInterval(function () {
        var time = 2;
        var title = document.title;
        var page = document.location.href.substring(document.location.href.lastIndexOf('/') + 1);
        var refferPage = document.referrer;
        var screenResulotion;

        if ($(window.parent) != undefined) {
            screenResulotion = $(window.parent).width() + " x " + $(window.parent).height();
        }
        else {
            screenResulotion = $(window).width() + " x " + $(window).height();
        }

        var path = "Handler/SaveSiteAnalyticsDetails.ashx?time=" + time + "&page=" + page + "&pagetitle=" + title + "&refferpage=" + refferPage + "&screenresulotion=" + screenResulotion + "&comment=";

        CallHandler(path, function (result) { })
    }, 2000);

}

function CheckUserBrowerSupportCSS3HTML5() {

    var test_canvas = document.createElement("canvas") //try and create sample canvas element
    var canvascheck = (test_canvas.getContext) ? true : false //check if object supports getContext() method, a method of the canvas element
    //alert(canvascheck) //alerts true if browser supports canvas element
    if (!canvascheck) {

        $(".breadcrumb").parent().closest('div').attr("id", "brdParent");
        $(".breadcrumb").parent().closest('div').attr("style", "float: left;width: 99%;");

        $(".content-container").find("#brdParent").each(function () {
            $("<div class='ErrorContainer' style='text-align:center;'>Please view this page in modern browsers. You can download it from <a href='#'> Chrome </a>, <a href='#'> Firfox </a> and <a href='#'> Internet Explorer  </a> .</div>").insertBefore(this);
        });
    }
}

function OnFail(result) { }

function BuilLeftNavigation() {
    var lnClass = "left-nav";
    var cc = $(".content-summary").attr("class").split(" ");
    if (cc.length >= 2) {
        lnClass = lnClass + " " + cc[1];
    }

    var ul = $("<ul/>");
    //$(ul).attr("class", "left-nav blue");
    $(ul).attr("class", lnClass);

    var chapterSection = GetChapterSectionList(chapterId);

    for (var i = 0; i < chapterSection.length; i++) {
        var li = $("<li/>");

        if (chapterSection[i].Id == sectionId) {
            $(li).attr("class", "active");
        }

        //$(li).html("<a href='javascript:void(0);' onClick='leftNavigationClick(\"" + chapterSection[i].Link + "\");'> " + chapterSection[i].Title + "</a>");
        $(li).html("<a href='javascript:void(0);' onClick='leftNavigationClick(" + chapterSection[i].ChapterId + "," + chapterSection[i].Id + ");'> " + chapterSection[i].Title + "</a>");
        $(ul).append(li);
    }

    $(".left-navigation").append(ul);
}

// build left navigation for Annonymus user 
function BuilLeftNavigationForAnnonymusUser() {
    var lnClass = "left-nav";
    var cc = $(".content-summary").attr("class").split(" ");
    if (cc.length >= 2) {
        lnClass = lnClass + " " + cc[1];
    }

    var ul = $("<ul/>");
    //$(ul).attr("class", "left-nav blue");
    $(ul).attr("class", lnClass);

    var chapterSection = GetChapterSectionList(chapterId);

    for (var i = 0; i < chapterSection.length; i++) {
        var li = $("<li/>");

        if (chapterSection[i].Id == sectionId) {
            $(li).attr("class", "active");
        }

        $(li).html(chapterSection[i].Title);
        $(ul).append(li);
    }

    $(".left-navigation").append(ul);
}

function BuildBreadcrumbs(chapterId, sectionId) {
    var chapterObject = GetChapterObject(chapterId);
    var sectionObject = GetChapterSectionObject(chapterId, sectionId);

    // integrate facebook like & share buttons
    ///var _URL = BASE_URL + "HintPage.aspx?chapterid=" + chapterId + "&sectionid=" + sectionId;
    //var _URL = BASE_URL + "course/fundamentals-of-computer/" + chapterId + "/" + sectionObject.PageName;
    var _URL = BASE_URL + "ViewContent.aspx?chapterId=" + chapterId + "&sectionId=" + sectionId + "&courseid=" + courseId;

    var ele = $("#content_navigation_1");
    $('<iframe src="//www.facebook.com/plugins/like.php?href=' + _URL + '&amp;width&amp;layout=button_count&amp;action=like&amp;show_faces=true&amp;share=true&amp;height=21&amp;appId=137095629816827" scrolling="no" frameborder="0" style="border:none; overflow:hidden; height:30px;" allowTransparency="true"></iframe>').insertBefore(ele);
    // end


    //build bredbrumbs
    li = $("<li/>");
    $(li).html("<a href='" + BASE_URL + "Dashboard.aspx" + "' target='_top'>" + "Home" + "<a/> <span class='divider'>/</span>");
    $(".breadcrumb").append(li);

    var li = $("<li/>");
    //$(li).html("<a href='" + BASE_URL + chapterObject.Link + "'>" + chapterObject.Title + "<a/> <span class='divider'>/</span>");
    //$(li).html("<a href='javascript:void(0);' onClick='ClickBreacrubms(\"" + chapterObject.Link + "\");'>" + chapterObject.Title + "<a/> <span class='divider'>/</span>");
    //$(li).html("<a target='_blank' href='" + BASE_URL + "CourseDetails.aspx?id=1'>" + chapterObject.Title + "<a/> <span class='divider'>/</span>");
    $(li).html("<a target='_blank' href='" + BASE_URL + "CourseDetails.aspx?id=" + chapterObject.CourseId + "'>" + chapterObject.Title + "<a/> <span class='divider'>/</span>");
    $(".breadcrumb").append(li);


    li = $("<li/>");
    //$(li).html("<a href='" + BASE_URL + sectionObject.Link + "'>" + sectionObject.Title + " <a/>");
    $(li).html(sectionObject.Title);
    $(".breadcrumb").append(li);

    // creating page header    
    $(".summary-header").html(sectionObject.Title);

    var div = $("<div/>");
    //$(div).html(sectionObject.Description);
    $(".content-summary").append(div)

    var time = parseInt(sectionObject.Time) / 60;

    var divTime = $("<div/>");
    $(divTime).attr("style", "float:right");
    $(divTime).html("Estimated time to complete: " + time + " minutes");
    $(".content-summary").append(divTime)

    $(".content-summary").attr("chpId", chapterId);
    $(".content-summary").attr("secId", sectionId);

    var currentChapterId = 0;
    for (var k = 0; k < Chapters.length; k++) {
        if (chapterObject.Id == Chapters[k].Id) {
            currentChapterId = k + 1;
            //break;
        }
    }

    $(".content-summary").find(".summary-header").each(function () {
        //$("<div style='margin-bottom: 3px;'>Chapter " + chapterObject.Id + " : " + chapterObject.Title + "</div>").insertBefore(this);
        $("<div style='margin-bottom: 3px;'>Chapter " + currentChapterId + " : " + chapterObject.Title + "</div>").insertBefore(this);
    });

    $("#dbHtmlContainer").html(unescape(sectionObject.PageContent));

    if (sectionObject.Description != "") {
        var divClass = "content-summary " + sectionObject.Description;
        var ulClass = "left-nav " + sectionObject.Description;

        var cc = $(".content-summary").attr("class", divClass);
        var ul = $(".left-nav").attr("class", ulClass);

        //$(ul).attr("class", ulClass);
    }
    //disable rigth click on page
    //$(".content-container").attr("oncontextmenu", "return false");
}

function ClickBreacrubms(link) {
    document.location = BASE_URL + link
}

function BuildNextPreviusButtonFunctional(chapterId, sectionId) {
    var chapterObject = GetChapterObject(chapterId);
    var sectionObject = GetChapterSectionObject(chapterId, sectionId);

    var sectionList = GetChapterSectionList(chapterId);

    var nxtSectionId = sectionId + 1;
    var preSectionId = sectionId - 1;

    if (preSectionId <= 0) {
        preSectionId = 1;
    }


    //if (sectionId == 1) {
    //if (sectionId == sectionList[0].Id) {
    if (Chapters[0].Id == chapterId && sectionId == sectionList[0].Id) {
        $(".content-navigation").find("div").each(function () {
            if ($(this).html().trim() == "Previous") {
                $(this).hide();
            }

        });

    }
    else if (sectionId == sectionList.length) {
        $(".content-navigation").find("div").each(function () {
            if ($(this).html().trim() == "Next") {
                //$(this).hide();
            }
        });
    }
}

function BuilLeftNavigationForSylabus() {
    var ul = $("<ul/>");
    $(ul).attr("class", "left-nav blue");

    var liHeader = $("<li/>");
    $(liHeader).attr("class", "active");
    $(liHeader).html("Chapters");
    $(ul).append(liHeader);

    for (var i = 0; i < Chapters.length; i++) {
        var li = $("<li/>");

        //if (Chapters[i].Id == sectionId) {
        $(li).attr("class", "active");
        //}

        $(li).html(Chapters[i].Title);
        //$(li).attr("onClick", "ShowSection(" + Chapters[i].Id + ")");
        $(li).attr("onClick", "ShowSection(this)");
        $(li).attr("chpId", Chapters[i].Id);

        $(ul).append(li);
        if (i == 0)
            ShowSection(li);
    }

    //$(".left-navigation").append(ul);chapters
    $("#chapters").append(ul);
}

//functio Show Sections
function ShowSection(link) {

    $("#chapters").find("li").each(function () {
        $(this).attr("class", "active");
    });

    var chpId = $(link).attr("chpId");
    $(link).attr("class", "head");
    $(link).css("text-transform", "none");

    $("#secions").html("");
    var chapObj = GetChapterObject(chpId);
    var secList = GetChapterSectionList(chpId);

    var ul = $("<ul/>");
    $(ul).attr("class", "left-nav blue");

    var liHeader = $("<li/>");
    $(liHeader).attr("class", "head");
    $(liHeader).html("Overview");
    $(ul).append(liHeader);

    for (var i = 0; i < secList.length; i++) {
        var li = $("<li/>");
        $(li).attr("class", "active");
        $(li).html("<a target='_blank' href='" + BASE_URL + "HintPage.aspx?chapterId=" + chpId + "&sectionId=" + secList[i].Id + "&courseid=" + courseId + "'>" + secList[i].Title + "</a>");
        $(ul).append(li);
    }

    $("#secions").append(ul);

    //$("#chapterIntro").html(secList[0].PageContent);

    /*for (var i = 0; i < Chapter_Indroduction.length; i++) {
    if (Chapter_Indroduction[i].ChapterId == chpId) {
    $("#chapterIntro").html(Chapter_Indroduction[i].Introduction);
    }
    }*/

}

// Dashboard Start and Continue functionality
/*function BuildStartorContinueText() {
CallHandler("Handler/GetUserChapterStatus.ashx?courseid=" + courseId, function (data) {
        OnCompleteStartContinue(data, isNewCourse);
    });
}*/

function BuildStartorContinueText(courseId, courseName, isNewCourse) {
    var title;
    if (isNewCourse) {
        title = "Start your course";
    }
    else {
        title = "Continue your course";
    }

    $("#StartCourse").html("<img id='imgStartContinue' src='static/images/c.png' alt='continue' style='margin-right:10px' /><a id='hyperlink' href='" + BASE_URL + 'CourseContent.aspx?courseid=' + courseId + "'>" + title + " - " + courseName + "</a>");

    $("#courseLoadder").hide();
    $("#courseContainer").show();
}

function OnCompleteStartContinue(courseId, isNewCourse) {
    if (result.Status == "Ok") {
        chapterId = result.ChapterId;
        sectionId = result.SectionId;
        courseId = result.CourseId;
        var chapterSection = GetChapterSectionObject(chapterId, sectionId);

        var title;

        if (isNewCourse) {
            title = "Start your course";
        }
        else {
            title = "Continue your course";
        }

        $("#StartCourse").html("<img id='imgStartContinue' src='static/images/c.png' alt='continue' style='margin-right:10px' /><a id='hyperlink' href='" + BASE_URL + 'CourseContent.aspx?courseid=' + courseId + "'>" + title + " - " + result.CourseName + "</a>");

        $("#courseLoadder").hide();
        $("#courseContainer").show();
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            //alert(result.Message); console.log(result.Message);
        }
    }
}

function AssingUrlToIFrame(courseid) {
    //PopulateChaptersByCourse(courseid);
    //CallHandler("Handler/GetUserChapterStatus.ashx?courseid=" + courseid, function (result) { onCompAssingUrlToIFrame(result, courseid); });
    PopulateChaptersByCourse(courseid, function () { CallHandler("Handler/GetUserChapterStatus.ashx?courseid=" + courseid, function (result) { onCompAssingUrlToIFrame(result, courseid); }); });
}

function onCompAssingUrlToIFrame(result, courseid) {
    if (result.Status == "Ok") {
        chapterId = result.ChapterId;
        sectionId = result.SectionId;

        var chapterSection = GetChapterSectionObject(chapterId, sectionId);

        while (chapterSection == null) {
            sectionId++;
            chapterSection = GetChapterSectionObject(chapterId, sectionId);
        }

        var link = BASE_URL + chapterSection.Link + "?courseid=" + courseid + "&chpid=" + chapterSection.ChapterId + "&secid=" + chapterSection.Id;
        $("#ifContent").attr("src", link);
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            //alert(result.Message); console.log(result.Message);
        }
    }
}

// assign url to hint page

function AssignUrlToHintPage(chapterId, sectionId, courseid) {

    var chapterSection = GetChapterSectionObject(chapterId, sectionId);

    /*while (chapterSection == null) {
    sectionId++;
    chapterSection = GetChapterSectionObject(chapterId, sectionId);
    }*/

    var link = BASE_URL + chapterSection.Link + "?courseid=" + courseid + "&chpid=" + chapterSection.ChapterId + "&secid=" + chapterSection.Id;
    //$("#ifHintContent").attr("src", BASE_URL + chapterSection.Link);
    $("#ifHintContent").attr("src", link);
}

// Getting Quiz
function GetQuizFromDatabase() {
    var chId = $(".content-summary").attr("chpId");
    var secId = $(".content-summary").attr("secId");

    CallHandler("Handler/GetQuiz.ashx?chapterid=" + chId + "&sectionid=" + secId, onCompletePopulateQuiz);
}

function onCompletePopulateQuiz(result) {
    $("#loaderQuiz").remove();
    if (result.Status == "Ok") {

        var chId = $(".content-summary").attr("chpId");
        var secId = $(".content-summary").attr("secId");
        $("#loaderQuiz").remove();

        if (result.Quiz != null) {
            if (result.Quiz.length > 0) {
                for (var i = 0; i < result.Quiz.length; i++) {
                    if (result.Quiz[i].ChapterId == chId && result.Quiz[i].SectionId == secId)
                        PopulateQuiz(result.Quiz[i], i);
                }
            }
        }
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            //alert(result.Message); console.log(result.Message);
        }
    }
}

function PopulateQuiz(quiz, counter) {
    //alert(1);
    // creating maincontainer
    var maincontainer = $("<div/>");
    $(maincontainer).attr("class", "quizContainer");
    //end

    var errorStatus = $("<div/>");
    $(errorStatus).attr("class", "ErrorContainer");
    $(errorStatus).attr("id", "errorStatus_" + counter);
    $(errorStatus).html("Please select appropriate answer.");
    $(errorStatus).attr("style", "display:none");
    $(maincontainer).append(errorStatus);

    // creating question text container
    var questionText = $("<div/>");
    $(questionText).attr("id", "QuesionText_" + counter);
    $(questionText).attr("class", "questionText");
    $(questionText).attr("quesId", quiz.Id);
    $(questionText).attr("isQuizMultiTure", quiz.IsMultiTrueAnswer);
    $(questionText).html("Question " + (parseInt(counter) + 1) + " : " + decodeURIComponent(quiz.QuestionText));

    $(maincontainer).append(questionText);
    // end

    // creating question option text
    var questionOption = $("<div/>");
    $(questionOption).attr("id", "QuestionOption_" + counter);
    $(questionOption).attr("class", "questionText");

    for (var i = 0; i < quiz.QuestionOptionList.length; i++) {

        var div = $("<div/>");
        $(div).attr("id", "QO" + i);
        $(div).html((i + 1) + ".  " + quiz.QuestionOptionList[i].QuestionOption);

        $(questionOption).append(div);
    }

    $(maincontainer).append(questionOption);
    // end

    // creating answer option text
    var answerOption = $("<div/>");
    $(answerOption).attr("id", "AnswerOption_" + counter);
    $(answerOption).attr("class", "answerOption");

    var html = "";
    for (var i = 0; i < quiz.AnswerOptionList.length; i++) {

        var div = $("<div/>");

        $(div).attr("id", "AO" + i);
        //$(div).attr("class", "radioDiv");

        // check if question has mulitiple correct answer
        if (!quiz.IsMultiTrueAnswer) {
            // populating single correct answer mode
            var s = quiz.AnswerOptionList[i].AnswerOption.replace("%2B", "+");
            s = s.replace("%2B", "+");
            s = s.replace("%2B", "+");
            s = s.replace("%2B", "+");

            if (s == quiz.AnswerText) {
                //$(div).html(s + "<input type='radio' id='Ans_" + i + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "' checked='true'/>");
                html += "<input type='radio' id='Ans_" + i + "_" + counter + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "' checked='true'/><label for='Ans_" + i + "_" + counter + "' style='float: left; margin-left: 5px;;width:94%;'>" + s + "</label>";
            }
            else {
                //$(div).html(s + "<input type='radio' id='Ans_" + i + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "'/>");
                html += "<input type='radio' id='Ans_" + i + "_" + counter + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "'/><label for='Ans_" + i + "_" + counter + "' style='float: left; margin-left: 5px;width:94%;'>" + s + "</label>";
            }
        }
        else {
            // populating multi correct answer mode
            var s = quiz.AnswerOptionList[i].AnswerOption.replace("%2B", "+");
            s = s.replace("%2B", "+");
            s = s.replace("%2B", "+");
            s = s.replace("%2B", "+");

            var isSelectedAns = false;

            if (quiz.AnswerText != null && quiz.AnswerText != undefined) {
                var userAnswers = quiz.AnswerText.split(",");

                for (var uaIndex = 0; uaIndex < userAnswers.length; uaIndex++) {
                    if (s == userAnswers[uaIndex]) {
                        isSelectedAns = true;
                        break;
                    }
                }
            }
            //if (s == quiz.AnswerText) {
            if (isSelectedAns) {
                //$(div).html(s + "<input type='checkbox' id='Ans_" + i + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "' checked='true'/>");
                html += "<input type='checkbox' id='Ans_" + i + "_" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "' checked='true'/><label for='Ans_" + i + "_" + counter + "' style='float: left; margin-left: 5px;;width:94%;'>" + s + "</label>";
            }
            else {
                //$(div).html(s + "<input type='checkbox' id='Ans_" + i + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "'/>");
                html += "<input type='checkbox' id='Ans_" + i + "_" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "'/><label for='Ans_" + i + "_" + counter + "' style='float: left; margin-left: 5px;width:94%;'>" + s + "</label>";
            }
        }

        //$(answerOption).append(div);
    }

    $(answerOption).html(html);

    $(maincontainer).append(answerOption);

    // creating submit button in populate quiz function
    var btnField = $("<div/>");
    $(btnField).attr("id", "btnField_" + counter);
    $(btnField).attr("style", "margin-top: 25px; width: 100%; float: left;");
    //added by anup
    if (quiz.IsAnsGiven) {
        var status = $("<div/>");
        $(status).attr("id", "status_" + counter);

        if (quiz.IsCorrect) {
            $(status).html("Your answer is correct.<br/>Your answer is '<b>" + quiz.AnswerText + "'</b>");
            $(status).attr("style", "color: green");
        }
        else {
            $(status).html("Your answer is incorrect.<br/>Your answer is '<b>" + quiz.AnswerText + "'</b>");
            $(status).attr("style", "color: red");
        }

        $(btnField).append(status);
    }
    else {

        var btn = $("<div/>");
        $(btn).attr("id", "btn_" + counter);
        $(btn).attr("class", "btn btn-primary");
        $(btn).html("Submit");
        $(btn).attr("onclick", "SaveQuiz(" + counter + ")");
        $(btnField).append(btn);

        var status = $("<div/>");
        $(status).attr("id", "status_" + counter);
        $(btnField).append(status);

        var loader = $("<div/>");
        $(loader).attr("id", "loader_" + counter);
        $(loader).html("<img src='" + BASE_URL + "static/images/waiting-loader.gif' />");
        $(loader).hide();
        $(btnField).append(loader);
    }


    $(maincontainer).append(btnField);
    //end
    //$(".content-main").append(maincontainer);
    $("#content_navigation_2").attr("style", "padding-top:15px");
    $(maincontainer).insertBefore("#content_navigation_2");
}

// save quiz function
function SaveQuiz(counter) {

    var userAnswer = "";
    var isCurrect;
    var isChecked;

    var isCurrectAnswer;
    var currectAnswer = "";

    var correctAnsCount = 0;
    var userSelectCorrectAns = 0;
    var userSelectAns = 0;

    $("#errorStatus_" + counter).attr("style", "display:none");

    var isMultiTrue = $("#QuesionText_" + counter).attr("isquizmultiture");

    if (isMultiTrue == "false") {
        $("#AnswerOption_" + counter).find(".ansRadio").each(function () {

            if ($(this).prop("checked")) {
                userAnswer = $(this).attr("answer");
                //userAnswer = userAnswer.replace("+", "%2B");
                isCurrect = $(this).attr("iscurrect");
                isChecked = true;
            }

            if ($(this).attr("iscurrect") == 'true') {
                currectAnswer = $(this).attr("answer");
            }
        });
    }
    else {
        $("#AnswerOption_" + counter).find(".ansRadio").each(function () {
            if ($(this).prop("checked")) {
                userAnswer += $(this).attr("answer") + ",";
                isCurrect = $(this).attr("iscurrect");
                isChecked = true;
                userSelectAns++;
                if (isCurrect == "true")
                    userSelectCorrectAns++;
            }

            if ($(this).attr("iscurrect") == 'true') {
                currectAnswer += $(this).attr("answer") + ",";
                correctAnsCount++;
            }
        });

        isCurrect = false;
        if (userSelectAns == userSelectCorrectAns && userSelectAns == correctAnsCount && userSelectCorrectAns == correctAnsCount) {
            isCurrect = true;
        }

        if (userAnswer.length > 0)
            userAnswer = userAnswer.substring(userAnswer.length - 1, 0);

        if (currectAnswer.length > 0)
            currectAnswer = currectAnswer.substring(currectAnswer.length - 1, 0);
    }


    var questionId = $("#QuesionText_" + counter).attr("quesId");

    if (!isChecked) {
        $("#errorStatus_" + counter).attr("style", "display:block");
    }
    else {

        var path = "Handler/SaveQuiz.ashx?questionId=" + questionId + "&useranswer=" + userAnswer + "&iscurrect=" + isCurrect;
        currectAnswer = currectAnswer.replace("%2B", "+");
        CallHandler(path, function (result) { onSavedQuizSuccessfully(result, isCurrect, currectAnswer, counter); });

        $("#btn_" + counter).hide();
        $("#loader_" + counter).show();
    }
}

//function onSavedQuizSuccessfully(result) {
function onSavedQuizSuccessfully(result, isCurrect, currectAnswer, counter) {
    if (result.Status == "Ok") {
        //alert(result.Status);    
        $("#loader_" + counter).hide();
        if (isCurrect == 'true' || isCurrect == true) {
            $("#status_" + counter).html("Your answer is correct.");
            $("#status_" + counter).attr("style", "color: green");
        }
        else {
            $("#status_" + counter).html("Your answer is incorrect.<br/>The correct answer is '<b>" + currectAnswer + "'</b>");
            $("#status_" + counter).attr("style", "color: red");
        }

        var chId = $(".content-summary").attr("chpId");
        var secId = $(".content-summary").attr("secId");

        var secObj = GetChapterSectionObject(chId, secId);
        var title = secObj.Title;
        var link = "chapterId=" + chId + " sectionId=" + secId;

        var activit = { "Title": escape(title), "Link": link, "ActivityType": "2" };
        SaveUserActivity(activit);
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            $("#status_" + counter).html("There are some problem in saving to your quiz answare. Please try after some time.");
            $("#btn_" + counter).show();
            $("#loader_" + counter).hide();
            ////alert(result.Message); console.log(result.Message);
        }
    }
}

//end

function next_previusClick(isNext) {

    //$(".content-navigation").hide();
    $(".content-navigation").find("div").each(function () {
        $(this).hide();
    });

    var d = $("<div/>");
    $(d).html("Loading next content....")
    $(d).attr("id", "loadingStatus");
    $(".content-navigation").append(d);
    //$(".content-navigation").html("Loading next content....");

    var chId = $(".content-summary").attr("chpId");
    var secId = $(".content-summary").attr("secId");

    var path = "Handler/GetUserQuizResStatus.ashx?chapterid=" + chId + "&sectionid=" + secId;

    if (isNext) {
        CallHandler(path, nextClick);
    }
    else {
        CallHandler(path, preClick);
    }
}



var RemoveErrorMessage20SecInterval;
//
function nextClick(result) {

    if (result.Status == "Ok") {
        $("#qeusError_1").remove();
        $("#qeusError_2").remove();

        if (result.IsUserFilledAllQuiz) {
            var chapterObject = GetChapterObject(chapterId);
            var sectionObject = GetChapterSectionObject(chapterId, sectionId);

            var sectionList = GetChapterSectionList(chapterId);

            var nxtSectionId = parseInt(sectionId) + 1;
            var preSectionId = parseInt(sectionId) - 1;

            if (preSectionId <= 0) {
                preSectionId = 1;
            }

            if (nxtSectionId > sectionList[sectionList.length - 1].Id) {
                chapterId++;
                if (chapterId > Chapters[Chapters.length - 1].Id) {
                    chapterId--;
                    secionList = GetChapterSectionList(chapterId);

                    //while (sectionList == null) {
                    while (sectionList == null || sectionList.length == 0) {
                        chapterId--;
                        sectionList = GetChapterSectionList(chapterId);
                    }

                    if (sectionId == secionList[secionList.length - 1].Id) {
                        // To DO 
                        // Set user iscomplete flag true when he complete final quiz
                        var path = "Handler/SetUsersIsCompleteFlag.ashx";
                        //CallHandler(path, function (result) { });
                        CallHandler(path, function (result) {
                            if (result.Status == "Ok") {
                                parent.document.location = BASE_URL + "Dashboard.aspx";
                            }
                            else if (result.Status == "Error") {
                                if (result.Message == "Session Expire") {
                                    alert("Your Session is Expire you are redirect to login page.");
                                    parent.document.location = BASE_URL + "Login.aspx";
                                }
                                else {
                                    $(".content-navigation").find("div").each(function () {
                                        $(this).show();
                                    });
                                    //$("#loadingStatus").remove();
                                    $(".content-navigation").find("#loadingStatus").each(function () {
                                        $(this).remove();
                                    });
                                    var i = 0;
                                    $(".content-main").find(".content-navigation").each(function () {
                                        i++;
                                        $("<div class='ErrorContainer' id='qeusError_" + i + "'>There is an error while processing your request. Please try again after some time.</div>").insertBefore(this);
                                    });
                                    //alert(result.Message);
                                }
                            }
                        });
                    }
                    else {
                        //sectionList = GetChapterSectionList(chapterId);
                        nxtSectionId = sectionList[0].Id;
                        sectionObject = GetChapterSectionObject(chapterId, nxtSectionId);

                        document.location = BASE_URL + sectionObject.Link + "?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
                    }
                }
                else {
                    sectionList = GetChapterSectionList(chapterId);

                    while (sectionList == null || sectionList.length == 0) {
                        chapterId++;
                        sectionList = GetChapterSectionList(chapterId);
                    }

                    nxtSectionId = sectionList[0].Id;
                    sectionObject = GetChapterSectionObject(chapterId, nxtSectionId);

                    document.location = BASE_URL + sectionObject.Link + "?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
                }
            }
            else {
                sectionObject = GetChapterSectionObject(chapterId, nxtSectionId);
                while (sectionObject == null) {
                    nxtSectionId++;
                    sectionObject = GetChapterSectionObject(chapterId, nxtSectionId);
                }

                /*if (sectionObject.IsDB) {
                document.location = BASE_URL + sectionObject.Link + "?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
                }
                else {
                document.location = BASE_URL + sectionObject.Link;
                }*/
                document.location = BASE_URL + sectionObject.Link + "?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
            }
        }
        else {
            //alert("Please fill all the quiz.");
            //$(".content-navigation").show();
            $(".content-navigation").find("div").each(function () {
                $(this).show();
            });
            //$("#loadingStatus").remove();
            $(".content-navigation").find("#loadingStatus").each(function () {
                $(this).remove();
            });
            var i = 0;
            $(".content-main").find(".content-navigation").each(function () {
                i++;
                $("<div class='ErrorContainer' id='qeusError_" + i + "'>Please fill in answer for all the questions.</div>").insertBefore(this);
            });


            var intverval = setInterval(
                function () {
                    $("#qeusError_1").remove();
                    $("#qeusError_2").remove();

                    clearInterval(intverval);
                }
                 , 20000);
        }
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            //$(".content-navigation").show();
            //$("#loadingStatus").remove();
            $(".content-navigation").find("#loadingStatus").each(function () {
                $(this).remove();
            });
            $(".content-navigation").find("div").each(function () {
                $(this).show();
            });
            //alert(result.Message); 
            console.log(result.Message);
        }
    }
}

function ClearAnswerFillMessage20Sec() {
    $("#qeusError_1").remove();
    $("#qeusError_2").remove();
}

function preClick() {
    /*var ref = document.location.href.indexOf(BASE_URL);
    if (ref >= 0) {
    try {
    var values = document.location.href.substring(BASE_URL.length).split("/");
    chapterId = values[2];
    sectionId = GetChapterSectionID(chapterId, values[3]);
    //alert(sectionId);
    }
    catch (e) {
    alert(e);
    }
    }*/

    var chapterObject = GetChapterObject(chapterId);
    var sectionObject = GetChapterSectionObject(chapterId, sectionId);

    var sectionList = GetChapterSectionList(chapterId);

    var nxtSectionId = sectionId + 1;
    var preSectionId = sectionId - 1;

    if (preSectionId < sectionList[0].Id) {
        //preSectionId = 1;
        chapterId--;
        sectionList = GetChapterSectionList(chapterId);

        //while (sectionList == null) {
        while (sectionList == null || sectionList.length == 0) {
            chapterId--;
            sectionList = GetChapterSectionList(chapterId);
        }

        preSectionId = sectionList[0].Id;
        sectionObject = GetChapterSectionObject(chapterId, preSectionId);
        //document.location = BASE_URL + sectionObject.Link;
        document.location = BASE_URL + sectionObject.Link + "?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
    }
    else {
        sectionObject = GetChapterSectionObject(chapterId, preSectionId);
        while (sectionObject == null) {
            preSectionId--;
            sectionObject = GetChapterSectionObject(chapterId, preSectionId);
        }
        //document.location = BASE_URL + sectionObject.Link;
        document.location = BASE_URL + sectionObject.Link + "?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
    }
}

/* Getting final quiz fro database  and show in final-quiz page */

function PopulatePercentage(value) {
    loadProgressBar(value);
}
var myProgressBar = null
var timerId = null
var vall = 0;
function loadProgressBar(val) {
    if (vall == 0) {
        vall++;
        myProgressBar = new ProgressBar("my_progress_bar_1", {
            borderRadius: 5,
            width: 250,
            height: 30,
            value: val,
            labelText: "{progress} % Complete",
            //orientation: ProgressBar.Orientation.Horizontal,
            //direction: ProgressBar.Direction.LeftToRight,
            //animationStyle: ProgressBar.AnimationStyle.LeftToRight2,
            //animationSpeed: 0.25,
            imageUrl: 'static/images/h_fg8.png',
            backgroundUrl: 'static/images/h_bg2.png'
        });
    }
    else {
        myProgressBar.setValue(val);
    }
}

function CheckUserLastFinalQuizQuestion(testId, courseId) {
    //CallHandler("Handler/GetUserLastFinalQuizQuestion.ashx", onSuccessGetFinalQuizes);
    CallHandler("Handler/GetUserLastFinalQuizQuestion.ashx?testid=" + testId, function (result) { onSuccessGetFinalQuizes(result, testId, courseId); });
}

function onSuccessGetFinalQuizes(result, testId, courseId) {
    if (result.Status == "Ok") {
        GetFinalQuizFromDatabase(result.LastAttemptQuestion, testId, courseId)
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            //alert(result.Message); 
            console.log(result.Message);
        }
    }
}

function GetFinalQuizFromDatabase(lastId, testId, courseId) {
    $(".content-container").attr("testid", testId);

    var result = { "Status": "Ok", "Message": "GetQuiz-Success", "FinalQuiz": [{"Id":271,"CourseId":1,"CourseName":null,"ChapterId":7,"ChapterName":null,"SectionId":196,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Power point slide could be saved as ___________.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Presentation\",\"IsCurrect\":false},{\"AnswerOption\":\"Web pages\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385216760000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Presentations","IsCurrect":false},{"AnswerOption":"Web pages","IsCurrect":false},{"AnswerOption":"Both A and B","IsCurrect":true},{"AnswerOption":"None of the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":82,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":55,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Hidden files/folders could be made visible using folder options.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385107955000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"True","IsCurrect":true},{"AnswerOption":"False","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":34,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":108,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"______________ is used to get reader attention to main points.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Paragraph spacing\",\"IsCurrect\":false},{\"AnswerOption\":\"Line spacing\",\"IsCurrect\":false},{\"AnswerOption\":\"Line markers\",\"IsCurrect\":false},{\"AnswerOption\":\"Bulleted and Numbered List\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385077423000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Paragraph spacing","IsCurrect":false},{"AnswerOption":"Line spacing","IsCurrect":false},{"AnswerOption":"Line markers","IsCurrect":false},{"AnswerOption":"Bulleted and Numbered List","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":140,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":66,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which tool is used to draw multi-sided shapes?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"The Rectangle Tool\",\"IsCurrect\":false},{\"AnswerOption\":\"The Polygon Tool\",\"IsCurrect\":true},{\"AnswerOption\":\"The Free-Form Select Tool\",\"IsCurrect\":false},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385095170000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"The Rectangle Tool","IsCurrect":false},{"AnswerOption":"The Polygon Tool","IsCurrect":true},{"AnswerOption":"The Free-Form Select Tool","IsCurrect":false},{"AnswerOption":"None of above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":91,"CourseId":1,"CourseName":null,"ChapterId":9,"ChapterName":null,"SectionId":237,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which icon is used in web browsers to indicate invalid/expired certificates?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Green lock icons\",\"IsCurrect\":false},{\"AnswerOption\":\"Red lock icons\",\"IsCurrect\":true},{\"AnswerOption\":\"Blue lock icons\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385115107000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Green lock icons","IsCurrect":false},{"AnswerOption":"Red lock icons","IsCurrect":true},{"AnswerOption":"Blue lock icons","IsCurrect":false},{"AnswerOption":"None of these","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":154,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":122,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Predefined set of colors, lines etc. is known as _____________.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Styles\",\"IsCurrect\":false},{\"AnswerOption\":\"Sorting\",\"IsCurrect\":false},{\"AnswerOption\":\"Themes\",\"IsCurrect\":true},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385130067000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Styles","IsCurrect":false},{"AnswerOption":"Sorting","IsCurrect":false},{"AnswerOption":"Themes","IsCurrect":true},{"AnswerOption":"None of above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":71,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":49,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which shortcut keys are used to open Windows Explorer?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Window Key + F\",\"IsCurrect\":false},{\"AnswerOption\":\"Window key + E\",\"IsCurrect\":true},{\"AnswerOption\":\"Window Key + C\",\"IsCurrect\":false},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385103278000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Window Key + F","IsCurrect":false},{"AnswerOption":"Window key + E","IsCurrect":true},{"AnswerOption":"Window Key + C","IsCurrect":false},{"AnswerOption":"None of above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":23,"CourseId":1,"CourseName":null,"ChapterId":7,"ChapterName":null,"SectionId":185,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Layout command is present in ________.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Header bar\",\"IsCurrect\":false},{\"AnswerOption\":\"Menu bar\",\"IsCurrect\":true},{\"AnswerOption\":\"Side bar\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385189991000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Header bar","IsCurrect":false},{"AnswerOption":"Menu bar","IsCurrect":true},{"AnswerOption":"Side bar","IsCurrect":false},{"AnswerOption":"All of the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":249,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":155,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which feature is use to copy a formula ?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Copy and pest\",\"IsCurrect\":false},{\"AnswerOption\":\"Drag and drop\",\"IsCurrect\":true},{\"AnswerOption\":\"Move column\",\"IsCurrect\":false},{\"AnswerOption\":\"Move Rows\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385210165000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Copy and pest","IsCurrect":false},{"AnswerOption":"Drag and drop","IsCurrect":true},{"AnswerOption":"Move column","IsCurrect":false},{"AnswerOption":"Move Rows","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":201,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":81,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"In Word 2007, Is it possible to customize quick access toolbar?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385143879000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"True","IsCurrect":true},{"AnswerOption":"False","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":13,"CourseId":1,"CourseName":null,"ChapterId":7,"ChapterName":null,"SectionId":181,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which view is used to preview the presentation?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"To preview of slide\",\"IsCurrect\":true},{\"AnswerOption\":\"To add new slide to slide show\",\"IsCurrect\":false},{\"AnswerOption\":\"To delete slide from slide show\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385189333000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"To preview of slide","IsCurrect":true},{"AnswerOption":"To add new slide to slide show","IsCurrect":false},{"AnswerOption":"To delete slide from slide show","IsCurrect":false},{"AnswerOption":"All of the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":118,"CourseId":1,"CourseName":null,"ChapterId":3,"ChapterName":null,"SectionId":25,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which Memory is known as Volatile memory?\n\n","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Secondary Storage\",\"IsCurrect\":false},{\"AnswerOption\":\"Hybrid Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Real time memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Primary Storage\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1384797840000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Secondary Storage","IsCurrect":false},{"AnswerOption":"Hybrid Memory","IsCurrect":false},{"AnswerOption":"Real time memory","IsCurrect":false},{"AnswerOption":"Primary Storage","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":70,"CourseId":1,"CourseName":null,"ChapterId":11,"ChapterName":null,"SectionId":217,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Stop Printer Sharing is present in ______ tab.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Ports\",\"IsCurrect\":false},{\"AnswerOption\":\"General\",\"IsCurrect\":false},{\"AnswerOption\":\"Advanced \",\"IsCurrect\":false},{\"AnswerOption\":\"Sharing\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385197781000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Ports","IsCurrect":false},{"AnswerOption":"General","IsCurrect":false},{"AnswerOption":"Advanced ","IsCurrect":false},{"AnswerOption":"Sharing","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":248,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":152,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Hide feature is use to hide _______.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Column\",\"IsCurrect\":false},{\"AnswerOption\":\"Row\",\"IsCurrect\":false},{\"AnswerOption\":\"Cell\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385209894000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Column","IsCurrect":false},{"AnswerOption":"Row","IsCurrect":false},{"AnswerOption":"Cell","IsCurrect":false},{"AnswerOption":"All of the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":12,"CourseId":1,"CourseName":null,"ChapterId":2,"ChapterName":null,"SectionId":11,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which options could be used to create Upper case/Capital characters?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Press CAPS Lock key\",\"IsCurrect\":false},{\"AnswerOption\":\"Press Shift + alphabet key\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1384768550000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Press CAPS Lock key","IsCurrect":false},{"AnswerOption":"Press Shift + alphabet key","IsCurrect":false},{"AnswerOption":"Both A and B","IsCurrect":true},{"AnswerOption":"None of above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":117,"CourseId":1,"CourseName":null,"ChapterId":3,"ChapterName":null,"SectionId":26,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"What is RAM ?\n","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Random Access Memory\",\"IsCurrect\":true},{\"AnswerOption\":\"Recurring Access Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Flash Access Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1384797558000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Random Access Memory","IsCurrect":true},{"AnswerOption":"Recurring Access Memory","IsCurrect":false},{"AnswerOption":"Flash Access Memory","IsCurrect":false},{"AnswerOption":"None of these","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":223,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":113,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"This is type of page orientation:","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\" Portrait\",\"IsCurrect\":false},{\"AnswerOption\":\"Landscape\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385200969000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":" Portrait","IsCurrect":false},{"AnswerOption":"Landscape","IsCurrect":false},{"AnswerOption":"Both A and B","IsCurrect":true},{"AnswerOption":"None of the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":280,"CourseId":1,"CourseName":null,"ChapterId":11,"ChapterName":null,"SectionId":216,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Sharing Printers describes how to use Windows to share a printer with others on network.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"True\",\"IsCurrect\":false},{\"AnswerOption\":\"False\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385220943000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"True","IsCurrect":false},{"AnswerOption":"False","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":48,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":68,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"MS Paint could save image in _______________ format.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Bitmap (bmp)\",\"IsCurrect\":false},{\"AnswerOption\":\"JPEG (JPG)\",\"IsCurrect\":false},{\"AnswerOption\":\"Portable Network Graphics (PNG)\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385071429000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Bitmap (bmp)","IsCurrect":false},{"AnswerOption":"JPEG (JPG)","IsCurrect":false},{"AnswerOption":"Portable Network Graphics (PNG)","IsCurrect":false},{"AnswerOption":"All the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":106,"CourseId":1,"CourseName":null,"ChapterId":1,"ChapterName":null,"SectionId":8,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Customized software application which meets the specific requirements is known as:","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"generalized packages\",\"IsCurrect\":false},{\"AnswerOption\":\"Utility programs\",\"IsCurrect\":false},{\"AnswerOption\":\"Application Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Customized packages\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1384792555000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"generalized packages","IsCurrect":false},{"AnswerOption":"Utility programs","IsCurrect":false},{"AnswerOption":"Application Software","IsCurrect":false},{"AnswerOption":"Customized packages","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":211,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":94,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Text section contain ?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Common word concepts\",\"IsCurrect\":false},{\"AnswerOption\":\"Tips\",\"IsCurrect\":false},{\"AnswerOption\":\"Commands\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385145580000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Common word concepts","IsCurrect":false},{"AnswerOption":"Tips","IsCurrect":false},{"AnswerOption":"Commands","IsCurrect":false},{"AnswerOption":"All of the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":269,"CourseId":1,"CourseName":null,"ChapterId":7,"ChapterName":null,"SectionId":194,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Notes are not visible in _________________.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Normal View\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide sorter View\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide show\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385216523000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Normal View","IsCurrect":false},{"AnswerOption":"Slide sorter View","IsCurrect":false},{"AnswerOption":"Slide show","IsCurrect":true},{"AnswerOption":"None of the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":143,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":54,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Files/folders deleted from _________ are permanently deleted. ","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Removable storage\",\"IsCurrect\":false},{\"AnswerOption\":\"Pen drive\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385096989000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Removable storage","IsCurrect":false},{"AnswerOption":"Pen drive","IsCurrect":false},{"AnswerOption":"Both A and B","IsCurrect":true},{"AnswerOption":"None of above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":232,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":127,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Excel files are also known as _________.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Workbook or spreadsheet\",\"IsCurrect\":false},{\"AnswerOption\":\"Workbook\",\"IsCurrect\":true},{\"AnswerOption\":\"Column\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385202689000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Workbook or spreadsheet","IsCurrect":false},{"AnswerOption":"Workbook","IsCurrect":true},{"AnswerOption":"Column","IsCurrect":false},{"AnswerOption":"All of the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":184,"CourseId":1,"CourseName":null,"ChapterId":9,"ChapterName":null,"SectionId":233,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which one is popular web browser?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Google chrome\",\"IsCurrect\":false},{\"AnswerOption\":\"Microsoft Internet Explorer\",\"IsCurrect\":false},{\"AnswerOption\":\"Apple Safari\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385139660000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Google chrome","IsCurrect":false},{"AnswerOption":"Microsoft Internet Explorer","IsCurrect":false},{"AnswerOption":"Apple Safari","IsCurrect":false},{"AnswerOption":"All the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":289,"CourseId":1,"CourseName":null,"ChapterId":9,"ChapterName":null,"SectionId":243,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"User shall attach files of up to ___ to an email. This is one of the best practices.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"1MB\",\"IsCurrect\":true},{\"AnswerOption\":\"5MB\",\"IsCurrect\":false},{\"AnswerOption\":\"10MB\",\"IsCurrect\":false},{\"AnswerOption\":\"20MB\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385224687000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"1MB","IsCurrect":true},{"AnswerOption":"5MB","IsCurrect":false},{"AnswerOption":"10MB","IsCurrect":false},{"AnswerOption":"20MB","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":83,"CourseId":1,"CourseName":null,"ChapterId":7,"ChapterName":null,"SectionId":189,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which tool is moved from one slide to another slide?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Scroll Bars\",\"IsCurrect\":false},{\"AnswerOption\":\"Next Slide and Previous Slide Buttons\",\"IsCurrect\":false},{\"AnswerOption\":\"Using Outline Pane\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385109134000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Scroll Bars","IsCurrect":false},{"AnswerOption":"Next Slide and Previous Slide Buttons","IsCurrect":false},{"AnswerOption":"Using Outline Pane","IsCurrect":false},{"AnswerOption":"All the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":189,"CourseId":1,"CourseName":null,"ChapterId":9,"ChapterName":null,"SectionId":238,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"What are the two section of any email message?\n","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Input and Output\",\"IsCurrect\":false},{\"AnswerOption\":\"Header and Body\",\"IsCurrect\":true},{\"AnswerOption\":\"Protocol \u0026 security\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385140387000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Input and Output","IsCurrect":false},{"AnswerOption":"Header and Body","IsCurrect":true},{"AnswerOption":"Protocol \u0026 security","IsCurrect":false},{"AnswerOption":"All the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":294,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":150,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"The content of the column is adjusted to column width using ________ feature.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Autofit\",\"IsCurrect\":true},{\"AnswerOption\":\"Autopilot\",\"IsCurrect\":false},{\"AnswerOption\":\"AutoCopy\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385308891000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Autofit","IsCurrect":true},{"AnswerOption":"Autopilot","IsCurrect":false},{"AnswerOption":"AutoCopy","IsCurrect":false},{"AnswerOption":"None of the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":246,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":149,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Excels default date format is ____________.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"January 1, 2001\",\"IsCurrect\":false},{\"AnswerOption\":\"1 January 2001\",\"IsCurrect\":false},{\"AnswerOption\":\"1-Jan-01\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385209468000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"January 1, 2001","IsCurrect":false},{"AnswerOption":"1 January 2001","IsCurrect":false},{"AnswerOption":"1-Jan-01","IsCurrect":true},{"AnswerOption":"None of the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":58,"CourseId":1,"CourseName":null,"ChapterId":9,"ChapterName":null,"SectionId":237,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"HTTP is based on __________.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Input/output devices\",\"IsCurrect\":false},{\"AnswerOption\":\"TCP/IP Protocols\",\"IsCurrect\":false},{\"AnswerOption\":\"Client/server principles\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385114928000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Input/output devices","IsCurrect":false},{"AnswerOption":"TCP/IP Protocols","IsCurrect":false},{"AnswerOption":"Client/server principles","IsCurrect":true},{"AnswerOption":"None of these","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":283,"CourseId":1,"CourseName":null,"ChapterId":9,"ChapterName":null,"SectionId":225,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Windows firewall settings have _________ tab.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"General\",\"IsCurrect\":false},{\"AnswerOption\":\"Exceptions\",\"IsCurrect\":false},{\"AnswerOption\":\"Advanced\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385223321000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"General","IsCurrect":false},{"AnswerOption":"Exceptions","IsCurrect":false},{"AnswerOption":"Advanced","IsCurrect":false},{"AnswerOption":"All the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":235,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":130,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which is valid way to move between cells?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Mouse\",\"IsCurrect\":false},{\"AnswerOption\":\"Scroll bar\",\"IsCurrect\":false},{\"AnswerOption\":\"Navigation keys\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385206499000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Mouse","IsCurrect":false},{"AnswerOption":"Scroll bar","IsCurrect":false},{"AnswerOption":"Navigation keys","IsCurrect":false},{"AnswerOption":"All of the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":47,"CourseId":1,"CourseName":null,"ChapterId":1,"ChapterName":null,"SectionId":7,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which of the following is used for Data Analysis?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Lotus Smart suites\",\"IsCurrect\":false},{\"AnswerOption\":\"Word Perfect\",\"IsCurrect\":false},{\"AnswerOption\":\"Apple Numbers\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and C\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1384766892000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Lotus Smart suites","IsCurrect":false},{"AnswerOption":"Word Perfect","IsCurrect":false},{"AnswerOption":"Apple Numbers","IsCurrect":false},{"AnswerOption":"Both A and C","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":152,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":120,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":" ___________ is feature of spreadsheet.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"List AutoFill\",\"IsCurrect\":false},{\"AnswerOption\":\"Pivot Table\",\"IsCurrect\":false},{\"AnswerOption\":\"Drag and Drop\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385129823000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"List AutoFill","IsCurrect":false},{"AnswerOption":"Pivot Table","IsCurrect":false},{"AnswerOption":"Drag and Drop","IsCurrect":false},{"AnswerOption":"All the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":104,"CourseId":1,"CourseName":null,"ChapterId":1,"ChapterName":null,"SectionId":5,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"An interface for a user to communicate with the computer is known as:","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Operating systems\",\"IsCurrect\":true},{\"AnswerOption\":\"utility program\",\"IsCurrect\":false},{\"AnswerOption\":\"Compression software\",\"IsCurrect\":false},{\"AnswerOption\":\"Anti virus\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1384791682000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Operating systems","IsCurrect":true},{"AnswerOption":"utility program","IsCurrect":false},{"AnswerOption":"Compression software","IsCurrect":false},{"AnswerOption":"Anti virus","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":209,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":90,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"MS Word 2007 has close option  _____________","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"In the Open dialog box\",\"IsCurrect\":false},{\"AnswerOption\":\"Inside Office button\",\"IsCurrect\":true},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385145030000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"In the Open dialog box","IsCurrect":false},{"AnswerOption":"Inside Office button","IsCurrect":true},{"AnswerOption":"All of the above","IsCurrect":false},{"AnswerOption":"None of the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":21,"CourseId":1,"CourseName":null,"ChapterId":11,"ChapterName":null,"SectionId":205,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which of the following is used to connect networks in larger geographic areas, such as India or the world.?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Local Area Network (LAN)\",\"IsCurrect\":false},{\"AnswerOption\":\"Wide Area Network (WAN) \",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385112097000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Local Area Network (LAN)","IsCurrect":false},{"AnswerOption":"Wide Area Network (WAN) ","IsCurrect":true},{"AnswerOption":"Both A and B","IsCurrect":false},{"AnswerOption":"None of these","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":267,"CourseId":1,"CourseName":null,"ChapterId":7,"ChapterName":null,"SectionId":192,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which feature is used to change the existing fonts?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Format Fonts\",\"IsCurrect\":false},{\"AnswerOption\":\"Change Case\",\"IsCurrect\":false},{\"AnswerOption\":\"Replace Fonts\",\"IsCurrect\":true},{\"AnswerOption\":\"Toggle case\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385216268000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Format Fonts","IsCurrect":false},{"AnswerOption":"Change Case","IsCurrect":false},{"AnswerOption":"Replace Fonts","IsCurrect":true},{"AnswerOption":"Toggle case","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":272,"CourseId":1,"CourseName":null,"ChapterId":7,"ChapterName":null,"SectionId":197,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Page set up option is present under ____________.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Menu bar\",\"IsCurrect\":true},{\"AnswerOption\":\"Header bar\",\"IsCurrect\":false},{\"AnswerOption\":\"Footer bar\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385216873000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Menu bar","IsCurrect":true},{"AnswerOption":"Header bar","IsCurrect":false},{"AnswerOption":"Footer bar","IsCurrect":false},{"AnswerOption":"None of the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":35,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":78,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"New component in Word 2007 which groups similar tasks is known as:","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Tab\",\"IsCurrect\":false},{\"AnswerOption\":\"Ribbon\",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385076343000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Tab","IsCurrect":false},{"AnswerOption":"Ribbon","IsCurrect":true},{"AnswerOption":"Both A and B","IsCurrect":false},{"AnswerOption":"None of these","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":141,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":67,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":" Which tool is used to specify that the existing picture will show through your selection?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"The Rectangle Tool\",\"IsCurrect\":false},{\"AnswerOption\":\"The Transparent Option Tool\",\"IsCurrect\":true},{\"AnswerOption\":\"The Free-Form Select Tool\",\"IsCurrect\":false},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385096548000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"The Rectangle Tool","IsCurrect":false},{"AnswerOption":"The Transparent Option Tool","IsCurrect":true},{"AnswerOption":"The Free-Form Select Tool","IsCurrect":false},{"AnswerOption":"None of above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":93,"CourseId":1,"CourseName":null,"ChapterId":7,"ChapterName":null,"SectionId":70,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which program is used to type in the text quickly and easily?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Printer\",\"IsCurrect\":false},{\"AnswerOption\":\"Notepad\",\"IsCurrect\":true},{\"AnswerOption\":\"MS Paint\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385075365000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Printer","IsCurrect":false},{"AnswerOption":"Notepad","IsCurrect":true},{"AnswerOption":"MS Paint","IsCurrect":false},{"AnswerOption":"All the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":72,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":146,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"What will happen if user saves the excel file with .doc extension?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Content of file may loss\",\"IsCurrect\":true},{\"AnswerOption\":\"Content of file remain same\",\"IsCurrect\":false},{\"AnswerOption\":\"Not possible to excel file with .doc extension\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above possible\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385183997000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Content of file may loss","IsCurrect":true},{"AnswerOption":"Content of file remain same","IsCurrect":false},{"AnswerOption":"Not possible to excel file with .doc extension","IsCurrect":false},{"AnswerOption":"None of the above possible","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":24,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":104,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which of the following action pushes the text down to the next line, but does not create a new paragraph?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Press CTRL %2B TAB keys\",\"IsCurrect\":false},{\"AnswerOption\":\"Press SHIFT %2B ENTER keys\",\"IsCurrect\":true},{\"AnswerOption\":\"Press CTRL PLUS ALT %2B DELETE\",\"IsCurrect\":false},{\"AnswerOption\":\"Press SHIFT PLUS CTRL %2B TAB\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385077212000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Press CTRL %2B TAB keys","IsCurrect":false},{"AnswerOption":"Press SHIFT %2B ENTER keys","IsCurrect":true},{"AnswerOption":"Press CTRL PLUS ALT %2B DELETE","IsCurrect":false},{"AnswerOption":"Press SHIFT PLUS CTRL %2B TAB","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":130,"CourseId":1,"CourseName":null,"ChapterId":3,"ChapterName":null,"SectionId":29,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":" ______________ is not a secondary storage device.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Pen drives\",\"IsCurrect\":false},{\"AnswerOption\":\"ROM\",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385088171000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Pen drives","IsCurrect":false},{"AnswerOption":"ROM","IsCurrect":true},{"AnswerOption":"Both A and B","IsCurrect":false},{"AnswerOption":"None of the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":45,"CourseId":1,"CourseName":null,"ChapterId":11,"ChapterName":null,"SectionId":204,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which options are used to link the computers on a network?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Satellites\",\"IsCurrect\":false},{\"AnswerOption\":\"Bluetooth\",\"IsCurrect\":false},{\"AnswerOption\":\"Cables\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385111788000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Satellites","IsCurrect":false},{"AnswerOption":"Bluetooth","IsCurrect":false},{"AnswerOption":"Cables","IsCurrect":false},{"AnswerOption":"All the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":150,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":50,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"More than one files/folders could be copied from My Documents.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"true\",\"IsCurrect\":true},{\"AnswerOption\":\"false\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385129263000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"true","IsCurrect":true},{"AnswerOption":"false","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":102,"CourseId":1,"CourseName":null,"ChapterId":1,"ChapterName":null,"SectionId":3,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which are the physical components of a computer system?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Monitor\",\"IsCurrect\":false},{\"AnswerOption\":\"CPU\",\"IsCurrect\":false},{\"AnswerOption\":\"Keyboard\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1384791294000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Monitor","IsCurrect":false},{"AnswerOption":"CPU","IsCurrect":false},{"AnswerOption":"Keyboard","IsCurrect":false},{"AnswerOption":"All the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":208,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":89,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Look in option is present under:","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"In the Open dialog box\",\"IsCurrect\":true},{\"AnswerOption\":\"In the Close dialog box\",\"IsCurrect\":false},{\"AnswerOption\":\"In the footer\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385144929000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"In the Open dialog box","IsCurrect":true},{"AnswerOption":"In the Close dialog box","IsCurrect":false},{"AnswerOption":"In the footer","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":3,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":144,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which option is used to view the worksheet before printout is taken?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Modifying a worksheet\",\"IsCurrect\":false},{\"AnswerOption\":\"Print preview\",\"IsCurrect\":true},{\"AnswerOption\":\"Format Cells\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385105280000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Modifying a worksheet","IsCurrect":false},{"AnswerOption":"Print preview","IsCurrect":true},{"AnswerOption":"Format Cells","IsCurrect":false},{"AnswerOption":"None of these","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":108,"CourseId":1,"CourseName":null,"ChapterId":2,"ChapterName":null,"SectionId":10,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which is an input device?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Microphone (Mic.) for voice as input\",\"IsCurrect\":false},{\"AnswerOption\":\"Optical/magnetic Scanner\",\"IsCurrect\":false},{\"AnswerOption\":\"Touch Screen\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1384793283000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Microphone (Mic.) for voice as input","IsCurrect":false},{"AnswerOption":"Optical/magnetic Scanner","IsCurrect":false},{"AnswerOption":"Touch Screen","IsCurrect":false},{"AnswerOption":"All the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":60,"CourseId":1,"CourseName":null,"ChapterId":11,"ChapterName":null,"SectionId":210,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"___________ is type of protocol.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Simple mail transport Protocol (SMTP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Transmission control Protocol (TCP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Post office Protocol (POP)\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385112604000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Simple mail transport Protocol (SMTP)","IsCurrect":false},{"AnswerOption":"Transmission control Protocol (TCP)","IsCurrect":false},{"AnswerOption":"Post office Protocol (POP)","IsCurrect":false},{"AnswerOption":"All the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":166,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":58,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which buttons are present in add remove program window?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Change\",\"IsCurrect\":false},{\"AnswerOption\":\"Remove\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385135176000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Change","IsCurrect":false},{"AnswerOption":"Remove","IsCurrect":false},{"AnswerOption":"Both A and B","IsCurrect":true},{"AnswerOption":"None of these","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":107,"CourseId":1,"CourseName":null,"ChapterId":2,"ChapterName":null,"SectionId":9,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which of the following is hardware device?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Input Device\",\"IsCurrect\":false},{\"AnswerOption\":\"Output Device\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1384793068000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Input Device","IsCurrect":false},{"AnswerOption":"Output Device","IsCurrect":false},{"AnswerOption":"All of the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":213,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":97,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Spacebar is used as __________.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Creator\",\"IsCurrect\":false},{\"AnswerOption\":\"Separator\",\"IsCurrect\":true},{\"AnswerOption\":\" All of the above\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385145833000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Creator","IsCurrect":false},{"AnswerOption":"Separator","IsCurrect":true},{"AnswerOption":" All of the above","IsCurrect":false},{"AnswerOption":"None of the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":165,"CourseId":1,"CourseName":null,"ChapterId":9,"ChapterName":null,"SectionId":221,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"_______ is computer program which interfere with computer operation.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Antivirus Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Computer viruses\",\"IsCurrect\":true},{\"AnswerOption\":\"Security essentials\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385135104000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Antivirus Software","IsCurrect":false},{"AnswerOption":"Computer viruses","IsCurrect":true},{"AnswerOption":"Security essentials","IsCurrect":false},{"AnswerOption":"None of these","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":270,"CourseId":1,"CourseName":null,"ChapterId":7,"ChapterName":null,"SectionId":195,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Shortcut key for spell check is ___.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"F1\",\"IsCurrect\":false},{\"AnswerOption\":\"F10\",\"IsCurrect\":false},{\"AnswerOption\":\"F11\",\"IsCurrect\":false},{\"AnswerOption\":\"F7\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385216633000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"F1","IsCurrect":false},{"AnswerOption":"F10","IsCurrect":false},{"AnswerOption":"F11","IsCurrect":false},{"AnswerOption":"F7","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":260,"CourseId":1,"CourseName":null,"ChapterId":7,"ChapterName":null,"SectionId":174,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"It is necessary to select blank presentation to create slide show? ","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Yes\",\"IsCurrect\":false},{\"AnswerOption\":\"No\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385213604000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Yes","IsCurrect":false},{"AnswerOption":"No","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":212,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":94,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"User could type text in:","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Text area\",\"IsCurrect\":true},{\"AnswerOption\":\"Footer area\",\"IsCurrect\":false},{\"AnswerOption\":\"Header area\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385145688000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Text area","IsCurrect":true},{"AnswerOption":"Footer area","IsCurrect":false},{"AnswerOption":"Header area","IsCurrect":false},{"AnswerOption":"None of the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":129,"CourseId":1,"CourseName":null,"ChapterId":3,"ChapterName":null,"SectionId":28,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Read Only Memory (ROM) is:","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Primary storage memory.\",\"IsCurrect\":false},{\"AnswerOption\":\"Main Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Non-volite memory\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385087818000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Primary storage memory.","IsCurrect":false},{"AnswerOption":"Main Memory","IsCurrect":false},{"AnswerOption":"Non-volite memory","IsCurrect":false},{"AnswerOption":"All of the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":22,"CourseId":1,"CourseName":null,"ChapterId":7,"ChapterName":null,"SectionId":180,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Slide sorter view is use to _____.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Set order of the slide\",\"IsCurrect\":false},{\"AnswerOption\":\"Sort the slide\",\"IsCurrect\":false},{\"AnswerOption\":\"Add special effects\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385189145000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Set order of the slide","IsCurrect":false},{"AnswerOption":"Sort the slide","IsCurrect":false},{"AnswerOption":"Add special effects","IsCurrect":false},{"AnswerOption":"All of the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":128,"CourseId":1,"CourseName":null,"ChapterId":2,"ChapterName":null,"SectionId":21,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Speakers are:","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Output device\",\"IsCurrect\":true},{\"AnswerOption\":\"Input Device\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385087077000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Output device","IsCurrect":true},{"AnswerOption":"Input Device","IsCurrect":false},{"AnswerOption":"Both A and B","IsCurrect":false},{"AnswerOption":"None of the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":233,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":128,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"By default, workbook have ____ worksheets. ","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"5\",\"IsCurrect\":false},{\"AnswerOption\":\"6\",\"IsCurrect\":false},{\"AnswerOption\":\"3\",\"IsCurrect\":true},{\"AnswerOption\":\"1\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385202855000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"5","IsCurrect":false},{"AnswerOption":"6","IsCurrect":false},{"AnswerOption":"3","IsCurrect":true},{"AnswerOption":"1","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":185,"CourseId":1,"CourseName":null,"ChapterId":9,"ChapterName":null,"SectionId":233,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"_______ is web browser provided by Microsoft.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Google chrome\",\"IsCurrect\":false},{\"AnswerOption\":\"Internet Explorer\",\"IsCurrect\":true},{\"AnswerOption\":\"Apple Safari\",\"IsCurrect\":false},{\"AnswerOption\":\"Mozilla Firefox\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385139754000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Google chrome","IsCurrect":false},{"AnswerOption":"Internet Explorer","IsCurrect":true},{"AnswerOption":"Apple Safari","IsCurrect":false},{"AnswerOption":"Mozilla Firefox","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":279,"CourseId":1,"CourseName":null,"ChapterId":11,"ChapterName":null,"SectionId":215,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"The file and printer sharing allows other computers on a network to access resources on your computer by using a _______.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Workstations\",\"IsCurrect\":false},{\"AnswerOption\":\"Microsoft network\",\"IsCurrect\":true},{\"AnswerOption\":\"Printer\",\"IsCurrect\":false},{\"AnswerOption\":\"Post office Protocol \",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385220640000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Workstations","IsCurrect":false},{"AnswerOption":"Microsoft network","IsCurrect":true},{"AnswerOption":"Printer","IsCurrect":false},{"AnswerOption":"Post office Protocol ","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":268,"CourseId":1,"CourseName":null,"ChapterId":7,"ChapterName":null,"SectionId":193,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"How to activate the textbox?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"By moving it\",\"IsCurrect\":false},{\"AnswerOption\":\"By clicking on it\",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385216404000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"By moving it","IsCurrect":false},{"AnswerOption":"By clicking on it","IsCurrect":true},{"AnswerOption":"Both A and B","IsCurrect":false},{"AnswerOption":"None of the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":80,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":93,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Shortcut key for cut:","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"CTRL %2B X\",\"IsCurrect\":true},{\"AnswerOption\":\"CTRL %2B V\",\"IsCurrect\":false},{\"AnswerOption\":\"CTRL %2B P\",\"IsCurrect\":false},{\"AnswerOption\":\"CTRL %2B O\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385273560000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"CTRL %2B X","IsCurrect":true},{"AnswerOption":"CTRL %2B V","IsCurrect":false},{"AnswerOption":"CTRL %2B P","IsCurrect":false},{"AnswerOption":"CTRL %2B O","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":32,"CourseId":1,"CourseName":null,"ChapterId":10,"ChapterName":null,"SectionId":249,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"_______ is one kind of social media website.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"News\",\"IsCurrect\":false},{\"AnswerOption\":\"Networking\",\"IsCurrect\":false},{\"AnswerOption\":\"Photo and Video Sharing\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385200305000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"News","IsCurrect":false},{"AnswerOption":"Networking","IsCurrect":false},{"AnswerOption":"Photo and Video Sharing","IsCurrect":false},{"AnswerOption":"All the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":11,"CourseId":1,"CourseName":null,"ChapterId":1,"ChapterName":null,"SectionId":5,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"_______________ programs bridge the gap between the functionality of operating systems and the needs of the users.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Operating System\",\"IsCurrect\":false},{\"AnswerOption\":\"Utility\",\"IsCurrect\":true},{\"AnswerOption\":\"System Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Application Software\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1384766572000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Operating System","IsCurrect":false},{"AnswerOption":"Utility","IsCurrect":true},{"AnswerOption":"System Software","IsCurrect":false},{"AnswerOption":"Application Software","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":257,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":168,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Edit picture feature option is present under _______ tab.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Home\",\"IsCurrect\":false},{\"AnswerOption\":\"Insert\",\"IsCurrect\":false},{\"AnswerOption\":\"View\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385212533000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Home","IsCurrect":false},{"AnswerOption":"Insert","IsCurrect":false},{"AnswerOption":"View","IsCurrect":false},{"AnswerOption":"All of the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":68,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":119,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which is widely used spreadsheet applications?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Microsoft word\",\"IsCurrect\":false},{\"AnswerOption\":\"Microsoft excel\",\"IsCurrect\":true},{\"AnswerOption\":\"Microsoft  PowerPoint\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385104535000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Microsoft word","IsCurrect":false},{"AnswerOption":"Microsoft excel","IsCurrect":true},{"AnswerOption":"Microsoft  PowerPoint","IsCurrect":false},{"AnswerOption":"All the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":174,"CourseId":1,"CourseName":null,"ChapterId":11,"ChapterName":null,"SectionId":205,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which of these statements is true ?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Servers tend to be more powerful than workstations.\",\"IsCurrect\":false},{\"AnswerOption\":\"Workstations tend to be more powerful than servers.\",\"IsCurrect\":false},{\"AnswerOption\":\"Workstations are any device through which human user can interact to utilize the network resources.\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and C\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385137202000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Servers tend to be more powerful than workstations.","IsCurrect":false},{"AnswerOption":"Workstations tend to be more powerful than servers.","IsCurrect":false},{"AnswerOption":"Workstations are any device through which human user can interact to utilize the network resources.","IsCurrect":false},{"AnswerOption":"Both A and C","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":126,"CourseId":1,"CourseName":null,"ChapterId":2,"ChapterName":null,"SectionId":14,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"How user provides input through touch screen devices?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Electronic buttons \",\"IsCurrect\":false},{\"AnswerOption\":\"Light pen\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385086018000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Electronic buttons ","IsCurrect":false},{"AnswerOption":"Light pen","IsCurrect":false},{"AnswerOption":"Both A and B","IsCurrect":true},{"AnswerOption":"None of these","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":131,"CourseId":1,"CourseName":null,"ChapterId":3,"ChapterName":null,"SectionId":31,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"CD stands for ?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Compact Disk\",\"IsCurrect\":true},{\"AnswerOption\":\"Digital Versatile Disc\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385089210000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Compact Disk","IsCurrect":true},{"AnswerOption":"Digital Versatile Disc","IsCurrect":false},{"AnswerOption":"Both A and B","IsCurrect":false},{"AnswerOption":"None of the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":236,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":133,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Each cell of a worksheet has ________.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Multiple referance \",\"IsCurrect\":false},{\"AnswerOption\":\"Unique referance\",\"IsCurrect\":true},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385206693000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Multiple referance ","IsCurrect":false},{"AnswerOption":"Unique referance","IsCurrect":true},{"AnswerOption":"All of the above","IsCurrect":false},{"AnswerOption":"None of the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":188,"CourseId":1,"CourseName":null,"ChapterId":7,"ChapterName":null,"SectionId":73,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which option allows user to browse an existing document?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Open Window\",\"IsCurrect\":true},{\"AnswerOption\":\"Save Window\",\"IsCurrect\":false},{\"AnswerOption\":\"Print Window\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385140286000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Open Window","IsCurrect":true},{"AnswerOption":"Save Window","IsCurrect":false},{"AnswerOption":"Print Window","IsCurrect":false},{"AnswerOption":"None of these","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":163,"CourseId":1,"CourseName":null,"ChapterId":7,"ChapterName":null,"SectionId":178,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which option helps in creation of presentation slides?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Normal\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide Sorter\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide Show\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385133843000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Normal","IsCurrect":false},{"AnswerOption":"Slide Sorter","IsCurrect":false},{"AnswerOption":"Slide Show","IsCurrect":false},{"AnswerOption":"All the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":115,"CourseId":1,"CourseName":null,"ChapterId":2,"ChapterName":null,"SectionId":20,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which is type of printer?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Laser Printer\",\"IsCurrect\":false},{\"AnswerOption\":\"Ink Jet Printer\",\"IsCurrect\":false},{\"AnswerOption\":\"Dot Matrix Printer\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1384796928000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Laser Printer","IsCurrect":false},{"AnswerOption":"Ink Jet Printer","IsCurrect":false},{"AnswerOption":"Dot Matrix Printer","IsCurrect":false},{"AnswerOption":"All the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":220,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":107,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"User could give borders to __________","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Paragraph\",\"IsCurrect\":false},{\"AnswerOption\":\"Text\",\"IsCurrect\":false},{\"AnswerOption\":\"Table\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385200071000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Paragraph","IsCurrect":false},{"AnswerOption":"Text","IsCurrect":false},{"AnswerOption":"Table","IsCurrect":false},{"AnswerOption":"All of the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":277,"CourseId":1,"CourseName":null,"ChapterId":11,"ChapterName":null,"SectionId":208,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"______is a set of rules to govern the data transfer between the devices.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Local Area Network\",\"IsCurrect\":false},{\"AnswerOption\":\"Workstations\",\"IsCurrect\":false},{\"AnswerOption\":\"Protocol\",\"IsCurrect\":true},{\"AnswerOption\":\"Local Area Network (LAN)\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385219894000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Local Area Network","IsCurrect":false},{"AnswerOption":"Workstations","IsCurrect":false},{"AnswerOption":"Protocol","IsCurrect":true},{"AnswerOption":"Local Area Network (LAN)","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":89,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":167,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Clip art includes ____________.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Sound\",\"IsCurrect\":false},{\"AnswerOption\":\"Animation\",\"IsCurrect\":false},{\"AnswerOption\":\"Movies\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385187234000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Sound","IsCurrect":false},{"AnswerOption":"Animation","IsCurrect":false},{"AnswerOption":"Movies","IsCurrect":false},{"AnswerOption":"All of the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":41,"CourseId":1,"CourseName":null,"ChapterId":9,"ChapterName":null,"SectionId":225,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"The exception tab lets user to add exceptions to permit inbound traffic.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385198169000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"True","IsCurrect":true},{"AnswerOption":"False","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":146,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":96,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which short cut keys could be used to move through the document?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"HOME\",\"IsCurrect\":false},{\"AnswerOption\":\"CTRL %2B HOME\",\"IsCurrect\":false},{\"AnswerOption\":\"CTRL %2B END\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385102071000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"HOME","IsCurrect":false},{"AnswerOption":"CTRL %2B HOME","IsCurrect":false},{"AnswerOption":"CTRL %2B END","IsCurrect":false},{"AnswerOption":"All of the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":252,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":160,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Is it necessary to select all the row and column present in workbook to draw a chart?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"No\",\"IsCurrect\":true},{\"AnswerOption\":\"Yes\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385211021000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"No","IsCurrect":true},{"AnswerOption":"Yes","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":204,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":85,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which option is present under Office button?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Create new documents\",\"IsCurrect\":false},{\"AnswerOption\":\"Open existing documents\",\"IsCurrect\":false},{\"AnswerOption\":\"Save documents in Word\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385144381000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Create new documents","IsCurrect":false},{"AnswerOption":"Open existing documents","IsCurrect":false},{"AnswerOption":"Save documents in Word","IsCurrect":false},{"AnswerOption":"All of the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":15,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":138,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Is it possible to move cell without moving it\u0027s data?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"True\",\"IsCurrect\":false},{\"AnswerOption\":\"False\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385182248000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"True","IsCurrect":false},{"AnswerOption":"False","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":293,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":132,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"What is the short cut key for undo?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Ctrl %2B X\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B Z\",\"IsCurrect\":true},{\"AnswerOption\":\"Ctrl %2B A\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B P\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385307887000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Ctrl %2B X","IsCurrect":false},{"AnswerOption":"Ctrl %2B Z","IsCurrect":true},{"AnswerOption":"Ctrl %2B A","IsCurrect":false},{"AnswerOption":"Ctrl %2B P","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":210,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":92,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Exit command is present _________ in MS-Word.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Inside Office button\",\"IsCurrect\":true},{\"AnswerOption\":\"Inside header\",\"IsCurrect\":false},{\"AnswerOption\":\"Inside footer\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385145290000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Inside Office button","IsCurrect":true},{"AnswerOption":"Inside header","IsCurrect":false},{"AnswerOption":"Inside footer","IsCurrect":false},{"AnswerOption":"All of the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":225,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":115,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Header and footer option are present under _____________.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"File\",\"IsCurrect\":false},{\"AnswerOption\":\"View\",\"IsCurrect\":false},{\"AnswerOption\":\"Insert\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385201226000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"File","IsCurrect":false},{"AnswerOption":"View","IsCurrect":false},{"AnswerOption":"Insert","IsCurrect":true},{"AnswerOption":"None of the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":177,"CourseId":1,"CourseName":null,"ChapterId":11,"ChapterName":null,"SectionId":211,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"____________ is based on the client/server principles.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Post office Protocol (POP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Hyper Text Transfer Protocol (HTTP)\",\"IsCurrect\":true},{\"AnswerOption\":\"Internet Protocol (IP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Post office Protocol (POP)\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385138338000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Post office Protocol (POP)","IsCurrect":false},{"AnswerOption":"Hyper Text Transfer Protocol (HTTP)","IsCurrect":true},{"AnswerOption":"Internet Protocol (IP)","IsCurrect":false},{"AnswerOption":"Post office Protocol (POP)","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":94,"CourseId":1,"CourseName":null,"ChapterId":7,"ChapterName":null,"SectionId":183,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"_____ layouts are available while creating new slide show.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"3\",\"IsCurrect\":false},{\"AnswerOption\":\"5\",\"IsCurrect\":false},{\"AnswerOption\":\"9\",\"IsCurrect\":true},{\"AnswerOption\":\"8\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385189652000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"3","IsCurrect":false},{"AnswerOption":"5","IsCurrect":false},{"AnswerOption":"9","IsCurrect":true},{"AnswerOption":"8","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":151,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":119,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which is widely used spreadsheet applications?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Microsoft word\",\"IsCurrect\":false},{\"AnswerOption\":\"Microsoft excel\",\"IsCurrect\":true},{\"AnswerOption\":\"Microsoft  PowerPoint\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385129735000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Microsoft word","IsCurrect":false},{"AnswerOption":"Microsoft excel","IsCurrect":true},{"AnswerOption":"Microsoft  PowerPoint","IsCurrect":false},{"AnswerOption":"All the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":103,"CourseId":1,"CourseName":null,"ChapterId":1,"ChapterName":null,"SectionId":4,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which is type of software?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Application software\",\"IsCurrect\":false},{\"AnswerOption\":\"System Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Operating system\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1384791447000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Application software","IsCurrect":false},{"AnswerOption":"System Software","IsCurrect":false},{"AnswerOption":"Operating system","IsCurrect":false},{"AnswerOption":"All of the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":198,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":77,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"MS Word allows user to:","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Creating a word document\",\"IsCurrect\":false},{\"AnswerOption\":\"Create table of contents\",\"IsCurrect\":false},{\"AnswerOption\":\"Print out multiple pages on a single sheet of paper\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]","ContentVersion":"1","DateCreated":"\/Date(1385143401000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Creating a word document","IsCurrect":false},{"AnswerOption":"Create table of contents","IsCurrect":false},{"AnswerOption":"Print out multiple pages on a single sheet of paper","IsCurrect":false},{"AnswerOption":"All of the above","IsCurrect":true}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":9,"CourseId":1,"CourseName":null,"ChapterId":4,"ChapterName":null,"SectionId":35,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Which is not a Microsoft operating system?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Windows 7\",\"IsCurrect\":false},{\"AnswerOption\":\"Windows XP\",\"IsCurrect\":false},{\"AnswerOption\":\"Solaris\",\"IsCurrect\":true},{\"AnswerOption\":\"Windows 8\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1384774577000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Windows 7","IsCurrect":false},{"AnswerOption":"Windows XP","IsCurrect":false},{"AnswerOption":"Solaris","IsCurrect":true},{"AnswerOption":"Windows 8","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":255,"CourseId":1,"CourseName":null,"ChapterId":6,"ChapterName":null,"SectionId":163,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Elements of chart are also moved when we move chart.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"true\",\"IsCurrect\":true},{\"AnswerOption\":\"false\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385212022000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"true","IsCurrect":true},{"AnswerOption":"false","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":66,"CourseId":1,"CourseName":null,"ChapterId":5,"ChapterName":null,"SectionId":56,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"Programs and components are managed using ___________________","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Install Hardware\",\"IsCurrect\":false},{\"AnswerOption\":\"Change or remove software\",\"IsCurrect\":false},{\"AnswerOption\":\"Add or remove programs\",\"IsCurrect\":true},{\"AnswerOption\":\"Install Software\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385074346000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Install Hardware","IsCurrect":false},{"AnswerOption":"Change or remove software","IsCurrect":false},{"AnswerOption":"Add or remove programs","IsCurrect":true},{"AnswerOption":"Install Software","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":55,"CourseId":1,"CourseName":null,"ChapterId":7,"ChapterName":null,"SectionId":177,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"How user could open the Power Point presentation?","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Copy it\",\"IsCurrect\":false},{\"AnswerOption\":\"Delete it\",\"IsCurrect\":false},{\"AnswerOption\":\"Double Click on it\",\"IsCurrect\":true},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385188778000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Copy it","IsCurrect":false},{"AnswerOption":"Delete it","IsCurrect":false},{"AnswerOption":"Double Click on it","IsCurrect":true},{"AnswerOption":"All of the above","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0},{"Id":161,"CourseId":1,"CourseName":null,"ChapterId":7,"ChapterName":null,"SectionId":172,"SectionName":null,"GroupNo":1,"Complexity":1,"QuestionText":"________ provides ideas/templates for variety of presentation types.","IsQuestionOptionPresent":false,"QuestionOption":"[]","AnswerOption":"[{\"AnswerOption\":\"Installed templates\",\"IsCurrect\":true},{\"AnswerOption\":\"Design templates\",\"IsCurrect\":false},{\"AnswerOption\":\"Blank presentations\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide Layout\",\"IsCurrect\":false}]","ContentVersion":"1","DateCreated":"\/Date(1385131946000)\/","CreatedBy":"admin","QuestionOptionList":[],"AnswerOptionList":[{"AnswerOption":"Installed templates","IsCurrect":true},{"AnswerOption":"Design templates","IsCurrect":false},{"AnswerOption":"Blank presentations","IsCurrect":false},{"AnswerOption":"Slide Layout","IsCurrect":false}],"IsAnsGiven":false,"IsCorrect":false,"AnswerText":null,"CorrectAnswer":null,"IsMultiTrueAnswer":false,"MigratedFinalQuizId":0}] };

    if (lastId > 0) {
        //CallHandler("Handler/GetFinalQuiz.ashx", function (result) { onCompletePopulateFinalQuiz(result, lastId); });
        CallHandler("Handler/GetFinalQuiz.ashx?testid=" + testId + "&courseid=" + courseId, function (result) { onCompletePopulateFinalQuiz(result, lastId); });
    }
    else {
        onCompletePopulateFinalQuiz(result, lastId);
    }
    //CallHandler("Handler/GetFinalQuiz.ashx?testid=" + testId + "&courseid=" + courseId, function (result) { onCompletePopulateFinalQuiz(result, lastId); });
}

var Final_Quiz = [];
var Final_Quiz_Counter = 0;

function onCompletePopulateFinalQuiz(result, lastId) {
    Final_Quiz = result.FinalQuiz;
    $("#loaderMainContain").remove();
    if (result.Status == "Ok") {
        if (result.FinalQuiz != null) {
            if (result.FinalQuiz.length > 0) {
                if (Final_Quiz_Counter < result.FinalQuiz.length) {
                    for (; Final_Quiz_Counter < result.FinalQuiz.length; Final_Quiz_Counter++) {
                        if (lastId == 0) {
                            PopulateFinalQuiz(result.FinalQuiz[Final_Quiz_Counter], Final_Quiz_Counter);
                            Final_Quiz_Counter++;
                            break;
                        }
                            //if (result.FinalQuiz[Final_Quiz_Counter].Id > lastId) {
                        else if (result.FinalQuiz[Final_Quiz_Counter].Id == lastId) {
                            Final_Quiz_Counter++;
                            PopulateFinalQuiz(result.FinalQuiz[Final_Quiz_Counter], Final_Quiz_Counter);
                            Final_Quiz_Counter++;
                            break;
                        }
                        //Final_Quiz_Counter++;
                    }
                }
            }
            else {
                $(".content-main").css("text-align", "center");
                $(".content-main").html("Sorry! this course does not have any quiz. Please <a target='_top' href='" + BASE_URL + "Dashboard.aspx'>click here</a> to go to home page.");
            }
        }
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            //alert(result.Message); 
            console.log(result.Message);
        }
    }
}

function PopulateFinalQuiz(quiz, counter) {
    var percent = parseFloat(counter) / Final_Quiz.length * 100;
    PopulatePercentage(percent);
    // adding hint link in the Header
    $("#hintlink").remove();
    var hintLink = $("<div/>");
    $(hintLink).attr("style", "float:right");
    $(hintLink).attr("id", "hintlink");
    //getting section and chapter id

    var path = BASE_URL + "HintPage.aspx?chapterId=" + quiz.ChapterId + "&sectionId=" + quiz.SectionId + "&courseid=" + quiz.CourseId;
    $(hintLink).html("<i class='icon-white icon-book'></i><a href='" + path + "' style='margin-left: 5px; color: #FFF;' target='_blank' title='click here to get help'>Hint</a>");
    $(".content-summary").append(hintLink);
    //------------------------------
    $(".content-main").html("");

    var status = $("<div/>");
    $(status).attr("class", "ErrorContainer");
    $(status).attr("id", "status_" + counter);
    $(status).hide();

    $(".content-main").append(status);
    //alert(1);
    // creating maincontainer
    var maincontainer = $("<div/>");
    //$(maincontainer).attr("style", "width: 100%; float: left;");
    $(maincontainer).attr("class", "quizContainer");
    //end

    // creating question text container
    var questionText = $("<div/>");
    $(questionText).attr("id", "QuesionText_" + counter);
    $(questionText).attr("class", "questionText");
    $(questionText).attr("quesId", quiz.Id);
    $(questionText).attr("isQuizMultiTure", quiz.IsMultiTrueAnswer);
    $(questionText).html("Question " + (parseInt(counter) + 1) + " : " + quiz.QuestionText);

    $(maincontainer).append(questionText);
    // end

    // creating question option text
    var questionOption = $("<div/>");
    $(questionOption).attr("id", "QuestionOption_" + counter);
    $(questionOption).attr("class", "questionText");

    for (var i = 0; i < quiz.QuestionOptionList.length; i++) {

        var div = $("<div/>");
        $(div).attr("id", "QO" + i);
        $(div).html((i + 1) + ".  " + quiz.QuestionOptionList[i].QuestionOption);

        $(questionOption).append(div);
    }

    $(maincontainer).append(questionOption);
    // end

    // creating answer option text
    var answerOption = $("<div/>");
    $(answerOption).attr("id", "AnswerOption_" + counter);
    $(answerOption).attr("class", "answerOption");

    var html = "";

    for (var i = 0; i < quiz.AnswerOptionList.length; i++) {

        var s = quiz.AnswerOptionList[i].AnswerOption.replace("%2B", "+");
        s = s.replace("%2B", "+");

        var div = $("<div/>");

        $(div).attr("id", "AO" + i);
        //$(div).attr("class", "radioDiv");

        if (!quiz.IsMultiTrueAnswer) {
            //$(div).html(s + "<input type='radio' id='Ans_" + i + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "'/>");
            html += "<input type='radio' id='Ans_" + i + "_" + counter + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "'/><label for='Ans_" + i + "_" + counter + "' style='float: left; margin-left: 5px;width:95%;'>" + s + "</label>";
        }
        else {
            html += "<input type='checkbox' id='Ans_" + i + "_" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "'/><label for='Ans_" + i + "_" + counter + "' style='float: left; margin-left: 5px;width:95%;'>" + s + "</label>";
        }

        //$(answerOption).append(div);
    }

    $(answerOption).html(html);

    $(maincontainer).append(answerOption);
    //end

    // creating submit button in populate final quiz function
    var btnField = $("<div/>");
    $(btnField).attr("id", "btnField_" + counter);
    $(btnField).attr("style", "margin-top: 25px; width: 100%; float: left;");

    var btn = $("<div/>");
    $(btn).attr("id", "btn_" + counter);
    $(btn).attr("class", "btn btn-primary");
    $(btn).html("Next");
    $(btn).attr("onclick", "SaveFinalQuiz(" + counter + ")");
    $(btnField).append(btn);

    var status = $("<div/>");
    $(status).attr("id", "status_" + counter);
    $(btnField).append(status);

    var loader = $("<div/>");
    $(loader).attr("id", "loader_" + counter);
    $(loader).html("<img src='" + BASE_URL + "static/images/waiting-loader.gif' />");
    $(loader).hide();
    $(btnField).append(loader);

    $(maincontainer).append(btnField);
    //end
    $(".content-main").append(maincontainer);
    //alert($("#QuesionText").html());
}

function SaveFinalQuiz(counter) {

    var userAnswer = "";
    var currectAnswer = "";
    var isCurrect;
    var isChecked;

    var correctAnsCount = 0;
    var userSelectCorrectAns = 0;
    var userSelectAns = 0;

    var isMultiTrue = $("#QuesionText_" + counter).attr("isquizmultiture");

    if (isMultiTrue == "false") {
        $("#AnswerOption_" + counter).find(".ansRadio").each(function () {

            if ($(this).prop("checked")) {
                userAnswer = $(this).attr("answer");
                isCurrect = $(this).attr("iscurrect");
                isChecked = true;
            }

            if ($(this).attr("iscurrect") == 'true') {
                currectAnswer = $(this).attr("answer");
            }
        });
    }
    else {
        $("#AnswerOption_" + counter).find(".ansRadio").each(function () {

            if ($(this).prop("checked")) {
                userAnswer += $(this).attr("answer") + ",";
                isCurrect = $(this).attr("iscurrect");
                isChecked = true;
                userSelectAns++;
                if (isCurrect == "true")
                    userSelectCorrectAns++;
            }

            if ($(this).attr("iscurrect") == 'true') {
                currectAnswer += $(this).attr("answer") + ",";
                correctAnsCount++;
            }
        });

        isCurrect = false;
        if (userSelectAns == userSelectCorrectAns && userSelectAns == correctAnsCount && userSelectCorrectAns == correctAnsCount) {
            isCurrect = true;
        }

        if (userAnswer.length > 0)
            userAnswer = userAnswer.substring(userAnswer.length - 1, 0);

        if (currectAnswer.length > 0)
            currectAnswer = currectAnswer.substring(currectAnswer.length - 1, 0);
    }

    var questionId = $("#QuesionText_" + counter).attr("quesId");
    var testId = $(".content-container").attr("testid");
    if (!isChecked) {
        //alert("Please checked answer");
        $("#status_" + counter).html("Please select your answer!");
        $("#status_" + counter).show();
    }
    else {
        var path = "Handler/SaveFinalQuiz.ashx?questionId=" + questionId + "&useranswer=" + userAnswer + "&iscurrect=" + isCurrect + "&testid=" + testId;
        //CallHandler(path, onFinalQuizSavedSuccessfully);
        CallHandler(path, function (result) { onFinalQuizSavedSuccessfully(result, counter); });
        $("#btn_" + counter).hide();
        $("#loader_" + counter).show();

    }
}

function onFinalQuizSavedSuccessfully(result, counter) {
    $("#btn_" + counter).show();
    $("#loader_" + counter).hide();
    if (result.Status == "Ok") {
        if (Final_Quiz != null) {
            if (Final_Quiz.length > 0) {
                if (Final_Quiz_Counter < Final_Quiz.length) {
                    PopulateFinalQuiz(Final_Quiz[Final_Quiz_Counter], Final_Quiz_Counter);
                    //ShowCompleteFinalQuizMessage();
                    Final_Quiz_Counter++;
                }
                else if (Final_Quiz_Counter == Final_Quiz.length) {
                    var percent = parseFloat(Final_Quiz_Counter) / Final_Quiz.length * 100;
                    PopulatePercentage(percent);
                    ShowCompleteFinalQuizMessage();
                }
            }
        }
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            //alert(result.Message); 
            console.log(result.Message);
        }
    }
}

function ShowCompleteFinalQuizMessage() {

    $(".content-main").html("");

    var status = $("<div/>");
    $(status).attr("class", "ErrorContainer");
    $(status).attr("style", "background-color: #C3FDB8; font-size: large;text-align: center;");
    $(status).attr("id", "Comlete");
    $(status).html("Congratulations! you have completed your test. Please wait for some time we will give your feedback shortly. <a href='" + BASE_URL + "Dashboard.aspx' target='_top'> Click here </a>to go Dashboard.");

    $(".content-main").append(status);

    getQueryStrings();
    var qs = getQueryStrings();
    var testId = qs["testid"];

    // To Do insert a record for test completion
    var path = "Handler/SaveUserTestStatus.ashx?testid=" + testId;
    CallHandler(path, onCompleteTest);
}

function onCompleteTest() { }


function UpdateChapterTime(chpId, secId) {

    /*var intverval = setInterval(function () {
        UpdateTime(chpId, secId)
    }, 10000);*/
}

function UpdateTime(chpId, secId) {
    var path = "Handler/UpdateChapterTime.ashx?chapterid=" + chpId + "&sectionid=" + secId;

    //CallHandler(path, onComUdateTime);
}

function onComUdateTime() { }

function onUpdateUserChapterStatusSuccess(result) {
    if (result.Status == "Ok") {
        var chId = $(".content-summary").attr("chpId");
        var secId = $(".content-summary").attr("secId");

        var chpObj = GetChapterObject(chId);
        var secObj = GetChapterSectionObject(chId, secId);

        var title = secObj.Title + " || " + chpObj.Title;
        //var link = secObj.Link + " || " + chpObj.Link;
        //var link = "HintPage.aspx?chapterId=" + chId + "&sectionId=" + secId + " || " + "HintPage.aspx?chapterId=" + chId + "&sectionId=" + secId;
        var link = "chapterId=" + chId + " sectionId=" + secId + "||" + "chapterId=" + chId + " sectionId=" + secId;
        var activit = { "Title": escape(title), "Link": link, "ActivityType": "1" };
        /*var activit = [];
        activit.push({ "Title": title, "Link": link, "ActivityType": "1" });*/
        SaveUserActivity(activit);
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            //alert(result.Message); 
            console.log(result.Message);
        }
    }
}

function UpdateUserChapterStatus(chpId, secId) {
    var path = "Handler/UpdateUserChapterStatus.ashx?chapterid=" + chpId + "&sectionid=" + secId + "&contentVersion=1";

    CallHandler(path, onUpdateUserChapterStatusSuccess);
}

function SaveTypingStats(level, timespan, accuracy, grossWPM, netWPM) {
    var path = "Handler/SaveTypingStats.ashx?level=" + level + "&timespan=" + timespan + "&accuracy=" + accuracy + "&grossWPM=" + grossWPM + "&netWPM=" + netWPM;
    CallHandler(path, function (result) { onSaveTypingSuccess(result, level); });
}

function onSaveTypingSuccess(result, level) {
    if (result.Status == "Ok") {
        var activit = { "Title": "Level " + level, "Link": "TypingTutorial.aspx", "ActivityType": "3" };
        /*var activit = [];
        activit.push({ "Title": "Level " + level, "Link": "TypingTutorial.aspx", "ActivityType": "3" });*/
        SaveUserActivity(activit);
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            //alert("Your Session is Expire you are redirect to login page.");
            //parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            //alert(result.Message); 
            console.log(result.Message);
        }
    }
}

function leftNavigationClick1(link) {
    var chId = $(".content-summary").attr("chpId");
    var secId = $(".content-summary").attr("secId");

    //var ref = document.location.href.indexOf(BASE_URL);
    var navSecId;
    var ref = 1;
    if (ref >= 0) {
        try {
            //var values = document.location.href.substring(BASE_URL.length).split("/");
            var values = link.split("/");
            chapterId = values[2];
            navSecId = GetChapterSectionID(chId, values[3]);
        }
        catch (e) {
            alert(e);
        }
    }

    if (navSecId > secId) {
        var path = "Handler/GetUserQuizResStatus.ashx?chapterid=" + chId + "&sectionid=" + secId;
        CallHandler(path, function (result) { onSuccessleftNavigationClick(result, link); });
    }
    else {
        document.location = BASE_URL + link;
    }
}

function leftNavigationClick(newChpId, navSecId) {
    var chId = $(".content-summary").attr("chpId");
    var secId = $(".content-summary").attr("secId");

    if (navSecId > secId) {
        sectionObject = GetChapterSectionObject(newChpId, navSecId);
        var link = sectionObject.Link + "?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
        var path = "Handler/GetUserQuizResStatus.ashx?chapterid=" + chId + "&sectionid=" + secId;
        CallHandler(path, function (result) { onSuccessleftNavigationClick(result, link); });
    }
    else if (navSecId == secId) {
        sectionObject = GetChapterSectionObject(newChpId, secId);
        var link = sectionObject.Link + "?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
        document.location = BASE_URL + link;
    }
    else {
        sectionObject = GetChapterSectionObject(newChpId, navSecId);
        var link = sectionObject.Link + "?courseid=" + courseId + "&chpid=" + sectionObject.ChapterId + "&secid=" + sectionObject.Id;
        document.location = BASE_URL + link;
    }
}


function onSuccessleftNavigationClick(result, link) {
    //alert(result.Status);
    //alert(link);

    if (result.Status == "Ok") {

        if (result.IsUserFilledAllQuiz) {
            document.location = BASE_URL + link;
        }
        else {

            $("#qeusError_1").remove();
            $("#qeusError_2").remove();

            var i = 0;
            $(".content-main").find(".content-navigation").each(function () {
                i++;
                $("<div class='ErrorContainer' id='qeusError_" + i + "'>Please fill in answer for all the questions.</div>").insertBefore(this);
            });

            var intverval = setInterval(
            function () {
                $("#qeusError_1").remove();
                $("#qeusError_2").remove();

                clearInterval(intverval);
            }
            , 20000);
        }
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            //alert(result.Message); 
            console.log(result.Message);
        }
    }
}

//Report issue
function ShowReportIssuePopup() {
    $('#ReportIssue').attr("style", "opacity: 1 !important;top: 2%");
    $("#ReportModelBack").attr("style", "display:block");

    var chId = $(".content-summary").attr("chpId");
    var secId = $(".content-summary").attr("secId");

    document.location.href.target = '_top';
    document.location.href = '../../../ReportIssue.aspx?cid=' + chId + '&sid=' + secId;
    //$(this).find("#ReportModelBack").attr("style", "display:block");
    //$('#ReportIssue').fadeIn(100);
}

function ShowReportIssuePopupTyping() {
    $('#ReportIssue').attr("style", "opacity: 1 !important;top: 2%");
    $("#ReportModelBack").attr("style", "display:block");
}

function HideReportIssuePopup() {
    //$('#ReportIssue').attr("style", "opacity: 1 !important;top: 10%");
    $('#ReportIssue').fadeOut(100);
    $("#txtTitle").val("");
    $("#txtDescription").val("");
    $("#txtEmail").val("");
    $("#txtExpectedContent").val("");
    $("#reportIssueStatus").html("");
    $("#reportIssueStatus").attr("style", "display:none");
    $("#ReportModelBack").attr("style", "display:none");
}

function CancelReportIssue() {
    parent.location = parent.location;
}

//function SaveReportIssue(title, desc, email, expContent) {
function SaveReportIssue() {
    $("#reportIssueStatus").html("");
    $("#reportIssueStatus").attr("style", "display:none");

    var title = $("#txtTitle").val();
    var desc = $("#txtDescription").val();
    var email = $("#txtEmail").val();
    var expContent = $("#txtExpectedContent").val();

    if (title == "" || desc == "" || email == "" || expContent == "") {
        $("#reportIssueStatus").html("All fields are mandatory.");
        $("#reportIssueStatus").attr("style", "display:block");
        return;
    }
    else if (!validateEmail(email)) {
        $("#reportIssueStatus").html("Please enter valid e-mail address.");
        $("#reportIssueStatus").attr("style", "display:block");
    }
    else {
        title = changeSpecialCharsQS(title);
        desc = changeSpecialCharsQS(desc);
        expContent = changeSpecialCharsQS(expContent);

        var queryStrings = GetQueryStringsForHtmPage();

        var chId = queryStrings["cid"];
        var secId = queryStrings["sid"];

        var path = "Handler/SaveReportIssue.ashx?title=" + title + "&description=" + desc + "&email=" + email + "&expectedContent=" + expContent + "&chapterId=" + chId + "&sectionId=" + secId;
        CallHandler(path, onSaveReportIssueSuccess);
    }
}

function onSaveReportIssueSuccess(result) {
    if (result.Status == "Ok") {

        $("#reportIssueStatus").attr("style", "display:block;background-color: lightgreen");
        $("#reportIssueStatus").html("your query submited successfully. Our technical team contact you shortly.");

        var courses = getQueryStrings();
        var courseId = courses["courseid"];
        PopulateChaptersByCourse(courseId, function () {
            var queryStrings = GetQueryStringsForHtmPage();

            var chId = queryStrings["cid"];
            var secId = queryStrings["sid"];

            var chpObj = GetChapterObject(chId);
            var secObj = GetChapterSectionObject(chId, secId);

            var title = secObj.Title + " || " + chpObj.Title;
            //var link = secObj.Link + " || " + chpObj.Link;
            var link = "chapterId=" + chId + " sectionId=" + secId + "||" + "chapterId=" + chId + " sectionId=" + secId;
            var activit = { "Title": escape(title), "Link": link, "ActivityType": "5" };
            //activit.push();
            SaveUserActivity(activit);
        });

        /*var queryStrings = GetQueryStringsForHtmPage();

        var chId = queryStrings["cid"];
        var secId = queryStrings["sid"];

        var chpObj = GetChapterObject(chId);
        var secObj = GetChapterSectionObject(chId, secId);

        var title = secObj.Title + " || " + chpObj.Title;
        //var link = secObj.Link + " || " + chpObj.Link;
        var link = "chapterId=" + chId + " sectionId=" + secId + "||" + "chapterId=" + chId + " sectionId=" + secId;
        var activit = { "Title": escape(title), "Link": link, "ActivityType": "5" };
        //activit.push();
        SaveUserActivity(activit);*/

        var intverval = setInterval(
        function () {
            $("#reportIssueStatus").html("");
            $("#reportIssueStatus").attr("style", "display:none");
            clearInterval(intverval);
            parent.location = parent.location;
        }
        , 5000);

        $("#txtTitle").val("");
        $("#txtDescription").val("");
        $("#txtEmail").val("");
        $("#txtExpectedContent").val("");
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            //alert(result.Message);
            $("#reportIssueStatus").attr("style", "display:block");
            $("#reportIssueStatus").html("Error: " + result.Message);
        }
    }
}

// save user atcivities
function SaveUserActivity(activity) {
    var jsonAct = JSON.stringify(activity)

    var path = "Handler/SaveUserActivity.ashx?activity=" + jsonAct;
    CallHandler(path, onSaveUserActivitySuccess);
}

function onSaveUserActivitySuccess(result) {

    if (result.Status == "Ok") {

    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            alert(result.Message);
        }
    }
}

// Validation Email
function validateEmail(emailText) {
    var pattern = /^[a-zA-Z0-9\-_]+(\.[a-zA-Z0-9\-_]+)*@[a-z0-9]+(\-[a-z0-9]+)*(\.[a-z0-9]+(\-[a-z0-9]+)*)*\.[a-z]{2,4}$/;
    if (pattern.test(emailText)) {
        return true;
    } else {
        return false;
    }
}
// end

//Replace special chars
function changeSpecialCharsQS(text) {
    text = text.replace('&', ' aaa ');
    return text;
}

function getSpectionChars(text) {
    text = text.replace(' aaa ', '&');
}

function isNumber(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}
//end

//----------May I Help you Funcationality--------------
function SaveStudentQuery(name, mobile, query) {
    var path = "Handler/SaveStudentsQuery.ashx?name=" + name + "&mobileNo=" + mobile + "&query=" + query;
    CallHandler(path, onSaveStudentQuerySuccess);
}

function onSaveStudentQuerySuccess(result) {
    if (result.Status == "Ok") {
        $("#buttons").hide();
        $("#queryStatus").html("your query submited successfully. Our technical team will contact you shortly.");

        /*var activit = [];
        activit.push({ "Title": "You have contacted support center for your query", "Link": "", "ActivityType": "4" });*/
        var activit = { "Title": "You have contacted support center for your query", "Link": "", "ActivityType": "4" };
        SaveUserActivity(activit);

        var intverval = setInterval(
        function () {
            $("#buttons").show();
            $("#queryStatus").html("");

            clearInterval(intverval);
        }
        , 10000);
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            alert(result.Message);
        }
    }
}

function showMayIHelpYouWindow() {
    $("#txtName").val("");
    $("#txtMobile").val("");
    $("#txtQuery").val("");
    $("#txtEmail").val("");

    $("#txtName").css("border-color", "#ccc");
    $("#txtMobile").css("border-color", "#ccc");
    $("#txtQuery").css("border-color", "#ccc");
    $("#txtEmail").css("border-color", "#ccc");

    $("#buttons").show();
    $("#queryStatus").html("");

    if ($("#ql_ShortlistHeadline").attr("class") == "rfloat arrowDown") {
        $("#ql_Tab").attr("style", "display:none");
        //$("#ql_Tab").hide(100);
        $("#ql_ShortlistHeadline").attr("class", "rfloat arrowUp");
    }
    else {
        $("#ql_Tab").attr("style", "display:block");
        //$("#ql_Tab").show(100);
        $("#ql_ShortlistHeadline").attr("class", "rfloat arrowDown");
        $("#txtName").focus();
    }

}

function SaveMayIHelpYouQuery() {
    $("#txtName").css("border-color", "#ccc");
    $("#txtMobile").css("border-color", "#ccc");
    $("#txtQuery").css("border-color", "#ccc");
    $("#txtEmail").css("border-color", "#ccc");

    if ($("#txtName").val() == "") {
        $("#txtName").css("border-color", "red");
        $("#txtName").focus();
        return;
    }

    if ($("#txtMobile").val() == "") {
        $("#txtMobile").css("border-color", "red");
        $("#txtMobile").focus();
        return;
    }
    if ($("#txtQuery").val() == "") {
        $("#txtQuery").css("border-color", "red");
        $("#txtQuery").focus();
        return;
    }



    var name = $("#txtName").val();
    var mobile = $("#txtMobile").val();
    //var query = $("#txtQuery").val() + " || Email Address:" + $("#txtEmail").val();

    if (!isNumber(mobile)) {
        $("#txtMobile").css("border-color", "red");
        $("#txtMobile").focus();
        return;
    }

    if (!validateEmail($("#txtEmail").val())) {
        $("#txtEmail").css("border-color", "red");
        $("#txtEmail").focus();
        return;
    }

    var query = $("#txtQuery").val() + " || Email Address:" + $("#txtEmail").val();

    SaveStudentQuery(name, mobile, query);

    $("#txtName").val("");
    $("#txtMobile").val("");
    $("#txtQuery").val("");
    $("#txtEmail").val("");
}
//-----------End May I Help you Functionality---------

//-----------Search Functionality--------------
function Search() {
    if ($("#txtSearch").val() != "") {
        parent.document.location = BASE_URL + "SearchResult.aspx?searchText=" + $("#txtSearch").val();
    }
}

function PopulateSearchResult(searchText) {
    if ($("#Content_hfSearchResult").val() != "" && $("#Content_hfSearchResult").val() != "null") {
        var searchResult = [];
        searchResult = JSON.parse($("#Content_hfSearchResult").val());

        $("#searchHeader").html("Search Result for '" + searchText + "'");

        if (searchResult.SearchedFiles.length > 0) {

            for (var i = 0; i < searchResult.SearchedFiles.length; i++) {
                var chpId;
                var secId;

                var values = searchResult.SearchedFiles[i].split("/");
                if (values[2] != "Images") {
                    chpId = values[2];
                    secId = GetChapterSectionID(chpId, values[3]);
                    var sectionObject = GetChapterSectionObject(chpId, secId);

                    var div = $("<div/>");
                    var act = $("<div/>");
                    $(act).html("<a target='_blank'  href='" + BASE_URL + "HintPage.aspx?chapterId=" + chpId + "&sectionId=" + secId + "'>" + sectionObject.Title + "</a>");

                    $(div).attr("class", "activityContainer");
                    $(div).append(act);

                    $("#searchContainer").append(div);
                }
            }
        }
        else {
            var div = $("<div/>");
            var act = $("<div/>");
            $(act).html("No search result found for '" + searchText + "'.");

            $(div).attr("class", "activityContainer");
            $(div).append(act);

            $("#searchContainer").append(div);
        }
    }
}
//-----------End Search Functionality--------------

function SearchKeyPress(key) {
    if (key.charCode == "13" || key.keyCode == "13") {
        Search();
    }
}

function ClearPasswordFields() {
    $("#txtOldPassword").val("");
    $("#txtNewPassword").val("");
    $("#txtRetypeNewPassword").val("");
    $("#changePasswordStatus").css("display", "none");
    $("#changePasswordStatus").html("");
}

// Re-Appear in final Quiz
function ReappearInFinalQuiz() {
    var path = "Handler/MoveUserFinalQuizResToArchive.ashx";
    CallHandler(path, onCompleteMoveQuizResponse);
}

function onCompleteMoveQuizResponse(result) {
    if (result.Status == "Ok") {
        parent.document.location = BASE_URL + "TakeFinalQuiz.aspx";
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            alert(result.Message);
        }
    }
}

function UploadSelectedFile(element) {

    if (BASE_URL == "") {
        BASE_URL = "http://localhost:6328/";
        //BASE_URL = "http://adminapps.in/course/";
        //BASE_URL = "http://moocacademy.in/"; //for mooc
        //BASE_URL = "http://myclasstest.com/mooc/";
    }

    var _URL = window.URL || window.webkitURL;

    var file = element.files[0];

    //alert(file.name);

    var sFileExtension = file.name.split('.')[file.name.split('.').length - 1];
    sFileExtension = sFileExtension.toLowerCase();
    if (sFileExtension == "jpg" || sFileExtension == "tif" || sFileExtension == "gif" || sFileExtension == "png" || sFileExtension == "jfif" || sFileExtension == "bmp") {
        var vid = element.id;
        vid = vid.replace("ctl00_ContentPlaceHolder1_fu_", "");

        if (file.size > 10000000) {
            alert('File too large!');
            return false;
        }

        var xhr = new XMLHttpRequest();
        var imageFetcher = new XMLHttpRequest();
        var maxId = 0;
        var type = "dynamic";
        var img;
        if ((file = element.files[0])) {
            img = new Image();
            img.onload = function () {
                xhr.onreadystatechange = function (event) {
                    var target = event.target ? event.target : event.srcElement;
                    if (target.readyState == 4) {
                        if (target.status == 200 || target.status == 304) {
                            //alert(target.responseText);

                            var ResJSON = [];
                            ResJSON = JSON.parse(target.responseText);
                            $("#imgProp").attr("orignal", ResJSON.FileUrl);
                        }
                    }
                };

                xhr.open('POST', BASE_URL + 'Handler/UploadFile.ashx?type=image&banner=true', true);
                xhr.setRequestHeader('X-FILE-NAME', file.name);
                xhr.send(file);
            };

            //alert(_URL.createObjectURL(file));
            img.id = "UploadedImage";
            img.src = _URL.createObjectURL(file);
            $("#imgLocal").attr("src", _URL.createObjectURL(file));
            $("#localImg").show();

            $("#status").html("");
            $("#status").hide();
        }
    }
    else {
        $("#status").html("Please upload valid image file");
        $("#status").show();
    }
}

function capitalize(s) {
    return s[0].toUpperCase() + s.slice(1);
}

// Populating Todays featured tools

function GetTodaysFeaturedTool(courseid) {
    $("#toolsLoader").show();
    $("#toolsContainer").hide();
    var path = "Handler/GetOnlineToolByDate.ashx?courseid=" + courseid;
    CallHandler(path, OnSuccessPupulateTodaysTool);
}

function OnSuccessPupulateTodaysTool(result) {
    $("#toolsLoader").hide();
    $("#toolsContainer").show();
    if (result.Status == "Ok") {
        if (result.OnlineTool != null && result.OnlineTool != "null") {
            $("#toolTitle").html("<b><a href='" + result.OnlineTool.ToolURL + "' target='_blank'>Title: " + result.OnlineTool.Title + "</a></b>");
            $("#toolLogo").html("<a href='" + result.OnlineTool.ToolURL + "' target='_blank'><img src='" + result.OnlineTool.LogoImageURL.replace("~/", "") + "' width='100' height='100' alt='" + result.OnlineTool.LogoImageName + "' /></a>");
            $("#toolDesc").html("<b>Description:</b> " + result.OnlineTool.Description);
            $("#toolWebsite").html("<b>Website: </b><a href='" + result.OnlineTool.ToolURL + "' target='_blank'>" + result.OnlineTool.ToolURL + "</a>");

            if (result.OnlineTool.LogoImageURL == "" || result.OnlineTool.LogoImageURL == null) {
                $("#toolLogo").hide();
            }
            else {
                $("#toolLogo").show();
            }
            if (result.OnlineTool.ToolURL == "" || result.OnlineTool.ToolURL == null) {
                $("#toolWebsite").hide();
            }
            else {
                $("#toolWebsite").show();
            }

            $("#emptyTool").hide();
            $("#Fun").show();
        }
        else {
            $("#toolTitle").html("");
            $("#toolLogo").html("");
            $("#toolDesc").html("");
            $("#toolWebsite").html("");
            $("#emptyTool").html("Selected course does not have any online tool!");
            $("#emptyTool").show();
            $("#Fun").hide();
        }
    }
    else if (result.Status == "Error") {
        if (result.Message == "Session Expire") {
            alert("Your Session is Expire you are redirect to login page.");
            parent.document.location = BASE_URL + "Login.aspx";
        }
        else {
            alert(result.Message);
        }
    }
}