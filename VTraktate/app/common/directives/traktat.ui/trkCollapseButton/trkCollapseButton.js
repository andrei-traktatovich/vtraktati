angular.module('traktat.ui')
.directive('trkCollapseButton', function() {
  
    return {
        templateUrl: 'app/common/directives/traktat.ui/trkCollapseButton/trkCollapseButton.tpl.html',
        scope : {
            state : '=',
            collapseTitle : '@',
            expandTitle : '@'
        },
        controller : function($scope) {
            toggle();
            $scope.toggle = toggle;
      
            function toggle() {
                $scope.state = !$scope.state;
                $scope.text = $scope.state ? $scope.collapseTitle : $scope.expandTitle;
            }
        }
    };
  
  
})