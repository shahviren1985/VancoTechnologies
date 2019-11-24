var BASE_URL = "http://localhost:6328/";
//var BASE_URL = "http://adminapps.in/course/";
//var BASE_URL = "http://moocacademy.in/"; //for mooc
//var BASE_URL = "http://myclasstest.com/mooc/"; //for godady


// creating ChapterIndex
var Chapter_Index = [];
Chapter_Index.push({ "Id": 1, "Title": "Computer Basics", "PageName": "computer-basics", "Link": "Course/fundamentals-of-computer/1/introduction.htm", "Time": "7200" }); //120min
Chapter_Index.push({ "Id": 2, "Title": "Computer Hardware and Storage Devices", "PageName": "computer-hardware", "Link": "Course/fundamentals-of-computer/2/introduction.htm", "Time": "14700" }); //245min
Chapter_Index.push({ "Id": 3, "Title": "Storage Devices", "PageName": "storage-devices", "Link": "Course/fundamentals-of-computer/3/introduction.htm", "Time": "8700" }); // 145min
Chapter_Index.push({ "Id": 4, "Title": "Operating System", "PageName": "operating-system", "Link": "Course/fundamentals-of-computer/4/introduction.htm", "Time": "17700" }); // 295min
Chapter_Index.push({ "Id": 5, "Title": "File management", "PageName": "file-management", "Link": "Course/fundamentals-of-computer/5/file-management-in-windows.htm", "Time": "16200" }); //270min
Chapter_Index.push({ "Id": 6, "Title": "MS Paint", "PageName": "ms-paint", "Link": "Course/fundamentals-of-computer/6/application-software.htm", "Time": "12900" }); // 215min
Chapter_Index.push({ "Id": 7, "Title": "Notepad", "PageName": "notepad", "Link": "Course/fundamentals-of-computer/7/computer-networks.htm", "Time": "6300" }); //105min
Chapter_Index.push({ "Id": 8, "Title": "Word", "PageName": "word", "Link": "Course/fundamentals-of-computer/8/introduction.htm", "Time": "90000" }); //1500min
Chapter_Index.push({ "Id": 9, "Title": "Excel", "PageName": "excel", "Link": "Course/fundamentals-of-computer/9/introduction.htm", "Time": "66900" }); //1115min
Chapter_Index.push({ "Id": 10, "Title": "Power point", "PageName": "power-point", "Link": "Course/fundamentals-of-computer/10/microsoft-powerpoint.htm", "Time": "68400" }); // 1140min
Chapter_Index.push({ "Id": 11, "Title": "Computer Networks", "PageName": "computer-networks", "Link": "Course/fundamentals-of-computer/11/computer-networks.html", "Time": "18300" }); // 305min
Chapter_Index.push({ "Id": 12, "Title": "Security", "PageName": "security", "Link": "Course/fundamentals-of-computer/12/introduction.htm", "Time": "14100" }); // 235min
Chapter_Index.push({ "Id": 13, "Title": "Internet", "PageName": "internet", "Link": "Course/fundamentals-of-computer/13/introduction.htm", "Time": "18300" }); // 305min
Chapter_Index.push({ "Id": 14, "Title": "Social media", "PageName": "social-media", "Link": "Course/fundamentals-of-computer/14/introduction.htm", "Time": "6300" }); // 105min


// chapter 1
var Chapter_1 = [];
//Chapter_1.push({ "Id": 1, "ChapterId": 1, "Title": "Introduction", "PageName": "Introduction.htm", "Description": "Some Description", "Link": "Course/fundamentals-of-computer/1/Introduction.htm", "Time": "900" }); //15min
Chapter_1.push({ "Id": 1, "ChapterId": 1, "Title": "Introduction", "PageName": "Introduction.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-basics/introduction.htm", "Time": "900" }); //15min
Chapter_1.push({ "Id": 2, "ChapterId": 1, "Title": "Computer Structure", "PageName": "computer-structure.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-basics/computer-structure.htm", "Time": "1800" }); //30min
Chapter_1.push({ "Id": 3, "ChapterId": 1, "Title": "Hardware", "PageName": "Hardware.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-basics/Hardware.htm", "Time": "900" }); //15min
Chapter_1.push({ "Id": 4, "ChapterId": 1, "Title": "Software", "PageName": "software.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-basics/software.htm", "Time": "900" }); //15min
Chapter_1.push({ "Id": 5, "ChapterId": 1, "Title": "System Software", "PageName": "system-software.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-basics/system-software.htm", "Time": "900" }); //15min
Chapter_1.push({ "Id": 6, "ChapterId": 1, "Title": "Application Software", "PageName": "application-software.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-basics/application-software.htm", "Time": "600" }); //10min
Chapter_1.push({ "Id": 7, "ChapterId": 1, "Title": "Generalized Packages", "PageName": "generalized-packages.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-basics/generalized-packages.htm", "Time": "900" }); //15min
Chapter_1.push({ "Id": 8, "ChapterId": 1, "Title": "Customized Packages", "PageName": "customized-packeges.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-basics/customized-packeges.htm", "Time": "300" }); //5min

// chapter 2
var Chapter_2 = [];
Chapter_2.push({ "Id": 9, "ChapterId": 2, "Title": "Introduction", "PageName": "introduction.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-hardware/introduction.htm", "Time": "600" }); // 10min
Chapter_2.push({ "Id": 10, "ChapterId": 2, "Title": "Input Devices", "PageName": "input-devices.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-hardware/input-devices.htm", "Time": "600" }); //10min
Chapter_2.push({ "Id": 11, "ChapterId": 2, "Title": "keyboard", "PageName": "keyboard.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-hardware/keyboard.htm", "Time": "1800" }); //30min
Chapter_2.push({ "Id": 12, "ChapterId": 2, "Title": "Mouse", "PageName": "mouse.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-hardware/mouse.htm", "Time": "1800" }); //30min
Chapter_2.push({ "Id": 13, "ChapterId": 2, "Title": "Optical Scanner", "PageName": "opticals-scanner.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-hardware/opticals-scanner.htm", "Time": "1800" }); //30min
Chapter_2.push({ "Id": 14, "ChapterId": 2, "Title": "Touch Screen", "PageName": "touch-screen.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-hardware/touch-screen.htm", "Time": "300" }); //5min
Chapter_2.push({ "Id": 15, "ChapterId": 2, "Title": "Microphone", "PageName": "microphone.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-hardware/microphone.htm", "Time": "900" }); //15min
Chapter_2.push({ "Id": 16, "ChapterId": 2, "Title": "Web Camera", "PageName": "web-camera.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-hardware/web-camera.htm", "Time": "300" }); //5min
Chapter_2.push({ "Id": 17, "ChapterId": 2, "Title": "Output-Devices", "PageName": "output-devices.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-hardware/output-devices.htm", "Time": "600" }); //10min
Chapter_2.push({ "Id": 18, "ChapterId": 2, "Title": "Monitor", "PageName": "monitor.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-hardware/monitor.htm", "Time": "1800" }); //30min
Chapter_2.push({ "Id": 19, "ChapterId": 2, "Title": "Display Resolution", "PageName": "display-resolution.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-hardware/display-resolution.htm", "Time": "300" }); //5min
Chapter_2.push({ "Id": 20, "ChapterId": 2, "Title": "Printers", "PageName": "printers.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-hardware/printers.htm", "Time": "2700" }); //45min
Chapter_2.push({ "Id": 21, "ChapterId": 2, "Title": "Speaker", "PageName": "speaker.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-hardware/speaker.htm", "Time": "300" }); //5min
Chapter_2.push({ "Id": 22, "ChapterId": 2, "Title": "Projector", "PageName": "projector.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-hardware/projector.htm", "Time": "600" }); //10min
Chapter_2.push({ "Id": 23, "ChapterId": 2, "Title": "Fundamentals of CPU", "PageName": "fundamentals-of-cpu.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-hardware/fundamentals-of-cpu.htm", "Time": "300" }); //5min

// chapter 3
var Chapter_3 = [];
Chapter_2.push({ "Id": 24, "ChapterId": 2, "Title": "Introduction", "PageName": "Introduction.htm", "Description": "", "Link": "Course/fundamentals-of-computer/storage-devices/Introduction.htm", "Time": "900" }); //15min
Chapter_2.push({ "Id": 25, "ChapterId": 2, "Title": "Storage Type", "PageName": "storage-type.htm", "Description": "", "Link": "Course/fundamentals-of-computer/storage-devices/storage-type.htm", "Time": "1200" }); //20min
Chapter_2.push({ "Id": 26, "ChapterId": 2, "Title": "Primary Storage", "PageName": "primary-storage.htm", "Description": "", "Link": "Course/fundamentals-of-computer/storage-devices/primary-storage.htm", "Time": "600" }); //10min
Chapter_2.push({ "Id": 27, "ChapterId": 2, "Title": "RAM Memory", "PageName": "ram-memory.htm", "Description": "", "Link": "Course/fundamentals-of-computer/storage-devices/ram-memory.htm", "Time": "1200" }); //20min
Chapter_2.push({ "Id": 28, "ChapterId": 2, "Title": "ROM Memory", "PageName": "rom-memory.htm", "Description": "", "Link": "Course/fundamentals-of-computer/storage-devices/rom-memory.htm", "Time": "1200" }); //20min
//Chapter_3.push({ "Id": 29, "ChapterId": 3, "Title": "Comparison", "PageName": "comparison.htm", "Description": "", "Link": "Course/fundamentals-of-computer/3/comparison.htm", "Time": "1200" }); //20min
Chapter_2.push({ "Id": 29, "ChapterId": 2, "Title": "Secondary Storage", "PageName": "secondary-storage.htm", "Description": "", "Link": "Course/fundamentals-of-computer/storage-devices/secondary-storage.htm", "Time": "600" }); //10min
Chapter_2.push({ "Id": 30, "ChapterId": 2, "Title": "Hard Disk Drive", "PageName": "hard-disk-drive.htm", "hard-disk-drive.htm": "", "Link": "Course/fundamentals-of-computer/storage-devices/hard-disk-drive.htm", "Time": "900" }); //15min
Chapter_2.push({ "Id": 31, "ChapterId": 2, "Title": "CDs and DVDs", "PageName": "cds-and-dvds.htm", "Description": "", "Link": "Course/fundamentals-of-computer/storage-devices/cds-and-dvds.htm", "Time": "900" }); //15min
Chapter_2.push({ "Id": 32, "ChapterId": 2, "Title": "USB Flash Drive", "PageName": "usb-flash-drive.htm", "Description": "", "Link": "Course/fundamentals-of-computer/storage-devices/usb-flash-drive.htm", "Time": "1200" }); //20min

