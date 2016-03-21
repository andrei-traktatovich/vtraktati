(function () {

    angular.module('order.data')
    .service('customers', customers);

    function customers($http) {
        var autoSuggestUrl = 'api/customer/autosuggest',
            contactPersonsUrl = 'api/customer/';

        return {
            getOptions: getOptions,
            getProfile : getProfile
        };

        function getOptions(val) {
            return $http.get(autoSuggestUrl, {
                params: {
                    val: val
                }
            }); 
        }

        function getContactPersons(customerId, search) {
            return $http.get(contactPersonsUrl + customerId + '/contacts', {
                params: {
                    search: search
                }
            }); 
        }

        function getProfile(val, officeId) {
            if (!officeId)
                throw new Error('customers.getProfile: null officeId specified');
            var url = 'api/customer/' + val + '/profile'
            return $http.get(url, { params: { officeId: officeId } });
        }
    }

})();