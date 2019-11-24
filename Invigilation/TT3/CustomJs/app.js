
var app = angular.module("ExamTimeTable", ["ngRoute"]);

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider
        .when("/", {
            templateUrl: "View/Home.html"
        })
        .when("/Professor", {
            templateUrl: "View/Professor.html",
            controller: "ProfessorCtrl"
        })
        .when("/ClassRoom", {
            templateUrl: "View/ClassRoom.html",
            controller: "ClassRoomCtrl"
        })
        .when("/CreateExam", {
            templateUrl: "View/CreateExam.html",
            controller: "CreateExamCtrl"
        })
        .when("/GenerateTimeTable", {
            templateUrl: "View/GenerateTimeTable.html",
            controller: "GenerateTimeTableCtrl"
        })
        .when("/Preference", {
            templateUrl: "View/Preference.html",
            controller: "SetPreferenceCtrl"
        })
        .when("/Login", {
            templateUrl: "View/Login.html",
            controller: "LoginCtrl"
        })
        .when("/SavedTimeTable", {
            templateUrl: "View/SavedTimetable.html",
            controller: "SavedTimetableCtrl"
        })
        .when("/SeatingArrangement",{
            templateUrl: "View/SeatingArrangement.html",
            controller: "SeatingArrangementCtrl"
        })
        .when("/ConfigureRoom", {
            templateUrl: "View/ConfigureRoom.html",
            controller: "ConfigureRoomCtrl"
        })
        .when("/Department",{
            templateUrl: "View/Department.html",
            controller: "DepartmentCtrl"
        })
        .otherwise({
            templateUrl: "View/Home.html"
        });

    //$locationProvider.html5Mode({
    //    enabled: true,
    //    requireBase: true
    //});
}])

app.controller("HeaderAndSidebarCtrl", function ($scope) {
    $scope.Logout = function () {
        localStorage.setItem('LoggedUser', '');
        window.location.href = CommonPath + "#!/Login"
    }
})

app.controller("ProfessorCtrl", function ($scope, $http,APIService) {
    $scope.AllUsers = [];
    $scope.GetAllProf = function () {
        APIService.GetFiles('ProfessorMaster').then(function(respo){
            if (respo.data == null || respo.data == '') {
                $scope.AllUsers = [];
            } else {
                $scope.AllUsers = JSON.parse(respo.data);
            }
            $('#sampleTable').DataTable().destroy();
            setTimeout(function () {
                $('.overlay').css('display', 'none');
                $('#sampleTable').DataTable();
            }, 1000)
        });
        //$http.get(CommonPath + '/api/Professor/SelectAllProfessor?UserGroup='+UserGroup).then(function (respo) {
            
        //})
    }
    $scope.GetAllProf();
    $scope.AllDepartment=[];
    $scope.GetAllDepartment = function () {
        APIService.GetFiles('Department').then(function (respo) {
            if (respo.data == null || respo.data == '') {
                $scope.AllDepartment = [];
            } else {
                $scope.AllDepartment = JSON.parse(respo.data);
                console.log($scope.AllDepartment);
            }
            $('#sampleTable').DataTable().destroy();
            setTimeout(function () {
                $('.overlay').css('display', 'none');
                $('#sampleTable').DataTable();
            }, 1000)
        });
    }
    $scope.GetAllDepartment();
    $scope.delete = function (index) {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to remove this professor/teacher from your list?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false
        },
            function () {
                $scope.AllUsers.splice(index, 1);
                var response = $scope.PostData($scope.AllUsers);
                response.done(function (respo) {
                    if (respo) {
                        swal("Deleted!", "Deleted Successfully", "success");
                        $scope.GetAllProf();
                    } else {
                        swal("Error", "Something went wrong Please try again", "error");
                    }
                });

            });
    };
    $scope.NewProf = {}
    $scope.CHeckValue = function (value) {
        if (value > 1) {
            return true;
        }
        else {
            return false;
        }
    }
    $scope.create = function () {

    };
    $.validate({
        form: '#ProfessorCreateForm',
        onSuccess: function ($form) {
            $scope.NewProf.AllowedReliver = $('#dd_AllowedReliver').val();
            $scope.NewProf.AllowedStandBy = $('#dd_AllowedStandBy').val();
            if ($scope.CurrentIndexToEditProf == -1) {
                if ($scope.AllUsers != null && $scope.AllUsers != undefined && $scope.AllUsers.length > 0) {
                    $scope.AllUsers.push($scope.NewProf);
                } else {
                    $scope.AllUsers = [];
                    $scope.AllUsers.push($scope.NewProf);
                }

            } else {
                $scope.AllUsers[$scope.CurrentIndexToEditProf] = $scope.NewProf;
            }
            var response = $scope.PostData($scope.AllUsers);
            response.done(function (respo) {
                $('#ProfessorModal').modal('toggle');
                if (respo) {
                    if ($scope.CurrentIndexToEditProf == -1) {
                        swal("Created!", "Record added Successfully", "success");
                    } else {
                        swal("Created!", "Record updated Successfully", "success");
                    }
                    $scope.GetAllProf();
                } else {
                    swal("Error", "Something went wrong Please try again", "error");
                }
            })
        }
    });
    $scope.PostData = function (alldata) {
        alldata = JSON.parse(angular.toJson(alldata))
        var InputData = {}
        InputData.data = escape(JSON.stringify(alldata));
        InputData.UserGroup = UserGroup;
        var settings = {
            "async": true,
            "crossDomain": true,
            "url": CommonPath + "/api/Professor/SaveProfessorList",
            "method": "POST",
            "headers": {
                "Content-Type": "application/json",
                "Cache-Control": "no-cache",
            },
            "processData": false,
            contentType: 'application/json; charset=utf-8',
            "data": JSON.stringify(InputData)
        }

        return $.ajax(settings)
    }
    $scope.CurrentIndexToEditProf = -1;
    $scope.edit = function (index) {
        $('#ProfessorModal').modal('show');
        $scope.CurrentIndexToEditProf = index;
        if (index == -1) {
            $scope.NewProf = {};
        } else {
            $scope.NewProf = $scope.AllUsers[index];
            if ($scope.NewProf.AllowedReliver) {
                $('#dd_AllowedReliver').val('true')
            } else {
                $('#dd_AllowedReliver').val('false')
            }
            if ($scope.NewProf.AllowedStandBy) {
                $('#dd_AllowedStandBy').val('true')
            } else {
                $('#dd_AllowedStandBy').val('false')
            }
        }
    };
});

app.controller("ClassRoomCtrl", function ($scope, $http) {
    $scope.AllRooms = [];
    $scope.GetAllRooms = function () {
        $http.get(CommonPath + '/api/Professor/SelectAllRomList?UserGroup=' + UserGroup).then(function (respo) {

            if (respo.data == null || respo.data == '') {
                $scope.AllRooms = [];

            } else {
                $scope.AllRooms = JSON.parse(respo.data);
            }
            $('#sampleTable').DataTable().destroy();
            setTimeout(function () {
                $('.overlay').css('display', 'none');
                $('#sampleTable').DataTable();
            }, 1000)
        });
    }
    $scope.GetAllRooms();
    $scope.NewRoom = '';
    $scope.CreateOrUpdate = function (index) {
        $('#RoomAddModal').modal('show');
        $scope.CurrentIndexToEditRoom = index;
        if (index == -1) {
            $scope.NewRoom = '';
            $("#RoomType").val('room');
            $('#txt_OrderNo').val('');
        } else {
            $scope.NewRoom = $scope.AllRooms[index].RoomName;
            $('#RoomType').val($scope.AllRooms[index].IsTypeRelOrStandBy)
            $('#txt_OrderNo').val($scope.AllRooms[index].OrderNo);
        }
    }


    $.validate({
        form: '#RoomCreateUpdateForm',
        onSuccess: function ($form) {
            if ($scope.CurrentIndexToEditRoom == -1) {
                var DuplicateResult = $scope.AllRooms.some(function (el) {
                    return el.RoomName === $scope.NewRoom;
                });
                if (!DuplicateResult) {
                    $scope.SingleRoom = {};
                    $scope.SingleRoom.RoomName = $scope.NewRoom;
                    $scope.SingleRoom.IsTypeRelOrStandBy = $("#RoomType").val();
                    $scope.SingleRoom.OrderNo = $("#txt_OrderNo").val();
                    $scope.AllRooms.push($scope.SingleRoom);
                    $scope.NewRoom = '';
                    var response = $scope.PostData($scope.AllRooms);
                    response.done(function (respo) {
                        $('#RoomAddModal').modal('toggle');
                        if (respo) {
                            swal("Created!", "Record added Successfully", "success");
                        } else {
                            swal("Error", "Something went wrong Please try again", "error");
                        }
                        $scope.GetAllRooms();
                    })
                } else {
                    swal("Duplicate", $scope.NewRoom + " is already present in system", "error");
                }
            } else {
                var DuplicateCounter = 0;
                for (var allr = 0; allr < $scope.AllRooms.length; allr++) {
                    if (allr == $scope.CurrentIndexToEditRoom) {
                        continue;
                    } else {
                        if ($scope.AllRooms[allr].RoomName == $scope.NewRoom) {
                            DuplicateCounter++;
                        }
                    }
                }
                if (DuplicateCounter > 0) {
                    swal("Duplicate", $scope.NewRoom + " is already present in system", "error");
                } else {
                    $scope.SingleRoom = {};
                    $scope.SingleRoom.RoomName = $scope.NewRoom;
                    $scope.SingleRoom.IsTypeRelOrStandBy = $("#RoomType").val();
                    $scope.SingleRoom.OrderNo = $("#txt_OrderNo").val();
                    $scope.AllRooms[$scope.CurrentIndexToEditRoom] = $scope.SingleRoom;
                    $scope.NewRoom = '';
                    $("#chkIsTypeReliver").prop("checked", false);
                    var response = $scope.PostData($scope.AllRooms);
                    response.done(function (respo) {
                        $('#RoomAddModal').modal('toggle');
                        if (respo) {
                            swal("Created!", "Record added Successfully", "success");
                        } else {
                            swal("Error", "Something went wrong Please try again", "error");
                        }
                        $scope.GetAllRooms();
                    })
                }
            }
        }
    });
    $scope.RemoveClassFromArray = function (index) {
        swal({
            title: "Are you sure?",
            text: "Are you sure you want to remove this room from your list?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false
        },
            function () {
                $scope.AllRooms.splice(index, 1);
                var response = $scope.PostData($scope.AllRooms);
                response.done(function (respo) {
                    $scope.GetAllRooms();
                    if (respo) {
                        swal("Deleted!", "Deleted Successfully", "success");
                    } else {
                        swal("Error", "Something went wrong Please try again", "error");
                    }
                });
            });
    }
    $scope.PostData = function (data) {
        alldata = JSON.parse(angular.toJson(data))
        var InputData = {}
        InputData.data = escape(JSON.stringify(alldata));
        InputData.UserGroup = UserGroup;
        var settings = {
            "async": true,
            "crossDomain": true,
            "url": CommonPath + "/api/Professor/SaveRoomList",
            "method": "POST",
            "headers": {
                "Content-Type": "application/json",
                "Cache-Control": "no-cache",
            },
            "processData": false,
            contentType: 'application/json; charset=utf-8',
            "data": JSON.stringify(InputData)
        }

        return $.ajax(settings)
    }
});