// chapter 4
var Chapter_4 = [];
Chapter_4.push({ "Id": 33, "ChapterId": 4, "Title": "Introduction", "PageName": "Introduction.htm", "Description": "", "Link": "Course/fundamentals-of-computer/operating-system/Introduction.htm", "Time": "1200" }); //20min
Chapter_4.push({ "Id": 34, "ChapterId": 4, "Title": "Functions of an Operating System", "PageName": "functions-of-an-operating-system.htm", "Description": "", "Link": "Course/fundamentals-of-computer/operating-system/functions-of-an-operating-system.htm", "Time": "2700" }); //45min
Chapter_4.push({ "Id": 35, "ChapterId": 4, "Title": "Operating System Vendors", "PageName": "operating-system-vendors.htm", "Description": "", "Link": "Course/fundamentals-of-computer/operating-system/operating-system-vendors.htm", "Time": "1200" }); //20min
Chapter_4.push({ "Id": 36, "ChapterId": 4, "Title": "Logging On", "PageName": "logging-on.htm", "Description": "", "Link": "Course/fundamentals-of-computer/operating-system/logging-on.htm", "Time": "900" }); //15min
Chapter_4.push({ "Id": 37, "ChapterId": 4, "Title": "Start Menu", "PageName": "start-menu.htm", "Description": "", "Link": "Course/fundamentals-of-computer/operating-system/start-menu.htm", "Time": "600" }); //10min
Chapter_4.push({ "Id": 38, "ChapterId": 4, "Title": "Overview of the Options", "PageName": "overview-of-the-options.htm", "Description": "", "Link": "Course/fundamentals-of-computer/operating-system/overview-of-the-options.htm", "Time": "1800" }); //30min
Chapter_4.push({ "Id": 39, "ChapterId": 4, "Title": "Task Bar", "PageName": "task-bar.htm", "Description": "", "Link": "Course/fundamentals-of-computer/operating-system/task-bar.htm", "Time": "900" }); //15min
Chapter_4.push({ "Id": 40, "ChapterId": 4, "Title": "Start Program", "PageName": "start-program.htm", "Description": "", "Link": "Course/fundamentals-of-computer/operating-system/start-program.htm", "Time": "600" }); //10min
Chapter_4.push({ "Id": 41, "ChapterId": 4, "Title": "Quitting Program", "PageName": "quitting-program.htm", "Description": "", "Link": "Course/fundamentals-of-computer/operating-system/quitting-program.htm", "Time": "600" }); //10min
Chapter_4.push({ "Id": 42, "ChapterId": 4, "Title": "Getting Help", "PageName": "getting-help.htm", "Description": "", "Link": "Course/fundamentals-of-computer/operating-system/getting-help.htm", "Time": "1800" }); //30min
Chapter_4.push({ "Id": 43, "ChapterId": 4, "Title": "Locating Files and Folders", "PageName": "locating-files-and-folders.htm", "Description": "", "Link": "Course/fundamentals-of-computer/operating-system/locating-files-and-folders.htm", "Time": "1200" }); //20min
Chapter_4.push({ "Id": 44, "ChapterId": 4, "Title": "Changing System Settings", "PageName": "changing-system-settings.htm", "Description": "", "Link": "Course/fundamentals-of-computer/operating-system/changing-system-settings.htm", "Time": "1200" }); //20min
Chapter_4.push({ "Id": 45, "ChapterId": 4, "Title": "Using My Computer", "PageName": "using-my-computer.htm", "Description": "", "Link": "Course/fundamentals-of-computer/operating-system/using-my-computer.htm", "Time": "900" }); //15min
Chapter_4.push({ "Id": 46, "ChapterId": 4, "Title": "Display the Storage Contents", "PageName": "display-the-storage-contents.htm", "Description": "", "Link": "Course/fundamentals-of-computer/operating-system/display-the-storage-contents.htm", "Time": "900" }); //15min
Chapter_4.push({ "Id": 47, "ChapterId": 4, "Title": "Start Lock and Shutdown Windows", "PageName": "start-lock-and-shutdown-windows.htm", "Description": "", "Link": "Course/fundamentals-of-computer/operating-system/start-lock-and-shutdown-windows.htm", "Time": "1200" }); //20min

// chapter 5
var Chapter_5 = [];
Chapter_5.push({ "Id": 48, "ChapterId": 5, "Title": "File Management in Windows", "PageName": "file-management-in-windows.htm", "Description": "", "Link": "Course/fundamentals-of-computer/file-management/file-management-in-windows.htm", "Time": "1200" }); //20min
Chapter_5.push({ "Id": 49, "ChapterId": 5, "Title": "Using Windows Explorer", "PageName": "using-windows-explorer.htm", "Description": "", "Link": "Course/fundamentals-of-computer/file-management/using-windows-explorer.htm", "Time": "900" }); //15min
Chapter_5.push({ "Id": 50, "ChapterId": 5, "Title": "Copying or Moving a File or Folder", "PageName": "copying-or-moving-a-file-or-folder.htm", "Description": "", "Link": "Course/fundamentals-of-computer/file-management/copying-or-moving-a-file-or-folder.htm", "Time": "1800" }); //30min
Chapter_5.push({ "Id": 51, "ChapterId": 5, "Title": "View File Details", "PageName": "view-file-details.htm", "Description": "", "Link": "Course/fundamentals-of-computer/file-management/view-file-details.htm", "Time": "900" }); //15min
Chapter_5.push({ "Id": 52, "ChapterId": 5, "Title": "Create New Folder", "PageName": "create-new-folder.htm", "Description": "", "Link": "Course/fundamentals-of-computer/file-management/create-new-folder.htm", "Time": "900" }); //15min
Chapter_5.push({ "Id": 53, "ChapterId": 5, "Title": "Rename a File or Folder", "PageName": "rename-a-file-or-folder.htm", "Description": "", "Link": "Course/fundamentals-of-computer/file-management/rename-a-file-or-folder.htm", "Time": "600" }); //10min
Chapter_5.push({ "Id": 54, "ChapterId": 5, "Title": "Delete a File or Folder", "PageName": "delete-a-file-or-folder.htm", "Description": "", "Link": "Course/fundamentals-of-computer/file-management/delete-a-file-or-folder.htm", "Time": "900" }); //15min
Chapter_5.push({ "Id": 55, "ChapterId": 5, "Title": "Hidden Files and Folders", "PageName": "hidden-files-and-folders.htm", "Description": "", "Link": "Course/fundamentals-of-computer/file-management/hidden-files-and-folders.htm", "Time": "900" }); //15min
Chapter_5.push({ "Id": 56, "ChapterId": 5, "Title": "Install Software Hardware", "PageName": "install-software-hardware.htm", "Description": "", "Link": "Course/fundamentals-of-computer/file-management/install-software-hardware.htm", "Time": "900" }); //15min
Chapter_5.push({ "Id": 57, "ChapterId": 5, "Title": "Install Software", "PageName": "install-software.htm", "Description": "", "Link": "Course/fundamentals-of-computer/file-management/install-software.htm", "Time": "900" }); //15min
Chapter_5.push({ "Id": 58, "ChapterId": 5, "Title": "Change or Remove Software", "PageName": "change-or-remove-software.htm", "Description": "", "Link": "Course/fundamentals-of-computer/file-management/change-or-remove-software.htm", "Time": "900" }); //15min
Chapter_5.push({ "Id": 59, "ChapterId": 5, "Title": "Add New Features", "PageName": "add-new-features.htm", "Description": "", "Link": "Course/fundamentals-of-computer/file-management/add-new-features.htm", "Time": "900" }); //15min
Chapter_5.push({ "Id": 60, "ChapterId": 5, "Title": "Install Hardware", "PageName": "install-hardware.htm", "Description": "", "Link": "Course/fundamentals-of-computer/file-management/install-hardware.htm", "Time": "900" }); //15min
Chapter_5.push({ "Id": 61, "ChapterId": 5, "Title": "Search in Windows", "PageName": "search-in-windows.htm", "Description": "", "Link": "Course/fundamentals-of-computer/file-management/search-in-windows.htm", "Time": "900" }); //15min
Chapter_5.push({ "Id": 62, "ChapterId": 5, "Title": "Device Manager", "PageName": "device-manager.htm", "Description": "", "Link": "Course/fundamentals-of-computer/file-management/device-manager.htm", "Time": "2700" }); //45min


// chapter 6
var Chapter_6 = [];
//Chapter_6.push({ "Id": 63, "ChapterId": 6, "Title": "Application Software", "PageName": "application-software.htm", "Description": "", "Link": "Course/fundamentals-of-computer/6/application-software.htm", "Time": "900" }); //15min
Chapter_6.push({ "Id": 63, "ChapterId": 6, "Title": "Introduction", "PageName": "introduction.htm", "Description": "", "Link": "Course/fundamentals-of-computer/ms-paint/introduction.htm", "Time": "1200" }); //20min
Chapter_6.push({ "Id": 64, "ChapterId": 6, "Title": "Application Software", "PageName": "application-software.htm", "Description": "", "Link": "Course/fundamentals-of-computer/ms-paint/application-software.htm", "Time": "900" }); //15min
Chapter_6.push({ "Id": 65, "ChapterId": 6, "Title": "The Toolbar", "PageName": "toolbar.htm", "Description": "", "Link": "Course/fundamentals-of-computer/ms-paint/toolbar.htm", "Time": "1800" }); //30min
Chapter_6.push({ "Id": 66, "ChapterId": 6, "Title": "Color Palette", "PageName": "color-palette.htm", "Description": "", "Link": "Course/fundamentals-of-computer/ms-paint/color-palette.htm", "Time": "900" }); //15min
Chapter_6.push({ "Id": 67, "ChapterId": 6, "Title": "The Option Tool", "PageName": "the-option-tool.htm", "Description": "", "Link": "Course/fundamentals-of-computer/ms-paint/the-option-tool.htm", "Time": "7200" }); //120min
Chapter_6.push({ "Id": 68, "ChapterId": 6, "Title": "Save Image", "PageName": "save-image.htm", "Description": "", "Link": "Course/fundamentals-of-computer/ms-paint/save-image.htm", "Time": "900" }); //15min
//Chapter_6.push({ "Id": 68, "ChapterId": 6, "Title": "Introduction", "PageName": "introduction.htm", "Description": "", "Link": "Course/fundamentals-of-computer/6/introduction.htm", "Time": "1200" }); //20min


// chapter 7
var Chapter_7 = [];
Chapter_7.push({ "Id": 69, "ChapterId": 7, "Title": "Introduction", "PageName": "introduction.htm", "Description": "", "Link": "Course/fundamentals-of-computer/notepad/introduction.htm", "Time": "900" }); //15min
Chapter_7.push({ "Id": 70, "ChapterId": 7, "Title": "Open Notepad", "PageName": "open-notepad.htm", "Description": "", "Link": "Course/fundamentals-of-computer/notepad/open-notepad.htm", "Time": "900" }); //15min
Chapter_7.push({ "Id": 71, "ChapterId": 7, "Title": "Save", "PageName": "save.htm", "Description": "", "Link": "Course/fundamentals-of-computer/notepad/save.htm", "Time": "900" }); //15min
Chapter_7.push({ "Id": 72, "ChapterId": 7, "Title": "Print", "PageName": "print.htm", "Description": "", "Link": "Course/fundamentals-of-computer/notepad/print.htm", "Time": "900" }); //15min
Chapter_7.push({ "Id": 73, "ChapterId": 7, "Title": "Open", "PageName": "open.htm", "Description": "", "Link": "Course/fundamentals-of-computer/notepad/open.htm", "Time": "600" }); //10min
Chapter_7.push({ "Id": 74, "ChapterId": 7, "Title": "Font", "PageName": "font.htm", "Description": "", "Link": "Course/fundamentals-of-computer/notepad/font.htm", "Time": "900" }); //15min
Chapter_7.push({ "Id": 75, "ChapterId": 7, "Title": "Word Wrap", "PageName": "word-wrap.htm", "Description": "", "Link": "Course/fundamentals-of-computer/notepad/word-wrap.htm", "Time": "1200" }); //20min

