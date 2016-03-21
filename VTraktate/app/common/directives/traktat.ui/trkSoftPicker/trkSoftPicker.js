angular.module('traktat.ui')
  .directive('trkSoftPicker', function (GlobalsService) {
      return {
          templateUrl: 'app/common/directives/traktat.ui/trkSoftPicker/trkSoftPicker.tpl.html',
          restrict: 'E',
          scope: {
              selection: '=',
              except: '='
          },
          controller: function ($scope, GlobalsService) {

              updateOptions();

              $scope.$watch("except",
                    function (newValue, oldValue) {
                        updateOptions();
                    });

              function updateOptions() {
                  $scope.items = GlobalsService.getExcept('soft', $scope.except, 'id');
              }

              console.log('soft picker');
              console.log($scope.items);
              console.log($scope.except);
              $scope.model = $scope;
          }
      };
  });