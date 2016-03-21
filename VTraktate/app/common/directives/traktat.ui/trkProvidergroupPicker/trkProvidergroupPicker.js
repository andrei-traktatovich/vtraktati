angular.module('traktat.ui')
  .directive('trkProvidergroupPicker', function (GlobalsService) {
      return {
          templateUrl: 'app/common/directives/traktat.ui/trkProvidergroupPicker/trkProvidergroupPicker.tpl.html',
          restrict: 'E',
          scope: {
              selection: '='
          },
          controller: function ($scope, GlobalsService) {
              $scope.items = GlobalsService.get('providerGroups');
              $scope.model = $scope;
          }
      };
  });