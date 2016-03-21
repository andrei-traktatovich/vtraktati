angular.module('auth')

    .controller('loginCtrl', function ($scope, LoginService, CredsService, $q, $rootScope) {
      
        $scope.credentials = CredsService.get();

        $scope.submit = function () {

            $scope.errorMessage = null;

            var deferred = $q.defer(),
                credentials = $scope.credentials;

            LoginService.login(credentials.userName, credentials.password)
                .then(function (data) {
                    console.log('logged in');
                    CredsService.set(credentials);
                    deferred.resolve();
                    try {
                        $rootScope.$state.go('home');
                    }
                    catch(error) {
                        console.log('error' + error);
                    }
                },
                function (err) {
                    $scope.errorMessage = err['error_description'];
                    deferred.reject();
                });

            return deferred.promise;
        }
    });
