// CredsService
// stores and retrieves user's login and password in local storage / clears user credentials

angular.module('auth')
    .factory('CredsService', ['LocalStorageService', function (LocalStorageService) {

        var CREDS_KEY = '___creds';

        function get() {
            var result = LocalStorageService.get(CREDS_KEY);
            if (!result || result.rememberMe != true)
                result = {
                    userName: '',
                    password: '',
                    rememberMe: false
                };
            console.log(result);
            return result;
        }

        function clear() {
            LocalStorageService.remove(CREDS_KEY);
        }

        function set(creds) {
            if (creds && creds.rememberMe)
                LocalStorageService.set(CREDS_KEY, creds);
            else
                clear();
        }

        return {
            get: get,
            clear: clear,
            set: set
        };
    }])