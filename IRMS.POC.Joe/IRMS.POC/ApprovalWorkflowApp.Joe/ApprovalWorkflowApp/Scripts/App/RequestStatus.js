(function () {
    var requestStatusModule = angular.module('RequestStatusModule', ['ui.bootstrap', 'ngTable', 'directivesModule']);

    var requestStatusController = function ($scope, $window, $http, NgTableParams) {
        this.tableParams = new NgTableParams({
            page: 1,   // show first page
            count: 7  // count per page
        }, {
            counts: [], // hide page counts control
            total: 1,  // value less than count hide pagination
            dataset: $scope.RequestStatusList
        });

        $scope.marginFunc = function (step) {
            console.log(step);
            if (step.RequestStatusName.length >= 15) {
                return "wizard-margin";
            }
        };

        $scope.isActive = function (status) {
            return status.TransactionDate != "";
        };

        console.log($scope.RequestStatusList);

        $scope.cashAccountsColumns = [];
        for (i = 0; i < $scope.RequestStatusList.length; i++) {

            if ($scope.RequestStatusList[i].RequestStatusName == 'Approved' && $scope.RequestStatusList[i].TransactionDate != "") {
                $scope.cashAccountsColumns.push(angular.copy($scope.RequestStatusList[i]));
            } else if ($scope.RequestStatusList[i].RequestStatusName == 'Request Rejected By Manager' && $scope.RequestStatusList[i].TransactionDate != "") {
                $scope.cashAccountsColumns.splice(i-1);
                $scope.cashAccountsColumns.push(angular.copy($scope.RequestStatusList[i]));
            } else if ($scope.RequestStatusList[i].RequestStatusName == 'Request Rejected By Manager') {

            } else if ($scope.RequestStatusList[i].RequestStatusName == 'Complete' && $scope.RequestStatusList[i].TransactionDate != "") {
                $scope.cashAccountsColumns.push(angular.copy($scope.RequestStatusList[i]));
            } else if ($scope.RequestStatusList[i].RequestStatusName == 'Rejected By App Support Team' && $scope.RequestStatusList[i].TransactionDate != "") {
                $scope.cashAccountsColumns.splice(i-2);
                $scope.cashAccountsColumns.push(angular.copy($scope.RequestStatusList[i]));
            } else if ($scope.RequestStatusList[i].RequestStatusName == 'Rejected By App Support Team') {
            }
            else {
                $scope.cashAccountsColumns.push(angular.copy($scope.RequestStatusList[i]));
            }
        };
    };

    requestStatusModule.controller('RequestStatusController', ['$scope', '$window', '$http', 'NgTableParams', requestStatusController]);
    requestStatusController.$inject = ["NgTableParams"];
}());