// chapter 8
var Chapter_8 = [];
Chapter_8.push({ "Id": 76, "ChapterId": 8, "Title": "Introduction", "PageName": "introduction.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/introduction.htm", "Time": "600" }); //10min
Chapter_8.push({ "Id": 77, "ChapterId": 8, "Title": "Features", "PageName": "features.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/features.htm", "Time": "1200" }); //20min
Chapter_8.push({ "Id": 78, "ChapterId": 8, "Title": "Word 2007", "PageName": "word-2007.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/word-2007.htm", "Time": "1800" }); //30min
Chapter_8.push({ "Id": 79, "ChapterId": 8, "Title": "Screen Layout", "PageName": "screen-layout.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/screen-layout.htm", "Time": "600" }); //10min
Chapter_8.push({ "Id": 80, "ChapterId": 8, "Title": "Menus", "PageName": "menus.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/menus.htm", "Time": "1800" }); //30min
Chapter_8.push({ "Id": 81, "ChapterId": 8, "Title": "Toolbars", "PageName": "toolbars.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/toolbars.htm", "Time": "3600" }); //60min
Chapter_8.push({ "Id": 82, "ChapterId": 8, "Title": "Rulers", "PageName": "rulers.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/rulers.htm", "Time": "900" }); //15min
Chapter_8.push({ "Id": 83, "ChapterId": 8, "Title": "Typing Screen Objects", "PageName": "typing-screen-objects.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/typing-screen-objects.htm", "Time": "1800" }); //30min
Chapter_8.push({ "Id": 84, "ChapterId": 8, "Title": "Scrollbars", "PageName": "scrollbars.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/scrollbars.htm", "Time": "1200" }); //20min
Chapter_8.push({ "Id": 85, "ChapterId": 8, "Title": "Managing Documents", "PageName": "managing-documents.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/managing-documents.htm", "Time": "1800" }); //30min
Chapter_8.push({ "Id": 86, "ChapterId": 8, "Title": "Create New Document", "PageName": "create-new-document.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/create-new-document.htm", "Time": "600" }); //10min
Chapter_8.push({ "Id": 87, "ChapterId": 8, "Title": "Open an Existing Document", "PageName": "open-an-existing-document.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/open-an-existing-document.htm", "Time": "600" }); //10min
Chapter_8.push({ "Id": 88, "ChapterId": 8, "Title": "Save a New or Existing Document", "PageName": "save-new-or-existing-document.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/save-new-or-existing-document.htm", "Time": "900" }); //15min
Chapter_8.push({ "Id": 89, "ChapterId": 8, "Title": "Find a Document", "PageName": "find-a-document.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/find-a-document.htm", "Time": "1200" }); //20min
Chapter_8.push({ "Id": 90, "ChapterId": 8, "Title": "Close a Document", "PageName": "close-a-document.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/close-a-document.htm", "Time": "600" }); //10min
Chapter_8.push({ "Id": 91, "ChapterId": 8, "Title": "Print a Document", "PageName": "print-a-document.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/print-a-document.htm", "Time": "1800" }); //30min
Chapter_8.push({ "Id": 92, "ChapterId": 8, "Title": "Exit Word Program", "PageName": "exit-word-program.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/exit-word-program.htm", "Time": "600" }); //10min
Chapter_8.push({ "Id": 93, "ChapterId": 8, "Title": "Keyboard Shortcuts", "PageName": "keyboard-shortcuts.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/keyboard-shortcuts.htm", "Time": "4800" }); //80min
Chapter_8.push({ "Id": 94, "ChapterId": 8, "Title": "Working with Text", "PageName": "working-with-text.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/working-with-text.htm", "Time": "600" }); //10min
Chapter_8.push({ "Id": 95, "ChapterId": 8, "Title": "Typing Text", "PageName": "typing-text.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/typing-text.htm", "Time": "900" }); //15min
Chapter_8.push({ "Id": 96, "ChapterId": 8, "Title": "Inserting Text", "PageName": "inserting-text.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/inserting-text.htm", "Time": "900" }); //15min
Chapter_8.push({ "Id": 97, "ChapterId": 8, "Title": "Spacebar and Tabs", "PageName": "spacebar-and-tabs.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/spacebar-and-tabs.htm", "Time": "600" }); //10min
Chapter_8.push({ "Id": 98, "ChapterId": 8, "Title": "Selecting Text", "PageName": "selecting-text.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/selecting-text.htm", "Time": "1800" }); //30min
Chapter_8.push({ "Id": 99, "ChapterId": 8, "Title": "Deleting Text", "PageName": "deleting-text.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/deleting-text.htm", "Time": "900" }); //15min
Chapter_8.push({ "Id": 100, "ChapterId": 8, "Title": "Replacing Text", "PageName": "replacing-text.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/replacing-text.htm", "Time": "900" }); //15min
Chapter_8.push({ "Id": 101, "ChapterId": 8, "Title": "Formatting Text", "PageName": "formatting-text.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/formatting-text.htm", "Time": "7200" }); //120min
Chapter_8.push({ "Id": 102, "ChapterId": 8, "Title": "Format Painter", "PageName": "format-painter.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/format-painter.htm", "Time": "3600" }); //60min
Chapter_8.push({ "Id": 103, "ChapterId": 8, "Title": "Format Paragraphs", "PageName": "format-paragraphs.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/format-paragraphs.htm", "Time": "7200" }); //120min
Chapter_8.push({ "Id": 104, "ChapterId": 8, "Title": "Line Markers", "PageName": "line-markers.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/line-markers.htm", "Time": "1800" }); //30min
Chapter_8.push({ "Id": 105, "ChapterId": 8, "Title": "Line Spacing", "PageName": "line-spacing.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/line-spacing.htm", "Time": "1800" }); //30min
Chapter_8.push({ "Id": 106, "ChapterId": 8, "Title": "Paragraph Spacing", "PageName": "paragraph-spacing.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/paragraph-spacing.htm", "Time": "1200" }); //20min
Chapter_8.push({ "Id": 107, "ChapterId": 8, "Title": "Borders and Shading", "PageName": "borders-and-shading.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/borders-and-shading.htm", "Time": "3600" }); //60min
Chapter_8.push({ "Id": 108, "ChapterId": 8, "Title": "Bulleted and Numbered Lists", "PageName": "bulleted-and-numberd-lists.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/bulleted-and-numberd-lists.htm", "Time": "1800" }); //30min
Chapter_8.push({ "Id": 109, "ChapterId": 8, "Title": "Copying Text and Moving Text", "PageName": "copying-text-and-moving-text.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/copying-text-and-moving-text.htm", "Time": "1800" }); //not found
Chapter_8.push({ "Id": 110, "ChapterId": 8, "Title": "Spelling and Grammar", "PageName": "spelling-and-grammer.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/spelling-and-grammer.htm", "Time": "3600" }); //60min
Chapter_8.push({ "Id": 111, "ChapterId": 8, "Title": "Page Formatting", "PageName": "page-formatting.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/page-formatting.htm", "Time": "5400" }); //90min
Chapter_8.push({ "Id": 112, "ChapterId": 8, "Title": "Page Margins", "PageName": "page-margins.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/page-margins.htm", "Time": "1800" }); //30min
Chapter_8.push({ "Id": 113, "ChapterId": 8, "Title": "Page Size and Orientation", "PageName": "page-size-and-orientation.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/page-size-and-orientation.htm", "Time": "1200" }); //30min
Chapter_8.push({ "Id": 114, "ChapterId": 8, "Title": "Zoom in to the Page", "PageName": "zoom-in-to-the-page.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/zoom-in-to-the-page.htm", "Time": "1800" }); //30min
Chapter_8.push({ "Id": 115, "ChapterId": 8, "Title": "Headers and Footers", "PageName": "headers-and-footers.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/headers-and-footers.htm", "Time": "3600" }); //60min
Chapter_8.push({ "Id": 116, "ChapterId": 8, "Title": "Page Numbers", "PageName": "page-numbers.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/page-numbers.htm", "Time": "1800" }); //30min
Chapter_8.push({ "Id": 117, "ChapterId": 8, "Title": "Inserting a Page Break", "PageName": "inserting-a-page-break.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/inserting-a-page-break.htm", "Time": "1200" }); //20min
Chapter_8.push({ "Id": 118, "ChapterId": 8, "Title": "Deleting a page break", "PageName": "deleting-a-page-break.htm", "Description": "", "Link": "Course/fundamentals-of-computer/word/deleting-a-page-break.htm", "Time": "600" }); //10min

