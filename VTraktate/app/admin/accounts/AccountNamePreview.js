// expecting backend to return TRUE if duplicate 

angular.module('common.services')
.factory('AccountNamePreview', function ($q, $http) {
    return {
        test: function (data, value) {
            var url = '/api/Account/Unique',
                data = {
                    id: data,
                    value: value
                };

            return $http.post(url, data);
        }
    };
});