app.controller("CreateExamCtrl", function ($scope, $http) {
    $scope.AllExamArray = [];
    $scope.AllExamArray.Dates = [];
    $scope.AllExamArray.Dates.Time = [];
    $scope.AllExamArray.Dates.Time.Rooms = [];
    $('#demoDate').datepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
        todayHighlight: true
    });
    function isDateInArray(needle, haystack) {
        for (var i = 0; i < haystack.length; i++) {
            var dat = new Date(haystack[i].toString())
            var dat2 = new Date(needle.toString())
            if (dat2.getTime() === dat.getTime()) {
                return true;
            }
        }
        return false;
    }
    $scope.AllRooms = [];
    $scope.GetAllRooms = function () {
        $http.get(CommonPath + '/api/Professor/SelectAllRomList?UserGroup=' + UserGroup).then(function (respo) {
            $scope.AllRooms = JSON.parse(respo.data);
            $('#AllRoomMultiSelect').empty();
            $("#AllRoomMultiSelect").append('<optgroup id="OptionGroup1" label="Available Room"></optgroup>');
            $.each($scope.AllRooms, function (i, obj) {
                $('#OptionGroup1').append($('<option>').text(obj.RoomName).attr('value', obj.RoomName));
            });
            $('#AllRoomMultiSelect').select2({
                closeOnSelect: false,
                placeholder: "Select Room"
            });
        });
    }

    $scope.AllExamArray = [];
    $scope.GetAllExamList = function () {
        $scope.AllExamArray = [];
        //$scope.$apply()
        $http.get(CommonPath + 'api/Professor/SelectAllExamList?UserGroup=' + UserGroup).then(function (respo1) {
            var respo = [];
            try {
                respo = $.parseJSON(respo1.data)
            } catch (e) {
                respo = [];
            }
            $scope.AllExamArray = respo;
            //var str = '';
            //for (var i = 0; i < respo.length; i++) {
            //    str += '<tr><td>' + (i + 1) + '</td><td>' + respo.ExamName + '</td><td></td><a class="btn btn-primary" ng-click="CreateOrUpdateExamName($index)" style="color:white"><i class="fa fa-lg fa-edit" style="margin-top: -3px;"></i> Edit</a>'+
            //        '<a class="btn btn-primary" ng - click="ExamScheduleModal(' + i+')" style = "color:white" > <i class="fa fa-lg fa-clock-o" style="margin-top: -3px;"></i> Schedule</a>'+
            //            '<a class="btn btn-primary" ng-click="deleteExam($index)" style="color:white"><i class="fa fa-lg fa-trash" style="margin-top: -3px;"></i> Delete</a></tr>'
            //}
            $('#sampleTable').DataTable().destroy();
            setTimeout(function () {
                $('.overlay').css('display', 'none');
                $('#sampleTable').DataTable();
            }, 1000)
        });
    }
    $scope.GetAllRooms();
    $scope.GetAllExamList();
    $scope.NewExam = {};
    $scope.NewExam.Dates = [];
    $scope.NewExamName = '';
    $scope.CreateOrUpdateExamName = function (index) {
        $scope.CurrentIndexToEditProf = index;
        $('#ExamNameModal').modal('toggle');
        if (index == -1) {
            $scope.NewExamName = '';
        } else {
            $scope.NewExamName = $scope.AllExamArray[index].ExamName;
        }
    }
    $.validate({
        form: '#ExamNameCreateForm',
        onSuccess: function ($form) {
            $('#ExamNameModal').modal('toggle');
            if ($scope.CurrentIndexToEditProf == -1) {
                var NewExamObj = {};
                NewExamObj.ExamName = $scope.NewExamName;
                $scope.AllExamArray.push(NewExamObj)
            } else {
                $scope.AllExamArray[$scope.CurrentIndexToEditProf].ExamName = $scope.NewExamName;
            }
            var response = $scope.PostData($scope.AllExamArray);
            response.done(function (respo) {
                if (respo) {
                    swal("Saved!", "Saved Successfully", "success");
                    //$scope.GetAllExamList();
                } else {
                    swal("Error", "Something went wrong Please try again", "error");
                }
            });
        }
    });
    $scope.deleteExam = function (index) {
        swal({
            title: "Are you sure?",
            text: "Your will not be able to recover this imaginary file!",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false
        },
            function () {
                $scope.AllExamArray.splice(index, 1);
                var response = $scope.PostData($scope.AllExamArray);
                response.done(function (respo) {
                    if (respo) {
                        swal("Deleted!", "Deleted Successfully", "success");
                        //$scope.GetAllExamList();
                    } else {
                        swal("Error", "Something went wrong Please try again", "error");
                    }
                });
            });
    };
    $scope.PostData = function (alldata) {
        alldata = JSON.parse(angular.toJson(alldata))
        var InputData = {}
        InputData.data = escape(JSON.stringify(alldata));
        InputData.UserGroup = UserGroup;
        var settings = {
            "async": true,
            "crossDomain": true,
            "url": CommonPath + "/api/Professor/SaveExamList",
            "method": "POST",
            "headers": {
                "Content-Type": "application/json",
                "Cache-Control": "no-cache",
            },
            "processData": false,
            contentType: 'application/json; charset=utf-8',
            "data": JSON.stringify(InputData)
        }
        return $.ajax(settings)
    }
    $scope.SerializeObjectToFormate = function (examArray) {
        var uniqueDates = [];
        var CreateDateArray = [];//All data of dates will be assigned
        for (var i = 0; i < examArray.length; i++) {
            if (!isDateInArray(examArray[i].OriginalDate, uniqueDates)) {
                uniqueDates.push(examArray[i].OriginalDate);
            }
        }
        for (var i = 0; i < uniqueDates.length; i++) {
            var ItemToCreate = {};
            ItemToCreate.SingleDate = uniqueDates[i];
            var CurrentDateData = examArray.filter(function (e) {
                return e.OriginalDate.getFullYear() == uniqueDates[i].getFullYear() && e.OriginalDate.getMonth() == uniqueDates[i].getMonth() && e.OriginalDate.getDate() == uniqueDates[i].getDate();
            });

            ItemToCreate.Time = [];
            for (var j = 0; j < CurrentDateData.length; j++) {
                var TimeObj = {};
                TimeObj.Room = [];
                TimeObj.FromTime = CurrentDateData[j].FromTime;
                TimeObj.ToTime = CurrentDateData[j].ToTime;
                TimeObj.Room = CurrentDateData[j].Room;
                ItemToCreate.Time.push(TimeObj)
            }
            CreateDateArray.push(ItemToCreate);
        }
        return CreateDateArray;
    }
    $scope.DeserializeObjectFromArray = function (ArrayObject) {
        var DatesArray = [];
        $scope.MainArrayToCreate = [];
        DatesArray = ArrayObject;
        if (DatesArray == undefined) {
            return [];
        }
        for (var i = 0; i < DatesArray.length; i++) {
            for (var j = 0; j < DatesArray[i].Time.length; j++) {
                $scope.DateDetail = {};
                $scope.DateDetail.OriginalDate = new Date(DatesArray[i].SingleDate.toString());
                $scope.DateDetail.FromTime = DatesArray[i].Time[j].FromTime;
                $scope.DateDetail.ToTime = DatesArray[i].Time[j].ToTime;
                $scope.DateDetail.Room = [];
                $scope.DateDetail.Room = DatesArray[i].Time[j].Room;
                $scope.MainArrayToCreate.push($scope.DateDetail)
            }
        }
        const criteria = ['OriginalDate', 'FromTime']
        $scope.MainArrayToCreate = multisort($scope.MainArrayToCreate, criteria)
        return $scope.MainArrayToCreate;
    }
    $scope.CurrentExamToEdit = [];
    $scope.ExamScheduleModal = function (index) {
        $scope.CurrentExamEditModalIndex = index;
        var CurrentExam = $scope.AllExamArray[index].Dates;
        $scope.CurrentExamNameForSchedule = $scope.AllExamArray[index].ExamName;
        $scope.CurrentExamToEdit = $scope.DeserializeObjectFromArray(CurrentExam);
        $('#ExamScheduleModal').modal('toggle');
    }
    $scope.CreateDate = function (dateString) {
        var dateParts = dateString.split("/");
        var dateObject = new Date(dateParts[2], dateParts[1] - 1, dateParts[0]); // month is 0-based
        return dateObject
    }
    $scope.AddDateToExam = function () {
        if ($('#demoDate').val() == '' || $('#FromTime').val() == '' || $('#ToTime').val() == '' || $('#AllRoomMultiSelect').val.length == 0) {

        } else {
            var ExamDateObject = {};
            ExamDateObject.OriginalDate = $scope.CreateDate($('#demoDate').val());
            ExamDateObject.FromTime = $('#FromTime').val();
            ExamDateObject.ToTime = $('#ToTime').val();
            ExamDateObject.Room = [];
            ExamDateObject.Room = $('#AllRoomMultiSelect').val();
            $scope.CurrentExamToEdit.push(ExamDateObject);
        }

    }
    $scope.DeleteCurrentExamObj = function (index) {
        $scope.CurrentExamToEdit.splice(index, 1);
    }
    $scope.SaveCurrentExamToEditObj = function () {
        var SaveObjSingle = {};
        SaveObjSingle.Dates = $scope.SerializeObjectToFormate($scope.CurrentExamToEdit);
        SaveObjSingle.ExamName = $scope.CurrentExamNameForSchedule;
        $scope.AllExamArray[$scope.CurrentExamEditModalIndex] = SaveObjSingle;
        var response = $scope.PostData($scope.AllExamArray);
        response.done(function (respo) {
            $('#ExamScheduleModal').modal('toggle');
            if (respo) {
                swal("Created!", "Exam Saved Successfully", "success");
            } else {
                swal("Error", "Something went wrong Please try again", "error");
            }

        })
    }
});

