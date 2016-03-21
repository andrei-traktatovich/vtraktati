angular.module('traktat.ui')
  .directive('trkConditionalComment', function (GlobalsService) {
      return {
          templateUrl: 'app/common/directives/traktat.ui/trkConditionalComment/trkConditionalComment.tpl.html',
          restrict: 'E',
          scope: {
              message: '=',
              heading : '@'
          }
      };
  });