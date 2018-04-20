(function () {
    var myRequestModule = angular.module('MyRequestModule', ['ui.bootstrap', 'ngTable','toastr']);

    var myRequestController = function ($scope, $rootScope, $window, $http, $uibModal, NgTableParams, toastr, toastrConfig) {
        this.tableParams = new NgTableParams({
            sorting: { RequestNo: "desc" },
            filter: { Approver: "" }
        }, {
            dataset: $scope.MyRequestList
        });
       
        $scope.reloadCheck = function () {
            if (sessionStorage.IsRequestSubmitted == 'true') {
                toastrConfig.positionClass = "toast-bottom-center";
                toastr.success('IRIS Task Updated','Success');
                sessionStorage.removeItem('IsRequestSubmitted');
            }
        };

        $scope.animationsEnabled = true;
        $scope.open = function (size, data) { 
            var infoDetails = [{ infoData: data}]
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

        $scope.animationsEnabled1 = true;
        $scope.openRequestModal = function (size, requestNo) {
            var RequestDetails = [{ RequestNo: requestNo }]
            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled1,
                templateUrl: 'RequestUpdateModal.html',
                controller: 'RequestUpdateModalInstanceCtrl',
                size: size,
                resolve: {
                    items: function () {
                        return RequestDetails;
                    }
                }
            });
            modalInstance.result.then(function (request) {
                $scope.Request = request;
            }, function () {

            });
        };
        $scope.toggleAnimation = function () {
            $scope.animationsEnabled1 = !$scope.animationsEnabled1;
        };

        $rootScope.$on("openLoginModal", function (event, data) {
            $scope.openLoginModal('md', data);
        });

        $scope.openLoginModal = function (size, data) {
            $scope.requestData = data;
            var modalInstance = $uibModal.open({
                templateUrl: 'LoginModal.html',
                controller: 'LoginModalInstanceCtrl',
                size: size,
            });

            modalInstance.result.then(function (user) {
                $scope.requestData.UserName = user.UserName;
                $http({
                    url: '/Home/UpdateRequest',
                    method: 'POST',
                    params: $scope.requestData
                }).then(function (result) {
                    console.log(result.data);
                    if (result.data == 'UPDATED') {
                        sessionStorage.IsRequestSubmitted = 'true';
                        window.location.reload();
                    }
                });
            }, function () {

            });
        };
    };

    var modalInstanceCtrl = function ($scope, $uibModalInstance,items) {
        $scope.infoDetails = items[0].infoData;
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    };

    var requestUpdateModalInstanceCtrl = function ($scope,$rootScope, $uibModalInstance, items) {
        $scope.ok = function () {
            console.log(items[0].RequestNo);
            $scope.Request.RequestNo = items[0].RequestNo;
            $rootScope.$emit("openLoginModal", $scope.Request);
            $uibModalInstance.close($scope.Request);
        };
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    };

    var loginModalInstanceCtrl = function ($scope, $http, $window, $rootScope, $uibModalInstance) {
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
                }
                else {
                    $scope.loginStatus = false;
                }
            });
        };
        $scope.cancel = function () {
            $scope.loginStatus = true;
            $uibModalInstance.dismiss('cancel');
        };
    };

    myRequestModule.controller('MyRequestController', ['$scope', '$rootScope', '$window', '$http', '$uibModal', 'NgTableParams', 'toastr', 'toastrConfig', myRequestController]);
    myRequestController.$inject = ["NgTableParams"];
    myRequestModule.controller('ModalInstanceCtrl', ['$scope', '$uibModalInstance', 'items', modalInstanceCtrl]);
    myRequestModule.controller('RequestUpdateModalInstanceCtrl', ['$scope','$rootScope', '$uibModalInstance', 'items', requestUpdateModalInstanceCtrl]);
    myRequestModule.controller('LoginModalInstanceCtrl', ['$scope', '$http', '$window', '$rootScope', '$uibModalInstance', loginModalInstanceCtrl]);
}());