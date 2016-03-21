angular.module('traktat.ui')
  .directive('trkStarsPicker', function (GlobalsService) {
      return {
          templateUrl: 'app/common/directives/traktat.ui/trkStarsPicker/trkStarsPicker.tpl.html',
          restrict: 'E',
          scope: {
              selection: '='
          },
          controller: function ($scope, GlobalsService) {
              $scope.items = [
                  { id: 0, title: 'нет' },
                  { id: 1, title: 'одна' },
                  { id: 2, title: 'две' },
                  { id: 3, title: 'три' }
              ];
              $scope.model = $scope;
          }
      };
  });