// chapter 9
var Chapter_9 = [];
Chapter_9.push({ "Id": 119, "ChapterId": 9, "Title": "Introduction", "PageName": "introduction.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/introduction.htm", "Time": "600" }); //10min
Chapter_9.push({ "Id": 120, "ChapterId": 9, "Title": "Features of Spreadsheets", "PageName": "features-of-spreadsheets.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/features-of-spreadsheets.htm", "Time": "1200" }); //20min
Chapter_9.push({ "Id": 121, "ChapterId": 9, "Title": "Features of MS-Excel 2007", "PageName": "features-of-ms-excel-2007.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/features-of-ms-excel-2007.htm", "Time": "600" }); //10min
Chapter_9.push({ "Id": 122, "ChapterId": 9, "Title": "Office themes and Excel styles", "PageName": "office-themes-and-excel-styles.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/office-themes-and-excel-styles.htm", "Time": "900" }); //15min
Chapter_9.push({ "Id": 123, "ChapterId": 9, "Title": "Formulas", "PageName": "formulas.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/formulas.htm", "Time": "300" }); //5min
Chapter_9.push({ "Id": 124, "ChapterId": 9, "Title": "Function AutoComplete", "PageName": "function-autocomplete.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/function-autocomplete.htm", "Time": "600" }); //10min
Chapter_9.push({ "Id": 125, "ChapterId": 9, "Title": "Sorting and Filtering", "PageName": "sorting-and-filtering.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/sorting-and-filtering.htm", "Time": "1200" }); //20min
Chapter_9.push({ "Id": 126, "ChapterId": 9, "Title": "Starting Excel", "PageName": "starting-excel.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/starting-excel.htm", "Time": "600" }); //10min
Chapter_9.push({ "Id": 127, "ChapterId": 9, "Title": "Excel Worksheet", "PageName": "excel-worksheet.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/excel-worksheet.htm", "Time": "1200" }); //20min
Chapter_9.push({ "Id": 128, "ChapterId": 9, "Title": "Selecting, Adding and Renaming Worksheets", "PageName": "selecting-adding-and-renaming-worksheets.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/selecting-adding-and-renaming-worksheets.htm", "Time": "900" }); //15min
Chapter_9.push({ "Id": 129, "ChapterId": 9, "Title": "Selecting Cells and Ranges", "PageName": "selecting-cells-and-ranges.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/selecting-cells-and-ranges.htm", "Time": "900" }); //15min
Chapter_9.push({ "Id": 130, "ChapterId": 9, "Title": "Navigating the Worksheet", "PageName": "navigating-the-worksheet.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/navigating-the-worksheet.htm", "Time": "600" }); //10min
Chapter_9.push({ "Id": 131, "ChapterId": 9, "Title": "Data Entry", "PageName": "data-entry.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/data-entry.htm", "Time": "1200" }); //20min
Chapter_9.push({ "Id": 132, "ChapterId": 9, "Title": "Editing Data", "PageName": "editing-data.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/editing-data.htm", "Time": "1200" }); //20min
Chapter_9.push({ "Id": 133, "ChapterId": 9, "Title": "Cell References", "PageName": "cell-references.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/cell-references.htm", "Time": "600" }); //10min
Chapter_9.push({ "Id": 134, "ChapterId": 9, "Title": "Find and Replace", "PageName": "find-and-replace.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/find-and-replace.htm", "Time": "900" }); //15min
Chapter_9.push({ "Id": 135, "ChapterId": 9, "Title": "Modifying a Worksheet", "PageName": "modifying-a-worksheet.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/modifying-a-worksheet.htm", "Time": "2400" }); //40min
Chapter_9.push({ "Id": 136, "ChapterId": 9, "Title": "Resizing Rows and Columns", "PageName": "resizing-rows-and-columns.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/resizing-rows-and-columns.htm", "Time": "1200" }); //20min
Chapter_9.push({ "Id": 137, "ChapterId": 9, "Title": "Insert moved or copied cells", "PageName": "insert-moved-or-copied-cells.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/insert-moved-or-copied-cells.htm", "Time": "900" }); //15min
Chapter_9.push({ "Id": 138, "ChapterId": 9, "Title": "Move or copy the contents of a cell", "PageName": "move-or-copy-the-contents-of-cell.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/move-or-copy-the-contents-of-cell.htm", "Time": "900" }); //15min
Chapter_9.push({ "Id": 139, "ChapterId": 9, "Title": "Copy cell values, cell formats, or formulas only", "PageName": "copy-cell-values-formats-formulas.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/copy-cell-values-formats-formulas.htm", "Time": "900" }); //15min
Chapter_9.push({ "Id": 140, "ChapterId": 9, "Title": "Drag and Drop", "PageName": "drag-and-drop.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/drag-and-drop.htm", "Time": "600" }); //10min
Chapter_9.push({ "Id": 141, "ChapterId": 9, "Title": "Freez Pangs", "PageName": "freeze-pans.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/freeze-pans.htm", "Time": "1200" }); //20min
Chapter_9.push({ "Id": 142, "ChapterId": 9, "Title": "Page Breaks", "PageName": "page-breaks.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/page-breaks.htm", "Time": "600" }); //10min
Chapter_9.push({ "Id": 143, "ChapterId": 9, "Title": "Page Setup", "PageName": "page-setup.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/page-setup.htm", "Time": "1800" }); //30min
Chapter_9.push({ "Id": 144, "ChapterId": 9, "Title": "Print Preview", "PageName": "print-preview.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/print-preview.htm", "Time": "600" }); //10min
Chapter_9.push({ "Id": 145, "ChapterId": 9, "Title": "Print", "PageName": "print.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/print.htm", "Time": "600" }); //10min
Chapter_9.push({ "Id": 146, "ChapterId": 9, "Title": "File Open, Save and Close", "PageName": "file-open-save-and-close.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/file-open-save-and-close.htm", "Time": "1800" }); //30min
Chapter_9.push({ "Id": 147, "ChapterId": 9, "Title": "Format Cells", "PageName": "format-cells.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/format-cells.htm", "Time": "2400" }); //40min
Chapter_9.push({ "Id": 148, "ChapterId": 9, "Title": "Format Cell Dialog Box", "PageName": "format-cell-dialog-box.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/format-cell-dialog-box.htm", "Time": "1800" }); //30min
Chapter_9.push({ "Id": 149, "ChapterId": 9, "Title": "Date and Time", "PageName": "date-and-time.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/date-and-time.htm", "Time": "1200" }); //20min
Chapter_9.push({ "Id": 150, "ChapterId": 9, "Title": "Format Coulumns and Rows", "PageName": "format-columns-and-rows.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/format-columns-and-rows.htm", "Time": "600" }); //10min
Chapter_9.push({ "Id": 151, "ChapterId": 9, "Title": "AutoFit Columns", "PageName": "autofit-columns.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/autofit-columns.htm", "Time": "1200" }); //20min
Chapter_9.push({ "Id": 152, "ChapterId": 9, "Title": "Hide Column or Row", "PageName": "hide-column-or-row.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/hide-column-or-row.htm", "Time": "900" }); //15min
Chapter_9.push({ "Id": 153, "ChapterId": 9, "Title": "Unhide Column or Row", "PageName": "unhide-column-or-row.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/unhide-column-or-row.htm", "Time": "600" }); //10min
Chapter_9.push({ "Id": 154, "ChapterId": 9, "Title": "Formulas and Functions", "PageName": "formulas-and-functions.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/formulas-and-functions.htm", "Time": "1800" }); //30min
Chapter_9.push({ "Id": 155, "ChapterId": 9, "Title": "Copy a Formula", "PageName": "copy-a-formula.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/copy-a-formula.htm", "Time": "900" }); //15min
Chapter_9.push({ "Id": 156, "ChapterId": 9, "Title": "Auto sum feature", "PageName": "auto-sum-features.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/auto-sum-features.htm", "Time": "3600" }); //60min
Chapter_9.push({ "Id": 157, "ChapterId": 9, "Title": "Charts", "PageName": "charts.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/charts.htm", "Time": "300" }); //5min
Chapter_9.push({ "Id": 158, "ChapterId": 9, "Title": "Types of Chart", "PageName": "types-of-charts.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/types-of-charts.htm", "Time": "2400" }); //40min
Chapter_9.push({ "Id": 159, "ChapterId": 9, "Title": "Components of a Chart", "PageName": "components-of-a-chart.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/components-of-a-chart.htm", "Time": "1200" }); //20min
Chapter_9.push({ "Id": 160, "ChapterId": 9, "Title": "Draw a Chart", "PageName": "draw-a-chart.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/draw-a-chart.htm", "Time": "5400" }); //90min
Chapter_9.push({ "Id": 161, "ChapterId": 9, "Title": "Editing of a Chart", "PageName": "editing-of-a-chart.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/editing-of-a-chart.htm", "Time": "2400" }); //40min
Chapter_9.push({ "Id": 162, "ChapterId": 9, "Title": "Resizing the Chart", "PageName": "resizing-the-chart.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/resizing-the-chart.htm", "Time": "600" }); //10min
Chapter_9.push({ "Id": 163, "ChapterId": 9, "Title": "Moving the Chart", "PageName": "moving-the-chart.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/moving-the-chart.htm", "Time": "300" }); //5min
Chapter_9.push({ "Id": 164, "ChapterId": 9, "Title": "Copying the Chart to Microsoft word", "PageName": "copying-the-chart.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/copying-the-chart.htm", "Time": "600" }); //10min
Chapter_9.push({ "Id": 165, "ChapterId": 9, "Title": "Graphics - Autoshapes and Smart Art", "PageName": "graphics-autoshapes-and-smart-art.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/graphics-autoshapes-and-smart-art.htm", "Time": "2400" }); //40min
Chapter_9.push({ "Id": 166, "ChapterId": 9, "Title": "Smart Art Graphics", "PageName": "smart-art-graphics.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/smart-art-graphics.htm", "Time": "900" }); //15min
Chapter_9.push({ "Id": 167, "ChapterId": 9, "Title": "Adding Clip Art", "PageName": "adding-clip-art.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/adding-clip-art.htm", "Time": "1800" }); //30min
Chapter_9.push({ "Id": 168, "ChapterId": 9, "Title": "Inserting and Editing a Picture from a File", "PageName": "inserting-and-editing-picture.htm", "Description": "", "Link": "Course/fundamentals-of-computer/excel/inserting-and-editing-picture.htm", "Time": "1200" }); //20min


// chapter 10
var Chapter_10 = [];
Chapter_10.push({ "Id": 169, "ChapterId": 10, "Title": "Microsoft Powerpoint", "PageName": "microsoft-powerpoint.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/microsoft-powerpoint.htm", "Time": "900" }); //15min
Chapter_10.push({ "Id": 170, "ChapterId": 10, "Title": "Introduction", "PageName": "introduction.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/introduction.htm", "Time": "1200" }); //20min
Chapter_10.push({ "Id": 171, "ChapterId": 10, "Title": "Starting a Powerpoint", "PageName": "starting-a-powerpoint.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/starting-a-powerpoint.htm", "Time": "3600" }); //60min
Chapter_10.push({ "Id": 172, "ChapterId": 10, "Title": "Installed Templates", "PageName": "installed-templates.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/installed-templates.htm", "Time": "1800" }); //30min
Chapter_10.push({ "Id": 173, "ChapterId": 10, "Title": "Design Template", "PageName": "design-template.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/design-template.htm", "Time": "1200" }); //20min
Chapter_10.push({ "Id": 174, "ChapterId": 10, "Title": "Blank Presentations", "PageName": "blank-presentations.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/blank-presentations.htm", "Time": "1800" }); //30min
Chapter_10.push({ "Id": 175, "ChapterId": 10, "Title": "Slide Layouts", "PageName": "slide-layouts.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/slide-layouts.htm", "Time": "3600" }); //60min
Chapter_10.push({ "Id": 176, "ChapterId": 10, "Title": "Selecting Content", "PageName": "selecting-content.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/selecting-content.htm", "Time": "1800" }); //30min
Chapter_10.push({ "Id": 177, "ChapterId": 10, "Title": "Open and Existing Presentations", "PageName": "open-an-existing-presentation.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/open-an-existing-presentation.htm", "Time": "1800" }); //30min
Chapter_10.push({ "Id": 178, "ChapterId": 10, "Title": "Viewing slides", "PageName": "viewing-slides.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/viewing-slides.htm", "Time": "1800" }); //30min
Chapter_10.push({ "Id": 179, "ChapterId": 10, "Title": "Normal View", "PageName": "normal-view.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/normal-view.htm", "Time": "1800" }); //30min
Chapter_10.push({ "Id": 180, "ChapterId": 10, "Title": "Slide Sorter View", "PageName": "slide-sorter-view.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/slide-sorter-view.htm", "Time": "1800" }); //30min
Chapter_10.push({ "Id": 181, "ChapterId": 10, "Title": "Slide Show View", "PageName": "slide-show-view.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/slide-show-view.htm", "Time": "1200" }); //20min
Chapter_10.push({ "Id": 182, "ChapterId": 10, "Title": "Design Tips", "PageName": "design-tips.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/design-tips.htm", "Time": "1800" }); //30min
Chapter_10.push({ "Id": 183, "ChapterId": 10, "Title": "Working with Slides", "PageName": "working-with-slides.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/working-with-slides.htm", "Time": "1800" }); //30min
Chapter_10.push({ "Id": 184, "ChapterId": 10, "Title": "Applying a Design Templates", "PageName": "applying-design-template.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/applying-design-template.htm", "Time": "1800" }); //30min
Chapter_10.push({ "Id": 185, "ChapterId": 10, "Title": "Changing Slide Layouts", "PageName": "changing-slide-layouts.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/changing-slide-layouts.htm", "Time": "1800" }); //30min
Chapter_10.push({ "Id": 186, "ChapterId": 10, "Title": "Insert/Edit Existing Slides as Your New Slides", "PageName": "insert-or-edit-existing-slides.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/insert-or-edit-existing-slides.htm", "Time": "3600" }); //60min
Chapter_10.push({ "Id": 187, "ChapterId": 10, "Title": "Reordering Slides", "PageName": "reordering-slides.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/reordering-slides.htm", "Time": "900" }); //15min
Chapter_10.push({ "Id": 188, "ChapterId": 10, "Title": "Hide Slides", "PageName": "hide-slides.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/hide-slides.htm", "Time": "600" }); //10min
Chapter_10.push({ "Id": 189, "ChapterId": 10, "Title": "Moving Between Slides", "PageName": "moving-between-slides.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/moving-between-slides.htm", "Time": "1800" }); //30min
Chapter_10.push({ "Id": 190, "ChapterId": 10, "Title": "Working with Text", "PageName": "working-with-text.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/working-with-text.htm", "Time": "1800" }); //30min
Chapter_10.push({ "Id": 191, "ChapterId": 10, "Title": "Inserting Text", "PageName": "inserting-text.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/inserting-text.htm", "Time": "1800" }); //30min
Chapter_10.push({ "Id": 192, "ChapterId": 10, "Title": "Formatting Text", "PageName": "formatting-text.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/formatting-text.htm", "Time": "7200" }); //120min
Chapter_10.push({ "Id": 193, "ChapterId": 10, "Title": "Text Box Properties", "PageName": "text-box-properties.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/text-box-properties.htm", "Time": "2400" }); //40min
Chapter_10.push({ "Id": 194, "ChapterId": 10, "Title": "Adding Notes", "PageName": "adding-notes.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/adding-notes.htm", "Time": "1200" }); //20min
Chapter_10.push({ "Id": 195, "ChapterId": 10, "Title": "Spell Check", "PageName": "spell-check.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/spell-check.htm", "Time": "3600" }); //60min
Chapter_10.push({ "Id": 196, "ChapterId": 10, "Title": "Saving and Printing", "PageName": "saving-and-printing.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/saving-and-printing.htm", "Time": "900" }); //15min
Chapter_10.push({ "Id": 197, "ChapterId": 10, "Title": "Page Setup", "PageName": "page-setup.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/page-setup.htm", "Time": "2400" }); //40min
Chapter_10.push({ "Id": 198, "ChapterId": 10, "Title": "Save as File", "PageName": "save-as-file.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/save-as-file.htm", "Time": "900" }); //15min
Chapter_10.push({ "Id": 199, "ChapterId": 10, "Title": "Save as Web Page", "PageName": "save-as-web-page.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/save-as-web-page.htm", "Time": "1200" }); //20min
Chapter_10.push({ "Id": 200, "ChapterId": 10, "Title": "Print", "PageName": "print.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/print.htm", "Time": "1800" }); //30min
Chapter_10.push({ "Id": 201, "ChapterId": 10, "Title": "Close Document", "PageName": "close-document.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/close-document.htm", "Time": "600" }); //10min
Chapter_10.push({ "Id": 202, "ChapterId": 10, "Title": "Exit Powerpoint Program", "PageName": "exit-powerpoint-program.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/exit-powerpoint-program.htm", "Time": "600" }); //10min
Chapter_10.push({ "Id": 203, "ChapterId": 10, "Title": "Keyboard Shortcuts", "PageName": "keyboard-shortcuts.htm", "Description": "", "Link": "Course/fundamentals-of-computer/power-point/keyboard-shortcuts.htm", "Time": "3600" }); //60min


