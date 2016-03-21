// expecting backend to return TRUE if duplicate 

angular.module('common.services')
.factory('PersonNamePreview', function ($q, $http) {
    return {
        test: function (data, value) {
            var url = 'api/people/namecheck',
                data = {
                    id: data,
                    value: value
                };

            return $http.post(url, data);
        }
    };
});