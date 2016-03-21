angular.module('traktat.ui')
  .directive('trkRegionPicker', function (GlobalsService) {
      return {
          templateUrl: 'app/common/directives/traktat.ui/trkRegionPicker/trkRegionPicker.tpl.html',
          restrict: 'E',
          scope: {
              selection: '='
          },
          controller: function ($scope, GlobalsService) {
              $scope.items = GlobalsService.get('regions');
              $scope.model = $scope;
          }
      };
  });