// chapter 11
var Chapter_11 = [];
Chapter_11.push({ "Id": 204, "ChapterId": 11, "Title": "Computer Networks", "PageName": "computer-networks.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-networks/computer-networks.htm", "Time": "900" }); //15min
Chapter_11.push({ "Id": 205, "ChapterId": 11, "Title": "Local Area Network", "PageName": "local-area-network.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-networks/local-area-network.htm", "Time": "1200" }); //20min
Chapter_11.push({ "Id": 206, "ChapterId": 11, "Title": "Metropolitan Area Network", "PageName": "metropolitan-area-network.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-networks/metropolitan-area-network.htm", "Time": "900" }); //15min
Chapter_11.push({ "Id": 207, "ChapterId": 11, "Title": "Wide Area Network", "PageName": "wide-area-network.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-networks/wide-area-network.htm", "Time": "900" }); //15min
Chapter_11.push({ "Id": 208, "ChapterId": 11, "Title": "Protocols", "PageName": "protocols.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-networks/protocols.htm", "Time": "1200" }); //20min
//Chapter_11.push({ "Id": 6, "ChapterId": 11, "Title": "Types of Protocols", "PageName": "types-of-protocols.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-networks/types-of-protocols.htm", "Time": "" });
//Chapter_11.push({ "Id": 7, "ChapterId": 11, "Title": "Transmission Control Protocol", "PageName": "transmission-control-protocol.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-networks/transmission-control-protocol.htm", "Time": "" });
Chapter_11.push({ "Id": 209, "ChapterId": 11, "Title": "Internet Protocol", "PageName": "internet-protocol.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-networks/internet-protocol.htm", "Time": "600" }); //10min
Chapter_11.push({ "Id": 210, "ChapterId": 11, "Title": "Post Office Protocol", "PageName": "postoffice-protocol.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-networks/postoffice-protocol.htm", "Time": "2100" }); //35min
//Chapter_11.push({ "Id": 10, "ChapterId": 11, "Title": "Saimple Mail Transport Protocol", "PageName": "simple-mail-transport-protocol.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-networks/simple-mail-transport-protocol.htm", "Time": "" });
Chapter_11.push({ "Id": 211, "ChapterId": 11, "Title": "Hyper Text Transfer Protocol", "PageName": "hyper-text-transfer-protocol.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-networks/hyper-text-transfer-protocol.htm", "Time": "600" }); //10min
Chapter_11.push({ "Id": 212, "ChapterId": 11, "Title": "File Transfer Protocol", "PageName": "file-transfer-protocol.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-networks/file-transfer-protocol.htm", "Time": "600" }); //10min
Chapter_11.push({ "Id": 213, "ChapterId": 11, "Title": "IP Address", "PageName": "ip-address.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-networks/ip-address.htm", "Time": "900" }); //15min
Chapter_11.push({ "Id": 214, "ChapterId": 11, "Title": "Share a Printer", "PageName": "share-a-printer.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-networks/share-a-printer.htm", "Time": "300" }); //5min
Chapter_11.push({ "Id": 215, "ChapterId": 11, "Title": "File and Printer Sharing", "PageName": "file-and-printer-sharing.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-networks/file-and-printer-sharing.htm", "Time": "600" }); //10min
Chapter_11.push({ "Id": 216, "ChapterId": 11, "Title": "Sharing Printers", "PageName": "sharing-printers.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-networks/sharing-printers.htm", "Time": "1200" }); //20min
Chapter_11.push({ "Id": 217, "ChapterId": 11, "Title": "Stop Printer Sharing", "PageName": "stop-printer-sharing.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-networks/stop-printer-sharing.htm", "Time": "600" }); //10min
Chapter_11.push({ "Id": 218, "ChapterId": 11, "Title": "Connect to Printer", "PageName": "connect-to-printer.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-networks/connect-to-printer.htm", "Time": "3600" }); //1hour
Chapter_11.push({ "Id": 219, "ChapterId": 11, "Title": "Setting or Removing Permissions", "PageName": "setting-or-removing-permissions.htm", "Description": "", "Link": "Course/fundamentals-of-computer/computer-networks/setting-or-removing-permissions.htm", "Time": "1800" }); //30min


// chapter 12
var Chapter_12 = [];
Chapter_12.push({ "Id": 220, "ChapterId": 12, "Title": "Introduction", "PageName": "introduction.htm", "Description": "", "Link": "Course/fundamentals-of-computer/security/introduction.htm", "Time": "900" }); //15min
Chapter_12.push({ "Id": 221, "ChapterId": 12, "Title": "Antivirus", "PageName": "antivirus.htm", "Description": "", "Link": "Course/fundamentals-of-computer/security/antivirus.htm", "Time": "900" }); //15min
Chapter_12.push({ "Id": 222, "ChapterId": 12, "Title": "Popular Antivirus Software", "PageName": "popular-antivirus-software.htm", "Description": "", "Link": "Course/fundamentals-of-computer/security/popular-antivirus-software.htm", "Time": "1800" }); //30min
Chapter_12.push({ "Id": 223, "ChapterId": 12, "Title": "Best Practices Antivirus", "PageName": "best-practices.htm", "Description": "", "Link": "Course/fundamentals-of-computer/security/best-practices.htm", "Time": "1200" }); //20min
Chapter_12.push({ "Id": 224, "ChapterId": 12, "Title": "Firewall", "PageName": "firewall.htm", "Description": "", "Link": "Course/fundamentals-of-computer/security/firewall.htm", "Time": "900" }); //15min
Chapter_12.push({ "Id": 225, "ChapterId": 12, "Title": "Configure Windows XP Firewall", "PageName": "configure-windows-xp-firewall.htm", "Description": "", "Link": "Course/fundamentals-of-computer/security/configure-windows-xp-firewall.htm", "Time": "1800" }); //30min
Chapter_12.push({ "Id": 226, "ChapterId": 12, "Title": "Best Practices Firewall", "PageName": "best-practices-firewall.htm", "Description": "", "Link": "Course/fundamentals-of-computer/security/best-practices-firewall.htm", "Time": "900" }); //15min
Chapter_12.push({ "Id": 227, "ChapterId": 12, "Title": "Security Essentials", "PageName": "security-essentials.htm", "Description": "", "Link": "Course/fundamentals-of-computer/security/security-essentials.htm", "Time": "1200" }); //20min
Chapter_12.push({ "Id": 228, "ChapterId": 12, "Title": "Safe Mode", "PageName": "safe-mode.htm", "Description": "", "Link": "Course/fundamentals-of-computer/security/safe-mode.htm", "Time": "900" }); //15min
Chapter_12.push({ "Id": 229, "ChapterId": 12, "Title": "Start the Computer in Safe Mode", "PageName": "start-the-computer-in-safe-mode.htm", "Description": "", "Link": "Course/fundamentals-of-computer/security/start-the-computer-in-safe-mode.htm", "Time": "900" }); //15min
Chapter_12.push({ "Id": 230, "ChapterId": 12, "Title": "MSConfig Utility", "PageName": "msconfig-utility.htm", "Description": "", "Link": "Course/fundamentals-of-computer/security/msconfig-utility.htm", "Time": "2700" }); //45min


// chapter 13
var Chapter_13 = [];
Chapter_13.push({ "Id": 231, "ChapterId": 13, "Title": "Introduction", "PageName": "introduction.htm", "Description": "", "Link": "Course/fundamentals-of-computer/internet/introduction.htm", "Time": "600" }); //10min
Chapter_13.push({ "Id": 232, "ChapterId": 13, "Title": "Browsers", "PageName": "browsers.htm", "Description": "", "Link": "Course/fundamentals-of-computer/internet/browsers.htm", "Time": "1200" }); //20min
Chapter_13.push({ "Id": 233, "ChapterId": 13, "Title": "Popular Web Browsers", "PageName": "popular-web-browsers.htm", "Description": "", "Link": "Course/fundamentals-of-computer/internet/popular-web-browsers.htm", "Time": "1200" }); //20min
Chapter_13.push({ "Id": 234, "ChapterId": 13, "Title": "Web Browser User Interface", "PageName": "web-browser-user-interface.htm", "Description": "", "Link": "Course/fundamentals-of-computer/internet/web-browser-user-interface.htm", "Time": "900" }); //15min
Chapter_13.push({ "Id": 235, "ChapterId": 13, "Title": "Internet Options", "PageName": "internet-options.htm", "Description": "", "Link": "Course/fundamentals-of-computer/internet/internet-options.htm", "Time": "900" }); //15min
Chapter_13.push({ "Id": 236, "ChapterId": 13, "Title": "Cleanup History", "PageName": "cleanup-history.htm", "Description": "", "Link": "Course/fundamentals-of-computer/internet/cleanup-history.htm", "Time": "3600" }); //60min
Chapter_13.push({ "Id": 237, "ChapterId": 13, "Title": "Protocol and Security", "PageName": "protocol-and-security.htm", "Description": "", "Link": "Course/fundamentals-of-computer/internet/protocol-and-security.htm", "Time": "1200" }); //20min
Chapter_13.push({ "Id": 238, "ChapterId": 13, "Title": "EMAIL System", "PageName": "email-system.htm", "Description": "", "Link": "Course/fundamentals-of-computer/internet/email-system.htm", "Time": "900" }); //15min
Chapter_13.push({ "Id": 239, "ChapterId": 13, "Title": "Popular Email Service Providers", "PageName": "popular-email-service-providers.htm", "Description": "", "Link": "Course/fundamentals-of-computer/internet/popular-email-service-providers.htm", "Time": "600" }); //10min
Chapter_13.push({ "Id": 240, "ChapterId": 13, "Title": "Password Strength", "PageName": "password-strength.htm", "Description": "", "Link": "Course/fundamentals-of-computer/internet/password-strength.htm", "Time": "600" }); //10min
Chapter_13.push({ "Id": 241, "ChapterId": 13, "Title": "SPAM Emails", "PageName": "spam-emails.htm", "Description": "", "Link": "Course/fundamentals-of-computer/internet/spam-emails.htm", "Time": "900" }); //15min
Chapter_13.push({ "Id": 242, "ChapterId": 13, "Title": "Social Engineering Emails", "PageName": "social-engineering-emails.htm", "Description": "", "Link": "Course/fundamentals-of-computer/internet/social-engineering-emails.htm", "Time": "1200" }); //20min
Chapter_13.push({ "Id": 243, "ChapterId": 13, "Title": "Email Best Practices", "PageName": "email-best-practices.htm", "Description": "", "Link": "Course/fundamentals-of-computer/internet/email-best-practices.htm", "Time": "1200" }); //20min
Chapter_13.push({ "Id": 244, "ChapterId": 13, "Title": "Search Engines", "PageName": "search-engines.htm", "Description": "", "Link": "Course/fundamentals-of-computer/internet/search-engines.htm", "Time": "600" }); //10min
Chapter_13.push({ "Id": 245, "ChapterId": 13, "Title": "Popular Search Engines", "PageName": "popular-search-engines.htm", "Description": "", "Link": "Course/fundamentals-of-computer/internet/popular-search-engines.htm", "Time": "600" }); //10min
Chapter_13.push({ "Id": 246, "ChapterId": 13, "Title": "Google Tricks", "PageName": "google-tricks.htm", "Description": "", "Link": "Course/fundamentals-of-computer/internet/google-tricks.htm", "Time": "1200" }); //20min
Chapter_13.push({ "Id": 247, "ChapterId": 13, "Title": "Downloads and Installations", "PageName": "downloads-and-installations.htm", "Description": "", "Link": "Course/fundamentals-of-computer/internet/downloads-and-installations.htm", "Time": "900" }); //15min


