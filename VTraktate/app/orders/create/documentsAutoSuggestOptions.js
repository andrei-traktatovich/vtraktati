(function () {
    angular.module('order.create')
    .service('documents', function ($http) {
        return {
            getAsync: getAsync
        };

        function getAsync(search) {

            return $http.get('api/order/document', {
                params: {
                    text: search
                }
            }).then(function (response) { console.log(response); return response.data; });
        }
    });
})();