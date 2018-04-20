var directivesModule = angular.module("directivesModule", []);

directivesModule.directive('statusDirective', function () {
    var controller = ['$scope', function ($scope) {

        $scope.marginFunc = function (step) {
            console.log(step);
            if (step.RequestStatusName.length >= 15) {
                return "wizard-margin";
            }
        };
    }];
        return {
            restrict: "E", //E = element, A = attribute, C = class, M = comment         
            scope: {
                //@ reads the attribute value, = provides two-way binding, & works with functions
                statelist: "="
            },
            template: '<div class="container">' +
                            '<div class="row bs-wizard" style="border-bottom:0;">'+
                                '<div scope="col" class="col-xs-2 bs-wizard-step" ng-class="step.class" ng-repeat="step in statelist">' +
                                    '<div class="text-center bs-wizard-stepnum" ng-class="marginFunc(step)">' + '{{step.RequestStatusName}}' + '</div>' +
                                    '<div class="progress"><div class="progress-bar"></div></div>'+
                                    '<a href="#" class="bs-wizard-dot"></a>'+
                                    '<div class="bs-wizard-info text-center">' + '{{step.TransactionDate}}' + '</div>' +
                                '</div>'+
                            '</div>'+
                        '</div>',
            controller: controller, //Embed a custom controller in the directive
            link: function (scope, element, attrs) {
                var tempStateName = "";

                scope.statelist.forEach(function (newJobItem) {
                    if (newJobItem.TransactionDate != "") {
                        tempStateName = newJobItem.RequestStatusName;
                    } 
                });
                console.log(tempStateName);
                scope.statelist.forEach(function (stateItem) {
                    if (stateItem.TransactionDate != "") {
                        if (stateItem.RequestStatusName == tempStateName) {
                            stateItem.class = "active";
                        } else {
                            stateItem.class = "complete";
                        }
                    } else {
                        stateItem.class = "disabled";
                    }
                });
            } //DOM manipulation
        }
    });