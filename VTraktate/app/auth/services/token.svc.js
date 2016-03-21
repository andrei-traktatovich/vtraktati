// TokenService
// returns auth token based on user's credentials 

angular.module('auth')
    .service('Token', function () {
        var value = null;

        return {
            get: function () { return angular.copy(value); },
            set: function (token) { value = token; },
            clear: function () { value = null;}
        }
    })
    .service('TokenService', [ '$http', '$q', 'Token', function ($http, $q, Token) {

        var END_POINT = '/Token',
            GRANT_TYPE = 'password';
        
        // TODO: this one may be useful elsewhere !!! 
        function serialize (obj) {
            var str = [];
            for (var p in obj) {
                if (obj.hasOwnProperty(p)) {
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                }
            }
            return str.join("&");
        }

        function getBearerToken(userName, password) {

            var deferred = $q.defer(),
                params = serialize({
                    userName: userName,
                    password: password,
                    grant_type: GRANT_TYPE
                });

            $http.post(END_POINT, params)
                .success(function (result) {
                    
                    if (result.access_token) {
                        Token.set(result.access_token);
                        deferred.resolve();
                    }
                    else {
                        deferred.reject('Некорректный ответ сервера. Не получен объект bearer token для пользователя ' + userName);
                    }
                })
                .error(function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        function clear() {
            Token.clear();
        }

        return {
            get: getBearerToken,
            clear: clear
        }
    }]);