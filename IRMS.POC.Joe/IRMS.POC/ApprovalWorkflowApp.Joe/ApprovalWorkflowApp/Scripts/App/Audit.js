(function () {
    var auditModule = angular.module('AuditModule', ['ui.bootstrap', 'ngTable', 'angularUtils.directives.dirPagination']);

    var auditController = function ($scope, $window, $http, $uibModal, NgTableParams) {
        //this.tableParams = new NgTableParams({
        //    sorting: { AuditId: "desc" },
        //    filter: { UserName: "" }
        //}, {
        //    dataset: $scope.AuditList
        //});

        

        var vm = this;
        vm.PageSettings = { counts: ['10', '25', '50', '100', '500'] };
        vm.AuditList = []; //declare an empty array
        vm.pageno = 1; // initialize page no to 1
        vm.total_count = 0;
        vm.itemsPerPage = vm.PageSettings.counts[0]; //this could be a dynamic value from a drop down
     //   vm.activeMenu = vm.PageSettings.counts[0];

        vm.getData = function (pageno) { // This would fetch the data on page change.
            //In practice this should be in a factory.
            vm.AuditList = [];
            console.log(pageno);
            $http({
                url: '/Admin/GetAuditList',
                method: 'get',
                params: { itemsPerPage: vm.itemsPerPage, pageno: pageno }
            }).then(function (result) {
                console.log(result.data);
                vm.AuditList = result.data;  //ajax request to fetch data into vm.data
                vm.total_count = result.data[0].TotalCount//total_count;
            });

            //$http.get("http://code.ciphertrick.com/api/list/page/" + vm.itemsPerPage + "/" + pageno).success(function (response) {
            //    vm.AuditList = response.data;  //ajax request to fetch data into vm.data
            //    vm.total_count = response.total_count;
            //});
        };
        vm.getData(vm.pageno); // Call the function to fetch initial data on page load.

        vm.count = function (count) {
         //   vm.activeMenu = count;
            vm.itemsPerPage = count;
        };

    };

    auditModule.controller('AuditController', ['$scope', '$window', '$http', '$uibModal', 'NgTableParams', auditController]);
    auditController.$inject = ["NgTableParams"];
}());