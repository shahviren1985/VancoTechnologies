angular.module('ExamSystemApp')
Examapp.controller("TranscriptCtrl", function ($scope, $http) {
    $scope.GetAllPedingRequest = function (value) {
        $http.get(_CommonUr + '/student/GetTranscriptData?status=' + value).then(function (response) {
            if (response.status == 200) {
                $scope.TranscriptRecords = [];
                if (response.data.length > 0) {
                    $scope.TranscriptRecords = response.data;
                } else {
                    toastr.success('No Record found...', {
                        positionClass: "toast-bottom-right",
                    });
                }
            } else {
                $scope.TranscriptRecords = [];
                toastr.error('Error while retriving data...', {
                    positionClass: "toast-bottom-right",
                });
            }
        })
    }
    $scope.GetAllPedingRequest(1);
    $scope.drpType = "1";
    $scope.OpenTranscript = function (RequestDetail) {
        window.open(_CommonurlUI + '/App/OnlineQueries/Transcript_Certificate.html?InwardNumber=' + localStorage.getItem("InwardNumber") + '&CollegeRegistrationNumber=' + RequestDetail.PNR + '&semester=6&AdmissionYear=' + RequestDetail.AdmissionYear + '&ToYear=' + localStorage.getItem("ToYear") + '&PassingYear=' + localStorage.getItem("PassingYear"), '_blank');
    }

    $scope.ComplatedRequest = function (Request) {
        $http.get(_CommonUr + '/student/CompleteTranscriptRequest?TranscriptId=' + Request.RequestId).then(function (response) {
            if (response.status == 200) {
                toastr.success(Request.RequestId + ' completed successfully', {
                    positionClass: "toast-bottom-right",
                });
                $scope.GetAllPedingRequest($scope.drpType);
            } else {
                $scope.TranscriptRecords = [];
                toastr.error('Error while completing request', {
                    positionClass: "toast-bottom-right",
                });
            }
        })
    }
});