app.controller("GenerateTimeTableCtrl", function ($scope, $http,APIService) {
    //prop Declared Required
    $scope.ProfConfig = [];
    $scope.AllRooms = []; //$scope.GetAllRooms();
    $scope.AllProfessor = []; //$scope.GetAllProfessor()
    $scope.AllExamArray = [];
    $scope.AllTimeTableData = [];
    $scope.AllMappingData = [];
    var PreferenceArray = [];
    var SelectedExamIndex = 0;

    $scope.ExamConfig={};
    $scope.ExamConfig.StandByDepartment='';
    $scope.ExamConfig.ReliverDepartment='';
    $scope.AllDepartment=[];
    $scope.GetAllDepartment = function () {
        APIService.GetFiles('Department').then(function (respo) {
            if (respo.data == null || respo.data == '') {
                $scope.AllDepartment = [];
            } else {
                $scope.AllDepartment = JSON.parse(respo.data);
                console.log($scope.AllDepartment);
            }
        });
    }
    $scope.GetAllDepartment();

    //Rendomly suffled Array
    function shuffle(array) {
        var currentIndex = array.length, temporaryValue, randomIndex;
        while (0 !== currentIndex) {
            randomIndex = Math.floor(Math.random() * currentIndex);
            currentIndex -= 1;
            temporaryValue = array[currentIndex];
            array[currentIndex] = array[randomIndex];
            array[randomIndex] = temporaryValue;
        }
        return array;
    }
    //Convert string date to Date data type
    function CreateDateType(strdate) {
        //var NewDate = strdate.split('/');
        //return new Date(NewDate[2], (NewDate[1] - 1), NewDate[0]);
        return new Date(strdate);
    }


    //Get All Room API
    $scope.GetAllRooms = function () {
        $http.get(CommonPath + '/api/Professor/SelectAllRomList?UserGroup=' + UserGroup).then(function (respo) {
            $scope.AllRooms = JSON.parse(respo.data);
        });
    }
    //Get All Professor API
    $scope.GetAllProfessor = function () {
        $http.get(CommonPath + '/api/Professor/SelectAllProfessor?UserGroup=' + UserGroup).then(function (respo) {
            $scope.AllProfessor = respo.data;
        })
    }
    //Get All Exam API
    $scope.GetAllExamList = function () {
        $http.get(CommonPath + '/api/Professor/SelectAllExamList?UserGroup=' + UserGroup).then(function (respo) {
            $scope.AllExamArray = $.parseJSON(respo.data);
            $('#dd_ExamName').empty();
            $('#dd_ExamName').append($('<option>').text("Select Exam").attr('value', ''));
            $.each($scope.AllExamArray, function (i, obj) {
                $('#dd_ExamName').append($('<option>').text(obj.ExamName).attr('value', i));
            });
        });
    }
    //Get All Preference
    $scope.GetAllPref = function () {
        $http.get(CommonPath + '/api/Professor/SelectAllItem?FileName=Preference.json&UserGroup='+UserGroup).then(function (respo) {
            try {
                PreferenceArray = $.parseJSON(respo.data)
            } catch (e) {
                PreferenceArray = [];
            }
        })
    }

    //Get All TimeTable List
    $scope.GetAllTimeTable = function () {
        $http.get(CommonPath + '/api/Professor/SelectAllItem?FileName=TimeTable.json&UserGroup=' + UserGroup).then(function (respo) {
            try {
                $scope.AllTimeTableData = $.parseJSON(respo.data)
            } catch (e) {
                $scope.AllTimeTableData = [];
            }
        })
    }

    //Get Mapping data
    $scope.GetAllMappingData = function () {
        $http.get(CommonPath + '/api/Professor/SelectAllItem?FileName=RoomTeacherMapping.json&UserGroup='+UserGroup).then(function (respo) {
            try {
                $scope.AllMappingData = $.parseJSON(respo.data)
            } catch (e) {
                $scope.AllMappingData = [];
            }
        })
    }

    $scope.GetAllPref();

    //Call All Common Function in Home
    $scope.GetAllRooms();
    $scope.GetAllProfessor();
    $scope.GetAllExamList();
    $scope.GetAllPref();
    $scope.GetAllTimeTable();
    $scope.GetAllMappingData();

    $scope.CreateExamvalidDatesArray = function (indexofex) {
        $scope.CreateExamvalidDates = [];
        for (var da = 0; da < $scope.AllExamArray[indexofex].Dates.length; da++) {
            $scope.CreateExamvalidDates.push(CreateDateType($scope.AllExamArray[indexofex].Dates[da].SingleDate));
        }
        $scope.CreateExamvalidDates.sort(function (a, b) {
            var dateA = new Date(a), dateB = new Date(b);
            return dateA - dateB;
        });
    }

    $scope.CreateLogicalArray = function (indexofExamArray) {
        LogicalArrayProfUsage2 = [];
        var LogicalArrayProfUsage = [];
        var SuffledArray = shuffle($scope.AllProfessor);
        $scope.CreateExamvalidDatesArray(indexofExamArray);
        for (suf = 0; suf < SuffledArray.length; suf++) {
            var DateArray = [];
            //var Actual = $scope.CreateExamvalidDates[0];
            //var ToDate = $scope.CreateExamvalidDates[$scope.CreateExamvalidDates.length - 1];
            for (var ad = 0; ad < $scope.CreateExamvalidDates.length; ad++) {
                d = $scope.CreateExamvalidDates[ad];
                var result = $.grep(PreferenceArray, function (e) {
                    var edate = new Date(e.DateOfLeave.toString())
                    return e.ProfessorName == SuffledArray[suf].name && edate.getFullYear() == d.getFullYear() && edate.getMonth() == d.getMonth() && edate.getDate() == d.getDate();
                });
                if (result.length > 0) {
                    continue;
                } else {
                    var TimeArray = [];
                    var result = $.grep($scope.AllExamArray[SelectedExamIndex].Dates, function (e) {
                        //var newdatestring = d.getDate() + '/' + (d.getMonth() + 1) + '/' + d.getFullYear();
                        var currentdat = new Date(e.SingleDate)
                        if (currentdat - d == 0) {
                            return true;
                        } else {
                            return false
                        }
                        //return currentdat=== d;
                    });
                    for (var time = 0; time < result[0].Time.length; time++) {
                        var LogicObj = {}
                        LogicObj.Identity = SuffledArray[suf].name;
                        LogicObj.Department = SuffledArray[suf].Department;
                        LogicObj.date = new Date(d);
                        LogicObj.time = result[0].Time[time].T;
                        LogicObj.FromTime = result[0].Time[time].FromTime;
                        LogicObj.ToTime = result[0].Time[time].ToTime;
                        LogicObj.DayUsageCount = 0;
                        LogicObj.ExamUsageCount = 0;
                        LogicObj.ReliverCount = 0;
                        LogicObj.StandByCount = 0;
                        LogicObj.IsUsed = false;
                        LogicObj.MaxSuperVision = SuffledArray[suf].MaxSuperVision;
                        LogicObj.AllowedReliver = SuffledArray[suf].AllowedReliver;
                        LogicObj.AllowedStandBy = SuffledArray[suf].AllowedStandBy;
                        LogicalArrayProfUsage2.push(LogicObj);
                    }
                }
            }
        }
        if(1==1){
            $scope.AssignProfessorToTimeTableWithRepeatSlotTeacher();
        }else{
            $scope.AssignProfessorToTimeTable();
        }
        
    }

    function removeDuplicates(originalArray, prop) {
        var newArray = [];
        var lookupObject = {};

        for (var i in originalArray) {
            lookupObject[originalArray[i][prop]] = originalArray[i];
        }

        for (i in lookupObject) {
            newArray.push(lookupObject[i]);
        }
        return newArray;
    }

    $scope.AssignProfessorToTimeTable = function () {
        //To check break for failure
        var IsRequiredBreak=false
        var FinalTimeTable = [];
        DateLoop: for (var ad = 0; ad < $scope.CreateExamvalidDates.length; ad++) {
            var result = $.grep($scope.AllExamArray[SelectedExamIndex].Dates, function (e) {
                //var newdatestring = $scope.CreateExamvalidDates[ad].getDate() + '/' + ($scope.CreateExamvalidDates[ad].getMonth() + 1) + '/' + $scope.CreateExamvalidDates[ad].getFullYear();
                //return e.SingleDate == newdatestring;
                var newdat = new Date(e.SingleDate)
                if (newdat.getFullYear() == $scope.CreateExamvalidDates[ad].getFullYear() && newdat.getMonth() == $scope.CreateExamvalidDates[ad].getMonth() && newdat.getDate() == $scope.CreateExamvalidDates[ad].getDate()) {
                    return true;
                }
                else {
                    return false
                }
            });
            var ReliverUsedOn1stSlot=[];
            TimeAssignLoop: for (var times = 0; times < result[0].Time.length; times++) {
                //Find Employee who has availablity on this time & Date
                var EmployeeFoundToAssign = $.grep(LogicalArrayProfUsage2, function (e) {
                    return e.ToTime == result[0].Time[times].ToTime && e.FromTime == result[0].Time[times].FromTime && e.date.getFullYear() == $scope.CreateExamvalidDates[ad].getFullYear() && e.date.getMonth() == $scope.CreateExamvalidDates[ad].getMonth() && e.date.getDate() == $scope.CreateExamvalidDates[ad].getDate() && e.IsUsed == false && e.MaxSuperVision > (e.ExamUsageCount); //+e.ReliverCount + e.StandByCount
                });

                //check is teacher alerady assigned to same date with different time? if yes than remove
                var _EmployeeFoundToAssign = EmployeeFoundToAssign;
                EmployeeFoundToAssign = [];
                for (var empf = 0; empf < _EmployeeFoundToAssign.length; empf++) {
                    var CheckInFinalTime = $.grep(FinalTimeTable, function (e) {
                        return e.dateAssigned === $scope.CreateExamvalidDates[ad] && e.ProfName == _EmployeeFoundToAssign[empf].Identity
                    });
                    if (CheckInFinalTime.length == 0) {
                        EmployeeFoundToAssign.push(_EmployeeFoundToAssign[empf])
                    }
                }
                if (EmployeeFoundToAssign.length == 0) {
                    var debug = 0;
                }

                var RoomArray = [];
                RoomArray = result[0].Time[times].Room;
                var IsFirstMappingloop=true;
                RoomAssignLoop: for (var romml = 0; romml < RoomArray.length; romml++) {
                    var FinalTimeTableobj = {};
                    var IsReliver = false;
                    var IsStandBy = false;
                    
                    //Check start whether Room Type is standby Or Reliver
                    var RoomFoundToCheckType = $.grep($scope.AllRooms, function (e) {
                        return e.RoomName == RoomArray[romml];
                    });
                    var checkIsTypeOfRoom = 'room';
                    try {
                        if (RoomFoundToCheckType.length >= 0) {
                            if (RoomFoundToCheckType[0].IsTypeRelOrStandBy == 'reliver') {
                                checkIsTypeOfRoom = 'reliver';
                            } else if (RoomFoundToCheckType[0].IsTypeRelOrStandBy == 'stand by') {
                                checkIsTypeOfRoom = 'stand by';
                            } else {
                                checkIsTypeOfRoom = 'room';
                            }
                        } 
                    } catch (e) {
                        checkIsTypeOfRoom = 'room';
                    }


                    if($('#chkMapping').prop("checked") && !IsFirstMappingloop && checkIsTypeOfRoom=='room'){
                        EmployeeFoundToAssign=angular.copy(Old_EmployeeFoundToAssign);
                    }
                    IsFirstMappingloop=false;

                    //Here is condition to make mapping
                    if($('#chkMapping').prop("checked") && checkIsTypeOfRoom=='room'){
                        //EmployeeFoundToAssign
                        var Old_EmployeeFoundToAssign=angular.copy(EmployeeFoundToAssign);
                        var FreshToAssign=[];
                        var mappingIndex = $scope.AllMappingData.findIndex(x => x.RoomName == RoomArray[romml]);
                        if(mappingIndex!=-1){
                            for(var MappingLoopIndex=0;MappingLoopIndex<$scope.AllMappingData[mappingIndex].AssignedRoom.length;MappingLoopIndex++){
                                var FoundAfterMapping=EmployeeFoundToAssign.filter(x=>x.Identity==$scope.AllMappingData[mappingIndex].AssignedRoom[MappingLoopIndex]);
                                $.merge(FreshToAssign,FoundAfterMapping);
                            }
                            EmployeeFoundToAssign=FreshToAssign;
                            //if configuration teacher not found than assign all teacher which are not configured
                            if(EmployeeFoundToAssign.length==0){
                                var NewToAssign=[];
                                NewToAssign=angular.copy(Old_EmployeeFoundToAssign);
                                for(var mappingI=0;mappingI<$scope.AllMappingData.length;mappingI++){
                                    for(var TeacherI=0;TeacherI<$scope.AllMappingData[mappingI].AssignedRoom.length;TeacherI++){
                                        var RemoveIndexFromMain=NewToAssign.findIndex(x=>x.Identity==$scope.AllMappingData[mappingI].AssignedRoom[TeacherI]);
                                        if(RemoveIndexFromMain!=-1){
                                            NewToAssign.splice(RemoveIndexFromMain, 1);
                                        }
                                    }
                                }
                                //NewToAssign Only Teacher which are not configured with Room
                                EmployeeFoundToAssign=NewToAssign;
                            }
                        }
                    }

                    
                    //code over Check whether Room Type is standby Or Reliver
                    var IndexOfProfToAssign = 0;
                    if (checkIsTypeOfRoom == 'reliver') {
                        IsReliver = true;
                        const criteria = ['ReliverCount', 'StandByCount', 'DayUsageCount', 'ExamUsageCount'];
                        EmployeeFoundToAssign = multisort(EmployeeFoundToAssign, criteria);

                        var _ConfigSettingArray=[];
                        if($scope.ExamConfig.ReliverDepartment!=''){
                            _ConfigSettingArray=EmployeeFoundToAssign.filter(x=>x.Department==$scope.ExamConfig.ReliverDepartment);
                        }
                        LoopToCkeckItemIndexOfEmpRelAllowed: for (var IndRel = 0; IndRel < EmployeeFoundToAssign.length; IndRel++) {
                            if (EmployeeFoundToAssign[IndRel].AllowedReliver) {
                                if($scope.ExamConfig.ReliverDepartment!='' && _ConfigSettingArray.length>0){
                                    if(EmployeeFoundToAssign[IndRel].Department==$scope.ExamConfig.ReliverDepartment){
                                        IndexOfProfToAssign = IndRel;
                                        ReliverUsedOn1stSlot.push(EmployeeFoundToAssign[IndRel].Identity);
                                        break LoopToCkeckItemIndexOfEmpRelAllowed;
                                    }else{
                                        continue;
                                    }
                                }else{
                                    IndexOfProfToAssign = IndRel;
                                    ReliverUsedOn1stSlot.push(EmployeeFoundToAssign[IndRel].Identity);
                                    break LoopToCkeckItemIndexOfEmpRelAllowed;    
                                }
                            } else {
                                if (IndRel == EmployeeFoundToAssign.length) {
                                    swal("Required Reliver", "Please add more professor on Reliver to procced", "error");
                                }
                                continue;
                            }
                        }
                    }
                    else if (checkIsTypeOfRoom == 'stand by') {
                        const criteria = ['StandByCount', 'ReliverCount', 'DayUsageCount', 'ExamUsageCount']
                        EmployeeFoundToAssign = multisort(EmployeeFoundToAssign, criteria)
                        IsStandBy = true;
                        LoopToCkeckItemIndexOfEmpRelAllowed: for (var IndRel = 0; IndRel < EmployeeFoundToAssign.length; IndRel++) {
                            if (EmployeeFoundToAssign[IndRel].AllowedStandBy) {
                                IndexOfProfToAssign = IndRel;
                                break LoopToCkeckItemIndexOfEmpRelAllowed;
                            } else {
                                if (IndRel == EmployeeFoundToAssign.length) {
                                    swal("Required Stand By", "Please add more professor on Stand By to procced", "error");
                                }
                                continue;
                            }
                        }
                    }
                    else {
                        //Allowed Stand By And Allowed Reliver will gives priority to set employee who has value false 
                        const criteria = ['ExamUsageCount', 'AllowedReliver', 'AllowedStandBy']//'ReliverCount', 'StandByCount', 'DayUsageCount', 
                        EmployeeFoundToAssign = multisort(EmployeeFoundToAssign, criteria);
                        if(ReliverUsedOn1stSlot.length>0){
                            for(var ReliverUseInd=0;ReliverUseInd<ReliverUsedOn1stSlot.length;ReliverUseInd++){
                                var IndexToUse=EmployeeFoundToAssign.findIndex(x=>x.Identity==ReliverUsedOn1stSlot[ReliverUseInd]);
                                if(IndexToUse!=-1){
                                    IndexOfProfToAssign=IndexToUse;
                                    ReliverUsedOn1stSlot.splice(IndexToUse,1);
                                }
                            }
                        }
                    }
                    if (EmployeeFoundToAssign.length > 0) {
                        FinalTimeTableobj.Room = RoomArray[romml];
                        FinalTimeTableobj.ProfName = EmployeeFoundToAssign[IndexOfProfToAssign].Identity;
                        FinalTimeTableobj.IsOnError = false;
                        FinalTimeTableobj.dateAssigned = $scope.CreateExamvalidDates[ad];
                        FinalTimeTableobj.Time = CreateTimeFormate(result[0].Time[times].FromTime) + ' to ' + CreateTimeFormate(result[0].Time[times].ToTime);
                        //FinalTimeTableobj.ToTime = result[0].Time[times].ToTime;
                        FinalTimeTable.push(FinalTimeTableobj);
                        if($('#chkMapping').prop("checked")){
                            var IndextoRemoveOnlyInSpec=Old_EmployeeFoundToAssign.findIndex(x=>x.Identity==EmployeeFoundToAssign[IndexOfProfToAssign].Identity);
                            Old_EmployeeFoundToAssign.splice(IndextoRemoveOnlyInSpec,1)
                        }
                        EmployeeFoundToAssign.splice(IndexOfProfToAssign, 1);
                        updateMainArray(FinalTimeTableobj.ProfName, FinalTimeTableobj.dateAssigned, FinalTimeTableobj.Time, IsReliver, IsStandBy)
                    } else {
                        if (FailureCount > 5) {
                            FinalTimeTableobj.Room = RoomArray[romml];
                            FinalTimeTableobj.ProfName = '';
                            FinalTimeTableobj.IsOnError = true;
                            FinalTimeTableobj.dateAssigned = $scope.CreateExamvalidDates[ad];
                            FinalTimeTableobj.Time = CreateTimeFormate(result[0].Time[times].FromTime) + ' to ' + CreateTimeFormate(result[0].Time[times].ToTime);
                            //FinalTimeTableobj.ToTime = result[0].Time[times].ToTime;
                            FinalTimeTable.push(FinalTimeTableobj);
                            swal("limit exhausted", "Please add more professor or adjust Max Supervision", "error");
                        } else {
                            FailureCount++;
                            IsRequiredBreak = true;
                            break;
                        }
                    }
                }
                if (IsRequiredBreak) {
                    break;
                }
            }
            if (IsRequiredBreak) {
                break;
            }
        }
        if (IsRequiredBreak) {
            $scope.GenerateTimeTable();
        } else {
            $scope.FFinalTimeTableObj = FinalTimeTable;
            $scope.CreateArrayToMakeTableEasy(FinalTimeTable)
        }
    }
    
    $scope.AssignProfessorToTimeTableWithRepeatSlotTeacher = function () {
        //To check break for failure
        var IsRequiredBreak=false
        var FinalTimeTable = [];
        DateLoop: for (var ad = 0; ad < $scope.CreateExamvalidDates.length; ad++) {
            var result = $.grep($scope.AllExamArray[SelectedExamIndex].Dates, function (e) {
                var newdat = new Date(e.SingleDate)
                if (newdat.getFullYear() == $scope.CreateExamvalidDates[ad].getFullYear() && newdat.getMonth() == $scope.CreateExamvalidDates[ad].getMonth() && newdat.getDate() == $scope.CreateExamvalidDates[ad].getDate()) {
                    return true;
                }
                else {
                    return false
                }
            });
            var ReliverUsedOn1stSlot=[];
            var PreviosSlotTeacherUsedInRoomToTakeupInReliver=[];
            TimeAssignLoop: for (var times = 0; times < result[0].Time.length; times++) {
                //Find Employee who has availablity on this time & Date
                var EmployeeFoundToAssign = $.grep(LogicalArrayProfUsage2, function (e) {
                    return e.ToTime == result[0].Time[times].ToTime && e.FromTime == result[0].Time[times].FromTime && e.date.getFullYear() == $scope.CreateExamvalidDates[ad].getFullYear() && e.date.getMonth() == $scope.CreateExamvalidDates[ad].getMonth() && e.date.getDate() == $scope.CreateExamvalidDates[ad].getDate() && e.IsUsed == false && e.MaxSuperVision > (e.ExamUsageCount); //+e.ReliverCount + e.StandByCount
                });

                if (EmployeeFoundToAssign.length == 0) {
                    var debug = 0;
                }

                var RoomArray = [];
                RoomArray = result[0].Time[times].Room;
                var IsFirstMappingloop=true;
                var _ConfigurationMaxUsageDepartmentReliver=0;
                var _ConfigurationMaxUsageDepartmentStandBy=0;
                RoomAssignLoop: for (var romml = 0; romml < RoomArray.length; romml++) {
                    var FinalTimeTableobj = {};
                    var IsReliver = false;
                    var IsStandBy = false;
                    
                    //Check start whether Room Type is standby Or Reliver
                    var RoomFoundToCheckType = $.grep($scope.AllRooms, function (e) {
                        return e.RoomName == RoomArray[romml];
                    });
                    var checkIsTypeOfRoom = 'room';
                    try {
                        if (RoomFoundToCheckType.length >= 0) {
                            if (RoomFoundToCheckType[0].IsTypeRelOrStandBy == 'reliver') {
                                checkIsTypeOfRoom = 'reliver';
                            } else if (RoomFoundToCheckType[0].IsTypeRelOrStandBy == 'stand by') {
                                checkIsTypeOfRoom = 'stand by';
                            } else {
                                checkIsTypeOfRoom = 'room';
                            }
                        } 
                    } catch (e) {
                        checkIsTypeOfRoom = 'room';
                    }


                    if($('#chkMapping').prop("checked") && !IsFirstMappingloop){
                        EmployeeFoundToAssign=angular.copy(Old_EmployeeFoundToAssign);
                    }
                    IsFirstMappingloop=false;

                    //Here is condition to make mapping
                    if($('#chkMapping').prop("checked")){
                        //EmployeeFoundToAssign
                        var Old_EmployeeFoundToAssign=angular.copy(EmployeeFoundToAssign);
                        var FreshToAssign=[];
                        var mappingIndex = $scope.AllMappingData.findIndex(x => x.RoomName == RoomArray[romml]);
                        if(mappingIndex!=-1){
                            for(var MappingLoopIndex=0;MappingLoopIndex<$scope.AllMappingData[mappingIndex].AssignedRoom.length;MappingLoopIndex++){
                                var FoundAfterMapping=EmployeeFoundToAssign.filter(x=>x.Identity==$scope.AllMappingData[mappingIndex].AssignedRoom[MappingLoopIndex]);
                                $.merge(FreshToAssign,FoundAfterMapping);
                            }
                            EmployeeFoundToAssign=FreshToAssign;
                            //if configuration teacher not found than assign all teacher which are not configured
                            if(EmployeeFoundToAssign.length==0){
                                var NewToAssign=[];
                                NewToAssign=angular.copy(Old_EmployeeFoundToAssign);
                                for(var mappingI=0;mappingI<$scope.AllMappingData.length;mappingI++){
                                    for(var TeacherI=0;TeacherI<$scope.AllMappingData[mappingI].AssignedRoom.length;TeacherI++){
                                        var RemoveIndexFromMain=NewToAssign.findIndex(x=>x.Identity==$scope.AllMappingData[mappingI].AssignedRoom[TeacherI]);
                                        if(RemoveIndexFromMain!=-1){
                                            NewToAssign.splice(RemoveIndexFromMain, 1);
                                        }
                                    }
                                }
                                //NewToAssign Only Teacher which are not configured with Room
                                EmployeeFoundToAssign=NewToAssign;
                            }
                        }
                    }

                    
                    //code over Check whether Room Type is standby Or Reliver
                    var IndexOfProfToAssign = 0;
                    if (checkIsTypeOfRoom == 'reliver') {
                        IsReliver = true;
                        const criteria = ['ReliverCount', 'StandByCount', 'DayUsageCount', 'ExamUsageCount'];
                        EmployeeFoundToAssign = multisort(EmployeeFoundToAssign, criteria);

                        var _ConfigSettingArray=[];
                        if($scope.ExamConfig.ReliverDepartment!='' && $scope.ExamConfig.Max_ReliverUser>=_ConfigurationMaxUsageDepartmentReliver){
                            _ConfigSettingArray=EmployeeFoundToAssign.filter(x=>x.Department==$scope.ExamConfig.ReliverDepartment);
                        }
                        LoopToCkeckItemIndexOfEmpRelAllowed: for (var IndRel = 0; IndRel < EmployeeFoundToAssign.length; IndRel++) {
                            if (EmployeeFoundToAssign[IndRel].AllowedReliver) {
                                if($scope.ExamConfig.ReliverDepartment!='' && _ConfigSettingArray.length>0){
                                    if(EmployeeFoundToAssign[IndRel].Department==$scope.ExamConfig.ReliverDepartment){
                                        IndexOfProfToAssign = IndRel;
                                        ReliverUsedOn1stSlot.push(EmployeeFoundToAssign[IndRel].Identity);
                                        _ConfigurationMaxUsageDepartmentReliver++;
                                        break LoopToCkeckItemIndexOfEmpRelAllowed;
                                    }else{
                                        continue;
                                    }
                                }else{
                                    IndexOfProfToAssign = IndRel;
                                    ReliverUsedOn1stSlot.push(EmployeeFoundToAssign[IndRel].Identity);
                                    break LoopToCkeckItemIndexOfEmpRelAllowed;    
                                }
                            } else {
                                if (IndRel == EmployeeFoundToAssign.length) {
                                    swal("Required Reliver", "Please add more professor on Reliver to procced", "error");
                                }
                                continue;
                            }
                        }
                    }
                    else if (checkIsTypeOfRoom == 'stand by') {
                        const criteria = ['StandByCount', 'ReliverCount', 'DayUsageCount', 'ExamUsageCount']
                        EmployeeFoundToAssign = multisort(EmployeeFoundToAssign, criteria)
                        IsStandBy = true;
                        var _ConfigSettingArray=[];
                        if($scope.ExamConfig.StandByDepartment!='' && $scope.ExamConfig.Max_StandByUser>=_ConfigurationMaxUsageDepartmentStandBy){
                            _ConfigSettingArray=EmployeeFoundToAssign.filter(x=>x.Department==$scope.ExamConfig.StandByDepartment);
                        }
                        LoopToCkeckItemIndexOfEmpRelAllowed: for (var IndRel = 0; IndRel < EmployeeFoundToAssign.length; IndRel++) {
                            if (EmployeeFoundToAssign[IndRel].AllowedStandBy) {
                                if($scope.ExamConfig.StandByDepartment!='' && _ConfigSettingArray.length>0){
                                    if(EmployeeFoundToAssign[IndRel].Department==$scope.ExamConfig.StandByDepartment){
                                        IndexOfProfToAssign = IndRel;
                                        ReliverUsedOn1stSlot.push(EmployeeFoundToAssign[IndRel].Identity);
                                        _ConfigurationMaxUsageDepartmentStandBy++;
                                        break LoopToCkeckItemIndexOfEmpRelAllowed;
                                    }else{
                                        continue;
                                    }
                                }else{
                                    IndexOfProfToAssign = IndRel;
                                    break LoopToCkeckItemIndexOfEmpRelAllowed;
                                }
                            } else {
                                if (IndRel == EmployeeFoundToAssign.length) {
                                    swal("Required Stand By", "Please add more professor on Stand By to procced", "error");
                                }
                                continue;
                            }
                        }
                    }
                    else {
                        //Allowed Stand By And Allowed Reliver will gives priority to set employee who has value false 
                        const criteria = ['ExamUsageCount', 'AllowedReliver', 'AllowedStandBy']//'ReliverCount', 'StandByCount', 'DayUsageCount', 
                        EmployeeFoundToAssign = multisort(EmployeeFoundToAssign, criteria);
                        if(ReliverUsedOn1stSlot.length>0 ){
                            for(var ReliverUseInd=0;ReliverUseInd<ReliverUsedOn1stSlot.length;ReliverUseInd++){
                                var IndexToUse=EmployeeFoundToAssign.findIndex(x=>x.Identity==ReliverUsedOn1stSlot[ReliverUseInd]);
                                if(IndexToUse!=-1){
                                    IndexOfProfToAssign=IndexToUse;
                                    ReliverUsedOn1stSlot.splice(IndexToUse,1);
                                }
                            }
                        }
                    }
                    if (EmployeeFoundToAssign.length > 0) {
                        if(checkIsTypeOfRoom == 'room'){
                            PreviosSlotTeacherUsedInRoomToTakeupInReliver.push(EmployeeFoundToAssign[IndexOfProfToAssign].Identity)
                        }
                        FinalTimeTableobj.Room = RoomArray[romml];
                        FinalTimeTableobj.ProfName = EmployeeFoundToAssign[IndexOfProfToAssign].Identity;
                        FinalTimeTableobj.IsOnError = false;
                        FinalTimeTableobj.dateAssigned = $scope.CreateExamvalidDates[ad];
                        FinalTimeTableobj.Time = CreateTimeFormate(result[0].Time[times].FromTime) + ' to ' + CreateTimeFormate(result[0].Time[times].ToTime);
                        //FinalTimeTableobj.ToTime = result[0].Time[times].ToTime;
                        FinalTimeTable.push(FinalTimeTableobj);
                        if($('#chkMapping').prop("checked")){
                            var IndextoRemoveOnlyInSpec=Old_EmployeeFoundToAssign.findIndex(x=>x.Identity==EmployeeFoundToAssign[IndexOfProfToAssign].Identity);
                            Old_EmployeeFoundToAssign.splice(IndextoRemoveOnlyInSpec,1)
                        }
                        EmployeeFoundToAssign.splice(IndexOfProfToAssign, 1);
                        updateMainArray(FinalTimeTableobj.ProfName, FinalTimeTableobj.dateAssigned, FinalTimeTableobj.Time, IsReliver, IsStandBy)
                    } else {
                        if (FailureCount > 5) {
                            FinalTimeTableobj.Room = RoomArray[romml];
                            FinalTimeTableobj.ProfName = '';
                            FinalTimeTableobj.IsOnError = true;
                            FinalTimeTableobj.dateAssigned = $scope.CreateExamvalidDates[ad];
                            FinalTimeTableobj.Time = CreateTimeFormate(result[0].Time[times].FromTime) + ' to ' + CreateTimeFormate(result[0].Time[times].ToTime);
                            //FinalTimeTableobj.ToTime = result[0].Time[times].ToTime;
                            FinalTimeTable.push(FinalTimeTableobj);
                            swal("limit exhausted", "Please add more professor or adjust Max Supervision", "error");
                        } else {
                            FailureCount++;
                            IsRequiredBreak = true;
                            break;
                        }
                    }
                }
                if (IsRequiredBreak) {
                    break;
                }
            }
            if (IsRequiredBreak) {
                break;
            }
        }
        if (IsRequiredBreak) {
            $scope.GenerateTimeTable();
        } else {
            $scope.FFinalTimeTableObj = FinalTimeTable;
            $scope.CreateArrayToMakeTableEasy(FinalTimeTable)
        }
    }

    function isDateInArray(needle, haystack) {
        for (var i = 0; i < haystack.length; i++) {
            if (needle.getTime() === haystack[i].getTime()) {
                return true;
            }
        }
        return false;
    }

    $scope.CreateArrayToMakeTableEasy = function (arrayfinal) {
        var FinalArrayToConsider = [];
        var dupes = {};
        var DistinctRooms = [];
        //Get Distinct Rooms
        $.each(arrayfinal, function (i, el) {
            if (!dupes[el.Room]) {
                dupes[el.Room] = true;
                var _roomObject={};
                _roomObject.RoomName=el.Room;
                _roomObject.RoomOrderNo=parseFloat($scope.AllRooms.filter(x=>x.RoomName==el.Room)[0].OrderNo)||0;
                DistinctRooms.push(_roomObject);
            }
        });
        $scope.DistinctRoomsForTabel = [];
        //$scope.DistinctRoomsForTabel = DistinctRooms.sort();

        const criteria = ['RoomOrderNo']
        //$scope.MainArrayToCreate = multisort($scope.MainArrayToCreate, criteria)
        $scope.DistinctRoomsForTabel = multisort(DistinctRooms, criteria);
        console.table($scope.DistinctRoomsForTabel)
        var uniqueDates = [];
        for (var i = 0; i < arrayfinal.length; i++) {
            if (!isDateInArray(arrayfinal[i].dateAssigned, uniqueDates)) {
                uniqueDates.push(arrayfinal[i].dateAssigned);
            }
        }
        for (var i = 0; i < uniqueDates.length; i++) {
            var CurrentDateData = arrayfinal.filter(function (e) {
                return e.dateAssigned.getFullYear() == uniqueDates[i].getFullYear() && e.dateAssigned.getMonth() == uniqueDates[i].getMonth() && e.dateAssigned.getDate() == uniqueDates[i].getDate();
            });
            var dupes2 = {};
            var singles2 = [];
            //Get Distinct Time
            $.each(CurrentDateData, function (i, el) {
                if (!dupes2[el.Time]) {
                    dupes2[el.Time] = true;
                    singles2.push(el.Time);
                }
            });
            for (var t = 0; t < singles2.length; t++) {
                var CurrentTimeData = CurrentDateData.filter(function (e) {
                    return e.Time == singles2[t];
                });
                if (DistinctRooms != CurrentTimeData.length) {
                    for (var jt = 0; jt < DistinctRooms.length; jt++) {
                        var index = CurrentTimeData.findIndex(x => x.Room == DistinctRooms[jt].RoomName);
                        if (index < 0) {
                            var Obj = {};
                            Obj.ProfName = '-';
                            Obj.Room = DistinctRooms[jt].RoomName;
                            Obj.RoomOrderNo =parseFloat($scope.AllRooms.filter(x=>x.RoomName==Obj.Room)[0].OrderNo)||0;
                            Obj.dateAssigned = CurrentTimeData[0].dateAssigned;
                            Obj.Time = CurrentTimeData[0].Time;
                            Obj.IsOnError=false;
                            CurrentTimeData.push(Obj);
                        }else{
                            CurrentTimeData[index].RoomOrderNo =parseFloat($scope.AllRooms.filter(x=>x.RoomName==CurrentTimeData[index].Room)[0].OrderNo)||0;
                        }
                    }
                }
                var FinalArrayToConsiderobj = {};
                FinalArrayToConsiderobj.date = uniqueDates[i];
                const criteria = ['RoomOrderNo']
                //$scope.MainArrayToCreate = multisort($scope.MainArrayToCreate, criteria)
                FinalArrayToConsiderobj.AssigedData = multisort(CurrentTimeData, criteria);
                console.table(FinalArrayToConsiderobj);
                FinalArrayToConsider.push(FinalArrayToConsiderobj);
            }
        }


        //Create Array for Display in Easy Angular
        var uniqueDates1 = [];
        for (var i = 0; i < FinalArrayToConsider.length; i++) {
            if (!isDateInArray(FinalArrayToConsider[i].date, uniqueDates1)) {
                uniqueDates1.push(FinalArrayToConsider[i].date);
            }
        }
        $scope.FinalArrayForTable = [];
        for (var j = 0; j < uniqueDates1.length; j++) {
            var CurrentDa = FinalArrayToConsider.filter(function (e) {
                if (uniqueDates1[j] - e.date == 0) {
                    return true;
                } else {
                    return false;
                }
            });
            var FinalArrayobj = {};
            FinalArrayobj.Date = uniqueDates1[j];
            FinalArrayobj.Associated = [];
            for (var a = 0; a < CurrentDa.length; a++) {
                var FinalArraysubobj = {};
                FinalArraysubobj = CurrentDa[a].AssigedData;
                FinalArrayobj.Associated.push(FinalArraysubobj);
            }
            $scope.FinalArrayForTable.push(FinalArrayobj);
        }

        if (!$scope.$$phase) {
            $scope.$apply();
        }

        GenerateHtmlTable($scope.DistinctRoomsForTabel, $scope.FinalArrayForTable)
    }

    function GenerateHtmlTable(uniquroo, finalar) {
        console.log('********************************************************** Professor Usage **********************************************************')
        console.table(removeDuplicates(LogicalArrayProfUsage2, 'Identity'))
        console.log('********************************************************** Professor Usage **********************************************************')
        var strHtml = ''
        strHtml += '<tr style="font-weight:900"><td>Sr</td><td>Date</td><td style="min-width:120px;">Time</td>'
        for (var i = 0; i < uniquroo.length; i++) {
            strHtml += '<td>' + uniquroo[i].RoomName + '</td>'
        }
        strHtml += '</tr>'

        //completed Header
        for (var k = 0; k < finalar.length; k++) {
            strHtml += '<tr>'
            var ischecfirst = true;
            strHtml += '<td style="vertical-align: middle;" rowspan="' + finalar[k].Associated.length + '">' + (k + 1) + '</td><td style="vertical-align: middle;" rowspan="' + finalar[k].Associated.length + '">' + CreateIndianDateform(finalar[k].Date) + '</td>'
            for (var h = 0; h < finalar[k].Associated.length; h++) {
                if (ischecfirst) {
                    ischecfirst = false;
                } else {
                    strHtml += '<tr>'
                }
                
                strHtml += '<td>' + finalar[k].Associated[h][0].Time + '</td>'
                for (var l = 0; l < finalar[k].Associated[h].length; l++) {
                    var colr='none';
                    if(finalar[k].Associated[h][l].IsOnError){
                        colr='#ffcdd2';
                    }
                    strHtml += '<td style="background: '+colr+';">' + finalar[k].Associated[h][l].ProfName + '</td>'
                }
                strHtml += '</tr>'
            }

            strHtml += '</tr>'
        }

        $('#tbodyOfExamList').html('')
        $('#tbodyOfExamList').html(strHtml)
        $('.overlay').css('display', 'none');
    }

    function IsUniqueFun(name) {
        return name === this.toString();
    }
    function updateMainArray(name, dateass, timeass, IsReliver, IsStandBy) {
        var IsFirst = true;
        var IsFirst2 = true;

        var ExamUsage = '';
        var IntExamUsage = 0;
        var ReliverCount = '';
        var StandByCount = '';
        var intrele = 0;
        var intStand = 0;
        var DayUsageCount = '';
        var intDayUsageCount = 0;
        for (var loc = 0; loc < LogicalArrayProfUsage2.length; loc++) {
            if (LogicalArrayProfUsage2[loc].Identity == name) {
                //Update Reliver & Stand By Count if
                if (IsFirst) {
                    var ExamUsage = LogicalArrayProfUsage2[loc].ExamUsageCount;
                    IntExamUsage = parseInt(ExamUsage);
                    IntExamUsage++;
                    var ReliverCount = LogicalArrayProfUsage2[loc].ReliverCount;
                    var StandByCount = LogicalArrayProfUsage2[loc].StandByCount;
                    var intStand = parseInt(StandByCount);
                    intrele = parseInt(ReliverCount);
                    if (IsReliver) {
                        intrele++;
                    }
                    if (IsStandBy) {
                        intStand++;
                    }
                    IsFirst = false;
                }
                LogicalArrayProfUsage2[loc].StandByCount = intStand;
                LogicalArrayProfUsage2[loc].ReliverCount = intrele;
                LogicalArrayProfUsage2[loc].ExamUsageCount = IntExamUsage;

                if (LogicalArrayProfUsage2[loc].date.getFullYear() == dateass.getFullYear() && LogicalArrayProfUsage2[loc].date.getMonth() == dateass.getMonth() && LogicalArrayProfUsage2[loc].date.getDate() == dateass.getDate()) {
                    if (IsFirst2) {
                        DayUsageCount = LogicalArrayProfUsage2[loc].DayUsageCount;
                        intDayUsageCount = parseInt(DayUsageCount);
                        intDayUsageCount++;
                    }
                    LogicalArrayProfUsage2[loc].DayUsageCount = intDayUsageCount;
                    if (LogicalArrayProfUsage2[loc].time == timeass) {
                        LogicalArrayProfUsage2[loc].IsUsed = true;
                    }
                }
            }

        }
    }

    function CreateIndianDateform(y) {
        var z = y.toString();
        var x = new Date(z);
        var dd = x.getDate();
        var mm = x.getMonth() + 1;
        var yyyy = x.getFullYear();
        if (dd < 10) {
            dd = '0' + dd;
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        var today = dd + '/' + mm + '/' + yyyy;
        return today;
    };

    function CreateTimeFormate(x) {
        var str = x.toString();
        if (str == '0' || str == '00' || str == '000') {
            str = '0000'
        }
        var len = str.length;
        var time = str.substring(0, len - 2) + ":" + str.substring(len - 2);
        return time;
    }

    var FailureCount = 0;
    $scope.GenerateTimeTable = function () {
        if ($('#dd_ExamName').val() == '') {

        } else {
            $('#TimeTableArea').css('display', '');
            SelectedExamIndex = $('#dd_ExamName').val();
            $scope.CreateLogicalArray($('#dd_ExamName').val());
        }
    }

    $scope.PostData = function (alldata) {
        alldata = JSON.parse(angular.toJson(alldata))
        var InputData = {}
        InputData.data = escape(JSON.stringify(alldata));
        InputData.FileName = "TimeTable.json";
        InputData.UserGroup = UserGroup;
        var settings = {
            "async": true,
            "crossDomain": true,
            "url": CommonPath + "/api/Professor/SaveAllItem",
            "method": "POST",
            "headers": {
                "Content-Type": "application/json",
                "Cache-Control": "no-cache",
            },
            "processData": false,
            contentType: 'application/json; charset=utf-8',
            "data": JSON.stringify(InputData)
        }
        return $.ajax(settings)
    }

    $scope.SaveTimeTable = function () {
        if ($scope.AllTimeTableData.filter(e => e.ExamName === $('#dd_ExamName option:selected').text()).length > 0) {
            swal("Duplicate", "Timetable Already saved with same name, Please remove one", "error");
        } else {
            var TT3obj = {};
            TT3obj.ExamName = $('#dd_ExamName option:selected').text();
            TT3obj.ExamTimeTableData = [];

            TT3obj.ExamTimeTableData = $scope.FinalArrayForTable;
            TT3obj.ExamTimeTableRoomData = $scope.DistinctRoomsForTabel;
            $scope.AllTimeTableData.push(TT3obj)
            var response = $scope.PostData($scope.AllTimeTableData);
            response.done(function (respo) {
                if (respo) {
                    swal("Created!", "TimeTable Saved Successfully", "success");
                    $scope.GetAllPref();
                } else {
                    swal("Error", "Something went wrong Please try again", "error");
                }
            })
        }
    }

    $scope.ConfigExamMaster=function(){
        $('#ConfigurationModal').modal('toggle');
    }
    $.validate({
        form: '#ExamConfigForm',
        onSuccess: function ($form) {
            $('#ConfigurationModal').modal('toggle');
            swal("Success", "Exam configuration saved successfully", "success");
        }
    });

});

app.controller("SetPreferenceCtrl", function ($scope, $http) {
    $scope.AllUsers = [];
    $scope.GetAllPref = function () {
        $http.get(CommonPath + '/api/Professor/SelectAllItem?FileName=Preference.json&UserGroup=' + UserGroup).then(function (respo) {
            try {
                $scope.AllPreference = $.parseJSON(respo.data)
            } catch (e) {
                $scope.AllPreference = [];
            }
            $('#sampleTable').DataTable().destroy();
            setTimeout(function () {
                $('.overlay').css('display', 'none');
                $('#sampleTable').DataTable();
            }, 1000)
        })
    }
    $scope.GetAllProf = function () {
        $http.get(CommonPath + '/api/Professor/SelectAllProfessor?UserGroup=' + UserGroup).then(function (respo) {
            $('#dd_ProfName')
                .empty()
                .append('<option value="">Select Professor</option>')
                .find('option:first')
                .attr("selected", "selected");
            $.each(respo.data, function (i, item) {
                $('#dd_ProfName').append($('<option>', {
                    value: item.name,
                    text: item.name
                }));
            });
        })
    }

    $scope.GetAllPref();
    $scope.GetAllProf();
    $('#demoDate').datepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
        todayHighlight: true
    });
    $scope.CreateDate = function (dateString) {
        var dateParts = dateString.split("/");
        var dateObject = new Date(dateParts[2], dateParts[1] - 1, dateParts[0]); // month is 0-based
        return dateObject
    }
    //Done
    $scope.delete = function (index) {
        swal({
            title: "Are you sure?",
            text: "Your will not be able to recover this imaginary file!",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false
        },
            function () {
                $scope.AllPreference.splice(index, 1);
                var response = $scope.PostData($scope.AllPreference);
                response.done(function (respo) {
                    if (respo) {
                        swal("Deleted!", "Deleted Successfully", "success");
                        $scope.GetAllPref();
                    } else {
                        swal("Error", "Something went wrong Please try again", "error");
                    }
                });

            });
    };


    $.validate({
        form: '#PreferenceAddForm',
        onSuccess: function ($form) {
            var PrefObj = {};
            PrefObj.ProfessorName = $('#dd_ProfName').val();
            PrefObj.DateOfLeave = $scope.CreateDate($('#demoDate').val());
            if ($scope.CurrentIndexToEditPref == -1) {
                $scope.AllPreference.push(PrefObj);
            } else {
                $scope.AllPreference[$scope.CurrentIndexToEditProf] = PrefObj;
            }
            var response = $scope.PostData($scope.AllPreference);
            response.done(function (respo) {
                $('#ProfessorModal').modal('toggle');
                if (respo) {
                    swal("Created!", "Record added Successfully", "success");
                    $scope.GetAllPref();
                } else {
                    swal("Error", "Something went wrong Please try again", "error");
                }
            })
        }
    });

    $scope.PostData = function (alldata) {
        alldata = JSON.parse(angular.toJson(alldata))
        var InputData = {}
        InputData.data = escape(JSON.stringify(alldata));
        InputData.FileName = "Preference.json";
        InputData.UserGroup = UserGroup;
        var settings = {
            "async": true,
            "crossDomain": true,
            "url": CommonPath + "/api/Professor/SaveAllItem",
            "method": "POST",
            "headers": {
                "Content-Type": "application/json",
                "Cache-Control": "no-cache",
            },
            "processData": false,
            contentType: 'application/json; charset=utf-8',
            "data": JSON.stringify(InputData)
        }

        return $.ajax(settings)
    }
    $scope.CurrentIndexToEditPref = -1;
    $scope.edit = function (index) {
        $('#ProfessorModal').modal('show');
        $scope.CurrentIndexToEditPref = index;
        if ($scope.CurrentIndexToEditPref == -1) {
            $('#dd_ProfName').val('');
            $('#demoDate').val('');
        } else {
            $('#dd_ProfName').val($scope.AllPreference[$scope.CurrentIndexToEditPref].ProfessorName);
            var z = $scope.AllPreference[$scope.CurrentIndexToEditPref].DateOfLeave.toString();
            var x = new Date(z);
            var dd = x.getDate();
            var mm = x.getMonth() + 1;
            var yyyy = x.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            var today = dd + '/' + mm + '/' + yyyy;
            $('#demoDate').val(today);
        }
    };
})

