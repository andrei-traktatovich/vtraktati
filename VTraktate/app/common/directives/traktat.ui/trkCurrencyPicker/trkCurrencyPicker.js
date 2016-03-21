angular.module('traktat.ui')
  .directive('trkCurrencyPicker', function (GlobalsService) {
      return {
          templateUrl: 'app/common/directives/traktat.ui/trkCurrencyPicker/trkCurrencyPicker.tpl.html',
          restrict: 'E',
          scope: {
              selection: '='
          },
          controller: function ($scope, GlobalsService) {
              $scope.items = GlobalsService.get('currencies');
              $scope.model = $scope;
          }
      };
  });