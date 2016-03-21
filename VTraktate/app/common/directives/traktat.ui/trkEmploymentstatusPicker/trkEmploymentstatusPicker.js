angular.module('traktat.ui')
  .directive('trkEmploymentstatusPicker', function (GlobalsService) {
      return {
          templateUrl: 'app/common/directives/traktat.ui/trkEmploymentstatusPicker/trkEmploymentstatusPicker.tpl.html',
          restrict: 'E',
          scope: {
              selection: '='
          },
          controller: function ($scope, GlobalsService) {
              $scope.items = GlobalsService.get('employmentStatuses');
              $scope.model = $scope;
          }
      };
  });