app.controller("LoginCtrl", function ($scope, $http) {
    $scope.User = {};
    $('.app-sidebar').css('display', 'none');
    $('#HeaderAndSidebarId').css('display', 'none');

    $.validate({
        form: '#LoginForm',
        onSuccess: function ($form) {
            $('#divErrror').css('display', 'none')
            var usedata = {};
            usedata.Username = $('#txt_Username').val();
            usedata.Password = $('#txt_Password').val();
            var settings = {
                "async": true,
                "crossDomain": true,
                "url": CommonPath + "/api/Professor/CheckLogin",
                "method": "POST",
                "headers": {
                    "Content-Type": "application/json",
                    "Cache-Control": "no-cache",
                },
                "processData": false,
                "data": JSON.stringify(usedata)
            }
            $.ajax(settings).done(function (response) {
                if (response == null) {
                    $('#divErrror').css('display', '');
                    $('#txt_Password').val('')
                } else {
                    var LoggedUser = response;
                    LoggedUser.Password = '';
                    console.log(response)
                    localStorage.setItem('LoggedUser', JSON.stringify(LoggedUser));
                    $('.app-sidebar').css('display', '');
                    $('#HeaderAndSidebarId').css('display', '');
                    UserGroup = response.UserGroup;
                    $('.UsernameOfLogged').html(LoggedUser.Username);
                    $('.designationOfUser').html(LoggedUser.Role);
                    window.location.href = CommonPath + '/#!/';
                }
            });
        }
    });
})

