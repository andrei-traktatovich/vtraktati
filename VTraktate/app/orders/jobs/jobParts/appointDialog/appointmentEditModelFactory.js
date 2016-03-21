(function () {
    angular.module('order.list')
    .service('appointmentEditModelFactory', appointmentEditModelFactory);

    function appointmentEditModelFactory() {
        return function (jobPart) {
            return {
                providerId: jobPart.provider.id,
                jobId: jobPart.jobId,
                currencyId: jobPart.currency.id || 1, // ATTN: MAGIC!!! 
                UOMId: jobPart.uom.id || 1, // 
                jobTypeId: jobPart.jobType.id,
                languageId: jobPart.language ? jobPart.language.id : null,
                startDate: jobPart.startDate,
                workInHouse: jobPart.workInHouse,
                statusId: jobPart.status.id,
                endDate: jobPart.endDate,
                initial: jobPart.initial,
                final: jobPart.final,
                id: jobPart.id // also need to provide stuff like daughter job etc .!!!! all thet stuff ! think about it !!! 
            }
        };
    }
})();