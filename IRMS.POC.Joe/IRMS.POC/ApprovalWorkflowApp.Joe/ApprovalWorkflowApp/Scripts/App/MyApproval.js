(function () {
    var myApprovalModule = angular.module('MyApprovalModule', ['ui.bootstrap', 'ngTable', 'toastr', 'angularSpinner']);

    var myApprovalController = function ($scope, $rootScope, $window, $http, $uibModal, NgTableParams, toastr, toastrConfig, usSpinnerService) {

        toastrConfig.positionClass = "toast-bottom-center";

        this.tableParams = new NgTableParams({
            sorting: { RequestNo: "desc" },
            filter: { Requester: "" }
        }, {
            dataset: $scope.MyApprovalList
        });

      //  $scope.ApprovalStatus = "";

        $scope.reloadCheck = function () {
            if (sessionStorage.sccuess == 'true') {
                toastr.success(sessionStorage.ApprovalStatus);
                sessionStorage.removeItem('sccuess');
                sessionStorage.removeItem('ApprovalStatus');
            }
        };

        $scope.Approval;

        $scope.animationsEnabled = true;
        $scope.open = function (size, data) {
            var infoDetails = [{ infoData: data }]
            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: 'moreInfoModal.html',
                controller: 'ModalInstanceCtrl',
                size: size,
                resolve: {
                    items: function () {
                        return infoDetails;
                    }
                }
            });

            
        };
        $scope.toggleAnimation = function () {
            $scope.animationsEnabled = !$scope.animationsEnabled;
        };

        $scope.openApprovalModal = function (size, requestNo) {
            var RequestDetails = [{ RequestNo: requestNo}]
            var modalInstance = $uibModal.open({
                templateUrl: 'approvalModal.html',
                controller: 'ApprovalModalInstanceCtrl',
                size: size,
                resolve: {
                    items: function () {
                        return RequestDetails;
                    }
                }
            });

            modalInstance.result.then(function (approval) {
                $scope.Approval = approval;
            }, function () {

            });
        };

        $rootScope.$on("openLoginModal", function (event, data) {
            $scope.openLoginModal('md',data);
        });

        $scope.openLoginModal = function (size, data) {
            $scope.approvalData = data;
            var modalInstance = $uibModal.open({
                templateUrl: 'LoginModal.html',
                controller: 'LoginModalInstanceCtrl',
                size: size,
            });

            modalInstance.result.then(function (user) {
                $scope.approvalData.UserName = user.UserName;
                usSpinnerService.spin('spinner-1');
                $http({
                    url: '/Approval/UpdateRequest',
                    method: 'POST',
                    params: $scope.approvalData
                }).then(function (result) {
                    console.log(result);
                    if (result.data.Status == 'UPDATED') {
                        console.log(result.data.Status);
                        sessionStorage.sccuess = 'true';
                        sessionStorage.ApprovalStatus = "Approval done. IRIS TaskId is -" + result.data.TaskId;
                        usSpinnerService.stop('spinner-1');
                        $window.location.reload();
                    }
                });
            }, function () {

            });
        };
    };

    var modalInstanceCtrl = function ($scope, $uibModalInstance, items) {
        $scope.infoDetails = items[0].infoData;
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    };

    var approvalModalInstanceCtrl = function ($scope, $rootScope, $uibModalInstance, items) {
        $scope.ok = function () {
            $scope.Approval.RequestNo = items[0].RequestNo;
            $rootScope.$emit("openLoginModal", $scope.Approval);
            $uibModalInstance.close($scope.Approval);
        };
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    };

    var loginModalInstanceCtrl = function ($scope, $http, $window, $rootScope, $uibModalInstance, toastr, toastrConfig) {
        toastrConfig.positionClass = "toast-bottom-center";
        $scope.loginStatus = true;
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
                  //  toastr.success('Successfully signed.');
                }
                else {
                  //  toastr.error('Please enter correct credentials','error');
                    $scope.loginStatus = false;
                }
            });
        };
        $scope.cancel = function () {
            $scope.loginStatus = true;
            $uibModalInstance.dismiss('cancel');
        };
    };

    myApprovalModule.controller('MyApprovalController', ['$scope', '$rootScope', '$window', '$http', '$uibModal', 'NgTableParams', 'toastr', 'toastrConfig', 'usSpinnerService', myApprovalController]);
    myApprovalController.$inject = ["NgTableParams"];
    myApprovalModule.controller('ModalInstanceCtrl', ['$scope', '$uibModalInstance', 'items', modalInstanceCtrl]);
    myApprovalModule.controller('ApprovalModalInstanceCtrl', ['$scope', '$rootScope', '$uibModalInstance', 'items', approvalModalInstanceCtrl]);
    myApprovalModule.controller('LoginModalInstanceCtrl', ['$scope', '$http', '$window', '$rootScope', '$uibModalInstance', 'toastr', 'toastrConfig', loginModalInstanceCtrl]);
}());