app.controller("SavedTimetableCtrl", function ($scope, $http) {
    $scope.AllTimeTableData = [];
    $scope.GetAllTimeTable = function () {
        $http.get(CommonPath + '/api/Professor/SelectAllItem?FileName=TimeTable.json&UserGroup=' + UserGroup).then(function (respo) {
            try {
                $scope.AllTimeTableData = $.parseJSON(respo.data)
                $('#dd_ExamName')
                    .empty()
                    .append('<option value="">Select Exam</option>')
                    .find('option:first')
                    .attr("selected", "selected");
                $.each($scope.AllTimeTableData, function (i, item) {
                    $('#dd_ExamName').append($('<option>', {
                        value: i,
                        text: item.ExamName
                    }));
                });
            } catch (e) {
                $scope.AllTimeTableData = [];
            }
        })
    }
    $scope.GetAllTimeTable();
    $scope.GetTimeTable = function () {
        //ExamTimeTableData[""0""].Associated[""0""]
        GenerateHtmlTable($scope.AllTimeTableData[$('#dd_ExamName').val()].ExamTimeTableRoomData, $scope.AllTimeTableData[$('#dd_ExamName').val()].ExamTimeTableData)
    }

    function CreateIndianDateform(y) {
        var z = y.toString();
        var x = new Date(z);
        var dd = x.getDate();
        var mm = x.getMonth() + 1;
        var yyyy = x.getFullYear();
        if (dd < 10) {
            dd = '0' + dd;
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        var today = dd + '/' + mm + '/' + yyyy;
        return today;
    };

    function CreateTimeFormate(x) {
        var str = x.toString();
        if (str == '0' || str == '00' || str == '000') {
            str = '0000'
        }
        var len = str.length;
        var time = str.substring(0, len - 2) + ":" + str.substring(len - 2);
        return time;
    }
    function GenerateHtmlTable(uniquroo, finalar) {
        $('#TimeTableArea').css('display', '');
        var strHtml = ''
        strHtml += '<tr style="font-weight:900"><td>Sr</td><td>Date</td><td style="min-width: 120px;">Time</td>'
        for (var i = 0; i < uniquroo.length; i++) {
            strHtml += '<td>' + uniquroo[i].RoomName + '</td>'
        }
        strHtml += '</tr>'

        //completed Header
        for (var k = 0; k < finalar.length; k++) {
            strHtml += '<tr>'
            var ischecfirst = true;
            strHtml += '<td rowspan="' + finalar[k].Associated.length + '">' + (k + 1) + '</td><td rowspan="' + finalar[k].Associated.length + '">' + CreateIndianDateform(finalar[k].Date) + '</td>'
            for (var h = 0; h < finalar[k].Associated.length; h++) {
                if (ischecfirst) {
                    ischecfirst = false;
                } else {
                    strHtml += '<tr>'
                }
                strHtml += '<td>' + finalar[k].Associated[h][0].Time + '</td>'
                for (var l = 0; l < finalar[k].Associated[h].length; l++) {
                    strHtml += '<td>' + finalar[k].Associated[h][l].ProfName + '</td>'
                }
                strHtml += '</tr>'
            }

            strHtml += '</tr>'
        }

        $('#tbodyOfExamList').html('')
        $('#tbodyOfExamList').html(strHtml)
        $('.overlay').css('display', 'none');
        GenerateHtmlTableForPrint(uniquroo, finalar)
    }

    function GenerateHtmlTableForPrint(uniquroo, finalar) {
        var strHtml = ''
        strHtml += '<tr style="font-weight:900"><td>Sr</td><td>Date</td><td style="min-width: 120px;">Time</td>'
        for (var i = 0; i < uniquroo.length; i++) {
            strHtml += '<td>' + uniquroo[i].RoomName + '</td>'
        }
        strHtml += '</tr>'
        var count = 1;
        //completed Header
        for (var k = 0; k < finalar.length; k++) {
            strHtml += '<tr>'
            var ischecfirst = true;

            for (var h = 0; h < finalar[k].Associated.length; h++) {
                strHtml += '<td>' + (count) + '</td><td>' + CreateIndianDateform(finalar[k].Date) + '</td>'
                //if (ischecfirst) {
                //    ischecfirst = false;
                //} else {
                //    strHtml += '<tr>'
                //}
                strHtml += '<td>' + finalar[k].Associated[h][0].Time + '</td>'
                for (var l = 0; l < finalar[k].Associated[h].length; l++) {
                    strHtml += '<td>' + finalar[k].Associated[h][l].ProfName + '</td>'
                }
                strHtml += '</tr>'
                count++;
            }

            strHtml += '</tr>'
        }

        $('#PrinntBody').html('')
        $('#PrinntBody').html(strHtml)
    }

    $scope.PrintTimeTable = function () {
        window.open(CommonPath + '/PrintTimeTable.html?TimeTableId=' + $('#dd_ExamName').val(), '_blank');
    }

    $scope.DeleteTimeTableCurrent = function () {
        var index = $('#dd_ExamName').val();
        swal({
            title: "Are you sure?",
            text: "Your will not be able to recover this Timetable!",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false
        },
            function () {
                $scope.AllTimeTableData.splice(index, 1);
                var response = $scope.PostData($scope.AllTimeTableData);
                response.done(function (respo) {
                    if (respo) {
                        swal("Deleted!", "Deleted Successfully", "success");
                        $scope.GetAllTimeTable();
                        $('#TimeTableArea').css('display', 'none');
                    } else {
                        swal("Error", "Something went wrong Please try again", "error");
                    }
                });

            });
    };
    $scope.PostData = function (alldata) {
        alldata = JSON.parse(angular.toJson(alldata))
        var InputData = {}
        InputData.data = escape(JSON.stringify(alldata));
        InputData.FileName = "TimeTable.json";
        InputData.UserGroup = UserGroup;
        var settings = {
            "async": true,
            "crossDomain": true,
            "url": CommonPath + "/api/Professor/SaveAllItem",
            "method": "POST",
            "headers": {
                "Content-Type": "application/json",
                "Cache-Control": "no-cache",
            },
            "processData": false,
            contentType: 'application/json; charset=utf-8',
            "data": JSON.stringify(InputData)
        }
        return $.ajax(settings)
    }
})

app.controller("SeatingArrangementCtrl",function($scope, $http){
    $scope.AddNewConfig={};
    $scope.AllRooms = [];
    $scope.GetAllRooms = function () {
        $http.get(CommonPath + '/api/Professor/SelectAllRomList?UserGroup=' + UserGroup).then(function (respo) {
            if (respo.data == null || respo.data == '') {
                $scope.AllRooms = [];

            } else {
                $scope.AllRooms = JSON.parse(respo.data);
            }        
            $.each($scope.AllRooms, function (i, obj) {
                $('#ClassRoom').append($('<option>').text(obj.RoomName).attr('value', i));
                $('#StudentOfClass').append($('<option>').text(obj.RoomName).attr('value', i));
            });
           
        });
    }
    $scope.TotalStudentSeatingArrangementMain=[];
    $scope.TotalStudentSeatingArrangementCurrent={};
    $scope.TotalStudentSeatingArrangementCurrent.SeatingArrange=[];
    $scope.GetAllSeatingArrangement = function () {
        $http.get(CommonPath + '/api/Professor/SelectAllItem?FileName=SeatingArrangement.json&UserGroup=' + UserGroup).then(function (respo) {
            try {
                $scope.TotalStudentSeatingArrangementMain = $.parseJSON(respo.data)
            } catch (e) {
                $scope.TotalStudentSeatingArrangementMain = [];
            }
        })
    }

    $scope.GetAllSeatingArrangement();
    $scope.AllExamArray = [];
    $scope.GetAllExamList = function () {
        $scope.AllExamArray = [];
        //$scope.$apply()
        $http.get(CommonPath + 'api/Professor/SelectAllExamList?UserGroup=' + UserGroup).then(function (respo1) {
            var respo = [];
            try {
                respo = $.parseJSON(respo1.data)
            } catch (e) {
                respo = [];
            }
            $scope.AllExamArray = respo;
            //var str = '';
            //for (var i = 0; i < respo.length; i++) {
            //    str += '<tr><td>' + (i + 1) + '</td><td>' + respo.ExamName + '</td><td></td><a class="btn btn-primary" ng-click="CreateOrUpdateExamName($index)" style="color:white"><i class="fa fa-lg fa-edit" style="margin-top: -3px;"></i> Edit</a>'+
            //        '<a class="btn btn-primary" ng - click="ExamScheduleModal(' + i+')" style = "color:white" > <i class="fa fa-lg fa-clock-o" style="margin-top: -3px;"></i> Schedule</a>'+
            //            '<a class="btn btn-primary" ng-click="deleteExam($index)" style="color:white"><i class="fa fa-lg fa-trash" style="margin-top: -3px;"></i> Delete</a></tr>'
            //}
            $('#sampleTable').DataTable().destroy();
            setTimeout(function () {
                $('.overlay').css('display', 'none');
                $('#sampleTable').DataTable();
            }, 1000)
        });
    }
    $scope.GetAllExamList();
    $scope.GetAllRooms();
    $scope.CurrentExamIndex=-1;
    $scope.ArrangeExam=function(ExamIndex){
        var IsPresentExam=$scope.TotalStudentSeatingArrangementMain.findIndex(x=>x.ExamName==$scope.AllExamArray[ExamIndex].ExamName);
        if(IsPresentExam>-1){
            $scope.TotalStudentSeatingArrangementCurrent=$scope.TotalStudentSeatingArrangementMain[IsPresentExam];
        }else{
            $scope.TotalStudentSeatingArrangementCurrent={};
            $scope.TotalStudentSeatingArrangementCurrent.SeatingArrange=[];
        }
        $scope.AddNewConfig.FromRollNumber=0;
        $scope.AddNewConfig.ToRollNumber=0;
        $scope.CurrentExamIndex=ExamIndex;
        $scope.CurrentExamUpdate=$scope.AllExamArray[ExamIndex];
        $('#StudentArrangeModal').modal('toggle');
        $("#StudentOfClass").prop("selectedIndex", 0);
        $("#ClassRoom").prop("selectedIndex", 0);
    }

    $scope.GetTotalCalculation=function(){
        $scope.AddNewConfig.TotalStudent=(($scope.AddNewConfig.ToRollNumber-$scope.AddNewConfig.FromRollNumber)+1);
    }
   
    $scope.AddSeatingArrengement=function(){
        $scope.TotalStudentSeatingArrangementCurrent.ExamName=$scope.AllExamArray[$scope.CurrentExamIndex].ExamName;
        $scope.AddNewConfig.ClassRoom=$scope.AllRooms[$('#ClassRoom').val()].RoomName;
        $scope.AddNewConfig.StudentOfClass=$scope.AllRooms[$('#StudentOfClass').val()].RoomName;
        var Config=angular.copy($scope.AddNewConfig);
        var FoundArray=$scope.TotalStudentSeatingArrangementCurrent.SeatingArrange.filter(x=>x.ClassRoom==$scope.AllRooms[$('#ClassRoom').val()].RoomName);
        var sumOfStudentInClass=FoundArray.sum("TotalStudent");
        if(sumOfStudentInClass>0){
            for(var seatI=0;seatI<$scope.TotalStudentSeatingArrangementCurrent.SeatingArrange.length;seatI++){
                if($scope.TotalStudentSeatingArrangementCurrent.SeatingArrange[seatI].ClassRoom==Config.ClassRoom){
                    $scope.TotalStudentSeatingArrangementCurrent.SeatingArrange[seatI].TotalStudent=(sumOfStudentInClass+Config.TotalStudent);
                }
            }
        }
        Config.TotalStudent=(sumOfStudentInClass+Config.TotalStudent);
        $scope.TotalStudentSeatingArrangementCurrent.SeatingArrange.push(Config);
    }

    Array.prototype.sum = function (prop) {
        var total = 0
        for ( var i = 0, _len = this.length; i < _len; i++ ) {
            total += this[i][prop]
        }
        return total
    }

    $scope.DeleteStudentArrangement=function(indexToDelete){
        $scope.TotalStudentSeatingArrangementCurrent.SeatingArrange.splice(indexToDelete,1);
    }
    
    $scope.SaveStudentArranagement=function(){
        var ExamIndexEdit=$scope.TotalStudentSeatingArrangementMain.findIndex(x=>x.ExamName==$scope.AllExamArray[$scope.CurrentExamIndex].ExamName)
        if(ExamIndexEdit>-1){
            $scope.TotalStudentSeatingArrangementMain[ExamIndexEdit]=$scope.TotalStudentSeatingArrangementCurrent;
        }else{
            $scope.TotalStudentSeatingArrangementMain.push($scope.TotalStudentSeatingArrangementCurrent);
        }

        var response = $scope.PostData($scope.TotalStudentSeatingArrangementMain);
        response.done(function (respo) {
            $('#ProfessorModal').modal('toggle');
            if (respo) {
                swal("Created!", "Record added Successfully", "success");
                $scope.GetAllPref();
            } else {
                swal("Error", "Something went wrong Please try again", "error");
            }
        })
    }

    $scope.PostData = function (alldata) {
        alldata = JSON.parse(angular.toJson(alldata))
        var InputData = {}
        InputData.data = escape(JSON.stringify(alldata));
        InputData.FileName = "SeatingArrangement.json";
        InputData.UserGroup = UserGroup;
        var settings = {
            "async": true,
            "crossDomain": true,
            "url": CommonPath + "/api/Professor/SaveAllItem",
            "method": "POST",
            "headers": {
                "Content-Type": "application/json",
                "Cache-Control": "no-cache",
            },
            "processData": false,
            contentType: 'application/json; charset=utf-8',
            "data": JSON.stringify(InputData)
        }

        return $.ajax(settings)
    }

    $scope.PrintSeatingArrangement=function(examIndexToPrint){
        var FindIndexPrint=$scope.TotalStudentSeatingArrangementMain.findIndex(x=>x.ExamName==$scope.AllExamArray[examIndexToPrint].ExamName);
        if(FindIndexPrint>-1){
            window.open(CommonPath + '/SeatingArrangementPrint.html?PrintTimeTableId=' + FindIndexPrint, '_blank');
        }else{
            swal("Error", "Configuration not done for the exam "+$scope.AllExamArray[examIndexToPrint].ExamName, "error"); 
        }
    }
})

app.controller("ConfigureRoomCtrl",function($scope, $http){
    $scope.AllRooms = [];
    $scope.GetAllRooms = function () {
        $http.get(CommonPath + '/api/Professor/SelectAllRomList?UserGroup=' + UserGroup).then(function (respo) {
            if (respo.data == null || respo.data == '') {
                $scope.AllRooms = [];

            } else {
                $scope.AllRooms = JSON.parse(respo.data);
            }
            $('#sampleTable').DataTable().destroy();
            setTimeout(function () {
                $('.overlay').css('display', 'none');
                $('#sampleTable').DataTable();
            }, 1000)
        });
    }

    $scope.GetAllProf = function () {
        $http.get(CommonPath + '/api/Professor/SelectAllProfessor?UserGroup='+UserGroup).then(function (respo) {
            $scope.Teachers = respo.data;
            $('#sampleTable').DataTable().destroy();
            setTimeout(function () {
                $('.overlay').css('display', 'none');
                $('#sampleTable').DataTable();
            }, 1000)
        })
    }
    $scope.PostData = function (alldata) {
        alldata = JSON.parse(angular.toJson(alldata))
        var InputData = {}
        InputData.data = escape(JSON.stringify(alldata));
        InputData.FileName = "RoomTeacherMapping.json";
        InputData.UserGroup = UserGroup;
        var settings = {
            "async": true,
            "crossDomain": true,
            "url": CommonPath + "/api/Professor/SaveAllItem",
            "method": "POST",
            "headers": {
                "Content-Type": "application/json",
                "Cache-Control": "no-cache",
            },
            "processData": false,
            contentType: 'application/json; charset=utf-8',
            "data": JSON.stringify(InputData)
        }
        return $.ajax(settings)
    }
    
    var MainArrayMapping=[];
    $scope.GetAllPref = function () {
        $http.get(CommonPath + '/api/Professor/SelectAllItem?FileName=RoomTeacherMapping.json&UserGroup='+UserGroup).then(function (respo) {
            try {
                MainArrayMapping = $.parseJSON(respo.data)
            } catch (e) {
                MainArrayMapping = [];
            }
        })
    }
    $scope.GetAllPref();

    $scope.GetAllRooms();

    var SelectedModalData={};
    $scope.CreateOrUpdate=function(index){
        $scope.RoomIndexToUpdate=index;
        $scope.CurrentRoomTOUpdate=$scope.AllRooms[index];
        var mappingIndex = MainArrayMapping.findIndex(x => x.RoomName == $scope.AllRooms[$scope.RoomIndexToUpdate].RoomName);
        if(mappingIndex==-1){
            SelectedModalData={};
            SelectedModalData.AssignedRoom=[];
        }else{
            SelectedModalData=MainArrayMapping[mappingIndex];
        }
        $('#ExamScheduleModal').modal('toggle');
        $scope.GetAllProf();
    }

    $scope.GetTeacherNames=function(indexOfRoom){
        var ThisTD=$scope.AllRooms[indexOfRoom].RoomName;
        var mappingIndex = MainArrayMapping.findIndex(x => x.RoomName == ThisTD);
        if(mappingIndex==-1){
            return '';
        }else{
            return MainArrayMapping[mappingIndex].AssignedRoom.toString();
        }
    }
    
    $scope.IsCheckReqired=function(teacherNameFromUI){
        var TeacherIndex=SelectedModalData.AssignedRoom.findIndex(x => x == teacherNameFromUI);
        if(TeacherIndex==-1){
            return false;
        }else{
            return true;
        }
    }

    $scope.SaveTeacherMapping=function(){
        var StudentMainArray={}
        StudentMainArray.RoomName=$scope.AllRooms[$scope.RoomIndexToUpdate].RoomName;
        StudentMainArray.AssignedRoom=[];
        $('.chkTeacherSelect').each(function(i, obj) {
            if(this.checked){
                var NameOfTeacher=angular.element($(this)).data().teachername;
                StudentMainArray.AssignedRoom.push(NameOfTeacher);
            }
        });

        var MainIndexMap= MainArrayMapping.findIndex(x => x.RoomName == $scope.AllRooms[$scope.RoomIndexToUpdate].RoomName);
        if(MainIndexMap==-1){
            MainArrayMapping.push(StudentMainArray);
        }else{
            MainArrayMapping[MainIndexMap]=StudentMainArray;
        }
        
        var response = $scope.PostData(MainArrayMapping);
        
        response.done(function (respo) {
            $('#ExamScheduleModal').modal('toggle');
            if (respo) {
                swal("Created!", "Record added Successfully", "success");
            } else {
                swal("Error", "Something went wrong Please try again", "error");
            }
        })
    }

});

app.controller("DepartmentCtrl",function($scope, $http,APIService){
    $scope.AllDepartment=[];
    $scope.GetAllDepartment = function () {
        APIService.GetFiles('Department').then(function (respo) {
            if (respo.data == null || respo.data == '') {
                $scope.AllDepartment = [];
            } else {
                $scope.AllDepartment = JSON.parse(respo.data);
            }
            $('#sampleTable').DataTable().destroy();
            setTimeout(function () {
                $('.overlay').css('display', 'none');
                $('#sampleTable').DataTable();
            }, 1000)
        });
    }
    $scope.GetAllDepartment();

    var IndexOfModalUpdate=-1;
    $scope.CreateOrUpdate=function(index){
        IndexOfModalUpdate=index;
        if(index==-1){
            $scope.CurrentDepartment='';
        }else{
            $scope.CurrentDepartment=$scope.AllDepartment[index];
        }
        $('#Departmentmodal').modal('toggle');
    }

    $.validate({
        form: '#DepartmentAddUpdate',
        onSuccess: function ($form) {
            var DuplicateIndex=$scope.AllDepartment.findIndex(x=>x==$scope.CurrentDepartment);
            if(DuplicateIndex==-1){
                if(IndexOfModalUpdate==-1){
                    $scope.AllDepartment.push($scope.CurrentDepartment);
                }else{
                    $scope.AllDepartment[IndexOfModalUpdate]=$scope.CurrentDepartment;
                }
                var alldata = JSON.parse(angular.toJson($scope.AllDepartment))
                var InputData = {}
                InputData.data = escape(JSON.stringify(alldata));
                InputData.FileName = "Department.json";
                InputData.UserGroup = UserGroup;
                APIService.SaveFiles(InputData).done(function(respo){
                    if (respo) {
                        $('#Departmentmodal').modal('toggle');
                        if(IndexOfModalUpdate==-1){
                            swal("Created!", "Created Successfully", "success");
                        }else{
                            swal("Updated!", "Updated Successfully", "success");
                        }
                    } else {
                        swal("Error", "Something went wrong Please try again", "error");
                    }
                });
            }else{
                swal("Duplicate", "Department "+$scope.CurrentDepartment+" is already present in system", "error");
            }
        }
    });

    $scope.delete = function (index) {
        swal({
            title: "Are you sure?",
            text: "Your will not be able to recover this imaginary file!",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false
        },
            function () {
                $scope.AllDepartment.splice(index, 1);
                var InputData = {}
                InputData.data = escape(JSON.stringify($scope.AllDepartment));
                InputData.FileName = "Department.json";
                InputData.UserGroup = UserGroup;
                APIService.SaveFiles(InputData).done(function(respo){
                    if (respo) {
                        swal("Deleted!", "Deleted Successfully", "success");
                    } else {
                        swal("Error", "Something went wrong Please try again", "error");
                    }
                });
                $scope.$apply();
            });
    };
});

app.filter('CustomDateForm', function () {
    return function (y) {
        var z = y.toString();
        var x = new Date(z);
        var dd = x.getDate();
        var mm = x.getMonth() + 1;
        var yyyy = x.getFullYear();
        if (dd < 10) {
            dd = '0' + dd;
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        var today = dd + '/' + mm + '/' + yyyy;
        return today;
    };
});

app.filter('ConvertTime', function () {
    return function (x) {
        var str = x.toString();
        if (str == '0' || str == '00' || str == '000') {
            str = '0000'
        }
        var len = str.length;
        var time = str.substring(0, len - 2) + ":" + str.substring(len - 2);
        return time;
    };
});

app.filter('CapitalFirstWord', function () {
    return function (x) {
        x = x.toLowerCase().replace(/\b[a-z]/g, function (letter) {
            return letter.toUpperCase();
        });
        return x;
    };
});

app.filter('GetCollegeCustomTextBoolean', function () {
    return function (x) {
        var strx = x.toString();
        if (strx == 'true') {
            return 'Yes';
        } else {
            return 'No'
        }
    };
});

app.service('APIService', function ($http) {
    this.SaveFiles = function (InputData) {
        var settings = {
            "async": true,
            "crossDomain": true,
            "url": CommonPath + "/api/Professor/SaveAllItem",
            "method": "POST",
            "headers": {
                "Content-Type": "application/json",
                "Cache-Control": "no-cache",
            },
            "processData": false,
            contentType: 'application/json; charset=utf-8',
            "data": JSON.stringify(InputData)
        }
        return $.ajax(settings)
    }
    this.GetFiles=function(filename){
        return $http.get(CommonPath + '/api/Professor/SelectAllItem?FileName='+filename+'.json&UserGroup=' + UserGroup);
    }
});

//code for converting html to excel
var tablesToExcel2 = (function () {
    var uri = 'data:application/vnd.ms-excel;base64,'
    , tmplWorkbookXML = '<?xml version="1.0"?><?mso-application progid="Excel.Sheet"?><Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet" xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet">'
      + '<DocumentProperties xmlns="urn:schemas-microsoft-com:office:office"><Author>Axel Richter</Author><Created>{created}</Created></DocumentProperties>'
      + '<Styles>'
      + '<Style ss:ID="Currency"><NumberFormat ss:Format="Currency"></NumberFormat></Style>'
      + '<Style ss:ID="Date"><NumberFormat ss:Format="Medium Date"></NumberFormat></Style>'
      + '</Styles>'
      + '{worksheets}</Workbook>'
    , tmplWorksheetXML = '<Worksheet ss:Name="{nameWS}"><Table>{rows}</Table></Worksheet>'
    , tmplCellXML = '<Cell{attributeStyleID}{attributeFormula}><Data ss:Type="{nameType}">{data}</Data></Cell>'
    , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
    , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
    return function (tables, wsnames, wbname, appname) {
        var ctx = "";
        var workbookXML = "";
        var worksheetsXML = "";
        var rowsXML = "";

        for (var i = 0; i < tables.length; i++) {
            if (!tables[i].nodeType) tables[i] = document.getElementById(tables[i]);
            for (var j = 0; j < tables[i].rows.length; j++) {
                rowsXML += '<Row>'
                for (var k = 0; k < tables[i].rows[j].cells.length; k++) {
                    var dataType = tables[i].rows[j].cells[k].getAttribute("data-type");
                    var dataStyle = tables[i].rows[j].cells[k].getAttribute("data-style");
                    var dataValue = tables[i].rows[j].cells[k].getAttribute("data-value");
                    dataValue = (dataValue) ? dataValue : tables[i].rows[j].cells[k].innerHTML;
                    var dataFormula = tables[i].rows[j].cells[k].getAttribute("data-formula");
                    dataFormula = (dataFormula) ? dataFormula : (appname == 'Calc' && dataType == 'DateTime') ? dataValue : null;
                    ctx = {
                        attributeStyleID: (dataStyle == 'Currency' || dataStyle == 'Date') ? ' ss:StyleID="' + dataStyle + '"' : ''
                           , nameType: (dataType == 'Number' || dataType == 'DateTime' || dataType == 'Boolean' || dataType == 'Error') ? dataType : 'String'
                           , data: (dataFormula) ? '' : dataValue
                           , attributeFormula: (dataFormula) ? ' ss:Formula="' + dataFormula + '"' : ''
                    };
                    rowsXML += format(tmplCellXML, ctx);
                }
                rowsXML += '</Row>'
            }
            ctx = { rows: rowsXML, nameWS: wsnames[i] || 'Sheet' + i };
            worksheetsXML += format(tmplWorksheetXML, ctx);
            rowsXML = "";
        }

        ctx = { created: (new Date()).getTime(), worksheets: worksheetsXML };
        workbookXML = format(tmplWorkbookXML, ctx);

        console.log(workbookXML);

        var link = document.createElement("A");
        link.href = uri + base64(workbookXML);
        link.download = wbname || 'Workbook.xls';
        link.target = '_blank';
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    }
})();

var tableToExcel = (function () {
    var uri = 'data:application/vnd.ms-excel;base64,',
    template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>',
    base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) },
    format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
    return function (table, name) {
        table = "sampleTable"
        name = "TimeTable"
        if (!table.nodeType) table = document.getElementById(table)
        var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
        window.location.href = uri + base64(format(template, ctx))
    }
})()
