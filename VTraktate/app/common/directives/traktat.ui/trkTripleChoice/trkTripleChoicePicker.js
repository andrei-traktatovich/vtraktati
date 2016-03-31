angular.module("traktat.ui")
  .directive("trkTripleChoicePicker", function () {
      return {
          templateUrl: "app/common/directives/traktat.ui/trkTitlePicker/trkTitlePicker.tpl.html",
          restrict: "E",
          scope: {
              selection: "="
          },
          controller: function ($scope) {
              $scope.items = [{ title: "-" }, { id: true, title: "Да" }, { id: false, title: "Нет" }]
              $scope.model = $scope;
          }
      };
  });