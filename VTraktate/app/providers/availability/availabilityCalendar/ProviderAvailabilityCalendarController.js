angular.module('app')
.controller('ProviderAvailabilityCalendarController', function ($scope, eventsource, providerId, GlobalsService, $modalInstance, $http, AvailabilityService, calendarDefaultOptions) {

    // CONST 
    var OCCUPIED_BY_US = 3;

    $scope.events = eventsource;

    $scope.calendar = { options: calendarDefaultOptions };
        
    $scope.statuses = GlobalsService.get('availabilityStatuses');
    $scope.addingItem = false;

    $scope.beginAddItem = function()
    {
        var dt = moment().toDate();
        $scope.newAvailability = {
            startDate: dt,
            endDate: null,
            statusId : OCCUPIED_BY_US
        };
        $scope.addingItem = true;
    }

    function reload() {
        AvailabilityService.getAvailability(providerId)
        .then(success, failure);

        function success(newEventSource) {
            
            // http://stackoverflow.com/questions/23006464/angularjs-ui-calendar-not-updating-events-on-calendar
            // add each new event individually ... 
            $scope.events.splice(0, $scope.events.length);
            for (var i = 0; i < newEventSource.length; i++)
                $scope.events.push(newEventSource[i]);
        }
        function failure() {
            toastr.error('Ошипко');
        }
    }
    $scope.close = function () { $modalInstance.close(); }
    $scope.cancel = function () {
        $scope.addingItem = false;
    };

    $scope.save = function () {
        $http.post('/api/provider/' + providerId + '/availability', $scope.newAvailability)
        .then(function () {
            $scope.addingItem = false;
            reload();
        }, failure);
        
        function failure() {
            toastr.error('Ошипко');
        }
    }

});