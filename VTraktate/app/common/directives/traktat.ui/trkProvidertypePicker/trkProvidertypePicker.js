angular.module('traktat.ui')
  .directive('trkProvidertypePicker', function (GlobalsService) {
      return {
          templateUrl: 'app/common/directives/traktat.ui/trkProvidertypePicker/trkProvidertypePicker.tpl.html',
          restrict: 'E',
          scope: {
              selection: '='
          },
          controller: function ($scope, GlobalsService) {
              $scope.items = GlobalsService.get('providerTypes');
              $scope.model = $scope;
          }
      };
  });