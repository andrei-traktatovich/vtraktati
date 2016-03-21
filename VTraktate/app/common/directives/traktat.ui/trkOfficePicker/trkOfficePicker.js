angular.module('traktat.ui')
  .directive('trkOfficePicker', function (GlobalsService) {
      return {
          templateUrl: 'app/common/directives/traktat.ui/trkOfficePicker/trkOfficePicker.tpl.html',
          restrict: 'E',
          scope: {
              selection: '='
          },
          controller: function ($scope, GlobalsService) {
              $scope.items = GlobalsService.get('offices');
              $scope.model = $scope;
          }
      };
  });