// chapter 14
var Chapter_14 = [];
Chapter_14.push({ "Id": 248, "ChapterId": 14, "Title": "Introduction", "PageName": "introduction.htm", "Description": "", "Link": "Course/fundamentals-of-computer/social-media/introduction.htm", "Time": "900" }); //15min
Chapter_14.push({ "Id": 249, "ChapterId": 14, "Title": "Social Media Websites", "PageName": "social-media-websites.htm", "Description": "", "Link": "Course/fundamentals-of-computer/social-media/social-media-websites.htm", "Time": "900" }); //15min
Chapter_14.push({ "Id": 250, "ChapterId": 14, "Title": "Popular Social Media", "PageName": "popular-social-media.htm", "Description": "", "Link": "Course/fundamentals-of-computer/social-media/popular-social-media.htm", "Time": "1800" }); //30min
Chapter_14.push({ "Id": 251, "ChapterId": 14, "Title": "Best Practices", "PageName": "best-practices.htm", "Description": "", "Link": "Course/fundamentals-of-computer/social-media/best-practices.htm", "Time": "2700" }); //45min
//Chapter_14.push({ "Id": 5, "ChapterId": 14, "Title": "Best Practices", "PageName": "best-practices.htm", "Description": "", "Link": "Course/fundamentals-of-computer/14/best-practices.htm", "Time": "" });


var Chapter_Indroduction = [];
Chapter_Indroduction.push({ "ChapterId": 1, "Introduction": "A computer is a device that takes raw data as input from the user, processes these this data and gives the result (output) and saves output for the future use. It i’s a general purpose device, that can be programmed to carry out a set of arithmetic or logical operations. Computer components are divided into two major categories, namely, hardware and software." });
Chapter_Indroduction.push({ "ChapterId": 2, "Introduction": "Computer hardware is the collection of physical devices that constitutes a computer system. Computer hardware refers to the physical parts or components of a computer such as monitor, keyboard, computer data storage, mouse, system unit (graphic cards, sound cards, memory, motherboard and chips), etc. all of which are physical objects that can be touched." });
Chapter_Indroduction.push({ "ChapterId": 3, "Introduction": "<p>Most components in a computer can take one of two states: on/off; we can represent these states by the binary digits 0 (off) and 1 (on). Each binary digit (or bit) has two possible values (0, 1), but groups of binary digits can be used to represent larger numbers. Most computer systems use groups of 8 bits as their basic building blocks. These 8-bit groups are referred to as bytes.</p><p> Storage space is normally expressed in bytes. A kilobyte (K) is 1,024 bytes. A megabyte (MB) is 1,024 kilobytes. A gigabyte (GB) is 1,024 megabytes. A terabyte is 1,024 gigabytes. A megabyte is sometimes “<i>rounded down</i>” to only 1,000 kilobytes,and likewise for the larger units.</p><p>Computers are designed to allow data to be input, processed, stored, and output. In this chapter, we'll be taking a casual look at a computer's design for storing data.</p>" });
Chapter_Indroduction.push({ "ChapterId": 4, "Introduction": "<p> An <b>operating system</b> (OS) is a software program that enables the computer hardware to communicate and operate with the computer software. Without an operating system, the computer hardware and software programs would be non-functional. </p><p>When computers were first introduced the user interacted with them using a command line interface, which required the user to perform a series of commands in order to interact with the computer and its hardware and software. Today, almost every computer is using a Graphical User Interface (GUI) operating system that is much easier to use and operate.</p><p>Examples of computer operating systems :</p><ul><li>Microsoft Windows 7 - PC and IBM compatible operating system. Microsoft Windows is the most commonly found and used operating system</li><li>Apple MacOS - Apple computer's operating system</li><li>Ubuntu Linux - A popular variant of Linux used with PC and IBM compatible computers</li><li>Google Android - operating system used with Android compatible phones</li><li>iOS - Operating system used with the Apple iPhone</li></ul>" });
Chapter_Indroduction.push({ "ChapterId": 5, "Introduction": "<p><b>Application software</b> is the program that helps you perform useful tasks on a computer. An accounting software, enterprise software, graphics software, media players, and office suites etc. are few example of application software. </p><p><b>MS PAINT</b></p> <p>Paint is a program used to draw, color, and edit pictures. You can use Paint like a digital sketchpad to make simple pictures or add text and designs to other pictures, such as those taken with your digital camera.</p><p>MS Paint application could be started using following steps :</p><ol><li>Click on Windows start button</li><li>Click on Programs and then accessories</li><li>Click on Paint</li></ol>" });
Chapter_Indroduction.push({ "ChapterId": 6, "Introduction": "<p><b>Application software</b> is the program that helps you perform useful tasks on a computer. An accounting software, enterprise software, graphics software, media players, and office suites etc. are few example of application software. </p><p><b>MS PAINT</b></p> <p>Paint is a program used to draw, color, and edit pictures. You can use Paint like a digital sketchpad to make simple pictures or add text and designs to other pictures, such as those taken with your digital camera.</p><p>MS Paint application could be started using following steps :</p><ol><li>Click on Windows start button</li><li>Click on Programs and then accessories</li><li>Click on Paint</li></ol>" });
Chapter_Indroduction.push({ "ChapterId": 7, "Introduction": "<p><b>Application software</b> is the program that helps you perform useful tasks on a computer. An accounting software, enterprise software, graphics software, media players, and office suites etc. are few example of application software.</p><p><b>NOTEPAD</b></p><p>Notepad is a handy program that a user can type in text quickly and easily. It is not for publishing a book but more of a scratchpad. This chapter will cover followingtopics on <i>Notepad</i> :</p><ul><li>Opening Notepad</li><li>Saving a document</li><li>Printing a document</li><li>Opening an existing document</li></ul>" });
Chapter_Indroduction.push({ "ChapterId": 8, "Introduction": "Word processing is an application program that allows you to create letters, reports, newsletters, tables, form letters, brochures, and Web pages. You can add pictures, tables, and charts to your documents using Microsoft Word." });
Chapter_Indroduction.push({ "ChapterId": 9, "Introduction": "<p>Microsoft Excel is one of the most widely used spreadsheet applications. It is a part of Microsoft Office suite. Spreadsheet is quite useful in entering, editing, analyzing and storing data. Arithmetic operations with numerical data such as addition, subtraction, multiplication and division can be done using Excel. You can sort numbers/characters according to different criteria and use simple financial, mathematical and statistical formulas.</p>" });
Chapter_Indroduction.push({ "ChapterId": 10, "Introduction": "<p>Microsoft PowerPoint 2007 enhances your presentations with pictures, sound effects, tables and charts. The main features of PowerPoint are :</p><ul><li>PowerPoint gives you several ways to create a presentation.</li><li>Creating slides is the root of all your work with PowerPoint. You can get your ideas across with a series of slides.</li><li>Adding text will help you put your ideas into words.</li><li>The multimedia features makes your slides sparkle. You can add clip art, sound effects, music, video clips etc.</li><li>Preparing a presentation is easy in PowerPoint. Once if you have created slides, you can put them in order, time your slide show, and present them to your audience.</li></ul>" });
Chapter_Indroduction.push({ "ChapterId": 11, "Introduction": "<p>Microsoft PowerPoint 2007 enhances your presentations with pictures, sound effects, tables and charts. The main features of PowerPoint are :</p><ul><li>PowerPoint gives you several ways to create a presentation.</li><li>Creating slides is the root of all your work with PowerPoint. You can get your ideas across with a series of slides.</li><li>Adding text will help you put your ideas into words.</li><li>The multimedia features makes your slides sparkle. You can add clip art, sound effects, music, video clips etc.</li><li>Preparing a presentation is easy in PowerPoint. Once if you have created slides, you can put them in order, time your slide show, and present them to your audience.</li></ul>" });
Chapter_Indroduction.push({ "ChapterId": 12, "Introduction": "<p>Computer security is the practice of defending computer/information from unauthorized access, use, disclosure, disruption, modification, perusal, inspection, recording or destruction.</p><p>This chapter focuses on understanding the basics tools to secure your computer/information from hazards. It includes explanations of important concepts, step by step guides for many procedures, and web links to more information and downloads for helpful security programs.</p><p>This chapter will majorly focus on following security tools to make your computer/information more secure:</p><ol><li>Antivirus</li><li>Firewall </li><li>Security Essentials</li><li>Safe mode</li><li>msconfig utility</li></ol>" });
Chapter_Indroduction.push({ "ChapterId": 13, "Introduction": "<p>The Internet is a global system of interconnected computer networks. It is a network of networks that consists of private, public, academic, business, and government networks, that are linked by a broad array of electronic, wireless and optical networking technologies.</p><p>You will learn about various tools and terms which are related to internet :</p><ol><li>Browsers</li><li>Protocol & security</li><li>Email system</li><li>Search engines</li><li>Downloads and installations</li></ol>" });
Chapter_Indroduction.push({ "ChapterId": 14, "Introduction": "<p>Media is an instrument on communication, like a newspaper or a radio, so <b>social media</b> would be a social instrument of communication. <b>Social media</b> a group of Internet-based applications/Websites for interaction among people; the people create, share, and/or exchange information and ideas in virtual communities and networks. Social media depends on mobile and web-based technologies to create highly interactive platforms through which individuals and communities share, co-create, discuss, and modify user-generated content.</p> <p>Social-media technologies take on many different forms including magazines, Internet forums, weblogs, social blogs, micro blogging, wikis, social networks, photographs or pictures, video, rating and social bookmarking. Technologies include blogging, picture-sharing, blogs, wall-posting, music-sharing and crowd sourcing to name a few.</p><p>Most popular kind of social media are :</p><ol><li>collaborative projects (for example, Wikipedia)</li><li>blogs and micro blogs (for example, Twitter)</li><li>content communities (for example, YouTube)</li><li>social networking sites (for example, Facebook)</li><li>virtual game worlds (e.g., World of Warcraft)</li><li>virtual social worlds (e.g. Second Life)</li></ol>" });

// creating left


var chapterId = 0;
var sectionId = 0;

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

function AppendHeader() {
    //var main = $(".content-container").parent();
    var main = $(".content-container");
    var headerContainer = $("<div/>");
    $(headerContainer).attr("class", "header-container");

    var top_navigation = $("<div/>");
    $(top_navigation).attr("class", "top-navigation");
    $(top_navigation).html('<ul class="top-nav" style="width: 36%;padding-right: 11%"><li class="top"><a href="' + BASE_URL + '" class="top" id="homeLink">Home</a></li><li class="user-name top">Welcome, Guest</li></ul>');
    $(headerContainer).append(top_navigation);


    var header = $("<div/>");
    $(header).attr("class", "header");
    $(header).html('<div class="logo"><img id="logoImage" src="' + BASE_URL + 'static/images/logo.png" style="height: 100%; border-radius: 4px;"></div><div id="divLogoHeader" class="college-name"></div><div class="search-container"><input type="text" class="simplebox" placeholder="Search..." id="txtSearch" onkeypress="SearchKeyPress(event);"><div class="btn btn-success" onclick="Search();">Go</div></div>');
    $(headerContainer).append(header);

    $(headerContainer).insertBefore(main);
    //$(main).append(headerContainer);
    $(".content").css("margin-bottom", "5%");
}

