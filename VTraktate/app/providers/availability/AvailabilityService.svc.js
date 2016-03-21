angular.module('app')
.factory('AvailabilityService', function ($q, $http) {
    return {
        getAvailability: getAvailability
    };

    function getAvailability(id) {
        var deferred = $q.defer(),
            url = 'api/provider/' + id + '/availability';
            
        $http.get(url)
        .then(function (response) {
            console.log('availability response = ');
            console.log(response);

            // map this to calendar viewmodel ... 

            var eventsource = mapToCalendarViewModel(response.data);
            deferred.resolve(eventsource);
        }, function () {
            deferred.reject();
        });

        return deferred.promise;
    };

    function mapToCalendarViewModel(rowData) {

        var jobParts = []; // rowData.jobParts.map(jobPartToJobPartViewModel);
        var availabilityStatuses = rowData.availabilityStatuses.map(availabilityStatusToAvailabilityStatusViewModel);
        var chores = []; // TODO

        var result = [jobParts, availabilityStatuses, chores];
        console.log('mapping result = ');
        console.log(result);
        return result;
    }

    function jobPartToJobPartViewModel(item) { return {}; }

    function availabilityStatusToAvailabilityStatusViewModel(item) {
        return {
            id: item.id,
            //allDay: true, // setting allday to true makes it miss by one day.
            type: 'availabilityStatus',
            title: item.status.name,
            start: item.startDate,
            end: item.endDate
        };
    }
    // TODO: chores 
});