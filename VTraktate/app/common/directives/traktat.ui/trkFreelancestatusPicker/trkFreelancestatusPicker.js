angular.module('traktat.ui')
  .directive('trkFreelancestatusPicker', function (GlobalsService) {
      return {
          templateUrl: 'app/common/directives/traktat.ui/trkFreelancestatusPicker/trkFreelancestatusPicker.tpl.html',
          restrict: 'E',
          scope: {
              selection: '='
          },
          controller: function ($scope, GlobalsService) {
              $scope.items = GlobalsService.get('freelanceStatuses');
              $scope.model = $scope;
          }
      };
  });