function AppendMayIHelpYouSection() {
    var mayIHelpYou = $("<div/>");
    $(mayIHelpYou).html('<div><div><div id="ql_SideBar" style="filter: none;"><div title="Click here for any help" id="ql_Display" onclick="showMayIHelpYouWindow();"' +
                ' style="background-color: #404040 !important; color: White; background-image: none;border-top-left-radius: 3px; border-top-right-radius: 3px;">' +
                ' <span><span id="ql_ShortlistHeadline" class="arrowUp rfloat"></span>May I help you?<span id="ql_Counter"></span> </span>' +
                ' </div><div id="ql_Tab" style="display: none"><div id="ql_List" style="text-align: center;background-color: #fff;padding: 2% 0;">' +
                ' <input type="text" id="txtName" placeholder="Full Name" /><span style="color: Red">*</span>' +
                ' <input type="text" id="txtMobile" placeholder="Mobile Number"  /><span style="color: Red">*</span>' +
                ' <input type="text" id="txtEmail1" placeholder="Email Address" />' +
                ' <textarea id="txtQuery" placeholder="Your question" cols="20" rows="2" style="max-height: 100px;margin-bottom: 3%; margin-left: 2%; padding: 6px;"></textarea>' +
                ' <span style="color: Red">*</span><div id="buttons" style="text-align: left; margin-left: 7%;">' +
                ' <input type="button" id="btnSubmit" value="Submit" class="btn btn-success" onclick="SaveMayIHelpYouQuery();" />' +
                ' <input type="button" id="btnClose" value="Close" class="btn btn-convert" onclick="showMayIHelpYouWindow();" style="text-align: left" />' +
                ' </div><div id="queryStatus" style="color: Green"></div><div id="ql_EmptyText"></div></div>' +
                ' <div id="ql_Foot" style="text-align: justify; height: 0%; text-align: left;"></div></div></div></div>');

    $(".content-container").append(mayIHelpYou);
}


