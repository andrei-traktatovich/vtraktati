(function () {
    angular.module('orders.utilities')
    .service('pricelistManager', pricelistManager);

    function pricelistManager($q) {
        return {
            getPrice: getPrice
        };

        function getPrice(customerId, jobTypeId, languageId) {
            
            var deferred = $q.defer();
            
            var key = [
                    customerId || 0,
                    jobTypeId || 0,
                    languageId || 0
            ].join(' ');
            var result = parseInt(localStorage.getItem(key));
            
            deferred.resolve(result);
            return deferred.promise;
        }

    }
})();