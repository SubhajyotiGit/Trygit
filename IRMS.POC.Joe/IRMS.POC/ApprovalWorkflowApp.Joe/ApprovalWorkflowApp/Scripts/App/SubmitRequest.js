(function () {
    var submitRequestModule = angular.module('SubmitRequestModule', ['isteven-multi-select', 'ui.bootstrap', 'ngTagsInput', 'ngFileUpload', 'toastr', 'angularSpinner']);

    var submitRequestController = function ($scope, $window, $http, $uibModal, Upload, toastr, toastrConfig, usSpinnerService) {

         toastrConfig.positionClass = "toast-bottom-center";

        $scope.UserDetails;
        $scope.Validated = true;
        $scope.IsRequestSubmitted = true;
        $scope.UserCredentials;

        $scope.IDSSPIsChecked = { AccessName: "IDS SharePoint", CheckStatus: 'false' };
        $scope.SASDDIsChecked = { AccessName: "SAS DD", CheckStatus: 'false' };
        $scope.HPALMIsChecked = { AccessName: "HP ALM", CheckStatus: 'false', ChildCheckStatusList: [{ AccessName: "IDS_Training", CheckStatus: false }, { AccessName: "IDS", CheckStatus: false }, { AccessName: "IDS_System", CheckStatus: false }] };
        $scope.MKSIsChecked = { AccessName: "MKS Integrity", CheckStatus: 'false', TrailNames: [] }; 
        $scope.DMCCIsChecked = { AccessName: "DMCC", CheckStatus: 'false', FilePath: null, FileName:null };  
        $scope.IDSDropZoneIsChecked = { AccessName: "IDS Drop Zone", CheckStatus: 'false', ChildCheckStatusList: [{ AccessName: "NA (O-Drive)", CheckStatus: false }, { AccessName: "EU (N-Drive)", CheckStatus: false }] };

        $scope.ValidationMessage = '';

        $scope.clearTextMgrNTId = function () {
            $scope.UserDetails.ManagerNetworkId = null;
        }
        
        $scope.SearchManager = function () {
            $http({
                url: '/SubmitRequest/SearchManager',
                method: 'POST',
                data: { managerNTId: $scope.UserDetails.ManagerNetworkId }
            }).then(function (result) {
                console.log(result.data);
                if (result.data.ManagerNetworkId != null) {
                    $scope.UserDetails.ManagerName= result.data.UserName;
                    $scope.UserDetails.ManagerEmailId = result.data.UserEmailId;
                    $scope.UserDetails.ManagerFirstName = result.data.UserFirstName;
                    $scope.UserDetails.ManagerLastName = result.data.UserLastName;
                    $scope.UserDetails.ManagerNetworkId = result.data.UserNetworkId;
                    $scope.UserDetails.ManagerWWID = result.data.UserWWID;
                }
                else {
                    toastr.error('Invalid Search Text. Please Enter Correct NTId of your Manager', 'Error');
                }
            });
        }

        // upload later on form submit or something similar
        $scope.submit = function () {
                $scope.upload($scope.file);
        };

        $scope.Validation = function () {

            if (!($scope.HPALMIsChecked.CheckStatus == 'true' || $scope.MKSIsChecked.CheckStatus == 'true' ||
                $scope.IDSDropZoneIsChecked.CheckStatus == 'true' || $scope.SASDDIsChecked.CheckStatus == 'true' ||
                $scope.DMCCIsChecked.CheckStatus == 'true' || $scope.IDSSPIsChecked.CheckStatus == 'true'))
            {
                $scope.ValidationMessage += "Please Select at least one application for the access" + '</br>';
            }

            if ($scope.HPALMIsChecked.CheckStatus == 'true') {
                var isValid = false;
                angular.forEach($scope.HPALMIsChecked.ChildCheckStatusList, function (value, key) {
                    if (value.CheckStatus == true) {
                        isValid = true;
                    }
                });
                if (isValid) {
                   
                } else {
                    $scope.ValidationMessage += "Please Select any HPALM option" + '</br>';
                }
            }

            if ($scope.MKSIsChecked.CheckStatus == 'true') {
                if ($scope.MKSIsChecked.TrailNames.length == 0) {
                    $scope.ValidationMessage += "Please Enter at least one Trail name in MKS Integrity Section" + '</br>';
                }
            }

            if ($scope.DMCCIsChecked.CheckStatus == 'true') {
                if ($scope.file == null || $scope.myForm.file.$error.maxSize) {
                    $scope.ValidationMessage += "Please upload a training transcript for DMCC Application " + '</br>';
                }
            }

            if ($scope.IDSDropZoneIsChecked.CheckStatus == 'true') {
                var isValid = false;
                angular.forEach($scope.IDSDropZoneIsChecked.ChildCheckStatusList, function (value, key) {
                    console.log(value.CheckStatus);
                    if (value.CheckStatus == true) {
                        isValid = true;
                    }
                });
                if (isValid) {

                } else {
                    $scope.ValidationMessage += "Please Select any IDS Drop Zone option" + '</br>';
                }
            }
        }

        $scope.animationsEnabled = true;
        $scope.open = function (size) {

            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: 'myModalContent.html',
                controller: 'ModalInstanceCtrl',
                size: size
            });

            modalInstance.result.then(function (user) {
                $scope.UserCredentials = user;
                $scope.Validated = false;
                

            }, function () {
              
            });
        };
        $scope.toggleAnimation = function () {
            $scope.animationsEnabled = !$scope.animationsEnabled;
        };

        $scope.animationsEnabled1 = true;
        $scope.OpenHelp = function (size) {
            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled1,
                templateUrl: 'HelpModal.html',
                controller: 'HelpModalInstanceCtrl',
                size: size
            });
        };
        $scope.toggleAnimation = function () {
            $scope.animationsEnabled1 = !$scope.animationsEnabled1;
        };

        // upload on file select
        $scope.upload = function (file) {
            console.log(file);
            Upload.upload({
                url: '/SubmitRequest/Upload',
                data: { file: file, user: $scope.UserDetails.UserNetworkId }
            }).then(function (resp) {
                console.log(resp.data);
                $scope.Message = resp.data.Message;
                $scope.DMCCIsChecked.FilePath = resp.data.FilePath;
                $scope.DMCCIsChecked.FileName = resp.config.data.file.name;
                console.log('Success ' + resp.config.data.file.name + 'uploaded. Response: ' + resp.data.FilePath);

                if (resp.data.FilePath != null) {
                    $scope.RequestDetails = {
                        UserId: $scope.UserDetails.UserWWID,
                        UserName: $scope.UserDetails.UserName,
                        FirstName: $scope.UserDetails.UserFirstName,
                        LastName: $scope.UserDetails.UserLastName,
                        Password: $scope.UserCredentials.Password,
                        NTId: $scope.UserDetails.UserNetworkId,
                        UserEmailId: $scope.UserDetails.UserEmailId,
                        ManagerName: $scope.UserDetails.ManagerName,
                        ManagerEmailId: $scope.UserDetails.ManagerEmailId,
                        ManagerFirstName: $scope.UserDetails.ManagerFirstName,
                        ManagerLastName: $scope.UserDetails.ManagerLastName,
                        ManagerNetworkId: $scope.UserDetails.ManagerNetworkId,
                        ManagerWWID: $scope.UserDetails.ManagerWWID,
                        ApplicationId: $scope.UserDetails.ApplicationId,
                        RequestStateId: "1",
                        IDSSPEntity: $scope.IDSSPIsChecked,
                        SASDDEntity: $scope.SASDDIsChecked,
                        HPALMEntity: $scope.HPALMIsChecked,
                        MKSIntegrity: $scope.MKSIsChecked,
                        DMCCEntity: $scope.DMCCIsChecked,
                        IDSDropZoneEntity: $scope.IDSDropZoneIsChecked
                    };
                    console.log($scope.RequestDetails);
                    $http({
                        url: '/SubmitRequest/SubmitRequest',
                        method: 'POST',
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify($scope.RequestDetails) 
                    }).then(function (result) {
                        console.log(result.data);
                        if (result.data == true) {
                            $scope.IsRequestSubmitted = false;
                        }
                    });
                }
            }, function (resp) {
                console.log('Error status: ' + resp.status);
            }, function (evt) {
                var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                console.log('progress: ' + progressPercentage + '% ' + evt.config.data.file.name);
            });
        };

        $scope.SubmitRequest = function () {
           // alert("hi");
            $scope.Validation();
            console.log($scope.ValidationMessage);
            if ($scope.ValidationMessage != '') {
                toastr.error($scope.ValidationMessage, 'Error');
                $scope.ValidationMessage = '';
                return;
            }
          //  usSpinnerService.spin('spinner-1');
            if ($scope.DMCCIsChecked.CheckStatus == 'true') {
                $scope.upload($scope.file);
            }
            else {
                $scope.RequestDetails = {
                    UserId: $scope.UserDetails.UserWWID,
                    UserName: $scope.UserDetails.UserName,
                    FirstName: $scope.UserDetails.UserFirstName,
                    LastName: $scope.UserDetails.UserLastName,
                    Password: $scope.UserCredentials.Password,
                    NTId: $scope.UserDetails.UserNetworkId,
                    UserEmailId: $scope.UserDetails.UserEmailId,
                    ManagerName: $scope.UserDetails.ManagerName,
                    ManagerEmailId: $scope.UserDetails.ManagerEmailId,
                    ManagerFirstName: $scope.UserDetails.ManagerFirstName,
                    ManagerLastName: $scope.UserDetails.ManagerLastName,
                    ManagerNetworkId: $scope.UserDetails.ManagerNetworkId,
                    ManagerWWID: $scope.UserDetails.ManagerWWID,
                    ApplicationId: $scope.UserDetails.ApplicationId,
                    RequestStateId: "1",
                    IDSSPEntity: $scope.IDSSPIsChecked,
                    SASDDEntity: $scope.SASDDIsChecked,
                    HPALMEntity: $scope.HPALMIsChecked,
                    MKSIntegrity: $scope.MKSIsChecked,
                    DMCCEntity: $scope.DMCCIsChecked,
                    IDSDropZoneEntity: $scope.IDSDropZoneIsChecked
                };

                $http({
                    url: '/SubmitRequest/SubmitRequest',
                    method: 'POST',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify($scope.RequestDetails)
                }).then(function (result) {
                    console.log(result.data);
                    if (result.data == true) {
                     //   usSpinnerService.stop('spinner-1');
                        $scope.IsRequestSubmitted = false;
                    }
                });
            }
            
        };

    };

    var modalInstanceCtrl = function ($scope,$http, $uibModalInstance) {
        $scope.loginStatus = true;
        console.log($scope.loginStatus);
        $scope.ok = function () {
            if ($scope.User == null || $scope.User.UserName == null || $scope.User.Password == null) {
                $scope.loginStatus = false;
                return;
            }
            $http({
                url: '/SubmitRequest/ValidateElectronicSign',
                method: 'POST',
                params: $scope.User
            }).then(function (result) {
                console.log(result.data);
                if (result.data == true) {
                    $uibModalInstance.close($scope.User);
                } else {
                    $scope.loginStatus = false;
                }
            });
        };

        $scope.cancel = function () {
            $scope.loginStatus = true;
            $uibModalInstance.dismiss('cancel');
        };
    };

    var helpModalInstanceCtrl = function ($scope, $uibModalInstance) {
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    };

    submitRequestModule.controller('SubmitRequestController', ['$scope', '$window', '$http', '$uibModal', 'Upload', 'toastr', 'toastrConfig', 'usSpinnerService', submitRequestController]);
    submitRequestModule.controller('ModalInstanceCtrl', ['$scope', '$http', '$uibModalInstance', modalInstanceCtrl]);
    submitRequestModule.controller('HelpModalInstanceCtrl', ['$scope', '$uibModalInstance', helpModalInstanceCtrl]);
}());