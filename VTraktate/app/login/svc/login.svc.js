angular.module('security', ['LocalStorageModule'])
    .service('loginService', loginService);

function loginService($http, $q, localStorageService) {

    var accessToken = null,
        isLoggedIn = false,
        url = '/Token',
        ACCESS_TOKEN = '___webapi2_access_token',
        userData = null;

    function getUserData() {
        return userData;
    }

    function getAccessToken() {
        if (!accessToken)
            accessToken = localStorageService.get(ACCESS_TOKEN);
        return accessToken;
    }

    function setAccessToken(token) {
        accessToken = token;
        localStorageService.set(ACCESS_TOKEN, token);
    }

    function clearAccessToken() {
        localStorageService.remove(ACCESS_TOKEN);
        accessToken = null;
    }

    function login(userName, password) {
        clearAccessToken();
        userData = null;

        var deferred = $q.defer();
        var data = {
            userName: userName,
            password: password,
            grant_type: 'password'
        };

    var str = serialize(data);

    $http.post(url, str)
            .success(function (result) {
                var token = result.access_token;

                if (token && result.user_data) {
                    setAccessToken(token);
                    userData = angular.fromJson(result.user_data);
                    deferred.resolve(userData);
                }
                else {
                    deferred.reject('empty or no authentication token supplied');
                }
            })
            .error(function (error) {
                deferred.reject(error);
            });

    return deferred.promise;
    }

    function logout() {
        clearAccessToken();
        userData = null;
        isLoggedIn = false;
    }

    return {
        isLoggedIn: isLoggedIn,
        login: login,
        logout: logout,
        token: getAccessToken(),
        userData: getUserData()
    };

    function serialize(obj) {
        var str = [];
        for (var p in obj) {
            if (obj.hasOwnProperty(p)) {
                str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
            }
        }
        return str.join("&");
    }
}
