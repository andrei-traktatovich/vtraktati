(function () {
    angular.module('order.job', [ 'constants' ]);

    angular.module('order.job')
    .service('jobFactory', jobFactory);

    function jobFactory(constants) {

        var defaultJobTemplate = {
            customerId: null,
            document: null,
            currencyId: constants.CURRENCIES.RUBLE,
            startDate: new Date(),
            UOMId: constants.DEFAULTJOBUOM, // TODO: get this from job Type !!! 
            statusId: constants.JOB_STATUSES.JOB_STATUS_CREATED,
            endDate: null, 
            initial: {
                volume: {},
                pricing: {}
            }
        };

        return {
            create: create,
            add: add
        };

        function create(template) {
            return angular.extend(new Job(), template);
        }

        function add(jobs, template) {
            var len = jobs && jobs.length;
            if (len)
                return angular.copy(jobs[len - 1]);
            else 
                return create(template);
        }

        function Job() {
            return { 
                customerId: null,
                document: null,
                currencyId: constants.CURRENCIES.RUBLE,
                startDate: new Date(),
                UOMId: constants.DEFAULTJOBUOM, // TODO: get this from job Type !!! 
                statusId: constants.JOB_STATUSES.JOB_STATUS_CREATED,
                endDate: null,
                initial: {

                    volume: {},
                    pricing: {}
                }
            };
        };
    }
})();