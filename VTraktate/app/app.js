angular.module('app',
    [
        'ngSanitize',
        'angularSpinner',

        'ui.bootstrap.datetimepicker',
        'internationalPhoneNumber',
        'customDirectives',
        'ui.router',
        'auth',
        'traktat.ui',
        'ngTable',
        'ngResource',
        'ui.select',
        'nsPopover',
        'ui.bootstrap',
        'pageslide-directive',
        'ui.layout',
        'rzModule',
        'ngTextTruncate',
        'common.services',
        'ui.calendar',
        'thirdPartyServices',
        'globals',
        'user',
        'providers',
        'grades',

        'orders'

        // thirdPartystuff:  incorporate into a separate module
        /*'angularSpinner','internationalPhoneNumber','ngTable', 'ui.select',  'nsPopover', 'ui.bootstrap','pageslide-directive','ui.layout',  'rzModule','ngTextTruncate','ui.calendar',
        */
    ])
.value('version', '2.1.10');

angular.module('app')
    .factory('ReqAuthInterceptor', ['Token', function (Token) {
        return {
            'request': function (config) {
                console.log('request interceptor:');
                console.log(config)
                var token = Token.get();
                if (token) {
                    config.headers['Authorization'] = 'Bearer ' + token;
                }
                return config;
            }
        }
    }])
    .factory('SpinInterceptor', function ($q, $rootScope) {
        return {
            'request': function (config) {
                /// set some timeout before spin actually appears ... 
                $rootScope.startSpin();
                return config;
            },
            'response': function (response) {
                $rootScope.stopSpin();
                return response;
            },
            'responseError': function (rejection) {
                $rootScope.stopSpin();
                return $q.reject(rejection);
            }

        };
    })

    .config(['$stateProvider', '$httpProvider', '$urlRouterProvider', '$provide', '$compileProvider',
        function ($stateProvider, $httpProvider, $urlRouterProvider, $provide, $compileProvider) {

            // 
            $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|file|dial):/);
            // error handling 
            $provide.decorator('$exceptionHandler', ['$log', '$delegate',
                function ($log, $delegate) {
                    return function (exception, cause) {
                        $log.debug('Default exception handler.');
                        $delegate(exception, cause);
                    };
                }]);

            // pushing http request interceptor for auth
            $httpProvider.interceptors.push('ReqAuthInterceptor');
            $httpProvider.interceptors.push('SpinInterceptor');
            $urlRouterProvider.otherwise('/');
            // configuring states

            $stateProvider.state('login', {
                templateUrl: 'app/auth/login/login.view.html',
                url: '/login',
                controller: 'loginCtrl',
                data: { auth: { noLogin: true } }
            });

            $stateProvider.state('home', {
                url: '/',
                templateUrl: 'app/home/home.view.html',
                controller: 'homeCtrl',
                data: { auth: { noLogin: false } }
            });

            $stateProvider.state('admin', {
                abstract: true,
                template: '<ui-view/>',
                data: { auth: { roles: [1] } }
            });

            $stateProvider.state('admin.account',
                {
                    url: '/admin/accounts',
                    templateUrl: 'app/admin/accounts/accounts.view.html',
                    controller: 'accountsCtrl',
                    data: {
                        tableParams: null, // initially, nothing 
                        tableDefaults: {
                            page: 1,
                            count: 12,
                            sorting: {},
                            filter: {},
                            total: 0
                        },

                        auth: { roles: [1] }
                    }

                });

            $stateProvider.state('admin.accountCreate',
                {
                    templateUrl: 'app/admin/accounts/accounts.edit.view.html',
                    controller: 'accountCreateCtrl',
                    data: { auth: { roles: [1] } },
                    params: { personId: null, personName: null, creatingAccount: null }
                });

            $stateProvider.state('admin.accountEdit',
                {
                    templateUrl: 'app/admin/accounts/accounts.edit.view.html',
                    controller: 'accountEditCtrl',
                    data: { auth: { roles: [1] } },
                    params: {
                        account: {
                            accountId: null,
                            email: null,
                            password: null,
                            confirmPassword: null,
                            roles: [],
                            loginDisabled: false,
                            name: null,
                        },
                        personName: null
                    }
                });

            $stateProvider.state('hr',
                {
                    abstract: true,
                    template: '<ui-view></ui-view>',
                    data: { auth: { roles: [1, 11, 5, 6, 4, 8, 9, 7, 12] } }
                });

            $stateProvider.state('hr.employees',
                {
                    url: '/hr/employees',
                    templateUrl: 'app/providers/providers.view.html',
                    controller: 'providersCtrl',
                    data: {
                        tableParams: null, // initially, nothing 
                        tableDefaults: {
                            page: 1,
                            count: 12,
                            sorting: {},
                            filter: null,
                            total: 0
                        },

                        auth: { roles: [1, 11, 5, 6, 7, 12] }
                    }
                });

            $stateProvider.state('hr.employees.profile',
                {
                    //url: '/hr/employee/profile',
                    templateUrl: 'app/providers/profile/provider-profile.tpl.html',
                    params: {
                        id: 0
                    },
                    // TODO: PUT IT IN A SEPARATE PLACE 
                    controller: function ($scope, $stateParams, $http, ActionAuthorizationService) {
                        var id = $stateParams.id;

                        $scope.readonly = !ActionAuthorizationService.isHR();
                        console.log('readonly = ' + $scope.readonly)

                        $http.get('/api/provider', { params: { id: id } })
                            .success(function (result) {
                                $scope.model = result;

                            });

                        $scope.contactsCount = function () {
                            return $scope.model && $scope.model.contactPersons && $scope.model.contactPersons.length || "нет";
                        }
                        $scope.servicesCount = function () {
                            return $scope.model && $scope.model.services && $scope.model.services.length || "нет";
                        }
                    }
                }
            );

            $stateProvider.state('hr.createIndividual', {
                url: '/hr/create',
                params: { type: { id: 1, title: 'Фрилансер' } },
                templateUrl: 'app/providers/create/individual/templates/individualCreateForm.tpl.html',
                controller: 'individualCreateFormCtrl'
            });

            $stateProvider.state('hr.modifyEmployment', {
                params: { id: null },
                templateUrl: '',
                controller: 'ModifyEmploymentController'
            });

            $stateProvider.state('hr.workTimeSheet',
                {
                    url: '/hr/worktimesheet',
                    template: '<h2>Табель</h2>',
                    data: { auth: { roles: [1, 12] } },
                });
        }])

    .controller('appController', function ($timeout, $scope, $rootScope, User, version, LoginService, StateGateKeeperService, usSpinnerService) {
        console.log('app controller entered.');

        $rootScope.version = version;

        $scope.getUser = User.get;

        var logout = function () {
            LoginService.logout();
        };

        var spinnerActive = false, spinnerPromise;

        $rootScope.startSpin = function () {
            if (!spinnerActive) {
                usSpinnerService.spin('spinner-1');
                spinnerActive = true;
                spinnerPromise = null;

                //spinnerPromise = $timeout(function () {
                //    usSpinnerService.spin('spinner-1');
                //    spinnerActive = true;
                //    spinnerPromise = null;
                //}, 100);
            }
        };

        $rootScope.stopSpin = function () {
            if (spinnerActive) {
                usSpinnerService.stop('spinner-1');
                spinnerActive = false;
            }
            if (spinnerPromise)
                $timeout.cancel(spinnerPromise);
        };

        $scope.source = [
            {
                text: 'Админ', glyph: 'glyphicon glyphicon-cog', state: 'admin', subitems:
                  [
                      { text: 'Аккаунты', state: 'admin.account' }
                  ]
            },
            {
                text: 'Кадры', glyph: 'glyphicon glyphicon-user', state: 'hr', subitems:
                  [
                      { text: 'Сотрудники', state: 'hr.employees' },
                      { text: 'Оценки', state: 'grades.list' },
                      { text: 'Табель', state: 'hr.workTimeSheet' }

                  ]

            },
            {
                text: 'Заказы', glyph: 'glyphicon glyphicon-tasks', state: 'orders', subitems:
                  [
                      { text: 'Список', state: 'orders.list' }
                  ]

            },
            { text: 'Выйти', glyph: 'glyphicon glyphicon-log-out', action: logout }
        ];



        $scope.gatekeeper = StateGateKeeperService;

        $scope.logout = logout;

    })

    .run(['$rootScope', 'SessionService', '$state', '$stateParams',
        function ($rootScope, SessionService, $state, $stateParams) {

            // setting root scope
            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;

            $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
                console.log('transitioning to state ' + toState.name);
            });

            $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
                console.log('done transitioning to state ' + toState.name);
            });

            $rootScope.$on('$stateChangeError', function (event, toState, toParams, fromState, fromParams, error) {
                console.log('Error transitioning to state: ' + error);
            });

            // intercepting state change to check authorization
            $rootScope.$on('$stateChangeStart',
                function (event, toState, toParams, fromState, fromParams) {
                    SessionService.checkAccess(event, toState, toParams, fromState, fromParams);
                })
        }]);