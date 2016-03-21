angular.module('traktat.ui')
  .directive('trkTitlePicker', function (GlobalsService) {
      return {
          templateUrl: 'app/common/directives/traktat.ui/trkTitlePicker/trkTitlePicker.tpl.html',
          restrict: 'E',
          scope: {
              selection: '='
          },
          controller: function ($scope, GlobalsService) {
              $scope.items = GlobalsService.get('titles');
              $scope.model = $scope;
          }
      };
  });