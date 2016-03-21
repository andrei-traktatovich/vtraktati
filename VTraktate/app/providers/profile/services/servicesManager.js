(function () {

    // encapsulate this into a separate module ... 
    var DEFAULT_CURRENCY_ID = 1,
        DEFAULT_SERVICE_UOM_ID = 2;

    angular.module('providers.profile.services')
    .factory('servicesManager', function ($http, _) {

        return {
            remove: remove,
            create: create,
            save: save
        };

        // private
        function update(model) {
            // model is expected to have a valid id
            var url = '/api/providerService/' + model.id;
            return $http.put(url, model);
        }
        // private
        function add(model) {
            // why a separate url for ADD scenario? is it justified? 
            // model is expected to have a valid provider Id
            var url = '/api/provider/' + model.providerId + '/services';
            return $http.post(url, model);
        }

        function save(model) {
            console.log('trying to save service');
            console.log(model);

            if (model.id)
                return update(model);
            else
                return add(model);
        }

        function remove(serviceId) {
            return $http.delete('/api/providerService/' + serviceId);
        }

        function create(data) {

            var proto = {
                id: null,
                serviceTypeId: null,
                qaStars: 0,
                minRate: null,
                maxRate: null,
                comment: '',
                currencyId: DEFAULT_CURRENCY_ID, 
                uomId: DEFAULT_SERVICE_UOM_ID 
            };

            return _.extend(data, proto);
        }

    });

})();