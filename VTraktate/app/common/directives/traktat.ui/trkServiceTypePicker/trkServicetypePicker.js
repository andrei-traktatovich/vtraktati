angular.module('traktat.ui')
  .directive('trkServicetypePicker', function (GlobalsService) {
      return {
          templateUrl: 'app/common/directives/traktat.ui/trkServicetypePicker/trkServicetypePicker.tpl.html',
          restrict: 'E',
          scope: {
              selection: '='
          },
          controller: function ($scope, GlobalsService) {
              $scope.items = GlobalsService.get('serviceTypes');
              $scope.model = $scope;
          }
      };
  });