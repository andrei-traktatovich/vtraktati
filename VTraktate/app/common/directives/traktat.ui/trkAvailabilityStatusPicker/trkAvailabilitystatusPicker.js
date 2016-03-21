angular.module('traktat.ui')
  .directive('trkAvailabilitystatusPicker', function (GlobalsService) {
      return {
          templateUrl: 'app/common/directives/traktat.ui/trkAvailabilitystatusPicker/trkAvailabilitystatusPicker.tpl.html',
          restrict: 'E',
          scope: {
              selection: '='
          },
          controller: function ($scope, GlobalsService) {
              $scope.items = GlobalsService.get('availabilityStatuses');
              $scope.model = $scope;
          }
      };
  });