function Init() {
    // Hide Main-Container till completely rendered
    $(".content-container").css("display", "none");
    $(".content-container").css("margin-left", "10%");
    $(".content-container").css("width", "80%");

    //Add Loader image while page is rendered
    var loaderContain = $("<div/>");
    $(loaderContain).attr("id", "loaderMainContain");
    $(loaderContain).attr("style", "width: 100%;float: left;text-align: center;font-size:24px;margin-top: 20%;")
    $(loaderContain).html("<img src='" + BASE_URL + "static/images/loading.gif' /> Loading... Please wait ");
    var ele = $(".content-container").parent();
    $(ele).append(loaderContain);

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
    $(divModal).html('<div class="modal fade" id="ReportIssue" tabindex="-2" role="dialog" aria-labelledby="myModalLabel2" aria-hidden="true"> ' +
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

    $(".content-main").append(divModal);

    //adding link-button for report issue
    var reportIssue = $("<div/>");
    $(reportIssue).attr("style", "float: left;");
    $(reportIssue).html("<a style='color: white' data-toggle='modal' onClick='ShowReportIssuePopup();' href='javascript:void(0);'>Report Issue</a>");
    $("#content_navigation_1").append(reportIssue);

    getQueryStrings();
    var qs = getQueryStrings();
    chapterId = qs["chapterId"];
    sectionId = qs["sectionId"];

    var ref = document.location.href.indexOf(BASE_URL);
    if (ref >= 0) {
        try {
            var values = parent.document.location.href.substring(BASE_URL.length).split("/");
            var value = values[0].split("?");

            if (value[0] == "HintPage.aspx") {
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
            else {
                AppendHeader();
                AppendMayIHelpYouSection();
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

                //CallHandler("Handler/GetUserChapterStatus.ashx", OnComplete);
                var ref = document.location.href.indexOf(BASE_URL);
                if (ref >= 0) {
                    try {
                        var values = document.location.href.substring(BASE_URL.length).split("/");
                        chapterId = unescape(values[2]);
                        chapterId = GetChapterID(chapterId);

                        if (values[3].indexOf('?') >= 0) {
                            sectionId = GetChapterSectionID(chapterId, values[3].substring(0, values[3].indexOf('?')));
                        }
                        else {
                            sectionId = GetChapterSectionID(chapterId, values[3]);
                        }

                        if (chapterId == 8 || chapterId == 9 || chapterId == 10) {
                            chapterId = 11;
                            var sectionList = GetChapterSectionList(chapterId);
                            sectionId = sectionList[0].Id;
                        }

                        //alert(sectionId);
                    }
                    catch (e) {
                        alert(e);
                    }
                }
                // creating left navigation
                BuilLeftNavigation();
                //creating breadcrumbs
                BuildBreadcrumbs(chapterId, sectionId);
                // creating and assinging next previus button functionality
                BuildNextPreviusButtonFunctional(chapterId, sectionId);
                // get Chapter and section Quiz
                GetQuizFromDatabase();
                // check if user browser support csss3 and html5
                CheckUserBrowerSupportCSS3HTML5();

                if (chapterId == Chapter_Index[Chapter_Index.length - 1].Id) {
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

//var path = BASE_URL + "Handler/GetUserChapterStatus.ashx";

var currentId;

function CallHandler(queryString, OnComp) {

    var path = BASE_URL + queryString;

    $.ajax({
        url: path,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        responseType: "json",
        cache: "false",
        //success: OnComplete,
        success: OnComp,
        error: OnFail
    });
    return false;
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

        $(li).html("<a href='javascript:void(0);' onClick='leftNavigationClick(\"" + chapterSection[i].Link + "\");'> " + chapterSection[i].Title + "</a>");
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

    var _URL = document.location.href;

    var ele = $("#content_navigation_1");
    $('<iframe src="//www.facebook.com/plugins/like.php?href=' + _URL + '&amp;width&amp;layout=button_count&amp;action=like&amp;show_faces=true&amp;share=true&amp;height=21&amp;appId=137095629816827" scrolling="no" frameborder="0" style="border:none; overflow:hidden; height:30px;" allowTransparency="true"></iframe>').insertBefore(ele);
    // end

    li = $("<li/>");
    $(li).html("<a href='" + BASE_URL + "Dashboard.aspx" + "' target='_top'>" + "Home" + "<a/> <span class='divider'>/</span>");
    $(".breadcrumb").append(li);

    var li = $("<li/>");
    $(li).html("<a target='_blank' href='" + BASE_URL + "CourseDetails.aspx?id=1'>" + chapterObject.Title + "<a/> <span class='divider'>/</span>");
    $(".breadcrumb").append(li);

    li = $("<li/>");
    $(li).html(sectionObject.Title);
    $(".breadcrumb").append(li);

    // creating page header    
    $(".summary-header").html(sectionObject.Title);

    var div = $("<div/>");
    $(div).html(sectionObject.Description);
    $(".content-summary").append(div)

    var time = parseInt(sectionObject.Time) / 60;

    var divTime = $("<div/>");
    $(divTime).attr("style", "float:right");
    $(divTime).html("Estimated time to complete: " + time + " minutes");
    $(".content-summary").append(divTime)

    $(".content-summary").attr("chpId", chapterId);
    $(".content-summary").attr("secId", sectionId);

    $(".content-summary").find(".summary-header").each(function () {
        $("<div style='margin-bottom: 3px;'>Chapter " + chapterObject.Id + " : " + chapterObject.Title + "</div>").insertBefore(this);
    });

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


    if (sectionId == 1) {
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

function GetChapterObject(chapterId) {
    for (var i = 0; i < Chapter_Index.length; i++) {
        if (Chapter_Index[i].Id == chapterId) {
            return Chapter_Index[i];
        }
    }
}

function GetChapterSectionObject(chapterId, sectionId) {
    //alert(chapterId);
    //alert(sectionId);
    var sectionObject = GetChapterSectionList(chapterId);

    for (var i = 0; i < sectionObject.length; i++) {
        if (sectionObject[i].Id == sectionId) {
            return sectionObject[i];
        }
    }
}

function GetChapterSectionID(chapterId, sectionName) {    
    var sectionObject = GetChapterSectionList(chapterId);

    for (var i = 0; i < sectionObject.length; i++) {
        if (sectionObject[i].PageName.toLowerCase() == sectionName.toLowerCase()) {
            return sectionObject[i].Id;
        }
    }
}

function GetChapterID(chapterName) {    
    for (var i = 0; i < Chapter_Index.length; i++) {
        if (Chapter_Index[i].PageName.toLowerCase() == chapterName.toLowerCase()) {
            return Chapter_Index[i].Id;
        }
    }
}

function GetChapterSectionList(chapterId) {
    var ChapterSection = [];
    //alert(chapterId);
    if (chapterId == 1) {
        for (var i = 0; i < Chapter_1.length; i++) {
            ChapterSection.push(Chapter_1[i]);
        }
    }
    else if (chapterId == 2) {
        for (var i = 0; i < Chapter_2.length; i++) {
            ChapterSection.push(Chapter_2[i]);
        }
    }
    else if (chapterId == 3) {
        for (var i = 0; i < Chapter_3.length; i++) {
            ChapterSection.push(Chapter_3[i]);
        }
    }
    else if (chapterId == 4) {
        for (var i = 0; i < Chapter_4.length; i++) {
            ChapterSection.push(Chapter_4[i]);
        }
    }
    else if (chapterId == 5) {
        for (var i = 0; i < Chapter_5.length; i++) {
            ChapterSection.push(Chapter_5[i]);
        }
    }
    else if (chapterId == 6) {
        for (var i = 0; i < Chapter_6.length; i++) {
            ChapterSection.push(Chapter_6[i]);
        }
    }
    else if (chapterId == 7) {
        for (var i = 0; i < Chapter_7.length; i++) {
            ChapterSection.push(Chapter_7[i]);
        }
    }
    else if (chapterId == 8) {
        for (var i = 0; i < Chapter_8.length; i++) {
            ChapterSection.push(Chapter_8[i]);
        }
    }
    else if (chapterId == 9) {
        for (var i = 0; i < Chapter_9.length; i++) {
            ChapterSection.push(Chapter_9[i]);
        }
    }
    else if (chapterId == 10) {
        for (var i = 0; i < Chapter_10.length; i++) {
            ChapterSection.push(Chapter_10[i]);
        }
    }
    else if (chapterId == 11) {
        for (var i = 0; i < Chapter_11.length; i++) {
            ChapterSection.push(Chapter_11[i]);
        }
    }
    else if (chapterId == 12) {
        for (var i = 0; i < Chapter_12.length; i++) {
            ChapterSection.push(Chapter_12[i]);
        }
    }
    else if (chapterId == 13) {
        for (var i = 0; i < Chapter_13.length; i++) {
            ChapterSection.push(Chapter_13[i]);
        }
    }
    else if (chapterId == 14) {
        for (var i = 0; i < Chapter_14.length; i++) {
            ChapterSection.push(Chapter_14[i]);
        }
    }

    return ChapterSection;
}

function BuilLeftNavigationForSylabus() {
    var ul = $("<ul/>");
    $(ul).attr("class", "left-nav blue");

    var liHeader = $("<li/>");
    $(liHeader).attr("class", "active");
    $(liHeader).html("Chapters");
    $(ul).append(liHeader);

    for (var i = 0; i < Chapter_Index.length; i++) {
        var li = $("<li/>");

        //if (Chapter_Index[i].Id == sectionId) {
        $(li).attr("class", "active");
        //}

        $(li).html(Chapter_Index[i].Title);
        //$(li).attr("onClick", "ShowSection(" + Chapter_Index[i].Id + ")");
        $(li).attr("onClick", "ShowSection(this)");
        $(li).attr("chpId", Chapter_Index[i].Id);

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
        $(li).html("<a target='_blank' href='" + BASE_URL + "HintPage.aspx?chapterId=" + chpId + "&sectionId=" + secList[i].Id + "'>" + secList[i].Title + "</a>");
        $(ul).append(li);
    }

    $("#secions").append(ul);

    for (var i = 0; i < Chapter_Indroduction.length; i++) {
        if (Chapter_Indroduction[i].ChapterId == chpId) {
            $("#chapterIntro").html(Chapter_Indroduction[i].Introduction);
        }
    }

}

// assign url to hint page
function AssignUrlToHintPage(chapterId, sectionId) {
    var chapterSection = GetChapterSectionObject(chapterId, sectionId);
    $("#ifHintContent").attr("src", BASE_URL + chapterSection.Link);
}

// Getting Quiz
function GetQuizFromDatabase() {
    var chId = $(".content-summary").attr("chpId");
    var secId = $(".content-summary").attr("secId");

    CallHandler("Handler/GetQuizForAnnonymusUser.ashx?chapterid=" + chId + "&sectionid=" + secId, onCompletePopulateQuiz);
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
            alert(result.Message);
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
                //html += "<input type='radio' id='Ans_" + i + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "' checked='true'/><label for='Ans_" + i + "' style='float: left; margin-left: 5px;;width:94%;'>" + s + "</label>";
                html += "<input type='radio' id='Ans_" + i + "_" + counter + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "' checked='true'/><label for='Ans_" + i + "_" + counter + "' style='float: left; margin-left: 5px;;width:94%;'>" + s + "</label>";
            }
            else {
                //$(div).html(s + "<input type='radio' id='Ans_" + i + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "'/>");
                //html += "<input type='radio' id='Ans_" + i + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "'/><label for='Ans_" + i + "' style='float: left; margin-left: 5px;width:94%;'>" + s + "</label>";
                html += "<input type='radio' id='Ans_" + i + "_" + counter + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "'/><label for='Ans_" + i + "_" + counter + "' style='float: left; margin-left: 5px;width:94%;'>" + s + "</label>";
            }
        }
        else {
            // populating multi correct answer mode
            var s = quiz.AnswerOptionList[i].AnswerOption.replace("%2B", "+");
            s = s.replace("%2B", "+");
            s = s.replace("%2B", "+");
            s = s.replace("%2B", "+");

            if (s == quiz.AnswerText) {
                //$(div).html(s + "<input type='checkbox' id='Ans_" + i + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "' checked='true'/>");
                //html += "<input type='checkbox' id='Ans_" + i + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "' checked='true'/><label for='Ans_" + i + "' style='float: left; margin-left: 5px;;width:94%;'>" + s + "</label>";
                html += "<input type='checkbox' id='Ans_" + i + "_" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "' checked='true'/><label for='Ans_" + i + "_" + counter + "' style='float: left; margin-left: 5px;;width:94%;'>" + s + "</label>";
            }
            else {
                //$(div).html(s + "<input type='checkbox' id='Ans_" + i + "' name='ans" + counter + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "'/>");
                //html += "<input type='checkbox' id='Ans_" + i + "' class='ansRadio' isCurrect='" + quiz.AnswerOptionList[i].IsCurrect + "' answer='" + quiz.AnswerOptionList[i].AnswerOption + "'/><label for='Ans_" + i + "' style='float: left; margin-left: 5px;width:94%;'>" + s + "</label>";
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

    var userAnswer;
    var isCurrect;
    var isChecked;

    var isCurrectAnswer;
    var currectAnswer;

    $("#errorStatus_" + counter).attr("style", "display:none");

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

    var questionId = $("#QuesionText_" + counter).attr("quesId");

    if (!isChecked) {
        $("#errorStatus_" + counter).attr("style", "display:block");
    }
    else {
        $("#btn_" + counter).hide();
        $("#loader_" + counter).show();
        if (isCurrect == 'true') {
            $("#status_" + counter).html("Your answer is correct.");
            $("#status_" + counter).attr("style", "color: green");
        }
        else {
            $("#status_" + counter).html("Your answer is incorrect.<br/>The currect answer is '<b>" + currectAnswer + "'</b>");
            $("#status_" + counter).attr("style", "color: red");
        }
        $("#loader_" + counter).hide();
    }
}

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

    nextClick();
}



var RemoveErrorMessage20SecInterval;
//
function nextClick() {
    var ref = document.location.href.indexOf(BASE_URL);
    if (ref >= 0) {
        try {
            var values = document.location.href.substring(BASE_URL.length).split("/");
            //chapterId = values[2];
            chapterId = unescape(values[2]);
            chapterId = GetChapterID(chapterId);
            sectionId = GetChapterSectionID(chapterId, values[3]);
            //alert(sectionId);
        }
        catch (e) {
            alert(e);
        }
    }

    var chapterObject = GetChapterObject(chapterId);
    var sectionObject = GetChapterSectionObject(chapterId, sectionId);

    var sectionList = GetChapterSectionList(chapterId);

    var nxtSectionId = sectionId + 1;
    var preSectionId = sectionId - 1;

    if (preSectionId <= 0) {
        preSectionId = 1;
    }

    if (nxtSectionId > sectionList[sectionList.length - 1].Id) {
        chapterId++;

        if (chapterId == 8 || chapterId == 9 || chapterId == 10) {
            chapterId = 11;
        }

        if (chapterId > Chapter_Index[Chapter_Index.length - 1].Id) {
            chapterId--;
            secionList = GetChapterSectionList(chapterId);
            if (sectionId == secionList[secionList.length - 1].Id) {
                parent.document.location = BASE_URL + "Dashboard.aspx";
            }
            else {
                //sectionList = GetChapterSectionList(chapterId);
                nxtSectionId = sectionList[0].Id;
                sectionObject = GetChapterSectionObject(chapterId, nxtSectionId);
                document.location = BASE_URL + sectionObject.Link;
            }
        }
        else {
            sectionList = GetChapterSectionList(chapterId);
            nxtSectionId = sectionList[0].Id;
            sectionObject = GetChapterSectionObject(chapterId, nxtSectionId);
            document.location = BASE_URL + sectionObject.Link;
        }
    }
    else {
        sectionObject = GetChapterSectionObject(chapterId, nxtSectionId);
        document.location = BASE_URL + sectionObject.Link;
    }
}


function ClearAnswerFillMessage20Sec() {
    $("#qeusError_1").remove();
    $("#qeusError_2").remove();
}

function preClick() {
    var ref = document.location.href.indexOf(BASE_URL);
    if (ref >= 0) {
        try {
            var values = document.location.href.substring(BASE_URL.length).split("/");
            //chapterId = values[2];
            chapterId = unescape(values[2]);
            chapterId = GetChapterID(chapterId);
            
            sectionId = GetChapterSectionID(chapterId, values[3]);
            //alert(sectionId);
        }
        catch (e) {
            alert(e);
        }
    }

    var chapterObject = GetChapterObject(chapterId);
    var sectionObject = GetChapterSectionObject(chapterId, sectionId);

    var sectionList = GetChapterSectionList(chapterId);

    var nxtSectionId = sectionId + 1;
    var preSectionId = sectionId - 1;

    if (preSectionId < sectionList[0].Id) {
        //preSectionId = 1;
        chapterId--;

        if (chapterId == 8 || chapterId == 9 || chapterId == 10) {
            chapterId = 7;
        }

        sectionList = GetChapterSectionList(chapterId);
        preSectionId = sectionList[0].Id;
        sectionObject = GetChapterSectionObject(chapterId, preSectionId);
        document.location = BASE_URL + sectionObject.Link;
    }
    else {
        sectionObject = GetChapterSectionObject(chapterId, preSectionId);
        document.location = BASE_URL + sectionObject.Link;
    }
}


function leftNavigationClick(link) {
    var chId = $(".content-summary").attr("chpId");
    var secId = $(".content-summary").attr("secId");

    //var ref = document.location.href.indexOf(BASE_URL);
    var navSecId;
    var ref = 1;
    if (ref >= 0) {
        try {
            //var values = document.location.href.substring(BASE_URL.length).split("/");
            var values = link.split("/");
            //chapterId = values[2];
            chapterId = unescape(values[2]);
            chapterId = GetChapterID(chapterId);

            navSecId = GetChapterSectionID(chId, values[3]);
        }
        catch (e) {
            alert(e);
        }
    }
    

    document.location = BASE_URL + link;
}

//Report issue
function ShowReportIssuePopup() {
    $('#ReportIssue').attr("style", "opacity: 1 !important;top: 2%");
    $("#ReportModelBack").attr("style", "display:block");
    //$(this).find("#ReportModelBack").attr("style", "display:block");
    //$('#ReportIssue').fadeIn(100);
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

        var chId = $(".content-summary").attr("chpId");
        var secId = $(".content-summary").attr("secId");

        var path = "Handler/SaveReportIssue.ashx?title=" + title + "&description=" + desc + "&email=" + email + "&expectedContent=" + expContent + "&chapterId=" + chId + "&sectionId=" + secId;
        CallHandler(path, onSaveReportIssueSuccess);
    }
}

function onSaveReportIssueSuccess(result) {
    if (result.Status == "Ok") {
        $("#reportIssueStatus").attr("style", "display:block;background-color: lightgreen");
        $("#reportIssueStatus").html("your query submited successfully. Our technical team contact you shortly.");
        //$('#ReportIssue').fadeOut(1000);

        var chId = $(".content-summary").attr("chpId");
        var secId = $(".content-summary").attr("secId");

        var chpObj = GetChapterObject(chId);
        var secObj = GetChapterSectionObject(chId, secId);

        var intverval = setInterval(
        function () {
            $('#ReportIssue').fadeOut(100);
            $("#reportIssueStatus").html("");
            $("#reportIssueStatus").attr("style", "display:none");
            $("#ReportModelBack").attr("style", "display:none");
            clearInterval(intverval);
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
    $("#txtEmail1").val("");

    $("#txtName").css("border-color", "#ccc");
    $("#txtMobile").css("border-color", "#ccc");
    $("#txtQuery").css("border-color", "#ccc");
    $("#txtEmail1").css("border-color", "#ccc");

    $("#buttons").show();
    $("#queryStatus").html("");

    if ($("#ql_ShortlistHeadline").attr("class") == "rfloat arrowDown") {
        $("#ql_Tab").attr("style", "display:none");
        $("#ql_ShortlistHeadline").attr("class", "rfloat arrowUp");
    }
    else {
        $("#ql_Tab").attr("style", "display:block");
        $("#ql_ShortlistHeadline").attr("class", "rfloat arrowDown");
        $("#txtName").focus();
    }

}

function SaveMayIHelpYouQuery() {
    $("#txtName").css("border-color", "#ccc");
    $("#txtMobile").css("border-color", "#ccc");
    $("#txtQuery").css("border-color", "#ccc");
    $("#txtEmail1").css("border-color", "#ccc");

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
    var email = $("#txtEmail1").val();
    //var query = $("#txtQuery").val() + " || Email Address:" + $("#txtEmail").val();

    if (!isNumber(mobile)) {
        $("#txtMobile").css("border-color", "red");
        $("#txtMobile").focus();
        return;
    }

    //if (!validateEmail($("#txtEmail").val())) {
    if (!validateEmail(email)) {
        $("#txtEmail1").css("border-color", "red");
        $("#txtEmail1").focus();
        return;
    }

    var query = $("#txtQuery").val() + " || Email Address:" + email;

    SaveStudentQuery(name, mobile, query);

    $("#txtName").val("");
    $("#txtMobile").val("");
    $("#txtQuery").val("");
    $("#txtEmail1").val("");
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
