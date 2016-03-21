(function () { 
    angular.module('orders.utilities')
    .service('jobTypesManager', jobTypesManager);

    function jobTypesManager(GlobalsService) {
        var jobTypes = [];
        rereadJobTypes();

        return {
            isJobTypeLinguistic: isJobTypeLinguistic,
            rereadJobTypes: rereadJobTypes,
            findJobTypeById: findJobTypeById
        };

        function rereadJobTypes() {
            jobTypes = GlobalsService.get('jobTypes');
            return jobTypes;
        }

        function findJobTypeById(id, throwErrorIfNotFound) { // throwErrorIfNotFound: default TRUE!!! 

            throwErrorIfNotFound = throwErrorIfNotFound !== undefined ? throwErrorIfNotFound : true;

            if (!jobTypes.length)
                rereadJobTypes();
            var result = _.findWhere(jobTypes, { id: id });
            
            if (!result && throwErrorIfNotFound)
                throw new Error('jobTypesManager: job type id ' + jobTypeId + ' not found.');

            return result;
        }

        function isJobTypeLinguistic(jobTypeId) {
            if (isNaN(jobTypeId))
                return false;
                
            var jobType = findJobTypeById(jobTypeId);
            if (!jobType)
                throw new Error('jobTypesManager.isJobTypeLinguistic job type id ' + jobTypeId + ' not found.');
            return jobType.isLinguistic;
        }
    }
})();