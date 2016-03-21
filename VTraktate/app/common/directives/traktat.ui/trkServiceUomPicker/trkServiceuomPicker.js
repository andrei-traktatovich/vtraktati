angular.module('traktat.ui')
  .directive('trkServiceuomPicker', function (GlobalsService) {
      return {
          templateUrl: 'app/common/directives/traktat.ui/trkServiceuomPicker/trkServiceuomPicker.tpl.html',
          restrict: 'E',
          scope: {
              selection: '='
          },
          controller: function ($scope, GlobalsService) {
              $scope.items = GlobalsService.get('serviceUoms');
              $scope.model = $scope;
          }
      };
  });