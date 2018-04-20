(function () {
    var supportApprovalModule = angular.module('SupportApprovalModule', ['ui.bootstrap', 'ngTable', 'toastr']);

    var supportApprovalController = function ($scope, $rootScope, $window, $http, $uibModal, NgTableParams, toastr, toastrConfig) {
        toastrConfig.positionClass = "toast-bottom-center";

        this.tableParams = new NgTableParams({
            sorting: { RequestNo: "desc" },
            filter: { Requester: "" }
        }, {
            dataset: $scope.SupportApprovalList
        });

        $scope.reloadCheck = function () {
            if (sessionStorage.IsSupportApprovalComplete == 'true') {
                toastr.options = {
                                "closeButton": false,
                                "debug": false,
                                "positionClass": "toast-bottom-full-width",
                                "onclick": null,
                                "showDuration": "300",
                                "hideDuration": "1000",
                                "timeOut": "5000",
                                "extendedTimeOut": "1000",
                                "showEasing": "swing",
                                "hideEasing": "linear",
                                "showMethod": "fadeIn",
                                "hideMethod": "fadeOut"
                            };
                toastr.success('Approval Done');
                sessionStorage.removeItem('IsSupportApprovalComplete');
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
            var RequestDetails = [{ RequestNo: requestNo }]
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
            $scope.openLoginModal('md', data);
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
                $http({
                    url: '/Approval/UpdateRequest',
                    method: 'POST',
                    params: $scope.approvalData
                }).then(function (result) {
                    console.log(result.data);
                    if (result.data.Status == 'UPDATED') {
                        sessionStorage.IsSupportApprovalComplete = 'true';
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

    supportApprovalModule.controller('SupportApprovalController', ['$scope', '$rootScope', '$window', '$http', '$uibModal', 'NgTableParams','toastr','toastrConfig', supportApprovalController]);
    supportApprovalController.$inject = ["NgTableParams"];
    supportApprovalModule.controller('ModalInstanceCtrl', ['$scope', '$uibModalInstance', 'items', modalInstanceCtrl]);
    supportApprovalModule.controller('ApprovalModalInstanceCtrl', ['$scope', '$rootScope', '$uibModalInstance', 'items', approvalModalInstanceCtrl]);
    supportApprovalModule.controller('LoginModalInstanceCtrl', ['$scope', '$http', '$window', '$rootScope', '$uibModalInstance', loginModalInstanceCtrl]);
}());