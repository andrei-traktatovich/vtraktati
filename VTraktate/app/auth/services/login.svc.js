angular.module('auth')
    .factory('LoginService', ['TokenService', 'ProfileService', '$rootScope', '$q', 'User', 'GlobalsService',
        function (TokenService, ProfileService, $rootScope, $q, User, GlobalsService) {

        function login(userName, password) {
            
            var deferred = $q.defer();

            TokenService.get(userName, password)
                .then(function () {
                    console.log('username and password ok');
                    ProfileService.get()
                        .then(function(data) {
                                console.log('profile ok');
                                User.set(data.user);
                                GlobalsService.set(data.globals);
                                deferred.resolve(data);

                            },
                            function(err) { deferred.reject(err); });
                }, function (err) {
                    deferred.reject(err);
                });
            return deferred.promise;
        }

        function logout() {

            User.clear();
            TokenService.clear();
            GlobalsService.clear();

            $rootScope.$emit("logout");
        }

        return {
            login: login,
            logout : logout
        }

    }]);

        