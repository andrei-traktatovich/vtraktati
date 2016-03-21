(function () {
    angular.module('orders.utilities')
    .service('jobInfoStorage', jobInfoStorage);

    function jobInfoStorage(LocalStorageService) {
        return {
            store: store,
            suggestRate: suggestRate,
            makeKey: makeKey
        };

        function makeKey(job) {
            var key = [
                    job.customerId || 0,
                    job.jobTypeId || 0,
                    job.languageId || 0
            ].join(' ');
            return key;
        }

        function store(job) {
            // TODO: refactor!
            if (job && job.initial && job.initial.pricing) {
                var key = makeKey(job);
                if (key) {
                    LocalStorageService.set(key, JSON.stringify({
                        rate: job.initial.pricing.rate, currencyId: job.currencyId
                    }));
                }
            }
        }

        function suggestRate(job) {
            var key = makeKey(job);
            if (key) {
                var item = LocalStorageService.get(key);
                if (item) {
                    item = angular.fromJson(item);
                    job.initial.pricing.rate = item.rate;
                    job.currencyId = item.currencyId;
                }
            }
            return job;
        }
    }
})();