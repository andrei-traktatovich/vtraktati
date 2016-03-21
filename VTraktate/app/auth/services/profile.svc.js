// ProfileService
// tries to get the user profile and globals from server. 
// assuming access token is set

angular.module('auth')
    .factory('ProfileService', ['$rootScope', '$q', '$http', function ($rootScope, $q, $http) {

        var END_POINT = '/api/account/profile';

        function get() {
            var deferred = $q.defer();
            $http.get(END_POINT)
            .then(function (data) {
                if (data && data.data && data.data.user && data.data.globals) {
                    console.log('profile service ok');
                    console.log(data.data);
                    deferred.resolve(data.data);
                }
                else
                    deferred.reject('Некорректный ответ сервера. Не получен профиль пользователя.');
            }, function (err) { deferred.reject(err); });

            return deferred.promise;
        }
        
        return {
            get: get
        }

    }]);

