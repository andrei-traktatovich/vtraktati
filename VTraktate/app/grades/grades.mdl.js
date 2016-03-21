(function () {

    angular.module('grades', ['traktat.ui', 'ngTable', 'ngResource', 'ui.router'])
    .config(['$stateProvider', '$httpProvider', '$urlRouterProvider', '$provide',
        function ($stateProvider, $httpProvider, $urlRouterProvider, $provide) {

            $stateProvider.state('grades', {
                abstract: true,
                template: '<ui-view></ui-view>',
                data: {
                    auth: { roles: [1, 11, 5, 6, 7, 12, 4, 8, 9 ] }
                }
            });

            $stateProvider.state('grades.list',
                        {
                            url: '/grades',

                            templateUrl: 'app/grades/gradeslist.tpl.html',
                            controllerAs: 'grades',
                            controller: 'gradesListController',
                            data: {
                                auth: { roles: [1, 11, 5, 6, 7, 12, 4, 8, 9 ] }
                            }
                        });

        }]);
})();