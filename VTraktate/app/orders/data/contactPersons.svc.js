(function () {
    // TODO: deprecated?? actually I am already getting this in the order form via customers.getProfile(id) 
    angular.module('order.data')
    .service('contactPersons', contactPersons);

    function contactPersons($http) {
        
        return {
            get: get
        };

        function get(customerId) {
            var url = 'api/customer/' + customerId + '/contacts';
            return $http.get(url, { params: { id: customerId } });
        }
    }
})();