angular.module('login', ['security', 'LocalStorageModule'])
    .controller('loginController', function ($scope, loginService, localStorageService, $q, $location, $rootScope) {
        var defaultCredentials = { userName : '', password : '', storeCreds : false };
            
        $scope.credentials = localStorageService.get('creds') || defaultCredentials;

        $scope.submit = function () {
            var deferred = $q.defer();

            if ($scope.credentials.storeCreds == true)
                localStorageService.set('creds', $scope.credentials);
            else
                localStorageService.remove('creds');

            loginService.login($scope.credentials.userName, $scope.credentials.password)
                .then(function (user) {
                    deferred.resolve();
                    $rootScope.isLoggedIn = true;
                    $rootScope.user = user;
                    
                    console.log('logged in as ');
                    console.dir(user);
                    $location.path('/index');
                },
                function (err) { $scope.errorMessage = 'Ошибка: ' + err['error_description']; deferred.reject(); });

            return deferred.promise;
        }
    });
